using System.Windows;

namespace AppSheet1
{
	/* Notes from AppSheet:
	 * 
	 * - We have a simple web service that exposes some dummy people data (id, name, age, number, photo, bio).
	 *		"id":21,
	 *		"name":"paul",
	 *		"age":48,
	 *		"number":"555-555-5555",
	 *		"photo":"https://appsheettest1.azurewebsites.net/male-11.jpg",
	 *		"bio":"Lorem ipsum...nunc."}
	 * - The goal is make an app that displays the 5 youngest users with valid us telephone numbers sorted by name
	 * - The UI for displaying the results is up to you --- this is a backend project so we don’t worry too much about the UI. You can use any third party packages / plugins / frameworks you like
	 * - contact Brian (cc’ed) with any questions. We are interested in the cleanness of code design and implementation
	 * - “extra credit” describe
	 *		- a way to automatically test the app
	 *		- how the design of the end-to-end service + app should change if the dataset were three orders of magnitude larger.
	 * 
	 * https://appsheettest1.azurewebsites.net/sample/
	 * 
	 * https://appsheettest1.azurewebsites.net/sample/list
	 * https://appsheettest1.azurewebsites.net/sample/list?token=b32b3
	 * 
	 * http://appsheettest1.azurewebsites.net/sample/detail/21
	 */

	/***************************************************************************************************
		App enum
	***************************************************************************************************/
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
	}
}
