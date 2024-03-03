using DatoveStrukutrySemPraceA.Entity.Graf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatoveStrukutrySemPraceA.Entity.ZeleznicniDoprava
{
    internal class Vypocty
    {
        //Seznam cest - v listu (dictionary s nejakym cislovanim?) budou ulozeny nazvy vrcholu
        public static Dictionary<string, List<string>> DejSeznamL(Graf<Stanice, Koleje> graf)
        {
            Dictionary<string, List<string>> seznamL = new Dictionary<string, List<string>>();
            int cisloCesty = 1;
            Dictionary<string, string> vstupniStanice = graf.DejVstupniVrcholyStruktury();
            foreach (var staniceKlicHodnota in vstupniStanice)
            {
                Stanice zkoumanyVrchol = graf.DejDataVrcholu(staniceKlicHodnota.Value);
                List<List<string>> vraceneCesty = new List<List<string>>();
                //Vytvoreni uvodniho listu do seznamu vracenych cest - z kazdeho vrcholu je vracena minimalne jedna cesta (pripadne by slo i osetrit empty listy kontrolou na to, zda prvni vrchol ma cesty
                List<String> uvodniCesta = new List<string>();
                vraceneCesty.Add(uvodniCesta);
                VytvorCestyZVrcholu(staniceKlicHodnota.Key, zkoumanyVrchol, vraceneCesty, uvodniCesta, graf);
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

        private static void VytvorCestyZVrcholu(string nazevStanice, Stanice stanice, List<List<String>> vraceneCesty, List<String> zkoumanaCesta, Graf<Stanice, Koleje> graf)
        {
            zkoumanaCesta.Add(nazevStanice);
            //Co je treba osetrit - vrcholy s vice hranami a vrcholy co jsou koncove a zaroven z nich pokracuje cesta
            //Zde klonuji stavajici cestu - je nutne ji ulozit - je platna i bez toho, aniz bych dotraverzoval na konec - jedna se o mezi jakysi zaznam

            //
            List<string> naslednici = graf.DejNaslednikyVrcholu(nazevStanice);
            if (stanice.Koncova && naslednici.Any())
            {
                foreach (string nazevCiloveStanice in naslednici)
                {
                    if (!JePovolenaCesta(stanice, nazevCiloveStanice, zkoumanaCesta)) continue;
                    //Pokud mam pouze jeden prvek v ceste, tak to znamena, ze v danem koncovem vrcholu jsem i zacal -> jedna se i o pocatecni - cestu velikosti 1 neukladam, nezajima me
                    if (zkoumanaCesta.Count > 1)
                    {
                        List<String> kopieExistujiciCesty = new List<string>(zkoumanaCesta);
                        vraceneCesty.Add(kopieExistujiciCesty);
                    }
                    VytvorCestyZVrcholu(nazevCiloveStanice, graf.DejDataVrcholu(nazevCiloveStanice), vraceneCesty, zkoumanaCesta, graf);
                }
                //Cyklus nad tim zabezpeci veskere dalsi vytvoreni cest => zde se mohu vratit
            }
            else if (!stanice.Koncova && naslednici.Any())
            {
                List<string> povoleniNaslednici = naslednici.Where((cilovaStanice) => JePovolenaCesta(stanice, cilovaStanice, zkoumanaCesta)).ToList();
                //nejprve musim vytvorit ostatni cesty, pote mohu pracovat s tou aktualni 
                for (int i = 1; i < povoleniNaslednici.Count; i++)
                {
                    List<String> kopieExistujiciCesty = new List<string>(zkoumanaCesta);
                    vraceneCesty.Add(kopieExistujiciCesty);
                    VytvorCestyZVrcholu(povoleniNaslednici[i], graf.DejDataVrcholu(povoleniNaslednici[i]), vraceneCesty, kopieExistujiciCesty, graf);
                }
                VytvorCestyZVrcholu(povoleniNaslednici.First(), graf.DejDataVrcholu(povoleniNaslednici.First()), vraceneCesty, zkoumanaCesta, graf);
            }
            else if (stanice.Koncova)
            {
                return;
            }
            else
            {
                throw new DataException("Vrchol není označen jako koncový ale nevycházejí z něj žádné hrany");
            }
        }

        public static List<List<string>> DejSeznamR(Graf<Stanice, Koleje> graf)
        {
            Dictionary<string, List<string>> seznamCest = DejSeznamL(graf);
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
        private static void NajdiNtice(HashSet<string> navstiveneVrcholyVCeste, List<string> aktualniDisjunktniCesty,
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
                if (jeDisjunktni)
                {
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
        private static bool JePovolenaCesta(Stanice stanice, string cilovaStanice, List<String> cesta)
        {
            if (stanice.PovoleneStaniceZDo.Count > 0)
            {
                if (cesta.Count <= 1)
                {
                    throw new ArgumentException("Je zadan seznam povolenych cest ale seznam je kratsi nez 1");
                }
                string nazevCilovehoVrcholu;
                //Ziskam predposledni vlozeny prvek do cesty
                stanice.PovoleneStaniceZDo.TryGetValue(cesta[cesta.Count() - 2], out nazevCilovehoVrcholu);
                return nazevCilovehoVrcholu != null && nazevCilovehoVrcholu.Equals(cilovaStanice);
            }
            else
            {
                return true;
            }
        }
    }
}
