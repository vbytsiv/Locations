using NUnit.Framework;
using Soply.Prototype.Locations.Data;
using Soply.Prototype.Locations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using System.Threading;

namespace Soply.Prototype.Locations.Test
{
    [TestFixture]
    public class ElasticSearchContextTest
    {
        private ElasticSearchContext context;
        string indexName;

        [TestFixtureSetUp]
        public void Setup()
        {
            indexName = "locs" + Guid.NewGuid().ToString().Substring(0, 3);
            context = new ElasticSearchContext(indexName, @"Data\Settings.json");

            context.SetupIndex();

            context.Add(GetTestLocations());

            //todo: how to know when indexing is done?
            Thread.Sleep(3000);
        }

        [TestCase("Aarhus", 1)]
        [TestCase("Aalborg", 1)]
        [TestCase("Denmark", 3)]
        [TestCase("Den", 3)]
        [TestCase("Odense Nordjylland", 2)]
        [TestCase("5", 3)]
        [TestCase("57", 1)]
        [TestCase("57.1", 0)]
        [TestCase("57.1 9.91", 1)]
        [TestCase("9", 1)]
        [TestCase("", 3)]
        public void Test(string query, int count)
        {
            var results = context.Search(query);
            Assert.That(results.Count(), Is.EqualTo(count));
        }

        private IEnumerable<Location> GetTestLocations()
        {
            return new List<Location>
            {
                new Location
                {
                    Id = 1,
                    Keywords = "DK Denmark Hovedstaden",
                    Latitude = "56.157204",
                    Longitude = "10.21068396",
                    Title = "Aarhus"
                },
                new Location
                {
                    Id = 2,
                    Keywords = "DK Denmark Nordjylland",
                    Latitude = "57.03371381",
                    Longitude = "9.916593382",
                    Title = "Aalborg"
                },
                new Location
                {
                    Id = 3,
                    Keywords = "DK Denmark Syddanmark",
                    Latitude = "55.40037681",
                    Longitude = "10.38333492",
                    Title = "Odense"
                }
            };
        }


        [TestFixtureTearDown]
        public void TearDown()
        {
            context.RemoveIndex(indexName);
        }
    }
}
