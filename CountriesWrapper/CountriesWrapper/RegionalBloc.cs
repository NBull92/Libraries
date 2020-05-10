using System.Collections.Generic;

namespace CountriesWrapper
{
    public class RegionalBloc
    {
        public string Acronym { get; set; }
        public string Name { get; set; }
        public IList<string> OtherAcronyms { get; set; }
        public IList<string> OtherNames { get; set; }
    }
}