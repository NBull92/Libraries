using System;
using System.Collections.Generic;

namespace CountriesWrapper
{
    public interface IRepository<T> where T : class
    {
        T Get(string name);
        IEnumerable<T> GetAll();
        void Add(T obj);
        void AddRange(IEnumerable<T> objects);
        IEnumerable<T> Find(Func<T, bool> predicate);
    }
}