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
            manager.InitialiseAsync();
            var countries = manager.GetCountries();

            foreach (var country in countries)
            {
                Console.WriteLine($@"{country.Name}");
            }

            Console.ReadKey();
        }
    }
}
