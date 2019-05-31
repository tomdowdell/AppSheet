using System;

namespace AppSheet1
{
	/***************************************************************************************************
		Const class
	***************************************************************************************************/
	/// <summary>
	/// Global constants.
	/// </summary>
	static public class Const
	{
		/// <summary>
		/// Regular expression to match phone numbers, e.g. (555) 555-5555.
		/// See https://stackoverflow.com/questions/18091324/regex-to-match-all-us-phone-number-formats
		/// </summary>
		// 
		public static readonly String PhoneNumberRegex = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
	}
}
