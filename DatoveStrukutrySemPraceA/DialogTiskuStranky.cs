using DatoveStrukutrySemPraceA.Editor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatoveStrukutrySemPraceA
{
    public partial class DialogTiskuStranky : Form
    {
        public Druh_posteroveho_tisku DruhTisku() {
            return poctemStran.Checked ? Druh_posteroveho_tisku.POCTEM_STRAN : Druh_posteroveho_tisku.MERITKEM;
        }
        public int PocetStranPosterovehoTiskuNaVysku() {
            return (int)pocetStranekNaVysku.Value;
        }
        public int PocetStranPosterovehoTiskuNaSirku() {
            return (int)pocetStranekNaSirku.Value;
        }
        public bool PosterovyTisk() { 
            return posterovyTisk.Checked;
        }
        public PaperKind VelikostStranky() {
            return (PaperKind)Enum.Parse(typeof(PaperKind), velikostCb.Text);
        }
        public Orientace Orientace() {
            return naSirku.Checked ? Editor.Orientace.NA_SIRKU : Editor.Orientace.NA_VYSKU;
        }
        public Tisknout Tisknout() {
            return celouSit.Checked? Editor.Tisknout.CELA_SIT : Editor.Tisknout.VIDITELNA_CAST;
        }
        public Pomer_stran PomerStran() {
            return zachovatBtn.Checked ? Pomer_stran.ZACHOVAT : Pomer_stran.ROZTAHNOUT; 
        }
        public Centrovani Centrovani() {
            return DleVrcholu.Checked ? Editor.Centrovani.DLE_VRCHOLU : Editor.Centrovani.DLE_KAMERY;
        }
        public Druh_posteroveho_tisku DruhPosterovehoTisku() {
            return poctemStran.Checked ? Druh_posteroveho_tisku.POCTEM_STRAN : Druh_posteroveho_tisku.MERITKEM;
        }
        public int Meritko() {
            return (int)meritkoNm.Value;
        }
        
        public Okraje Okraje() {
            return new Okraje((int)vlevoNm.Value, (int)vpravoNm.Value, (int)nahoreNm.Value, (int)doleNm.Value);
        }
        public string TextVZahlavi() {
            return textVZahlaviTxt.Text;
        }
        public string TextVZapati() {
            return textVZapatiTxt.Text;
        }
        public DialogTiskuStranky()
        {
            InitializeComponent();
            velikostCb.Items.AddRange(new Object[] { PaperKind.A2, PaperKind.A3, PaperKind.A4, PaperKind.A5 });
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void poctemStran_Click(object sender, EventArgs e)
        {
            if (poctemStran.Checked)
            {
                meritkem.Checked = false;
            }
        }

        private void meritkem_Click(object sender, EventArgs e)
        {
            if (meritkem.Checked)
            {
                poctemStran.Checked = false;
            }
        }

        private void vpravoTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void velikostCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void naVysku_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void naSirku_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void PotvrdBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void zrusBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void celouSit_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void DialogTiskuStranky_Load(object sender, EventArgs e)
        {

        }

        public void NactiUlozeneVlastnosti(VlastniVlastnostiTisku vlastniVlastnostiTisku,
            PrinterSettings nastaveniTiskarny, PageSettings vlastnostiTisku) {
            velikostCb.Text = vlastnostiTisku.PaperSize.Kind.ToString();
            if (vlastnostiTisku.Landscape)
            {
                naSirku.Checked = true;
            }
            else { 
                naVysku.Checked = true;
            }
            Okraje okraje = vlastniVlastnostiTisku.Okraje;
            vlevoNm.Value = okraje.Vlevo;
            vpravoNm.Value = okraje.Vpravo;
            nahoreNm.Value = okraje.Nahore;
            doleNm.Value = okraje.Dole;
            if (vlastniVlastnostiTisku.Tisknout == Editor.Tisknout.CELA_SIT)
            {
                celouSit.Checked = true;
            }
            else {
                viditelnouCast.Checked = true;
            }
            //if(vlastniVlastnostiTisku)
        }
    }

    public class Okraje {
        public int Vlevo {  get; set; }
        public int Vpravo { get; set; }
        public int Nahore {  get; set; }
        public int Dole {  get; set; }
        public Okraje(int vlevo, int vpravo, int nahore, int dole) { 
            Vlevo = vlevo;
            Vpravo = vpravo;
            Nahore = nahore;
            Dole = dole;
        }
    }
}
