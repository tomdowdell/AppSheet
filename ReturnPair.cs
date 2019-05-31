using System.Net;

namespace AppSheet1
{
	/***************************************************************************************************
		ReturnPair class
	***************************************************************************************************/
	/// <summary>
	/// Used to pair error information with an object of type <typeparamref name="T"/> for return from
	/// restful get calls.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	struct ReturnPair<T> where T : class
	{
		public HttpStatusCode HttpStatusCode { get; set; }

		public T Object { get; set; }
	}
}
