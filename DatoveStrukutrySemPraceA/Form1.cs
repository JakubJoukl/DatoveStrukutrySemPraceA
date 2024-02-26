using DatoveStrukutrySemPraceA.Entity.Graf;
using DatoveStrukutrySemPraceA.Entity.ZeleznicniDoprava;
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
            
            Vrchol<Stanice, Koleje> v23 = new Vrchol<Stanice, Koleje>();
            Vrchol<Stanice, Koleje> v21 = new Vrchol<Stanice, Koleje>();
            Vrchol<Stanice, Koleje> v22 = new Vrchol<Stanice, Koleje>();
            Vrchol<Stanice, Koleje> v24 = new Vrchol<Stanice, Koleje>();
            Vrchol<Stanice, Koleje> v12 = new Vrchol<Stanice, Koleje>();
            Vrchol<Stanice, Koleje> v13 = new Vrchol<Stanice, Koleje>();
            Vrchol<Stanice, Koleje> v14 = new Vrchol<Stanice, Koleje>();
            Vrchol<Stanice, Koleje> v15 = new Vrchol<Stanice, Koleje>();
            Vrchol<Stanice, Koleje> v30 = new Vrchol<Stanice, Koleje>();
            Vrchol<Stanice, Koleje> v16 = new Vrchol<Stanice, Koleje>();
            Vrchol<Stanice, Koleje> v17 = new Vrchol<Stanice, Koleje>();
            Vrchol<Stanice, Koleje> v29 = new Vrchol<Stanice, Koleje>();
            Vrchol<Stanice, Koleje> v18 = new Vrchol<Stanice, Koleje>();
            Vrchol<Stanice, Koleje> v19 = new Vrchol<Stanice, Koleje>();
            Vrchol<Stanice, Koleje> v27 = new Vrchol<Stanice, Koleje>();
            Vrchol<Stanice, Koleje> v28 = new Vrchol<Stanice, Koleje>();

            v23.Data = new Stanice { Koncova = false, Pocatecni = true };
            v23.PridejHranu(new Hrana<Stanice, Koleje>()
            {
                CilovyVrchol = v12,
                Data = new Koleje()
            });

            grafStanic.VlozBranu(v21);
            grafStanic.VlozBranu(v22);
            grafStanic.VlozBranu(v23);
            grafStanic.VlozBranu(v24);
            grafStanic.VlozBranu(v29);
            grafStanic.VlozBranu(v30);
            v27.Data = new Stanice { Koncova = true, Pocatecni = false };
            v28.Data = new Stanice { Koncova = true, Pocatecni = false };
            v29.Data = new Stanice { Koncova = true, Pocatecni = false };
            v30.Data = new Stanice { Koncova = true, Pocatecni = false };
        }
    }
}
