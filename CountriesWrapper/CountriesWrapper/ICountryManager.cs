using System.Collections.Generic;

namespace CountriesWrapper
{
    public interface ICountryManager
    {
        void Initialise();
        IEnumerable<Country> GetCountries();
        Country GetCountry(string countryName);
    }
}
