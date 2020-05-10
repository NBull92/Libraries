using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CountriesWrapper
{
    public interface ICountryManager
    {
        void Initialise();
        IEnumerable<Country> GetCountries();
    }

    public class CountryManager : ICountryManager
    {
        private const string Url = "https://restcountries.eu/rest/v2/";
        private readonly IRepository<Country> _repository = new CountryRepository();

        public async void Initialise()
        {
            var client = new HttpClient { BaseAddress = new Uri(Url) };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("all"); // Blocking call! Program will wait here until a response is received or a timeout occurs.

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var countries = JsonSerializer.Deserialize<List<Country>>(dataObjects, options);               
                _repository.AddRange(countries);
            }
            else
            {
                throw new Exception(response.RequestMessage.ToString());
            }
            
            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }

        public IEnumerable<Country> GetCountries()
        {
            return _repository.GetAll();
        }
    }

    public interface IRepository<T> where T : class
    {
        T Get(string name);
        IEnumerable<T> GetAll();
        void Add(T obj);
        void AddRange(IEnumerable<T> objects);
        IEnumerable<T> Find(Func<T, bool> predicate);
    }


    class CountryRepository : IRepository<Country>
    {
        private readonly List<Country> _countries = new List<Country>();

        public Country Get(string name)
        {
            return _countries.FirstOrDefault(c => c.Name.Equals(name));
        }

        public IEnumerable<Country> GetAll()
        {
            return _countries;
        }

        public void Add(Country obj)
        {
            _countries.Add(obj);
        }

        public void AddRange(IEnumerable<Country> objects)
        {
            _countries.AddRange(objects);
        }

        public IEnumerable<Country> Find(Func<Country, bool> predicate)
        {
            return _countries.Where(predicate);
        }
    }
}
