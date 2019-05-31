using System;
using System.Net;

namespace AppSheet1
{
	/***************************************************************************************************
		Retrievable class
	***************************************************************************************************/
	/// <summary>
	/// Base class for types that are retrieved from the Sample service. To keep things simple this
	/// requires an Is-A relationship. There are a variety of alternatives depending on circumstances.
	/// </summary>
	public abstract class Retrievable
	{
		public HttpStatusCode StatusCode { get; set; }

		public DateTime RetrievalTimestamp { get; set; }
	}
}
