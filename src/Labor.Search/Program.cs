using Labor.CSV;
using Labor.Elastic;
using Labor.Elastic.Models;
using log4net;
using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TinyCsvParser;

namespace Labor.Search
{
    class Program
    {
        private const string INDEX_NAME = "indexb";

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static ElasticSearchClient<NocPostitle> client;

        static void Main(string[] args)
        {
            var connectionString = new ConnectionString("http", "localhost", 9200);

            client = new ElasticSearchClient<NocPostitle>(connectionString, INDEX_NAME);

            //            GET / index / type / _search
            //{
            //                "size": 0,
            //"query": {
            //                    "terms": {
            //                        "country":  ["France","Spain","Germany"]
            //    }
            //},
            //"aggs": {
            //  "group_by_country": {
            //     "terms": {
            //        "field": "country",
            //        "size" :0 
            //     },
            //     "aggs":{
            //      "top_hits_country"   :{
            //       "top_hits"   :{
            //           "size":1
            //       }
            //      }
            //     }
            //    }
            //  }
            // }.

            bool found = false;
            string postitle;
            int id;

            using (var writer = new StreamWriter("C:\\Data\\SEL_PROCESSES_16_17_NOS.csv"))
            {
                writer.WriteLine($"CAR_CHC_ID;Adv_Name_E;NOC;Postitle");

                foreach (var data in GetSelProcesses("C:\\Data\\SEL_PROCESSES_16_17.csv"))
                {
                    Console.Write(".");

                    found = false;
                    postitle = data.AdvNameE;
                    id = data.CarChcId;

                    var response = client.Search(s => s
                       .Query(q => q
                           .Match(m => m
                               .Field(f => f.Postitle)
                               .Query(postitle)
                           )
                       )
                       .Aggregations(a => a.TopHits("top_hits_postitle", p => p.Size(1)))
                   );

                    foreach (var item in response)
                    {
                        writer.WriteLine($"{id};{postitle};{item.Noc};{item.Postitle}");

                        found = true;
                    }

                    if (false == found)
                    {
                        writer.WriteLine($"{id};{postitle}");
                    }

                    found = false;
                }
            }
        }

        private static IEnumerable<SelProcess> GetSelProcesses(string fileName)
        {
            return Parsers
                .SelProcessMapper
                .ReadFromFile(fileName, Encoding.ASCII)
                .Where(x => x.IsValid)
                .Select(x => x.Result)
                .AsEnumerable();
        }

        private static string BestMatch(string postitle)
        {
            return null;
        }

        private static IEnumerable<SomcData> GetSomcData(string fileName)
        {
            return Parsers
                .SomcDataMapper
                .ReadFromFile(fileName, Encoding.ASCII)
                .Where(x => x.IsValid)
                .Select(x => x.Result)
                .AsEnumerable();
        }
    }
}
