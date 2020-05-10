using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountriesWrapper
{
    public interface ICountryManager
    {
        Task Initialise();
        IEnumerable<Country> GetCountries();
        Country GetCountry(string countryName);
    }
}
