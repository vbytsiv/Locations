using Nest;
using Newtonsoft.Json.Linq;
using Soply.Prototype.Locations.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Soply.Prototype.Locations.Data
{
    public class ElasticSearchContext
    {
        private readonly ElasticClient client;
        private readonly string indexName;
        private readonly string settingsPath;

        //todo:move to config
        private readonly string uri = "http://e1769d00633423e91f3b85fe2555dab9.us-east-1.aws.found.io:9200";

        public ElasticSearchContext(string indexName, string settingsPath)
        {
            this.indexName = indexName;
            this.settingsPath = settingsPath;
            var node = new Uri(uri);

            var settings = new ConnectionSettings(node,
                defaultIndex: indexName
            );

            client = new ElasticClient(settings);
        }

        public void SetupIndex()
        {
            if (client.IndexExists(this.indexName).Exists) return;

            var settings = JObject.Parse(File.ReadAllText(this.settingsPath));

            string result = string.Empty;

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
               result = client.UploadString(uri + "/" + this.indexName, "POST", settings.ToString());
            }

            Console.WriteLine(result);
        }

        public void RemoveIndex()
        {
            client.DeleteIndex(this.indexName);
        }

        public void RemoveIndex(string indexName)
        {
            client.DeleteIndex(indexName);
        }

        public void Add(IEnumerable<Location> locations)
        {
            client.IndexMany(locations);
        }

        public void Add(Location location)
        {
            client.Index(location);
        }

        public IEnumerable<Location> Search(string query)
        {
            var result = client.Search<Location>(s => s
                .From(0)
                .Size(10)
                .Query(q => q.Bool(
                    bq => bq.Should(
                        qt => qt.Match(
                            st => st.OnField(f => f.Title).Query(query).Boost(5)),
                        qt => qt.Match(
                            sk => sk.OnField(f => f.Keywords).Query(query).Boost(1)),
                        qt => qt.Match(
                            slt => slt.OnField(f => f.Latitude).Query(query).Boost(1)),
                        qt => qt.Match(
                            sln => sln.OnField(f => f.Longitude).Query(query).Boost(1))))));
            return result.Documents;
        }
    }
}
