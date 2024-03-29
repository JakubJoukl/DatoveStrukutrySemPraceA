﻿using DatoveStrukutrySemPraceA.Entity.Graf;
using DatoveStrukutrySemPraceA.Entity.ZeleznicniDoprava;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace DatoveStrukutrySemPraceA.Editor
{
    public class Editor
    {
        public TYP_AKCE PredchoziAkce { get; set; } = TYP_AKCE.VYTVOR_VRCHOL;
        public TYP_AKCE ProvadenaAkce { get; set; } = TYP_AKCE.VYTVOR_VRCHOL;
        public int PosunKameryX { get; set; } = 0;
        public int PosunKameryY { get; set; } = 0;
        public int PredchoziX { get; set; } = 0;
        public int PredchoziY { get; set; } = 0;
        //Meritko je zde /, v GUI * - je nutne provadet inverzni transformace
        public double Meritko { get; set; } = 1.0;
        public int Sirka { get; set; } = 40;
        public int CisloNovehoVrcholu { get; set; } = 1;
        public Graf<Stanice, Koleje> GrafStanic { get; set; }
        public string VybranyVrchol { get; set; } = null;
        public string NajetyVrchol { get; set; } = null;
        public TYP_PRVKU? ZvolenyTypPrvku { get; set; } = Editor.TYP_PRVKU.VSTUPNI;

        //vzdalenost je sirka (2 * pulka sirky)
        //Rectangle rec = new Rectangle((int)((stanice.X * editor.Meritko) - editor.Sirka / 2) + editor.PosunKameryX, (int)((stanice.Y * editor.Meritko) - editor.Sirka / 2) + editor.PosunKameryY,
        //(int)((editor.Sirka * editor.Meritko)), (int)((editor.Sirka * editor.Meritko)));
        private bool JeVeCtvercoveVzdalenostiVrcholu(MouseEventArgs e, int xPorovnavaneStanice, int yPorovnavaneStanice)
        {
            return (e.X - PosunKameryX) / Meritko > (xPorovnavaneStanice - (Sirka)) && (e.X - PosunKameryX) / Meritko < (xPorovnavaneStanice + (Sirka)) &&
                   (e.Y - PosunKameryY) / Meritko > (yPorovnavaneStanice - (Sirka)) && (e.Y - PosunKameryY) / Meritko < (yPorovnavaneStanice + (Sirka));
        }

        public string DejNazevNaklikleCtvercoveVzdalenostiVrcholu(MouseEventArgs e) {
            foreach (var vrcholNazev in GrafStanic.dejSeznamVrcholu())
            {
                Stanice stanice = GrafStanic.DejDataVrcholu(vrcholNazev);
                if (JeVeCtvercoveVzdalenostiVrcholu(e, stanice.X, stanice.Y))
                {
                    return vrcholNazev;
                }
            }
            return null;
        }

        //vraci zda je nutne prekreslovat
        public bool Klik(MouseEventArgs mouseEventArgs) {

            int souradniceX = (int)((mouseEventArgs.X - PosunKameryX) / Meritko);
            int souradniceY = (int)((mouseEventArgs.Y - PosunKameryY) / Meritko);

            if (mouseEventArgs.Button == MouseButtons.Right)
            {
                return SmazNejblizsiVrchol(new Point(souradniceX, souradniceY));
            }
            if (ProvadenaAkce != TYP_AKCE.VYTVOR_VRCHOL && Control.ModifierKeys != Keys.Shift) return false;
            //MouseEventArgs mouseEventArgs = (MouseEventArgs)e;
            if (ZaznamenanKlikNaVrchol(mouseEventArgs)) return true;

            if (mouseEventArgs.Button == MouseButtons.Left && (Control.ModifierKeys & Keys.Control) == 0 && ProvadenaAkce != TYP_AKCE.PRESUN_BODU)
            {
                return VytvorVrchol(mouseEventArgs);
            }
            else
            {
                //ZacniPosun(e);
                return false;
            }
        }

        private bool SmazNejblizsiVrchol(Point nakliknutyBod) {
            string uvodniStaniceHranyKeSmazani = null;
            string cilovaStaniceHranyKeSmazani = null;
            double nejmensiVzdalenost = double.MaxValue;

            foreach (var vrcholNazev in GrafStanic.dejSeznamVrcholu())
            {
                List<string> nasledniciVrcholu = GrafStanic.DejNaslednikyVrcholu(vrcholNazev);
                Stanice staniceZ = GrafStanic.DejDataVrcholu(vrcholNazev);

                foreach (var naslednikVrcholu in nasledniciVrcholu)
                {
                    Stanice staniceDo = GrafStanic.DejDataVrcholu(naslednikVrcholu);
                    //List<Point> ciloveBody = DejStartovniAKonecnouPoziciCar(staniceZ.X, staniceZ.Y, staniceDo.X, staniceDo.Y);
                    Point nejblizsiBod = getClosestPointOnSegment(staniceZ.X, staniceZ.Y, staniceDo.X, staniceDo.Y, nakliknutyBod.X, nakliknutyBod.Y);
                    // distanc can be calculated as follows
                    int distX = nakliknutyBod.X - nejblizsiBod.X;
                    int distY = nakliknutyBod.Y - nejblizsiBod.Y;

                    double result = Math.Sqrt(distX * distX + distY * distY);

                    if (result < nejmensiVzdalenost) {
                        uvodniStaniceHranyKeSmazani = vrcholNazev;
                        cilovaStaniceHranyKeSmazani = naslednikVrcholu;
                        nejmensiVzdalenost = result;
                    }
                }
            }

            if (uvodniStaniceHranyKeSmazani != null && cilovaStaniceHranyKeSmazani != null) {
                return GrafStanic.OdeberHranu(uvodniStaniceHranyKeSmazani, cilovaStaniceHranyKeSmazani);
            }
            return false;
        }

        public bool ZaznamenanKlikNaVrchol(MouseEventArgs mouseEventArgs) {
            string nazevVrcholu = DejNazevNaklikleCtvercoveVzdalenostiVrcholu(mouseEventArgs);
            if (nazevVrcholu != null)
            {
                return ZpracujKlikNaCtvercovouVzdalenostVrcholu(mouseEventArgs, nazevVrcholu);
            }
            else {
                return false;
            }
        }

        public List<Point> DejStartovniAKonecnouPoziciCar(int x1, int y1, int x2, int y2)
        {
            int startX;
            int startY;
            int cilX;
            int cilY;

            if (Math.Abs(x1 - x2) < Sirka)
            {
                if (y1 < y2)
                {
                    //startY = y1 + editor.Sirka / 2;
                    cilY = y2 - Sirka / 2;
                }
                else
                {
                    //startY = y1 - editor.Sirka / 2;
                    cilY = y2 + Sirka / 2;
                }
                cilX = x2;
            }
            else
            {
                if (x1 < x2)
                {
                    //startX = x1 + editor.Sirka / 2;
                    cilX = x2 - Sirka / 2;
                }
                else
                {
                    //startX = x1 - editor.Sirka / 2;
                    cilX = x2 + Sirka / 2;
                }
                cilY = y2;
            }

            /*
            if (y1 < y2)
            {
                startY = y1 + editor.Sirka / 2;
                cilY = y2 - editor.Sirka / 2;
            }
            else {
                startY = y1 - editor.Sirka / 2;
                cilY = y2 + editor.Sirka / 2;
            }*/

            return new List<Point>()
            {
                new Point(x1, y1),
                new Point(cilX, cilY)
            };
        }

        private bool VytvorVrchol(MouseEventArgs mouseEventArgs)
        {
            VybranyVrchol = null;
            string nazevVrcholu = "v" + CisloNovehoVrcholu;

            int souradniceX = (int)((mouseEventArgs.X - PosunKameryX) / Meritko);
            int souradniceY = (int)((mouseEventArgs.Y - PosunKameryY) / Meritko);

            if (ZvolenyTypPrvku == TYP_PRVKU.VSTUPNI)
            {
                GrafStanic.PridejVrchol(nazevVrcholu, new Stanice { Koncova = false, Pocatecni = true, X = souradniceX, Y = souradniceY }, true);
            }
            else if (ZvolenyTypPrvku == TYP_PRVKU.PRUJEZDOVY)
            {
                GrafStanic.PridejVrchol(nazevVrcholu, new Stanice { Koncova = false, Pocatecni = false, X = souradniceX, Y = souradniceY }, false);
            }
            else if (ZvolenyTypPrvku == TYP_PRVKU.VYSTUPNI)
            {
                GrafStanic.PridejVrchol(nazevVrcholu, new Stanice { Koncova = true, Pocatecni = false, X = souradniceX, Y = souradniceY }, false);
            }
            else if (ZvolenyTypPrvku == TYP_PRVKU.VSTUPNI_VYSTUPNI) 
            {
                GrafStanic.PridejVrchol(nazevVrcholu, new Stanice { Koncova = true, Pocatecni = true, X = souradniceX, Y = souradniceY }, true);
            }
            else
            {
                throw new ArgumentException("Neznama hodnota.");
            }
            CisloNovehoVrcholu++;
            return true;
        }

        private bool ZpracujKlikNaCtvercovouVzdalenostVrcholu(MouseEventArgs mouseEventArgs, string vrcholNazev)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                OdeberVrchol(vrcholNazev);
                return true;
            }
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

        private void OdeberVrchol(string vrcholNazev)
        {
            GrafStanic.DejDataVrcholu(vrcholNazev);
            foreach (var nazevVrcholu in GrafStanic.dejSeznamVrcholu())
            {
                Stanice stanice = GrafStanic.DejDataVrcholu(nazevVrcholu);
                List<string> cestyKeSmazani = new List<string>();

                foreach (var povolenaCestaZDo in stanice.PovoleneStaniceZDo)
                {
                    string cestaZ = povolenaCestaZDo.Key;
                    string cestaDo = povolenaCestaZDo.Value;
                    if (cestaZ.Equals(vrcholNazev) || cestaDo.Equals(vrcholNazev))
                    {
                        cestyKeSmazani.Add(cestaZ);
                    }
                }
                foreach (var cesta in cestyKeSmazani)
                {
                    stanice.PovoleneStaniceZDo.Remove(cesta);
                }
            }
            GrafStanic.OdeberVrchol(vrcholNazev);
            VybranyVrchol = null;
        }

        //true pokud doslo k zmene pozadujici prekresleni
        public bool ZpracujPohybMysi(MouseEventArgs e) {
            //Nejprve zjistim, zda jsem ve fazi pohybu kamerou
            if (ProvadenaAkce == TYP_AKCE.PRESUN_KAMEROU)
            {
                PosunKamerou(e);
                return true;
            }
            else if (ProvadenaAkce == TYP_AKCE.PRESUN_BODU && VybranyVrchol != null) {
                PrenastavSouradniceStanice(e);
                return true;
            }

            string vrcholNazev = DejNazevNaklikleCtvercoveVzdalenostiVrcholu(e);

            //Vrchol, ktery je ve ctvercove vzdalenosti
            if (vrcholNazev != null) {
                NajetyVrchol = vrcholNazev;
                return true;
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

        public void PrenastavSouradniceStanice(MouseEventArgs e)
        {
            Stanice stanice = GrafStanic.DejDataVrcholu(VybranyVrchol);
            int souradniceX = (int)((e.X - PosunKameryX) / Meritko);
            int souradniceY = (int)((e.Y - PosunKameryY) / Meritko);
            stanice.X = souradniceX;
            stanice.Y = souradniceY;
        }

        public void PosunKamerou(MouseEventArgs e) {
            PosunKameryX += e.X - PredchoziX;
            PosunKameryY += e.Y - PredchoziY;
            PredchoziX = e.X;
            PredchoziY = e.Y;
        }

        public void ZahajPohybKamery(MouseEventArgs e) {
            PredchoziAkce = ProvadenaAkce;
            ProvadenaAkce = TYP_AKCE.PRESUN_KAMEROU;
            PredchoziX = e.X;
            PredchoziY = e.Y;
        }

        public void UkonciProvadenouAkci(MouseEventArgs e) {
            ProvadenaAkce = PredchoziAkce;
            PredchoziX = 0;
            PredchoziY = 0;
            if (ProvadenaAkce != TYP_AKCE.VYTVOR_VRCHOL)
            {
                VybranyVrchol = null;
                NajetyVrchol = null;
            }
        }

        public void UlozTextDoSouboru(string ukladanyText)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "TXT soubory (*.txt)|*.txt";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // First Event Creates file and writes default content to it - works ok 
                File.WriteAllText(saveFileDialog.FileName, ukladanyText);
                //NewFileCreated(this, new FileCreatedEventArgs() { Template = Template.BBMF, FilePath = saveFileDialog.FileName });
            }
        }

        public bool NactiGrafZeSouboru() {
            OpenFileDialog openFileDialog = new OpenFileDialog(); 
            openFileDialog.Filter = "BIN soubory (*.bin)|*.bin";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.GrafStanic = Persistence.Perzistence<Stanice, Koleje>.NactiGrafZeSouboru(openFileDialog.FileName);
                this.CisloNovehoVrcholu = DejCisloNejvyssihoVrcholu();
                return true;
            }
            return false;
        }

        private int DejCisloNejvyssihoVrcholu() {
            int nalezeneNejvyssiCislo = 0;
            foreach (var vrchol in GrafStanic.dejSeznamVrcholu())
            {
                int cisloVrcholu = int.Parse(vrchol.Substring(1));
                if (cisloVrcholu > nalezeneNejvyssiCislo) {
                    nalezeneNejvyssiCislo = cisloVrcholu;
                }
            }
            return nalezeneNejvyssiCislo + 1;
        }

        public void UlozGrafDoSouboru()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "BIN soubory (*.bin)|*.bin";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Persistence.Perzistence<Stanice, Koleje>.UlozGrafDoSouboru(saveFileDialog.FileName, this.GrafStanic);
            }
        }


        /**
         * Returns closest point on segment to point
         * @param sx1 - segment x coord 1
         * @param sy1 - segment y coord 1
         * @param sx2 - segment x coord 2
         * @param sy2 - segment y coord 2
         * @param px - point x coord
         * @param py - point y coord
         * @return closets point on segment to point
         */
        public static Point getClosestPointOnSegment(int sx1, int sy1, int sx2, int sy2, int px, int py)
        {
            double xDelta = sx2 - sx1;
            double yDelta = sy2 - sy1;

            if ((xDelta == 0) && (yDelta == 0))
            {
                throw new Exception("Segment start equals segment end");
            }

            double u = ((px - sx1) * xDelta + (py - sy1) * yDelta) / (xDelta * xDelta + yDelta * yDelta);

            Point closestPoint;
            if (u < 0)
            {
                closestPoint = new Point(sx1, sy1);
            }
            else if (u > 1)
            {
                closestPoint = new Point(sx2, sy2);
            }
            else
            {
                closestPoint = new Point((int)Math.Round(sx1 + u * xDelta), (int)Math.Round(sy1 + u * yDelta));
            }
            return closestPoint;
        }

        public enum TYP_PRVKU
        {
            VSTUPNI,
            PRUJEZDOVY,
            VYSTUPNI,
            VSTUPNI_VYSTUPNI
        }

        public enum TYP_AKCE { 
            VYTVOR_VRCHOL,
            PRESUN_KAMEROU,
            PRESUN_BODU
        }
    }
}
