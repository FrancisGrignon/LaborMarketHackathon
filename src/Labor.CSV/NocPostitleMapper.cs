using Labor.Elastic.Models;
using TinyCsvParser.Mapping;

namespace Labor.CSV
{
    public class NocPostitleMapper : CsvMapping<NocPostitle>
    {
        public NocPostitleMapper()
        {
            // NOC;Postitle
            MapProperty(0, x => x.Noc);
            MapProperty(1, x => x.Postitle);
        }
    }
}
