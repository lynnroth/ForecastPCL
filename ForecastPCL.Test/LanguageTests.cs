﻿namespace ForecastPCL.Test
{
    using System;
    using System.Configuration;

    using ForecastIOPortable;

    using NUnit.Framework;

    [TestFixture]
    public class LanguageTests
    {
        // These coordinates came from the Forecast API documentation,
        // and should return forecasts with all blocks.
        private const double AlcatrazLatitude = 37.8267;
        private const double AlcatrazLongitude = -122.423;

        private const double MumbaiLatitude = 18.975;
        private const double MumbaiLongitude = 72.825833;

        /// <summary>
        /// API key to be used for testing. This should be specified in the
        /// test project's app.config file.
        /// </summary>
        private string apiKey;

        /// <summary>
        /// Sets up all tests by retrieving the API key from app.config.
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            this.apiKey = ConfigurationManager.AppSettings["ApiKey"];
        }

        [Test]
        public void AllLanguagesHaveValues()
        {
            foreach (Language language in Enum.GetValues(typeof(Language)))
            {
                Assert.That(() => language.ToValue(), Throws.Nothing);
            }
        }

        [Test]
        public async void UnicodeLanguageIsSupported()
        {
            var client = new ForecastApi(this.apiKey);
            var result = await client.GetWeatherDataAsync(AlcatrazLatitude, AlcatrazLongitude, Unit.Auto, Language.Chinese);
            Assert.That(result, Is.Not.Null);
        }
    }
}
