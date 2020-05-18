using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountriesWrapper
{
    public interface ICountryManager
    {
        Task InitialiseAsync();
        IEnumerable<Country> GetCountries();
        Country GetCountry(string countryName);
        void SetRepository(IRepository<Country> repository);
    }
}
