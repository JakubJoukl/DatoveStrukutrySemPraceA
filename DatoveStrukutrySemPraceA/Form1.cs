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

        private Graf<Stanice, Koleje> DejPokusnyGraf()
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

            return grafStanic;
        }

        private Graf<Stanice, Koleje> DejCelyGraf()
        {
            Graf<Stanice, Koleje> grafStanic = new Graf<Stanice, Koleje>();
            grafStanic.PridejVrchol("v109", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v107", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v101", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v102", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v108", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v112", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v114", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v120", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v122", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v124", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v126", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v128", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v130", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v132", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v140", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v113", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v111", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v103", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v104", new Stanice { Koncova = false, Pocatecni = true }, true, false);
            grafStanic.PridejVrchol("v106", new Stanice { Koncova = false, Pocatecni = true }, true, false);

            grafStanic.PridejVrchol("v51", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v52", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v53", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v54", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v55", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v57", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v56", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v61", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v62", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v60", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v59", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v58", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v65", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v66", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v67", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v70", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v63", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v64", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v68", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v69", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v71", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v72", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v74", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v73", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v76", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v75", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v78", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v77", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v82", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v81", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v80", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v79", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v84", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v83", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v85", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v86", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v87", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v88", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v95", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v89", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v90", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v92", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v91", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v94", new Stanice { Koncova = false, Pocatecni = false }, false, false);
            grafStanic.PridejVrchol("v93", new Stanice { Koncova = false, Pocatecni = false }, false, false);

            grafStanic.PridejVrchol("v134", new Stanice { Koncova = true, Pocatecni = false }, false, true);
            grafStanic.PridejVrchol("v136", new Stanice { Koncova = true, Pocatecni = false }, false, true);
            grafStanic.PridejVrchol("v138", new Stanice { Koncova = true, Pocatecni = false }, false, true);
            grafStanic.PridejVrchol("v602", new Stanice { Koncova = true, Pocatecni = false }, false, true);
            grafStanic.PridejVrchol("v601", new Stanice { Koncova = true, Pocatecni = false }, false, true);
            grafStanic.PridejVrchol("v301", new Stanice { Koncova = true, Pocatecni = false }, false, true);
            grafStanic.PridejVrchol("v302", new Stanice { Koncova = true, Pocatecni = false }, false, true);

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
            grafStanic.PridejHranu("v53", "v52", new Koleje());
            grafStanic.PridejHranu("v52", "v134", new Koleje());
            grafStanic.PridejHranu("v53", "v136", new Koleje());
            grafStanic.PridejHranu("v53", "v138", new Koleje());

            grafStanic.PridejHranu("v60", "v63", new Koleje());
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

            grafStanic.PridejPovolenouCestu("v95", "v88", "v89");
            grafStanic.PridejPovolenouCestu("v95", "v87", "v90");

            return grafStanic;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Graf<Stanice, Koleje> grafStanic = DejPokusnyGraf();
            Graf<Stanice, Koleje> grafStanic = DejCelyGraf();

            //grafStanic.DejSeznamL();
            grafStanic.DejSeznamR();

            //Perzistence<Stanice, Koleje>.UlozGrafDoSouboru("test.txt", grafStanic);
            //Graf<Stanice, Koleje> graf2 = Perzistence<Stanice, Koleje>.NactiGrafZeSouboru("test.txt");
            Console.WriteLine("Konec");
        }
    }
}
