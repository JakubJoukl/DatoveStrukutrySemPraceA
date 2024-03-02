using DatoveStrukutrySemPraceA.Entity.Graf;
using DatoveStrukutrySemPraceA.Entity.ZeleznicniDoprava;
using DatoveStrukutrySemPraceA.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatoveStrukutrySemPraceA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Graf<Stanice, Koleje> grafStanic = new Graf<Stanice, Koleje>();

            grafStanic.PridejVrchol("v23", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v21", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v22", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v24", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v12", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v13", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v14", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v15", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v30", new Stanice { Koncova = true, Pocatecni = true }, true, true);
            grafStanic.PridejVrchol("v16", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v17", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v29", new Stanice { Koncova = true, Pocatecni = true }, true, true);
            grafStanic.PridejVrchol("v18", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v19", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v27", new Stanice { Koncova = true, Pocatecni = false }, false, true);
            grafStanic.PridejVrchol("v28", new Stanice { Koncova = true, Pocatecni = false }, false, true);

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

            //grafStanic.DejSeznamL();
            grafStanic.DejSeznamR();

            //Perzistence<Stanice, Koleje>.UlozGrafDoSouboru("test.txt", grafStanic);
            //Graf<Stanice, Koleje> graf2 = Perzistence<Stanice, Koleje>.NactiGrafZeSouboru("test.txt");
            Console.WriteLine("Konec");
        }
    }
}
