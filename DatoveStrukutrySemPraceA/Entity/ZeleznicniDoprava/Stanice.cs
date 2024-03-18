using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatoveStrukutrySemPraceA.Entity.ZeleznicniDoprava
{
    public class Stanice
    {
        [JsonProperty]
        public bool Koncova { get; set; }
        [JsonProperty]
        public bool Pocatecni { get; set; }

        [JsonProperty]
        public Dictionary<string, string> PovoleneStaniceZDo { get; set; } = new Dictionary<string, string>();

        public void PridejPovolenouCestu(string nazevStanice, string nazevStaniceZ, string nazevStaniceDo)
        {
            this.PovoleneStaniceZDo[nazevStaniceZ] = nazevStaniceDo;
        }

        [JsonProperty]
        public int X { get; set; }
        [JsonProperty]
        public int Y { get; set; }
    }
}
