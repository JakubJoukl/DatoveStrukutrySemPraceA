using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatoveStrukutrySemPraceA.Entity.Graf
{
    internal class Graf<DV, DH>
    {
        private List<Vrchol<DV, DH>> vstupniVrcholy = new List<Vrchol<DV, DH>>();
        private List<Vrchol<DV, DH>> vrcholy = new List<Vrchol<DV, DH>>();

        public void VlozBranu(Vrchol<DV, DH> brana)
        {
            vstupniVrcholy.Add(brana);
        }

        public List<Vrchol<DV, DH>> ZpristupniBrany()
        {
            return vstupniVrcholy;
        }

        public bool AnulujBranu(Vrchol<DV, DH> brana)
        {
            return vstupniVrcholy.Remove(brana);
        }
    }
}
