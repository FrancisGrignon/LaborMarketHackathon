using Nest;

namespace Labor.Elastic.Models
{
    public class NocPostitle
    {
        [Text]
        public int Noc { get; set; }

        [Text]
        public string Postitle { get; set; }
    }
}
