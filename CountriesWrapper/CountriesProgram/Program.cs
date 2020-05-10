using CountriesWrapper;
using System;

namespace CountriesProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            ICountryManager manager = new CountryManager();
            manager.Initialise();
            var countries = manager.GetCountries();

            foreach (var country in countries)
            {
                Console.WriteLine($@"{country.Name}");
            }

            Console.ReadKey();
        }
    }
}
