using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;

namespace AppSheet1
{
	/***************************************************************************************************
		Person class
	***************************************************************************************************/
	/// <summary>
	/// Represents a person as defined in the <see cref="SampleService"/>. Property names were improved
	/// (e.g. Number -> PhoneNumber) so they don't spread virally into other places.
	/// </summary>
	public class Person : Retrievable
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("age")]
		public int Age { get; set; }

		[JsonProperty("number")]
		public string PhoneNumber { get; set; }

		public bool IsPhoneNumberValid() => (null == PhoneNumber) ? false : Regex.IsMatch(PhoneNumber, Const.PhoneNumberRegex);

		[JsonProperty("photo")]
		public string PhotoUriString { get; set; }

		public BitmapImage Photo { get; set;  }

		[JsonProperty("bio")]
		public string Bio { get; set; }

		public override string ToString() => $"{Id} {Name} {Age} {PhoneNumber} {Bio}";
	}
}
