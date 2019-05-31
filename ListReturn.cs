using Newtonsoft.Json;
using System.Collections.Generic;

namespace AppSheet1
{
	/***************************************************************************************************
		ListReturn class
	***************************************************************************************************/
	/// <summary>
	/// Return value for calls to <see cref="SampleService.GetListAsync(string)"./>
	/// </summary>
	public class ListReturn
	{
		[JsonProperty("result")]
		public List<int> Result { get; } = new List<int>();

		[JsonProperty("token")]
		public string Token { get; set; }
	}
}
