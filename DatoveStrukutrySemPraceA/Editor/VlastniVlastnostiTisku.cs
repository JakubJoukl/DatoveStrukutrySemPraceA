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

    public enum Orientace
    {
        NA_SIRKU,
        NA_VYSKU
    }

    public enum Tisknout
    {
        CELA_SIT,
        VIDITELNA_CAST
    }

    public enum Pomer_stran
    {
        ZACHOVAT,
        ROZTAHNOUT
    }

    public enum Centrovani
    {
        DLE_VRCHOLU,
        DLE_KAMERY
    }

    public enum Druh_posteroveho_tisku
    {
        POCTEM_STRAN,
        MERITKEM
    }
}
