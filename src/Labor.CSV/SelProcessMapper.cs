using Labor.Elastic.Models;
using TinyCsvParser.Mapping;

namespace Labor.CSV
{
    public class SelProcessMapper : CsvMapping<SelProcess>
    {
        public SelProcessMapper()
        {
            // CAR_CHC_ID;Sel_Process_Nbr;Department_E;Department_F;Open_Date;Close_Date;Adv_Name_E;Adv_Name_F;url

            MapProperty(0, x => x.CarChcId);
            MapProperty(1, x => x.SelProcessBr);
            MapProperty(2, x => x.DepartmentE);
            MapProperty(3, x => x.DepartmentF);
            MapProperty(4, x => x.OpenDate);
            MapProperty(5, x => x.CloseDate);
            MapProperty(6, x => x.AdvNameE);
            MapProperty(7, x => x.AdvNameF);
            MapProperty(8, x => x.Url);
        }
    }
}
