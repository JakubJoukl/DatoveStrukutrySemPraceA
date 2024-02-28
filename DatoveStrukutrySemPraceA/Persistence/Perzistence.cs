using DatoveStrukutrySemPraceA.Entity.Graf;
using DatoveStrukutrySemPraceA.Entity.ZeleznicniDoprava;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DatoveStrukutrySemPraceA.Persistence
{
    internal class Perzistence<DV, DH>
    {
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true
        };

        //TODO aktualne rozbite
        public static Graf<DV, DH> NactiGrafZeSouboru(string nazev) {
            Graf<DV, DH> graf = JsonSerializer.Deserialize<Graf<DV, DH>>(File.OpenRead(nazev), options);
            return graf;
        }

        //TODO aktualne rozbite
        public static void UlozGrafDoSouboru(string nazev, Graf<DV, DH> graf) {
            string jsonString = JsonSerializer.Serialize(graf, options);
            Console.WriteLine(jsonString);
            File.WriteAllText(nazev, jsonString);
        }
    }
}
