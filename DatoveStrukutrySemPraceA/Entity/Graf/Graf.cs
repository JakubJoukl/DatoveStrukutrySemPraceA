using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DatoveStrukutrySemPraceA.Entity.Graf
{
    internal class Graf<DV, DH>
    {
        private Dictionary<string, Vrchol<DV, DH>> Vrcholy { get; set; } = new Dictionary<string, Vrchol<DV, DH>>();

        public List<string> dejSeznamVrcholu() { 
            return Vrcholy.Keys.ToList();
        }

        public Dictionary<string, string> DejVstupniVrcholyStruktury()
        {
            Dictionary<string, string> vstupniVrcholy = new Dictionary<string, string>();
            foreach (var vrcholNazev in Vrcholy)
            {
                string nazevVrcholu = vrcholNazev.Key;
                Vrchol<DV, DH> vrchol = vrcholNazev.Value;
                if (vrchol.JeVstupni)
                {
                    vstupniVrcholy[nazevVrcholu] = nazevVrcholu;
                }
            }
            return vstupniVrcholy;
        }

        public int PocetHranZVrcholu(string nazevVrcholu) {
            return Vrcholy[nazevVrcholu].VychazejiciHrany.Count;
        }

        public List<string> DejNaslednikyVrcholu(string nazevVrcholu) {
            List<string> seznamNasledniku = new List<string>();
            Vrcholy[nazevVrcholu].VychazejiciHrany.ForEach((hrana) =>
            {
                seznamNasledniku.Add(hrana.CilovyVrchol.Nazev);
            });
            return seznamNasledniku;
        }

        public DV DejDataVrcholu(string nazevVrcholu) { 
            return Vrcholy[nazevVrcholu].Data;
        }

        public DH DejDataHrany(string nazevVrcholuZ, string nazevVrcholuDo) {
            Vrchol<DV, DH> vrcholZ = Vrcholy[nazevVrcholuZ];
            Vrchol<DV, DH> vrcholDo = Vrcholy[nazevVrcholuDo];
            foreach (var hrana in vrcholZ.VychazejiciHrany)
            {
                if (hrana.CilovyVrchol == vrcholDo) return hrana.Data;
            }
            throw new Exception("Hrana mezi vrcholy nenalezena");
        }

        public void PridejVrchol(string nazevVrcholu, DV dataVrcholu, bool vstupniVrchol, bool koncovyVrchol)
        {
            Vrcholy[nazevVrcholu] = new Vrchol<DV, DH>() { Data = dataVrcholu, Nazev = nazevVrcholu, JeKoncovy = koncovyVrchol, JeVstupni = vstupniVrchol };
        }

        public void PridejVrchol(string nazevVrcholu, DV dataVrcholu) {
            PridejVrchol(nazevVrcholu, dataVrcholu, false, false);
        }

        public void PridejHranu(string nazevVrcholuZ, string nazevVrcholuDo, DH dataHrany) {
            Vrchol<DV, DH> vrcholZ = Vrcholy[nazevVrcholuZ];
            Vrchol<DV, DH> vrcholDo = Vrcholy[nazevVrcholuDo];
            vrcholZ.PridejHranu(new Hrana<DV, DH> { Data = dataHrany, CilovyVrchol = vrcholDo });
        }

        private Dictionary<string, Vrchol<DV, DH>> DejVstupniVrcholy()
        {
            Dictionary<string, Vrchol<DV, DH>> vstupniVrcholy = new Dictionary<string, Vrchol<DV, DH>>();
            foreach (var vrcholNazev in Vrcholy)
            {
                string nazevVrcholu = vrcholNazev.Key;
                Vrchol<DV, DH> vrchol = vrcholNazev.Value;
                if (vrchol.JeVstupni)
                {
                    vstupniVrcholy[nazevVrcholu] = vrchol;
                }
            }
            return vstupniVrcholy;
        }

        //seznam hran
        private class Hrana<DV, DH>
        {
            public Vrchol<DV, DH> CilovyVrchol { get; set; }
            public DH Data { get; set; }

            public Hrana()
            {

            }

            public Hrana(Vrchol<DV, DH> cilovyVrchol, DH data)
            {
                this.CilovyVrchol = cilovyVrchol;
                this.Data = data;
            }
        }

        private class Vrchol<DV, DH>
        {
            public List<Hrana<DV, DH>> VychazejiciHrany { get; set; } = new List<Hrana<DV, DH>>();
            public DV Data { get; set; }
            public bool JeVstupni {  get; set; }
            public bool JeKoncovy { get; set; }
            public string Nazev {  get; set; }

            public Vrchol()
            {

            }

            public Vrchol(List<Hrana<DV, DH>> vychazejiciHrany, DV data)
            {
                this.VychazejiciHrany = vychazejiciHrany;
                this.Data = data;
            }

            public void PridejHranu(Hrana<DV, DH> hrana)
            {
                VychazejiciHrany.Add(hrana);
            }
        }
    }
}
