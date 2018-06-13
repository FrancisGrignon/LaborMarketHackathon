using Nest;
using System;

namespace Labor.Elastic.Models
{
    // CAR_CHC_ID;Sel_Process_Nbr;Department_E;Department_F;Open_Date;Close_Date;Adv_Name_E;Adv_Name_F;url
    public class SelProcess
    {
        [Number]
        public int CarChcId { get; set; }

        [Text]
        public string SelProcessBr { get; set; }

        [Text]
        public string DepartmentE { get; set; }

        [Text]
        public string DepartmentF { get; set; }

        [Date]
        public DateTime OpenDate { get; set; }

        [Date]
        public DateTime CloseDate { get; set; }

        [Text]
        public string AdvNameE { get; set; }

        [Text]
        public string AdvNameF { get; set; }

        [Text]
        public string Url { get; set; }
    }
}
