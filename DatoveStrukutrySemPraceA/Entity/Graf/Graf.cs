using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

        public void PridejPovolenouCestu(string nazevVrcholu, string nazevVrcholuZ, string nazevVrcholuDo) {
            if (Vrcholy.ContainsKey(nazevVrcholuZ) && Vrcholy.ContainsKey(nazevVrcholuDo)) {
                Vrchol<DV, DH> vrchol = Vrcholy[nazevVrcholu];
                vrchol.PovoleneVrcholyZDo[nazevVrcholuZ] = nazevVrcholuDo;
            } else {
                throw new ArgumentException("Vrchol z/do neexistuje");
            }
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
            Dictionary<string, List<string>> seznamL = new Dictionary<string, List<string>>();
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

            Console.WriteLine("Velikost seznamu L: " + seznamL.Count);
            return seznamL;
        }

        private void VytvorCestyZVrcholu(Vrchol<DV, DH> vrchol, List<List<String>> vraceneCesty, List<String> zkoumanaCesta) {
            zkoumanaCesta.Add(vrchol.Nazev);
            //Co je treba osetrit - vrcholy s vice hranami a vrcholy co jsou koncove a zaroven z nich pokracuje cesta
            //Zde klonuji stavajici cestu - je nutne ji ulozit - je platna i bez toho, aniz bych dotraverzoval na konec - jedna se o mezi jakysi zaznam

            //

            if (vrchol.JeKoncovy && vrchol.VychazejiciHrany.Any())
            {
                foreach (Hrana<DV, DH> hrana in vrchol.VychazejiciHrany)
                {
                    if (!JePovolenaCesta(vrchol, hrana.CilovyVrchol, zkoumanaCesta)) continue;
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
                List<Hrana<DV, DH>> povoleneHrany = vrchol.VychazejiciHrany.Where((zkoumanaHrana) => JePovolenaCesta(vrchol, zkoumanaHrana.CilovyVrchol, zkoumanaCesta)).ToList();
                //nejprve musim vytvorit ostatni cesty, pote mohu pracovat s tou aktualni 
                for (int i = 1; i < povoleneHrany.Count; i++) {
                    List<String> kopieExistujiciCesty = new List<string>(zkoumanaCesta);
                    vraceneCesty.Add(kopieExistujiciCesty);
                    VytvorCestyZVrcholu(povoleneHrany[i].CilovyVrchol, vraceneCesty, kopieExistujiciCesty);
                }
                VytvorCestyZVrcholu(povoleneHrany.First().CilovyVrchol, vraceneCesty, zkoumanaCesta);
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
            //nejak potrebuji zajistit, aby v pripade nalezeni schody probehlo hledani znovu, ale bez zkoumani vrcholu z te cesty?

            //nastartuji cykly pro dvojice z kazdeho vrcholu...
            foreach (var vnejsiCestaKlicHodnota in seznamCest)
            {
                string vnejsiNazev = vnejsiCestaKlicHodnota.Key;
                //Slo by i jako nejaky posun ve forcyklu? Mozna - algoritmus bude muset pridavat pouze ten prvni nalezeny vrchol
                HashSet<string> seznamUzZkoumanychVrcholuCest = new HashSet<string>();
                seznamUzZkoumanychVrcholuCest.UnionWith(vnejsiCestaKlicHodnota.Value);

                //Najde Ntice?
                NajdiNtice(seznamUzZkoumanychVrcholuCest, new List<string> { vnejsiNazev }, seznamCest, seznamR);
                //seznamR = seznamR.Distinct(ListEqualityComparer<string>.Default).ToList();

                //seznamR.ForEach(s => Console.WriteLine("{" + string.Join(",", s.ToArray()) + "}"));
                //Console.WriteLine("Velikost seznamu R: " + seznamR.Count);

                //continue excludne porovnavani aktualni cesty sama se sebou
                /*bool allMatchesHaveBeenFound = true;
                do {
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
                } while (!allMatchesHaveBeenFound);*/
            }

            //odeberu duplikaty
            seznamR = seznamR.Distinct(ListEqualityComparer<string>.Default).ToList();
            Console.WriteLine("Velikost seznamu R: " + seznamR.Count);
            Dictionary<int, int> poctyMnozin = new Dictionary<int, int>();
            poctyMnozin[2] = 0;
            poctyMnozin[3] = 0;
            poctyMnozin[4] = 0;
            poctyMnozin[5] = 0;
            seznamR.ForEach(mnozina => {
                poctyMnozin[mnozina.Count] = poctyMnozin[mnozina.Count] + 1;
            });

            //seznamR.ForEach(s => Console.WriteLine("{" + string.Join(",", s.ToArray()) + "}"));
            return seznamR;
        }

        //Nejaka zasobnikova struktura
        //Pro jeden cyklus?
        public void NajdiNtice(HashSet<string> navstiveneVrcholyVCeste, List<string> aktualniDisjunktniCesty,
            //seznamCest (seznamL) a seznamR jsou konstanty, nemodifikuji je zde!!!
            Dictionary<string, List<String>> seznamCest, List<List<string>> seznamR)
        {
            foreach (var cesta in seznamCest)
            {
                //Pokud je vrchol v ceste tak pokracuji
                if (aktualniDisjunktniCesty.Contains(cesta.Key))
                    continue;

                //Jedna se o vrcholy cesty
                List<string> zkoumanaCesta = cesta.Value;

                bool jeDisjunktni = true;
                foreach (string cestaVrchol in zkoumanaCesta)
                {
                    if (navstiveneVrcholyVCeste.Contains(cestaVrchol))
                    {
                        jeDisjunktni = false; break;
                    }
                }
                if (jeDisjunktni) {
                    HashSet<string> kopieNavstivenychVrcholu = new HashSet<string>();
                    kopieNavstivenychVrcholu.UnionWith(navstiveneVrcholyVCeste);
                    kopieNavstivenychVrcholu.UnionWith(zkoumanaCesta);

                    List<string> aktualniDisjunktniCestyKopie = new List<string>();
                    aktualniDisjunktniCestyKopie.AddRange(aktualniDisjunktniCesty);
                    aktualniDisjunktniCestyKopie.Add(cesta.Key);
                    seznamR.Add(aktualniDisjunktniCestyKopie);
                    NajdiNtice(kopieNavstivenychVrcholu, aktualniDisjunktniCestyKopie, seznamCest, seznamR);
                }
            }
        }

        //Pokud nemam zadane povolene cesty tak je povoleno vse
        //Pokud mam v ceste pouze jeden Vrchol, tak to znamena, ze jsem ve stavajicim vrcholu - v aktualni implementaci toto neni povolene pro prvni bod - chovani je vyjimka? 
        private bool JePovolenaCesta(Vrchol<DV, DH> vrchol, Vrchol<DV, DH> cilovyVrchol, List<String> cesta) {
            if (vrchol.PovoleneVrcholyZDo.Count > 0)
            {
                if (cesta.Count <= 1)
                {
                    throw new ArgumentException("Je zadan seznam povolenych cest ale seznam je kratsi nez 1");
                }
                string nazevCilovehoVrcholu;
                //Ziskam predposledni vlozeny prvek do cesty
                vrchol.PovoleneVrcholyZDo.TryGetValue(cesta[cesta.Count() - 2], out nazevCilovehoVrcholu);
                return nazevCilovehoVrcholu != null && nazevCilovehoVrcholu.Equals(cilovyVrchol.Nazev);
            }
            else {
                return true;
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
