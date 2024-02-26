using DatoveStrukutrySemPraceA.Entity.Graf;
using DatoveStrukutrySemPraceA.Entity.ZeleznicniDoprava;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DatoveStrukutrySemPraceA.Persistence
{
    internal class Persistence<DV, DH>
    {
        public static Graf<DV, DH> NactiGrafZeSouboru(string nazev) {
            Graf<DV, DH> graf = JsonSerializer.Deserialize<Graf<DV, DH>>(nazev);
            return graf;
        }

        public static void UlozGrafDoSouboru(string nazev, Graf<DV, DH> graf) {
            string jsonString = JsonSerializer.Serialize(graf);
            Console.WriteLine(jsonString);
            File.Create(jsonString);
        }
    }
}
