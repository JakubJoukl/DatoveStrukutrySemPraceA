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
    public partial class DialogTiskuStranky : Form
    {
        public DialogTiskuStranky()
        {
            InitializeComponent();
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
    }
}
