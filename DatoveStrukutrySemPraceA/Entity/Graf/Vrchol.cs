using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatoveStrukutrySemPraceA.Entity.Graf
{
    //seznam vrcholu
    internal class Vrchol<DV, DH>
    {
        private List<Hrana<DV, DH>> VychazejiciHrany { get; set; } = new List<Hrana<DV, DH>>();
        public DV Data { get; set; }

        public Vrchol()
        {

        }

        public Vrchol(List<Hrana<DV, DH>> vychazejiciHrany, DV data)
        {
            this.VychazejiciHrany = vychazejiciHrany;
            this.Data = data;
        }

        public void PridejHranu(Hrana<DV, DH> hrana) { 
            VychazejiciHrany.Add(hrana);
        }
    }
}
