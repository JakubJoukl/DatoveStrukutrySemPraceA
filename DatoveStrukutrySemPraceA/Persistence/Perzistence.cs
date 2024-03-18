using DatoveStrukutrySemPraceA.Entity.Graf;
using DatoveStrukutrySemPraceA.Entity.ZeleznicniDoprava;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatoveStrukutrySemPraceA.Persistence
{
    internal class Perzistence<DV, DH>
    {
        //TODO aktualne rozbite
        public static Graf<DV, DH> NactiGrafZeSouboru(string nazev) {
            Graf<DV, DH> graf = JsonConvert.DeserializeObject<Graf<DV, DH>>(File.ReadAllText(nazev));
            return graf;
        }

        //TODO aktualne rozbite
        public static void UlozGrafDoSouboru(string nazev, Graf<DV, DH> graf) {
            string jsonString = JsonConvert.SerializeObject(graf);
            Console.WriteLine(jsonString);
            File.WriteAllText(nazev, jsonString);
        }
    }
}
