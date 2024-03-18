using DatoveStrukutrySemPraceA.Editor;
using DatoveStrukutrySemPraceA.Entity.Graf;
using DatoveStrukutrySemPraceA.Entity.ZeleznicniDoprava;
using DatoveStrukutrySemPraceA.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatoveStrukutrySemPraceA
{
    public partial class Form1 : Form
    {
        private PrintDocument printDocument;

        int aktualniTistenaStranka = 1;
        int celkovyPocetStranDokumentu = 2;
        int zbyvajiciPocetStranTisku;
        VlastniVlastnostiTisku vlastniVlastnosti = new VlastniVlastnostiTisku();

        public Form1()
        {
            InitializeComponent();

            printDocument = new PrintDocument();
            // Nazev tiskové úlohy, jak se bude zobrazovat ve spravci tisku
            printDocument.DocumentName = "PG2_Úkol_03 - Dopravní síť";

            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);

            // Vytvoříme Dialog Tisk:
            // printDialog = new PrintDialog();
            // Nastavíme dokument pro dialog Tisk:
            printDialog.Document = printDocument;

            // Vytvoříme dialog Vzhled stránky:
            // pageSetupDialog = new PageSetupDialog();
            // Nastavíme dokument pro dialog Vzhled stránky:
            pageSetupDialog.Document = printDocument;

            // Vytvoříme Dialog Náhled:
            // printPreviewDialog = new PrintPreviewDialog();
            // Nastavíme dokument pro dialog Náhled:
            printPreviewDialog.Document = printDocument;


            // naplneni seznamu tiskaren do Comboboxu
            foreach (string strPrintName in PrinterSettings.InstalledPrinters)
            {
                seznamTiskarenComboBox.Items.Add(strPrintName);
            }
        }

        public void DejMinAMaxXYSouradnice(out int MinX, out int MaxX, out int MinY, out int MaxY) {
            MinX = int.MaxValue;
            MaxX = int.MinValue;
            MinY = int.MaxValue;
            MaxY = int.MinValue;
            bool byloNastaveno = false;
            foreach (var vrcholNazev in editor.GrafStanic.dejSeznamVrcholu()) { 
                Stanice stanice = editor.GrafStanic.DejDataVrcholu(vrcholNazev);
                if (stanice.X < MinX) { 
                    MinX = stanice.X;
                }
                if (stanice.X > MaxX) { 
                   MaxX = stanice.X;
                }
                if (stanice.Y < MinY) { 
                    MinY = stanice.Y;
                }
                if (stanice.Y > MaxY) { 
                    MaxY = stanice.Y;
                }
                byloNastaveno = true;
            }
            if (!byloNastaveno) {
                MinX = 0;
                MaxX = 100;
                MinY = 0;
                MaxY = 100;
            }
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {

            Graphics g = e.Graphics;

            RectangleF rectPageBounds = e.PageBounds;
            g.DrawRectangles(Pens.Black, new RectangleF[] { rectPageBounds });
            //RectangleF rectVisibleClipBounds = g.VisibleClipBounds;
            //g.DrawRectangles(Pens.Green, new RectangleF[] { rectVisibleClipBounds });

            RectangleF rectMarginBounds = e.MarginBounds;
            g.DrawRectangles(Pens.Red, new RectangleF[] { rectMarginBounds });
            g.SetClip(rectMarginBounds);

            int MinX;
            int MaxX;
            int MinY;
            int MaxY;

            DejMinAMaxXYSouradnice(out MinX, out MaxX, out MinY, out MaxY);

            //X tisku = e.MarginBounds.Width
            float pomerTiskuX = (float)e.MarginBounds.Width / (canvas.Width - 0);

            //Y tisku = e.MarginBounds.Height
            float pomerTiskuY = (float)e.MarginBounds.Height / (canvas.Height - 0);
            float pomerTisku;

            if (pomerTiskuX == float.NegativeInfinity || pomerTiskuX == float.PositiveInfinity)
            {
                pomerTiskuX = 0;
            }


            if (pomerTiskuY == float.NegativeInfinity || pomerTiskuY == float.PositiveInfinity)
            {
                pomerTiskuY = 0;
            }

            float posunTiskuX = 0;
            float posunTiskuY = 0;

            if (pomerTiskuX < pomerTiskuY)
            {
                pomerTisku = pomerTiskuX;
                posunTiskuY = (e.MarginBounds.Height - canvas.Height * pomerTisku) / 2f;
            }
            else
            {
                pomerTisku = pomerTiskuY;
                posunTiskuX = (e.MarginBounds.Width - canvas.Width * pomerTisku) / 2f;
            }

            g.TranslateTransform(e.MarginBounds.Left + posunTiskuX, e.MarginBounds.Top + posunTiskuY);
            if (pomerTisku != 0)
            {
                g.ScaleTransform(pomerTisku, pomerTisku);
            }

            Kresli(g);

            g.ResetTransform();

            // varianta s formatovanim textu
            Font font7 = new Font("Arial Bold", 7f, GraphicsUnit.Millimeter);
            string textHorni = "PG2_Úkol_03 - Dopravní síť: Pardubice a okolí\n";

            if (aktualniTistenaStranka == 1)
                textHorni += "Stránka 1 (síť včetně podkladové mapy)";
            else
                textHorni += "Stránka 2 (pouze síť)";
            StringFormat strfmt = new StringFormat();
            strfmt.Alignment = StringAlignment.Center;
            strfmt.LineAlignment = StringAlignment.Center;
            Rectangle rectTextHorni = new Rectangle(e.PageBounds.Left, e.PageBounds.Top,
                                                    e.PageBounds.Width, e.MarginBounds.Top - e.PageBounds.Top);
            g.DrawString(textHorni, font7, Brushes.Black, rectTextHorni, strfmt);

            //presne umisteni textu
            Font font5 = new Font("Arial", 5f, GraphicsUnit.Millimeter);
            string textDolniL = "Petr Veselý";
            SizeF sizeTextDolniL = g.MeasureString(textDolniL, font5);
            g.DrawString(textDolniL, font5, Brushes.Black,
                         1, e.PageBounds.Height - e.PageBounds.Top - sizeTextDolniL.Height - 1);


            string textDolniR = "Tisk: " + DateTime.Now.ToString("d/M/yyyy HH:mm:ss");
            SizeF sizeTextDolniR = g.MeasureString(textDolniR, font5);
            g.DrawString(textDolniR, font5, Brushes.Black,
                         e.PageBounds.Left + e.PageBounds.Width - sizeTextDolniR.Width - 1,
                         e.PageBounds.Top + e.PageBounds.Height - sizeTextDolniR.Height - 1);


            aktualniTistenaStranka++;
            zbyvajiciPocetStranTisku--;
            if (zbyvajiciPocetStranTisku > 0)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;

        }

        Editor.Editor editor;
        Button zvolenyPrvek;

        private Graf<Stanice, Koleje> DejPokusnyGraf()
        {
            Graf<Stanice, Koleje> grafStanic = new Graf<Stanice, Koleje>();

            grafStanic.PridejVrchol("v23", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v21", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v22", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v24", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v12", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v13", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v14", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v15", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v30", new Stanice { Koncova = true, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v16", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v17", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v29", new Stanice { Koncova = true, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v18", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v19", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v27", new Stanice { Koncova = true, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v28", new Stanice { Koncova = true, Pocatecni = false }, false);

            grafStanic.PridejHranu("v23", "v12", new Koleje());
            grafStanic.PridejHranu("v21", "v14", new Koleje());
            grafStanic.PridejHranu("v22", "v15", new Koleje());
            grafStanic.PridejHranu("v24", "v13", new Koleje());
            grafStanic.PridejHranu("v12", "v14", new Koleje());
            grafStanic.PridejHranu("v13", "v15", new Koleje());
            grafStanic.PridejHranu("v14", "v30", new Koleje());
            grafStanic.PridejHranu("v15", "v16", new Koleje());
            grafStanic.PridejHranu("v30", "v17", new Koleje());
            grafStanic.PridejHranu("v16", "v17", new Koleje());
            grafStanic.PridejHranu("v16", "v19", new Koleje());
            grafStanic.PridejHranu("v17", "v29", new Koleje());
            grafStanic.PridejHranu("v29", "v18", new Koleje());
            grafStanic.PridejHranu("v18", "v19", new Koleje());
            grafStanic.PridejHranu("v18", "v27", new Koleje());
            grafStanic.PridejHranu("v19", "v28", new Koleje());

            return grafStanic;
        }

        private Graf<Stanice, Koleje> DejCelyGraf()
        {
            Graf<Stanice, Koleje> grafStanic = new Graf<Stanice, Koleje>();
            grafStanic.PridejVrchol("v109", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v107", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v101", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v102", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v108", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v112", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v114", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v120", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v122", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v124", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v126", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v128", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v130", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v132", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v140", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v113", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v111", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v103", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v104", new Stanice { Koncova = false, Pocatecni = true }, true);
            grafStanic.PridejVrchol("v106", new Stanice { Koncova = false, Pocatecni = true }, true);

            grafStanic.PridejVrchol("v51", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v52", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v53", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v54", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v55", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v57", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v56", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v61", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v62", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v60", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v59", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v58", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v65", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v66", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v67", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v70", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v63", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v64", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v68", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v69", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v71", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v72", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v74", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v73", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v76", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v75", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v78", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v77", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v82", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v81", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v80", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v79", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v84", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v83", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v85", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v86", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v87", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v88", new Stanice { Koncova = false, Pocatecni = false }, false);

            Stanice s95 = new Stanice { Koncova = false, Pocatecni = false };
            grafStanic.PridejVrchol("v95", s95, false);

            s95.PridejPovolenouCestu("v95", "v88", "v89");
            s95.PridejPovolenouCestu("v95", "v87", "v90");

            grafStanic.PridejVrchol("v89", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v90", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v92", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v91", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v94", new Stanice { Koncova = false, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v93", new Stanice { Koncova = false, Pocatecni = false }, false);

            grafStanic.PridejVrchol("v134", new Stanice { Koncova = true, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v136", new Stanice { Koncova = true, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v138", new Stanice { Koncova = true, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v602", new Stanice { Koncova = true, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v601", new Stanice { Koncova = true, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v301", new Stanice { Koncova = true, Pocatecni = false }, false);
            grafStanic.PridejVrchol("v302", new Stanice { Koncova = true, Pocatecni = false }, false);

            grafStanic.PridejHranu("v113", "v61", new Koleje());
            grafStanic.PridejHranu("v111", "v61", new Koleje());
            grafStanic.PridejHranu("v109", "v62", new Koleje());
            grafStanic.PridejHranu("v107", "v62", new Koleje());
            grafStanic.PridejHranu("v103", "v65", new Koleje());
            grafStanic.PridejHranu("v101", "v57", new Koleje());
            grafStanic.PridejHranu("v102", "v56", new Koleje());
            grafStanic.PridejHranu("v104", "v67", new Koleje());
            grafStanic.PridejHranu("v106", "v70", new Koleje());
            grafStanic.PridejHranu("v108", "v70", new Koleje());
            grafStanic.PridejHranu("v112", "v60", new Koleje());
            grafStanic.PridejHranu("v114", "v54", new Koleje());
            grafStanic.PridejHranu("v120", "v69", new Koleje());
            grafStanic.PridejHranu("v122", "v64", new Koleje());
            grafStanic.PridejHranu("v124", "v64", new Koleje());
            grafStanic.PridejHranu("v126", "v59", new Koleje());
            grafStanic.PridejHranu("v128", "v59", new Koleje());
            grafStanic.PridejHranu("v130", "v58", new Koleje());
            grafStanic.PridejHranu("v132", "v55", new Koleje());
            grafStanic.PridejHranu("v140", "v51", new Koleje());

            grafStanic.PridejHranu("v61", "v68", new Koleje());
            grafStanic.PridejHranu("v62", "v68", new Koleje());
            grafStanic.PridejHranu("v68", "v71", new Koleje());
            grafStanic.PridejHranu("v65", "v71", new Koleje());
            grafStanic.PridejHranu("v57", "v65", new Koleje());
            grafStanic.PridejHranu("v57", "v66", new Koleje());

            grafStanic.PridejHranu("v56", "v66", new Koleje());
            grafStanic.PridejHranu("v56", "v67", new Koleje());
            grafStanic.PridejHranu("v70", "v74", new Koleje());
            grafStanic.PridejHranu("v60", "v63", new Koleje());
            grafStanic.PridejHranu("v54", "v60", new Koleje());
            grafStanic.PridejHranu("v54", "v69", new Koleje());
            grafStanic.PridejHranu("v64", "v82", new Koleje());
            grafStanic.PridejHranu("v59", "v75", new Koleje());
            grafStanic.PridejHranu("v75", "v82", new Koleje());
            grafStanic.PridejHranu("v75", "v81", new Koleje());
            grafStanic.PridejHranu("v58", "v81", new Koleje());
            grafStanic.PridejHranu("v52", "v55", new Koleje());
            grafStanic.PridejHranu("v55", "v58", new Koleje());
            grafStanic.PridejHranu("v51", "v52", new Koleje());
            grafStanic.PridejHranu("v51", "v53", new Koleje());
            grafStanic.PridejHranu("v52", "v134", new Koleje());
            grafStanic.PridejHranu("v53", "v136", new Koleje());
            grafStanic.PridejHranu("v53", "v138", new Koleje());

            grafStanic.PridejHranu("v63", "v74", new Koleje());
            grafStanic.PridejHranu("v63", "v73", new Koleje());
            grafStanic.PridejHranu("v69", "v73", new Koleje());
            grafStanic.PridejHranu("v67", "v76", new Koleje());
            grafStanic.PridejHranu("v74", "v76", new Koleje());
            grafStanic.PridejHranu("v66", "v72", new Koleje());
            grafStanic.PridejHranu("v72", "v78", new Koleje());
            grafStanic.PridejHranu("v72", "v77", new Koleje());
            grafStanic.PridejHranu("v71", "v78", new Koleje());
            grafStanic.PridejHranu("v78", "v80", new Koleje());
            grafStanic.PridejHranu("v76", "v77", new Koleje());
            grafStanic.PridejHranu("v77", "v79", new Koleje());

            grafStanic.PridejHranu("v82", "v84", new Koleje());
            grafStanic.PridejHranu("v73", "v84", new Koleje());
            grafStanic.PridejHranu("v81", "v83", new Koleje());

            grafStanic.PridejHranu("v80", "v94", new Koleje());
            grafStanic.PridejHranu("v94", "v602", new Koleje());
            grafStanic.PridejHranu("v80", "v85", new Koleje());
            grafStanic.PridejHranu("v79", "v85", new Koleje());
            grafStanic.PridejHranu("v79", "v86", new Koleje());
            grafStanic.PridejHranu("v85", "v87", new Koleje());
            grafStanic.PridejHranu("v87", "v89", new Koleje());
            grafStanic.PridejHranu("v87", "v95", new Koleje());
            grafStanic.PridejHranu("v89", "v92", new Koleje());
            grafStanic.PridejHranu("v92", "v94", new Koleje());
            grafStanic.PridejHranu("v92", "v601", new Koleje());

            grafStanic.PridejHranu("v95", "v89", new Koleje());
            grafStanic.PridejHranu("v95", "v90", new Koleje());

            grafStanic.PridejHranu("v84", "v86", new Koleje());
            grafStanic.PridejHranu("v86", "v88", new Koleje());
            grafStanic.PridejHranu("v88", "v95", new Koleje());
            grafStanic.PridejHranu("v88", "v90", new Koleje());
            grafStanic.PridejHranu("v90", "v91", new Koleje());
            grafStanic.PridejHranu("v91", "v301", new Koleje());
            grafStanic.PridejHranu("v91", "v93", new Koleje());

            grafStanic.PridejHranu("v83", "v88", new Koleje());
            grafStanic.PridejHranu("v83", "v93", new Koleje());
            grafStanic.PridejHranu("v93", "v302", new Koleje());

            return grafStanic;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Graf<Stanice, Koleje> grafStanic = DejCelyGraf();
            //Graf<Stanice, Koleje> grafStanic = DejCelyGraf();

            //grafStanic.DejSeznamL();
            //Vypocty.DejSeznamR(grafStanic);

            //Perzistence<Stanice, Koleje>.UlozGrafDoSouboru("test.txt", grafStanic);
            //Graf<Stanice, Koleje> graf2 = Perzistence<Stanice, Koleje>.NactiGrafZeSouboru("test.txt");

            editor = new Editor.Editor();
            editor.GrafStanic = new Graf<Stanice, Koleje>();
            editor.ZvolenyTypPrvku = Editor.Editor.TYP_PRVKU.VSTUPNI;
            zvolenyPrvek = vstupniUzel;
            zvolenyPrvek.BackColor = Color.DarkOliveGreen;
            ovladaciMenu.AutoSize = true;
        }

        private void prekresli() {
            canvas.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ovladaciMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ovladaciMenu_Click(object sender, EventArgs e)
        {

        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Kresli(e.Graphics);
        }

        private void Kresli(Graphics g)
        {
            Pen cernePero = new Pen(Color.Black, 2);

            NakresliCary(g);

            NakresliVrcholy(g, cernePero);
        }

        private void NakresliVrcholy(Graphics g, Pen cernePero)
        {
            //pote nakreslim vrcholy
            foreach (var vrcholNazev in editor.GrafStanic.dejSeznamVrcholu())
            {
                Stanice stanice = editor.GrafStanic.DejDataVrcholu(vrcholNazev);
                Brush stetecSBarvou = vrcholNazev.Equals(editor.VybranyVrchol) ? Brushes.RoyalBlue : vrcholNazev.Equals(editor.NajetyVrchol) ? Brushes.DarkMagenta : stanice.PovoleneStaniceZDo.Count > 0 ? Brushes.Olive : (stanice.Koncova && stanice.Pocatecni) ? Brushes.Lime : stanice.Pocatecni ? Brushes.Orange : stanice.Koncova ? Brushes.IndianRed : Brushes.White;
                if (stanice.Koncova || stanice.Pocatecni)
                {
                    Rectangle rec = new Rectangle(
                        (int)((stanice.X * editor.Meritko) - (editor.Sirka * editor.Meritko) / 2) + editor.PosunKameryX,
                        (int)((stanice.Y * editor.Meritko) - (editor.Sirka * editor.Meritko) / 2) + editor.PosunKameryY,
                        (int)(editor.Sirka * editor.Meritko),
                        (int)(editor.Sirka * editor.Meritko)
                    );
                    g.FillRectangle(stetecSBarvou, rec);
                    g.DrawRectangle(cernePero, rec);
                }
                else
                {
                    RectangleF rec = new Rectangle(
                        (int)((stanice.X * editor.Meritko) - (editor.Sirka * editor.Meritko) / 2) + editor.PosunKameryX,
                        (int)((stanice.Y * editor.Meritko) - (editor.Sirka * editor.Meritko) / 2) + editor.PosunKameryY,
                        (int)(editor.Sirka * editor.Meritko),
                        (int)(editor.Sirka * editor.Meritko)
                    );
                    g.FillEllipse(stetecSBarvou, rec);
                    g.DrawEllipse(cernePero, rec);
                }
                g.DrawString(vrcholNazev, DefaultFont, Brushes.Black, new Point((int)((stanice.X + editor.Sirka / 2 + 5) * editor.Meritko) + editor.PosunKameryX, (int)((stanice.Y + editor.Sirka / 2 + 5) * editor.Meritko) + editor.PosunKameryY));
            }
        }

        private void NakresliCary(Graphics g)
        {
            //nejprve nakreslim cesty
            Pen cernePeroCar = new Pen(Color.Black, (int)(5 * editor.Meritko));

            int velikostKonce = (int)(5 * editor.Meritko < 3 ? 3 : editor.Meritko * 5 > 10 ? 10 : 5 * editor.Meritko);

            cernePeroCar.CustomEndCap = new AdjustableArrowCap(velikostKonce, velikostKonce);
            foreach (var vrcholNazev in editor.GrafStanic.dejSeznamVrcholu())
            {
                List<string> nasledniciVrcholu = editor.GrafStanic.DejNaslednikyVrcholu(vrcholNazev);
                Stanice staniceZ = editor.GrafStanic.DejDataVrcholu(vrcholNazev);
                foreach (var naslednikVrcholu in nasledniciVrcholu)
                {
                    Stanice staniceDo = editor.GrafStanic.DejDataVrcholu(naslednikVrcholu);
                    List<Point> ciloveBody = DejStartovniAKonecnouPoziciCar(staniceZ.X, staniceZ.Y, staniceDo.X, staniceDo.Y);
                    g.DrawLine(cernePeroCar, (int)(ciloveBody[0].X * editor.Meritko) + editor.PosunKameryX, (int)(ciloveBody[0].Y * editor.Meritko) + editor.PosunKameryY, (int)(ciloveBody[1].X * editor.Meritko) + editor.PosunKameryX, (int)(ciloveBody[1].Y * editor.Meritko) + editor.PosunKameryY);
                }
            }
        }

        private List<Point> DejStartovniAKonecnouPoziciCar(int x1, int y1, int x2, int y2) {
            int startX;
            int startY;
            int cilX;
            int cilY;

            if (Math.Abs(x1 - x2) < editor.Sirka) {
                if (y1 < y2)
                {
                    //startY = y1 + editor.Sirka / 2;
                    cilY = y2 - editor.Sirka / 2;
                }
                else
                {
                    //startY = y1 - editor.Sirka / 2;
                    cilY = y2 + editor.Sirka / 2;
                }
                cilX = x2;
            } else {
                if (x1 < x2)
                {
                    //startX = x1 + editor.Sirka / 2;
                    cilX = x2 - editor.Sirka / 2;
                }
                else {
                    //startX = x1 - editor.Sirka / 2;
                    cilX = x2 + editor.Sirka / 2;
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

        private void vstupniUzel_click(object sender, EventArgs e)
        {
            ZvyrazniVybraneTlacitko(vstupniUzel);
        }

        private void prujezdovyUzel_click(object sender, EventArgs e)
        {
            ZvyrazniVybraneTlacitko(prujezdovyUzel);
        }

        private void koncovyUzel_click(object sender, EventArgs e)
        {
            ZvyrazniVybraneTlacitko(koncovyUzel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            editor.VybranyVrchol = null;
            ZvyrazniVybraneTlacitko(presunUzel);
            prekresli();
        }

        private void vstupniKoncovy_Click(object sender, EventArgs e)
        {
            ZvyrazniVybraneTlacitko(vstupniKoncovy);
        }

        private void ZvyrazniVybraneTlacitko(Button tlacitko) {
            if (tlacitko == presunUzel)
            {
                editor.ZvolenyTypPrvku = null;
                editor.ProvadenaAkce = Editor.Editor.TYP_AKCE.PRESUN_BODU;
                editor.PredchoziAkce = Editor.Editor.TYP_AKCE.PRESUN_BODU;
            }
            else
            {
                editor.ZvolenyTypPrvku = tlacitko == vstupniUzel ? Editor.Editor.TYP_PRVKU.VSTUPNI : tlacitko == prujezdovyUzel ? Editor.Editor.TYP_PRVKU.PRUJEZDOVY : tlacitko == koncovyUzel? Editor.Editor.TYP_PRVKU.VYSTUPNI : Editor.Editor.TYP_PRVKU.VSTUPNI_VYSTUPNI;
                editor.ProvadenaAkce = Editor.Editor.TYP_AKCE.VYTVOR_VRCHOL;
                editor.PredchoziAkce = Editor.Editor.TYP_AKCE.VYTVOR_VRCHOL;
            }
            zvolenyPrvek.BackColor = Color.White;
            zvolenyPrvek = tlacitko;
            zvolenyPrvek.BackColor = Color.DarkGreen;
            ovladaciMenu.Update();
        }

        private void canvas_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                if(editor.Klik((MouseEventArgs)e)) prekresli();
            }
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            string nazevNakliklehoVrcholu = editor.DejNazevNaklikleCtvercoveVzdalenostiVrcholu(e);
            if (nazevNakliklehoVrcholu == null)
            {
                if (Control.ModifierKeys == Keys.Control)
                {
                    editor.ZahajPohybKamery(e);
                }
            } else if (editor.ProvadenaAkce == Editor.Editor.TYP_AKCE.PRESUN_BODU) {
                editor.VybranyVrchol = nazevNakliklehoVrcholu;
                editor.PrenastavSouradniceStanice(e);
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            editor.UkonciProvadenouAkci(e);
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(editor.ZpracujPohybMysi(e)) prekresli();
        }

        private void canvas_MouseWheel(object sender, MouseEventArgs e) {
            int scrollDirection = e.Delta;
            if (scrollDirection > 0)
            {
                editor.Meritko *= 1.02;
            }
            else {
                if (editor.Meritko > 0.05)
                {
                    editor.Meritko /= 1.02;
                }
            }
            prekresli();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void vystupSeznamu_TextChanged(object sender, EventArgs e)
        {

        }

        private void dejSeznamL_click(object sender, EventArgs e)
        {
            string radky = DejVystupSeznamuL();
            vystupSeznamu.Text = radky;
        }

        private void seznamR_click(object sender, EventArgs e)
        {
            string radky = "Seznam L:" + Environment.NewLine;
            radky += DejVystupSeznamuL();
            radky += "Seznam R:" + Environment.NewLine;
            radky += DejVystupSeznamuR();
            vystupSeznamu.Text = radky;
        }

        private string DejVystupSeznamuL()
        {
            string radky = "";
            Dictionary<string, List<string>> seznamL = Vypocty.DejSeznamL(editor.GrafStanic);
            foreach (var klicHodnodnota in seznamL)
            {
                string radek = klicHodnodnota.Key + ": { ";
                for (int i = 0; i < klicHodnodnota.Value.Count; i++)
                {
                    if (i < klicHodnodnota.Value.Count - 1)
                    {
                        radek += klicHodnodnota.Value[i] + ", ";
                    }
                    else
                    {
                        radek += klicHodnodnota.Value[i];
                    }
                }
                radek += " }";
                radky += radek + Environment.NewLine;
            }

            return radky;
        }

        private string DejVystupSeznamuR() {
            string radky = "";
            int cisloCesty = 1;
            List<List<string>> seznamR = Vypocty.DejSeznamR(editor.GrafStanic);
            foreach (var cesta in seznamR) {
                string radek = "C" + cisloCesty + " {";

                for(int i = 0; i < cesta.Count; i++)
                {
                    if (i < cesta.Count - 1)
                    {
                        radek += cesta[i] + ", ";
                    }
                    else
                    {
                        radek += cesta[i];
                    }
                }

                radek += " }";
                radky += radek + Environment.NewLine;
                cisloCesty++;
            }

            return radky;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            //form2.Parent = this;
            form2.ShowDialog();
            if (form2.klikNaPotvrdit) {
                string prvniZ = form2.vylucneCesty["prvniZ"];
                string prvniDo = form2.vylucneCesty["prvniDo"];
                string druhaZ = form2.vylucneCesty["druhyZ"];
                string druhaDo = form2.vylucneCesty["druhyDo"];
                string vyhybka = form2.vylucneCesty["vyhybka"];
                if (prvniZ != null && prvniDo != null && druhaZ != null && druhaDo != null && vyhybka != null)
                {
                    editor.GrafStanic.DejDataVrcholu(prvniZ);
                    editor.GrafStanic.DejDataVrcholu(prvniDo);
                    editor.GrafStanic.DejDataVrcholu(druhaZ);
                    editor.GrafStanic.DejDataVrcholu(druhaDo);
                    editor.GrafStanic.DejDataVrcholu(vyhybka);
                    if (prvniZ == null || prvniDo == null || druhaZ == null || druhaDo == null || vyhybka == null
                        || !editor.GrafStanic.DejNaslednikyVrcholu(prvniZ).Contains(vyhybka)
                        || !editor.GrafStanic.DejNaslednikyVrcholu(vyhybka).Contains(prvniDo)
                        || !editor.GrafStanic.DejNaslednikyVrcholu(druhaZ).Contains(vyhybka)
                        || !editor.GrafStanic.DejNaslednikyVrcholu(vyhybka).Contains(druhaDo)
                        )
                    {
                        throw new Exception("Některý z vrcholů v grafu neexistuje");
                    }
                    else
                    {
                        Stanice staniceVyhybky = editor.GrafStanic.DejDataVrcholu(vyhybka);
                        staniceVyhybky.PridejPovolenouCestu(vyhybka, prvniZ, prvniDo);
                        staniceVyhybky.PridejPovolenouCestu(vyhybka, druhaZ, druhaDo);
                        prekresli();
                    }
                }
                else {
                    throw new Exception("Některý z vrcholů v grafu neexistuje");
                }
            }
        }

        private void exportGraf_Click(object sender, EventArgs e)
        {
            editor.UlozGrafDoSouboru();
        }

        private void importGraf_Click(object sender, EventArgs e)
        {
            if(editor.NactiGrafZeSouboru()) prekresli();
        }

        private void exportSeznamLDoSouboru_click(object sender, EventArgs e)
        {
            editor.UlozTextDoSouboru(DejVystupSeznamuL());
        }

        private void exportSeznamRDoSouboru_Click(object sender, EventArgs e)
        {
            editor.UlozTextDoSouboru(DejVystupSeznamuR());
        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog.AllowSomePages = true;
            printDialog.PrinterSettings.MinimumPage = 1;
            printDialog.PrinterSettings.MaximumPage = celkovyPocetStranDokumentu;
            printDialog.PrinterSettings.FromPage = 1;
            printDialog.PrinterSettings.ToPage = celkovyPocetStranDokumentu;

            // zobrazime dialog
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // Tistene stanky 

                switch (printDialog.PrinterSettings.PrintRange)
                {
                    case PrintRange.AllPages:
                        aktualniTistenaStranka = 1;
                        zbyvajiciPocetStranTisku = celkovyPocetStranDokumentu;
                        break;
                    case PrintRange.SomePages:
                        aktualniTistenaStranka = printDialog.PrinterSettings.FromPage;
                        zbyvajiciPocetStranTisku = printDialog.PrinterSettings.ToPage
                                          - printDialog.PrinterSettings.FromPage + 1;
                        break;
                }

                // Nastaveni orientace
                // printDialog.Document.DefaultPageSettings.Landscape (true, false)

                // Vlastní tisk
                printDocument.Print();
            }
        }

        private void náhledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (printDialog.PrinterSettings.PrintRange)
            {
                case PrintRange.AllPages:
                    aktualniTistenaStranka = 1;
                    zbyvajiciPocetStranTisku = celkovyPocetStranDokumentu;
                    break;
                case PrintRange.SomePages:
                    aktualniTistenaStranka = printDialog.PrinterSettings.FromPage;
                    zbyvajiciPocetStranTisku = printDialog.PrinterSettings.ToPage
                                               - printDialog.PrinterSettings.FromPage + 1;
                    break;
            }

            printPreviewDialog.ShowDialog();
        }

        private void vzhledStránkyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogTiskuStranky dialogTiskuStranky = new DialogTiskuStranky();
            if (dialogTiskuStranky.ShowDialog() == DialogResult.OK) {
                PageSettings vlastnostiTisku = printDocument.DefaultPageSettings;
                PrinterSettings nastaveniTiskarny = vlastnostiTisku.PrinterSettings;
                PaperKind zvolenyDruhPapiru = dialogTiskuStranky.VelikostStranky();

                foreach (PaperSize vybranyFormat in nastaveniTiskarny.PaperSizes)
                {
                    if (vybranyFormat.Kind.Equals(zvolenyDruhPapiru))
                    {
                        vlastnostiTisku.PaperSize = vybranyFormat;
                        break;
                    }
                }

                Okraje okraje = dialogTiskuStranky.Okraje();
                vlastnostiTisku.Landscape = dialogTiskuStranky.Orientace() == Orientace.NA_SIRKU;
                vlastnostiTisku.Margins = new Margins(okraje.Vlevo, okraje.Nahore, okraje.Vpravo, okraje.Dole);
                vlastniVlastnosti = new VlastniVlastnostiTisku();
                vlastniVlastnosti.Tisknout = dialogTiskuStranky.Tisknout();
                vlastniVlastnosti.PomerStran = dialogTiskuStranky.PomerStran();
                vlastniVlastnosti.Centrovani = dialogTiskuStranky.Centrovani();
                vlastniVlastnosti.TextVZahlavi = dialogTiskuStranky.TextVZahlavi();
                vlastniVlastnosti.TextVZapati = dialogTiskuStranky.TextVZapati();
                vlastniVlastnosti.PosterovyTisk = dialogTiskuStranky.PosterovyTisk();
                vlastniVlastnosti.Okraje = dialogTiskuStranky.Okraje();
                vlastniVlastnosti.Meritko = dialogTiskuStranky.Meritko();
                vlastniVlastnosti.Centrovani = dialogTiskuStranky.Centrovani();
                vlastniVlastnosti.PosterovyTisk = dialogTiskuStranky.PosterovyTisk();
                vlastniVlastnosti.PocetStranNaSirku = dialogTiskuStranky.PocetStranPosterovehoTiskuNaSirku();
                vlastniVlastnosti.PocetStranNaVysku = dialogTiskuStranky.PocetStranPosterovehoTiskuNaVysku();
            }
        }
    }
}
