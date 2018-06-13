using Labor.Elastic.Models;
using TinyCsvParser.Mapping;

namespace Labor.CSV
{
    public class SomcDataMapper : CsvMapping<SomcData>
    {
        public SomcDataMapper()
        {
            // SOMCID;CARCHCID;SOMCCOMBOID;EDESC;FDESC;subelementdesc
            MapProperty(1, x => x.CarChcId);
            MapProperty(3, x => x.EDesc);
            MapProperty(4, x => x.FDesc);
            MapProperty(2, x => x.SomcComboId);
            MapProperty(0, x => x.SomcId);
            MapProperty(5, x => x.SubElementDesc);
        }
    }
}
