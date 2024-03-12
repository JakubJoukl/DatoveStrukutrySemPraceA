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
    public partial class Form2 : Form
    {
        public bool klikNaPotvrdit;
        public Dictionary<string, string> vylucneCesty = new Dictionary<string, string>();
        public Form2()
        {
            InitializeComponent();
        }

        private void zrusit_Click(object sender, EventArgs e)
        {
            klikNaPotvrdit = false;
            this.Close();
        }

        private void potvrdit_Click(object sender, EventArgs e)
        {
            klikNaPotvrdit = true;
            vylucneCesty["prvniZ"] = prvniZ.Text;
            vylucneCesty["prvniDo"] = prvniDo.Text;
            vylucneCesty["druhyZ"] = druhyZ.Text;
            vylucneCesty["druhyDo"] = druhyDo.Text;
            vylucneCesty["vyhybka"] = vyhybka.Text;
            this.Close();
        }
    }
}
