using System;
using System.Collections.Generic;
using System.Linq;

namespace CountriesWrapper
{
    internal class CountryRepository : IRepository<Country>
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