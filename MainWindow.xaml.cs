using System;
using System.Windows;

namespace AppSheet1
{
	/***************************************************************************************************
		MainWindow class
	***************************************************************************************************/
	/// <summary>
	/// Interaction logic for MainWindow.xaml.
	/// </summary>
	public partial class MainWindow : Window
	{
		MiddleService MiddleService { get; } = new MiddleService();

		/*******************************************************************************
			$
		*******************************************************************************/
		
		public MainWindow()
		{
			InitializeComponent();

			// Populate e_sortBy and select 0th item by default
			foreach (var cur in typeof(PersonSort).GetEnumValues())
				e_sortBy.Items.Add(cur);
			e_sortBy.SelectedIndex = 0;
		}

		/*******************************************************************************
			E_go_Click()
		*******************************************************************************/
		/// <summary>
		/// Call the middle service to retrieve people with criteria specified in this view.
		/// </summary>
		async void E_go_Click(object sender, RoutedEventArgs e)
		{
			// Determine the maximum number of rows to retrieve
			if (!Int32.TryParse(e_maxRows.Text, out int maxRows))
			{
				e_maxRows.Text = String.Empty;
				maxRows = Int32.MaxValue;
			}

			// Attempt to retrieve people from the middle service and update the UI
			try
			{
				var people = await MiddleService.GetPeopleAsync(maxRows, (PersonSort)e_sortBy.SelectedItem, e_descending.IsChecked.Value, e_validDetails.IsChecked.Value, e_validPhoneNumber.IsChecked.Value, e_showPhoto.IsChecked.Value);

				e_list.Items.Clear();

				foreach (var cur in people)
					e_list.Items.Add(cur);
			}
			catch (Exception x)
			{
				MessageBox.Show($"Error retrieving people.");
			}
		}

		/*******************************************************************************
			E_useCache_Checked()
		*******************************************************************************/
		/// <summary>
		/// Have the middle service use caching.
		/// </summary>
		void E_useCache_Checked(object sender, RoutedEventArgs e) => MiddleService.UseCache = true;

		/*******************************************************************************
			E_useCache_Unchecked()
		*******************************************************************************/
		/// <summary>
		/// Have the middle service avoid caching.
		/// </summary>
		void E_useCache_Unchecked(object sender, RoutedEventArgs e) => MiddleService.UseCache = false;
	}
}
