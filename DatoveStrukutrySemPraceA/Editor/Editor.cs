using DatoveStrukutrySemPraceA.Entity.Graf;
using DatoveStrukutrySemPraceA.Entity.ZeleznicniDoprava;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatoveStrukutrySemPraceA.Editor
{
    public class Editor
    {
        public int PosunKameryX { get; set; } = 0;
        public int PosunKameryY { get; set; } = 0;
        public double Meritko { get; set; } = 1.0;
        public int Sirka { get; set; } = 40;
        public int CisloNovehoVrcholu { get; set; } = 1;
        public Graf<Stanice, Koleje> GrafStanic { get; set; }
        public string VybranyVrchol { get; set; } = null;
        public string NajetyVrchol { get; set; } = null;
        public TYP_PRVKU ZvolenyPrvek { get; set; } = Editor.TYP_PRVKU.VSTUPNI;

        //vzdalenost je sirka (2 * pulka sirky)
        private bool JeVeCtvercoveVzdalenosti(MouseEventArgs e, int xPorovnavaneStanice, int yPorovnavaneStanice)
        {
            return e.X > xPorovnavaneStanice - ((Sirka / 2) * 2 * Meritko) && e.X < xPorovnavaneStanice + ((Sirka / 2) * 2 * Meritko) &&
                   e.Y > yPorovnavaneStanice - ((Sirka / 2) * 2 * Meritko) && e.Y < yPorovnavaneStanice + ((Sirka / 2) * 2 * Meritko);
        }

        //vraci zda je nutne prekreslovat
        public bool Klik(MouseEventArgs mouseEventArgs) {
            //MouseEventArgs mouseEventArgs = (MouseEventArgs)e;

            foreach (var vrcholNazev in GrafStanic.dejSeznamVrcholu())
            {
                Stanice stanice = GrafStanic.DejDataVrcholu(vrcholNazev);
                if (JeVeCtvercoveVzdalenosti(mouseEventArgs, stanice.X, stanice.Y))
                {
                    if (VybranyVrchol == null)
                    {
                        VybranyVrchol = vrcholNazev;
                        return true;
                    }
                    else if (!vrcholNazev.Equals(VybranyVrchol))
                    {
                        //Zde udelam napojeni vrcholu
                        if (mouseEventArgs.Button == MouseButtons.Left)
                        {
                            GrafStanic.PridejHranu(VybranyVrchol, vrcholNazev, new Koleje());
                            VybranyVrchol = null;
                            return true;
                        }
                        else
                        {
                            bool odebrano = GrafStanic.OdeberHranu(VybranyVrchol, vrcholNazev);
                            VybranyVrchol = null;
                            return odebrano;
                        }
                    }
                    else
                    {
                        VybranyVrchol = null;
                        return true;
                    }
                }
            }

            if (mouseEventArgs.Button == MouseButtons.Left)
            {
                VybranyVrchol = null;
                string nazevVrcholu = "v" + CisloNovehoVrcholu;
                if (ZvolenyPrvek == TYP_PRVKU.VSTUPNI)
                {
                    GrafStanic.PridejVrchol(nazevVrcholu, new Stanice { Koncova = false, Pocatecni = true, X = mouseEventArgs.X, Y = mouseEventArgs.Y }, true, false);
                }
                else if (ZvolenyPrvek == TYP_PRVKU.PRUJEZDOVY)
                {
                    GrafStanic.PridejVrchol(nazevVrcholu, new Stanice { Koncova = false, Pocatecni = false, X = mouseEventArgs.X, Y = mouseEventArgs.Y }, false, false);
                }
                else
                {
                    GrafStanic.PridejVrchol(nazevVrcholu, new Stanice { Koncova = true, Pocatecni = false, X = mouseEventArgs.X, Y = mouseEventArgs.Y }, false, true);
                }
                CisloNovehoVrcholu++;
                return true;
            }
            else { 
                return false; 
            }
        }

        public bool ZpracujPohybMysi(MouseEventArgs e) {
            foreach (var vrcholNazev in GrafStanic.dejSeznamVrcholu())
            {
                Stanice stanice = GrafStanic.DejDataVrcholu(vrcholNazev);
                if (JeVeCtvercoveVzdalenosti(e, stanice.X, stanice.Y))
                {
                    NajetyVrchol = vrcholNazev;
                    return true;
                }
            }
            if (NajetyVrchol != null)
            {
                if (NajetyVrchol.Equals(VybranyVrchol))
                {
                    NajetyVrchol = null;
                    return false;
                }
                else
                {
                    NajetyVrchol = null;
                    return true;
                }
            }
            else {
                return false;
            }
        }

        public enum TYP_PRVKU
        {
            VSTUPNI,
            PRUJEZDOVY,
            VYSTUPNI
        }
    }
}
