using Labor.CSV;
using Labor.Elastic;
using Labor.Elastic.Models;
using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TinyCsvParser;

namespace labor.import
{
    class Program
    {
        private const string INDEX_NAME = "posters";

        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            var connectionString = new ConnectionString("http", "localhost", 9200);

            ImportSomcData(connectionString, "C:\\Data\\SOMC_DATA.csv");
            ImportNocPostitle(connectionString, "C:\\Data\\NOC 2011 Postitles.csv");

            log.Info("Import completed");
        }

        private static void ImportSomcData(ConnectionString connectionString, string fileName)
        {
            var client = new ElasticSearchClient<SomcData>(connectionString, "indexa");

            // Creates the Index, if neccessary:
            client.CreateIndex();

            // Bulk Insert Data:
            foreach (var batch in GetSomcData(fileName).Batch(100))
            {
                var response = client.BulkInsert(batch);
            }
        }

        private static void ImportNocPostitle(ConnectionString connectionString, string fileName)
        {
            var client = new ElasticSearchClient<NocPostitle>(connectionString, "indexb");

            // Creates the Index, if neccessary:
            client.CreateIndex();

            // Bulk Insert Data:
            foreach (var batch in GetNocPostitle(fileName).Batch(100))
            {
                var response = client.BulkInsert(batch);
            }
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

        private static IEnumerable<NocPostitle> GetNocPostitle(string fileName)
        {
            //var parser = Parsers.NocPostitleMapper;

            //foreach (var row in parser.ReadFromFile(fileName, Encoding.UTF8))
            //{
            //    if (row.IsValid)
            //    {
            //        yield return row.Result;
            //    }
            //}


            return Parsers
                .NocPostitleMapper
                .ReadFromFile(fileName, Encoding.UTF8)
                .Where(x => x.IsValid)
                .Select(x => x.Result)
                .AsEnumerable();
        }
    }
}
