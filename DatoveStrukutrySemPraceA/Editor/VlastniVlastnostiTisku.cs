using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatoveStrukutrySemPraceA.Editor
{
    public class VlastniVlastnostiTisku
    {
        public Tisknout Tisknout {  get; set; }
        public Pomer_stran PomerStran { get; set; }
        public Centrovani Centrovani { get; set; }
        public string TextVZahlavi {  get; set; }
        public string TextVZapati { get; set; }
        public bool PosterovyTisk { get; set; }
        public Okraje Okraje {  get; set; }
        public Druh_posteroveho_tisku DruhPosterovehoTisku { get; set; }
        public int Meritko { get; set; }
        public int PocetStranNaVysku { get; set; }
        public int PocetStranNaSirku { get; set; }
    }
}
