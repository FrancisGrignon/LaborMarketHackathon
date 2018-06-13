using Nest;

namespace Labor.Elastic.Models
{
    public class SomcData
    {
        [Number]
        public int SomcId { get; set; }

        [Number]
        public int CarChcId { get; set; }

        [Number]
        public int SomcComboId { get; set; }

        [Text]
        public string EDesc { get; set; }

        [Text]
        public string FDesc { get; set; }

        [Text]
        public string SubElementDesc { get; set; }
    }
}
