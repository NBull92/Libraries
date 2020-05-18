using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace CountriesWrapper.Tests
{
    [TestFixture]
    public class CountryManagerTests
    {
        private CountryManager _countryManager;
        private IRepository<Country> _countryRepository;

        [SetUp]
        public void SetUp()
        {
            _countryRepository = Substitute.For<IRepository<Country>>();
            _countryManager = new CountryManager();
            _countryManager.SetRepository(_countryRepository);
        }
        
        [Test]
        public void GetCountries_NoCountriesExist_ReturnsEmptyList()
        {
            // Arrange
            _countryRepository.GetAll().Returns(new List<Country>());

            // Act
            var result = _countryManager.GetCountries();

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetCountries_CountriesExist_ReturnsPopulatedList()
        {
            // Arrange
            _countryRepository.GetAll().Returns(new List<Country>
            {
                new Country(),
                new Country(),
                new Country(),
            });

            // Act
            var result = _countryManager.GetCountries();

            // Assert
            Assert.That(result, Is.Not.Empty);
        }
        
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void GetCountry_NoCountriesExists_ReturnsNull(string country)
        {
            // Act
            var result = _countryManager.GetCountry(country);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetCountry_UserRequestedNonListedCountry_ReturnsNull()
        {
            // Arrange
            PopulateCountryRepositoryList();

            // Act
            var result = _countryManager.GetCountry("England");

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetCountry_UserRequestedListedCountry_ReturnsNull()
        {
            // Arrange
            PopulateCountryRepositoryList();

            // Act
            var result = _countryManager.GetCountry("United Kingdom");

            // Assert
            Assert.That(result, Is.Null);
        }

        private void PopulateCountryRepositoryList()
        {
            _countryRepository.AddRange(new List<Country>
            {
                new Country {Name = "United Kingdom"},
                new Country {Name = "United States of America"},
                new Country {Name = "Australia"},
            });
        }
    }
}
