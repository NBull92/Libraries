using CountriesWrapper;
using System;

namespace CountriesProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        private static async void Run()
        {
            ICountryManager manager = new CountryManager();
            await manager.Initialise();
            var countries = manager.GetCountries();

            foreach (var country in countries)
            {
                Console.WriteLine($@"{country.Name}");
            }

            Console.ReadKey();
        }
    }
}
