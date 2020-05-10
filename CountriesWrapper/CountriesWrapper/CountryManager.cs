using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace CountriesWrapper
{
    public class CountryManager : ICountryManager
    {
        private const string Url = "https://restcountries.eu/rest/v2/";
        private readonly IRepository<Country> _repository = new CountryRepository();

        public async Task InitialiseAsync()
        {
            var client = new HttpClient { BaseAddress = new Uri(Url) };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("all"); 

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
            
            client.Dispose();
        }

        public IEnumerable<Country> GetCountries()
        {
            return _repository.GetAll();
        }

        public Country GetCountry(string countryName)
        {
            return _repository.Get(countryName);
        }
    }
}