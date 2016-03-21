using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace Game1
{
    public partial class FormArcade : Form
    {
        public string Pseudo { get; set; }
        Rectangle rect;

        public FormArcade(Rectangle rectWindows)
        {
            InitializeComponent();
            rect = rectWindows;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Pseudo = tbxPseudo.Text;
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void FormArcade_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point( + (rect.Width / 2) - (this.Width / 2), rect.Y + rect.Height / 2 - (this.Height / 2));
        }
    }
}
