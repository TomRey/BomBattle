using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    public partial class FormMulti : Form
    {
        public string Pseudo { get; set; }
        public string IP { get; set; }
        public int Type { get; set; }
        Rectangle rect;
        public FormMulti(Rectangle rectWindows)
        {
            InitializeComponent();
            rect = rectWindows;
        }

        private void btnCreer_Click(object sender, EventArgs e)
        {
            Pseudo = tbxPseudo.Text;
            IP = tbxIp.Text;
            Type = rdbHote.Checked ? 0 : 1;
            Dispose();
        }

        private void btnRetour_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void FormMulti_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(rect.X+(rect.Width/2)-(this.Width/2), rect.Y+rect.Height/2-(this.Height/2));
        }
    }
}
