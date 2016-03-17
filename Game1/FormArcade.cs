using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    public partial class FormArcade : Form
    {
        public string Pseudo { get; set; }

        public FormArcade()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Pseudo = tbxPseudo.Text;
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
