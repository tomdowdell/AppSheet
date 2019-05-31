using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppSheet1
{
	/***************************************************************************************************
		SampleService class
	***************************************************************************************************/
	/// <summary>
	/// API for the AppSheet sample web service. This is thin wrapper and adds no additional functionality.
	/// </summary>
	internal class SampleService
	{
		static readonly string Scheme = "https";
		static readonly string Host = "appsheettest1.azurewebsites.net";
		static readonly string RootPath = "/sample";
		static readonly string ListPath = $"{RootPath}/list";
		static readonly string DetailPath = $"{RootPath}/detail";

		UriBuilder List_UriBuilder { get; } = CreateUriBuilder(ListPath);
		UriBuilder Detail_UriBuilder { get; } = CreateUriBuilder(DetailPath);

		HttpClient HttpClient { get; } = new HttpClient();

		/*******************************************************************************
			CreateUriBuilder()
		*******************************************************************************/
		/// <summary>
		/// Creates a <see cref="UriBuilder"/> object initialized to the specified <paramref name="path"/>.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		static UriBuilder CreateUriBuilder(string path) => new UriBuilder
		{
			Scheme = Scheme,
			Host = Host,
			Path = path
		};

		/*******************************************************************************
			ReadJsonAsync()
		*******************************************************************************/
		/// <summary>
		/// Reads a JSON object of type <typeparamref name="T"/>. Returns a <see cref="ReturnPair{T}"/>
		/// which will contain the <see cref="HttpStatusCode"/> and, if successful, the object.
		/// </summary>
		async Task<ReturnPair<T>> ReadJsonAsync<T>(Uri uri) where T : class
		{
			var returnInfo = new ReturnPair<T>();

			var response = await HttpClient.GetAsync(uri);

			var objAsJson = await response.Content.ReadAsStringAsync();

			returnInfo.HttpStatusCode = response.StatusCode;
			if (HttpStatusCode.OK == response.StatusCode)
				returnInfo.Object = JsonConvert.DeserializeObject<T>(objAsJson);

			return returnInfo;
		}

		/*******************************************************************************
			GetListAsync()
		*******************************************************************************/
		/// <summary>
		/// Call endpoints .../list and .../list?token=<paramref name="token"/>.
		/// Returns a packaged <see cref="ListReturn"/> object.
		/// </summary>
		/// <returns></returns>
		public async Task<ReturnPair<ListReturn>> GetListAsync(string token)
		{
			if (null == token)
				List_UriBuilder.Query = null;
			else
				List_UriBuilder.Query = $"token={token}";

			var result = await ReadJsonAsync<ListReturn>(List_UriBuilder.Uri);

			return result;
		}

		/*******************************************************************************
			GetDetailAsync()
		*******************************************************************************/
		/// <summary>
		/// Call endpoint .../detail/{id}. Returns a packaged <see cref="Person"/> object.
		/// </summary>
		/// <returns></returns>
		public async Task<ReturnPair<Person>> GetDetailAsync(int id)
		{
			Detail_UriBuilder.Path = $"{DetailPath}/{id}";

			return await ReadJsonAsync<Person>(Detail_UriBuilder.Uri);
		}

	}
}