using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatoveStrukutrySemPraceA.Entity.ZeleznicniDoprava
{
    public class Stanice
    {

        public bool Koncova { get; set; }
        public bool Pocatecni { get; set; }

        public Dictionary<string, string> PovoleneStaniceZDo { get; set; } = new Dictionary<string, string>();

        public void PridejPovolenouCestu(string nazevStanice, string nazevStaniceZ, string nazevStaniceDo)
        {
            this.PovoleneStaniceZDo[nazevStaniceZ] = nazevStaniceDo;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
