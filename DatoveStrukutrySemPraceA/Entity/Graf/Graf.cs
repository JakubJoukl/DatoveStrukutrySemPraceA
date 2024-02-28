using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatoveStrukutrySemPraceA.Entity.Graf
{
    internal class Graf<DV, DH>
    {
        private Dictionary<string, Vrchol<DV, DH>> Vrcholy { get; set; } = new Dictionary<string, Vrchol<DV, DH>>();

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

        //Bude se hodit pro algoritmus
        private Dictionary<string, Vrchol<DV, DH>> DejVstupniVrcholy() {
            Dictionary<string, Vrchol<DV, DH>> vstupniVrcholy = new Dictionary<string, Vrchol<DV, DH>>();
            foreach (var vrcholNazev in Vrcholy) {
                string nazevVrcholu = vrcholNazev.Key;
                Vrchol<DV, DH> vrchol = vrcholNazev.Value;
                if (vrchol.JeVstupni) {
                    vstupniVrcholy[nazevVrcholu] = vrchol;
                }
            }
            return vstupniVrcholy;
        }

        //Seznam cest - v listu (dictionary s nejakym cislovanim?) budou ulozeny nazvy vrcholu
        public List<List<String>> DejSeznamL() {
            List<List<String>> seznamL = new List<List<String>>();
            Dictionary<string, Vrchol<DV, DH>> vstupniVrcholy = DejVstupniVrcholy();
            foreach (var vrcholNazev in vstupniVrcholy)
            {
                Vrchol<DV, DH> zkoumanyVrchol = vrcholNazev.Value;
                List<List<String>> vraceneCesty = new List<List<string>>();
                //Vytvoreni uvodniho listu do seznamu vracenych cest - z kazdeho vrcholu je vracena minimalne jedna cesta (pripadne by slo i osetrit empty listy kontrolou na to, zda prvni vrchol ma cesty
                List<String> uvodniCesta = new List<string>();
                vraceneCesty.Add(uvodniCesta);
                VytvorCestyZVrcholu(zkoumanyVrchol, vraceneCesty, uvodniCesta);
                vraceneCesty.ForEach(cesta =>
                {
                    seznamL.Add(cesta);
                    Console.WriteLine("{" + String.Join(",", cesta.ToArray()) + "}");
                });
            }
            return seznamL;
        }

        private void VytvorCestyZVrcholu(Vrchol<DV, DH> vrchol, List<List<String>> vraceneCesty, List<String> zkoumanaCesta) {
            zkoumanaCesta.Add(vrchol.Nazev);
            //Co je treba osetrit - vrcholy s vice hranami a vrcholy co jsou koncove a zaroven z nich pokracuje cesta
            //Zde klonuji stavajici cestu - je nutne ji ulozit - je platna i bez toho, aniz bych dotraverzoval na konec - jedna se o mezi jakysi zaznam
            if (vrchol.JeKoncovy && vrchol.VychazejiciHrany.Any())
            {
                foreach (Hrana<DV, DH> hrana in vrchol.VychazejiciHrany)
                {
                    //Pokud mam pouze jeden prvek v ceste, tak to znamena, ze v danem koncovem vrcholu jsem i zacal -> jedna se i o pocatecni - cestu velikosti 1 neukladam, nezajima me
                    if (zkoumanaCesta.Count > 1) {
                        List<String> kopieExistujiciCesty = new List<string>(zkoumanaCesta);
                        vraceneCesty.Add(kopieExistujiciCesty);
                    }
                    VytvorCestyZVrcholu(hrana.CilovyVrchol, vraceneCesty, zkoumanaCesta);
                }
                //Cyklus nad tim zabezpeci veskere dalsi vytvoreni cest => zde se mohu vratit
            }
            else if (!vrchol.JeKoncovy && vrchol.VychazejiciHrany.Any())
            {
                //nejprve musim vytvorit ostatni cesty, pote mohu pracovat s tou aktualni 
                for (int i = 1; i < vrchol.VychazejiciHrany.Count; i++) {
                    List<String> kopieExistujiciCesty = new List<string>(zkoumanaCesta);
                    vraceneCesty.Add(kopieExistujiciCesty);
                    VytvorCestyZVrcholu(vrchol.VychazejiciHrany[i].CilovyVrchol, vraceneCesty, kopieExistujiciCesty);
                }
                VytvorCestyZVrcholu(vrchol.VychazejiciHrany.First().CilovyVrchol, vraceneCesty, zkoumanaCesta);
            } else if (vrchol.JeKoncovy) {
                return;
            }
            else {
                throw new DataException("Vrchol není označen jako koncový ale nevycházejí z něj žádné hrany");
            }
        }

        public void DejSeznamR() {
            List<List<String>> seznamCest = DejSeznamL();
            foreach (var item in seznamCest)
            {
                
            }
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
