using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppSheet1
{
	/***************************************************************************************************
		MiddleService class
	***************************************************************************************************/
	/// <summary>
	/// This service sits atop the <see cref="AppSheet1.SampleService"/> to completely abstract the <see cref="AppSheet1.SampleService"/>
	/// from the app. It adds caching (see <see cref="Cache"/>) and a variety of ways to specify result set criteria
	/// (see <see cref="GetPeopleAsync(int, PersonSort, bool, bool, bool, bool)"/>.
	/// </summary>
	public class MiddleService
	{
		/// <summary>
		/// Local sample service object.
		/// </summary>
		SampleService SampleService { get; } = new SampleService();

		/// <summary>
		/// Gets or sets whether the <see cref="Cache"/> is to be used.
		/// </summary>
		public bool UseCache
		{
			get => null != Cache;
			set
			{
				if (value == UseCache)
					return;

				if (value)
					Cache = new Dictionary<int, Person>();
				else
					Cache = null;
			}
		}

		/// <summary>
		/// In-memory cache of <see cref="People"/>. Based on requirements (~27 objects x 1000 x ~50k per object)
		/// it could grow to ~1.3G which may not be ideal. Depending on need, there are a variety of alternatives
		/// such as file caching, adding a maximum size (FIFO), only cache object that meet current retrieval criteria
		/// etc.
		/// </summary>
		Dictionary<int, Person> Cache { get; set; }

		/*******************************************************************************
			GetPersonAsync_()
		*******************************************************************************/
		/// <summary>
		/// Private method used to retrieve a <see cref="Person"/> object from
		/// the sample service. If there is an error retrieving the person then
		/// an object is still created with error information. If the cache is
		/// in use the person will be cached.
		/// </summary>
		async Task<Person> GetPersonAsync_(int id)
		{
			var info = await SampleService.GetDetailAsync(id);

			Person person = null;

			if (System.Net.HttpStatusCode.OK == info.HttpStatusCode)
				person = info.Object;
			else
				person = new Person() { Id = id };

			person.StatusCode = info.HttpStatusCode;
			person.RetrievalTimestamp = DateTime.UtcNow;

			if (UseCache)
				Cache.Add(id, person);

			return person;
		}

		/*******************************************************************************
			GetPersonAsync()
		*******************************************************************************/
		/// <summary>
		/// Public method used to retrieve a <see cref="Person"/> object from either
		/// the cache (if in use and cached) or the sample service.
		/// </summary>
		public async Task<Person> GetPersonAsync(int id, bool validDetails, bool wantPhoto)
		{
			// If the cache is in use and contains the requested person then retrieve it
			if (UseCache && Cache.TryGetValue(id, out var person))
			{
				// Nothing more to do here!
			}
			// Get the person from the sample service
			else
				person = await GetPersonAsync_(id);

			// Handle validDetails
			if (validDetails && System.Net.HttpStatusCode.OK != person.StatusCode)
				return null;

			// Handle wantPhoto
			if (wantPhoto)
			{
				if (null == person.Photo && !String.IsNullOrEmpty(person.PhotoUriString))
					person.Photo = new System.Windows.Media.Imaging.BitmapImage(new Uri(person.PhotoUriString));
			}
			else if (null != person.Photo)
				person.Photo = null;

			return person;
		}

		/*******************************************************************************
			GetPeopleAsync()
		*******************************************************************************/
		/// <summary>
		/// Returns a list of <see cref="Person"/> objects from either the cache or sample
		/// service based on the specified result set criteria.
		/// </summary>
		/// <param name="maxRows">Maximum number of rows to return.</param>
		/// <param name="personSort">Property to sort.</param>
		/// <param name="descending">Sort descending if true.</param>
		/// <param name="validDetails">Only return rows with valid details, i.e. successful call to sample service.</param>
		/// <param name="wantValidPhoneNumber">Filter out malformed phone numbers if true.</param>
		/// <param name="wantPhoto">Include photo, provided one exists, if true.</param>
		/// <returns></returns>
		public async Task<List<Person>> GetPeopleAsync(int maxRows, PersonSort personSort, bool descending, bool validDetails, bool wantValidPhoneNumber, bool wantPhoto)
		{
			string token = null;

			var people = new List<Person>();

			// Get all people...
			do
			{
				var info = await SampleService.GetListAsync(token);

				if (System.Net.HttpStatusCode.OK != info.HttpStatusCode)
					return null;

				var listReturn = info.Object;

				foreach (var cur in listReturn.Result)
				{
					var person = await GetPersonAsync(cur, validDetails, wantPhoto);

					if (null != person && ((!wantValidPhoneNumber) || person.IsPhoneNumberValid()))
						people.Add(person);
				}

				token = listReturn.Token;

			} while (null != token);

			// Return in ascending or descending order, limiting the maximum number of rows
			IOrderedEnumerable<Person> temp;
			if (descending)
				temp = people.OrderByDescending(o => GetSortKey(o));
			else
				temp = people.OrderBy(o => GetSortKey(o));

			return temp.Take(maxRows).ToList();

			object GetSortKey(Person p)
			{
				switch (personSort)
				{
					case PersonSort.Age: return p.Age;
					case PersonSort.Bio: return p.Bio;
					case PersonSort.Id: return p.Id;
					case PersonSort.Name: return p.Name;
					case PersonSort.PhoneNumber: return p.PhoneNumber;
				}

				return null;
			}
		}
	}
}
