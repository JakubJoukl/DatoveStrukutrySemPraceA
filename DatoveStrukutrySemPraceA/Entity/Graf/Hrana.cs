using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DatoveStrukutrySemPraceA.Entity.Graf
{
    //seznam hran
    internal class Hrana<DV, DH>
    {
        public Vrchol<DV, DH> CilovyVrchol { get; set; }
        public DH Data { get; set; }

        public Hrana() { 
        
        }

        public Hrana(Vrchol<DV, DH> cilovyVrchol, DH data)
        {
            this.CilovyVrchol = cilovyVrchol;
            this.Data = data;
        }
    }
}
