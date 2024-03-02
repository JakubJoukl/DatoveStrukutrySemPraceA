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
        public Dictionary<string, List<String>> DejSeznamL() {
            Dictionary<string, List<String>> seznamL = new Dictionary<string, List<string>>();
            int cisloCesty = 1;
            Dictionary<string, Vrchol<DV, DH>> vstupniVrcholy = DejVstupniVrcholy();
            foreach (var vrcholNazev in vstupniVrcholy)
            {
                Vrchol<DV, DH> zkoumanyVrchol = vrcholNazev.Value;
                List<List<string>> vraceneCesty = new List<List<string>>();
                //Vytvoreni uvodniho listu do seznamu vracenych cest - z kazdeho vrcholu je vracena minimalne jedna cesta (pripadne by slo i osetrit empty listy kontrolou na to, zda prvni vrchol ma cesty
                List<String> uvodniCesta = new List<string>();
                vraceneCesty.Add(uvodniCesta);
                VytvorCestyZVrcholu(zkoumanyVrchol, vraceneCesty, uvodniCesta);
                vraceneCesty.ForEach(cesta =>
                {
                    string nazevCesty = "A" + cisloCesty;
                    seznamL[nazevCesty] = cesta;
                    Console.WriteLine(nazevCesty + " {" + string.Join(",", cesta.ToArray()) + "}");
                    cisloCesty++;
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

        public List<List<string>> DejSeznamR() {
            Dictionary<string, List<String>> seznamCest = DejSeznamL();
            List<List<string>> seznamR = new List<List<string>>();

            //kazdou cestu musim porovnat s kazdou cestou -> nejake dva cykly, jeden pro prochazeni pro veskere nalezene cesty
            //a druhy pro porovnavani vrcholu teto cesty s vrcholy ostatnich cest -> tim ziskam dvojice/trojice atd
            //Pro spravne chovani musim uchovavat seznam jiz projitych vrcholu (jak se chova s krizovatkou?)
            //Nejak budu muset hlidat v pripade nalezeni trojice/ctverice/petice i to, aby predchozi par nezanikl
            //Bude vhodne ulohu omezit tak, aby v pripade nalezeni maximalniho paru (dvojice/trojice) se aktualni pruchod algoritmu ukoncil?
            //TODO ted je tam bug - naleznu vzdy jen jendnu nejpocetnejsi disjunktni cestu mezi vrcholy (po oprave i jeji podcasti)
            //nejak potrebuji zajistit, aby v pripade nalezeni schody probehlo hledani znovu, ale bez zkoumani vrcholu z te cesty?
            foreach (var vnejsiCestaKlicHodnota in seznamCest)
            {
                string vnejsiNazev = vnejsiCestaKlicHodnota.Key;
                //Slo by i jako nejaky posun ve forcyklu? Mozna - algoritmus bude muset pridavat pouze ten prvni nalezeny vrchol
                HashSet<string> seznamUzZkoumanychVrcholuCest = new HashSet<string>
                {
                    vnejsiNazev
                };

                //continue excludne porovnavani aktualni cesty sama se sebou
                bool allMatchesHaveBeenFound = true;
                do
                {
                    //Tento list asi nebudu potrebovat?
                    List<string> vnejsiSeznamVrcholu = vnejsiCestaKlicHodnota.Value;
                    HashSet<string> navstiveneVrcholy = new HashSet<string>(vnejsiSeznamVrcholu);
                    List<string> seznamVylucnychCest = new List<string>
                    {
                        vnejsiNazev
                    };
                    allMatchesHaveBeenFound = true;
                    foreach (var vnitrniCestaKlicHodnota in seznamCest)
                    {
                        string vnitrniNazev = vnitrniCestaKlicHodnota.Key;
                        if (seznamUzZkoumanychVrcholuCest.Contains(vnitrniNazev)) continue;
                        List<string> vnitrniSeznamVrcholu = vnitrniCestaKlicHodnota.Value;
                        bool cestyJsouVylucne = true;

                        foreach (string vnitrniVrchol in vnitrniSeznamVrcholu)
                        {
                            if (navstiveneVrcholy.Contains(vnitrniVrchol))
                            {
                                cestyJsouVylucne = false; break;
                            }
                        }

                        if (cestyJsouVylucne)
                        {
                            seznamVylucnychCest.Add(vnitrniNazev);
                            if (seznamVylucnychCest.Count == 2) {
                                seznamUzZkoumanychVrcholuCest.Add(vnitrniNazev);
                            }

                            //neni tato podminka zbytecna?
                            if (seznamVylucnychCest.Count > 1)
                            {
                                //Zajistuje, aby byly zachovany i mensi podmnoziny nez je delka 3
                                //Nekontroluje duplicitu dvojic/trojic/ctveric?
                                allMatchesHaveBeenFound = false;
                                seznamR.Add(new List<string>(seznamVylucnychCest));
                                Console.WriteLine("{" + string.Join(",", seznamVylucnychCest.ToArray()) + "}");
                            }
                            foreach (string vrchol in vnitrniSeznamVrcholu)
                            {
                                navstiveneVrcholy.Add(vrchol);
                            }
                        }
                    }
                } while (!allMatchesHaveBeenFound);
            }

            //odeberu duplikaty
            seznamR = seznamR.Distinct(ListEqualityComparer<string>.Default).ToList();
            return seznamR;
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

            public Dictionary<string, string> PovoleneVrcholyZDo { get; set; } = new Dictionary<string, string>();

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
