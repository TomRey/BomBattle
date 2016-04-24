using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.IO;

namespace Game1
{
    public partial class FormArcade : Form
    {
        public string Pseudo { get; set; }
        int score = 0;

        public FormArcade(int score)
        {
            InitializeComponent();
            this.score = score;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Pseudo = tbxPseudo.Text;

            // This text is added only once to the file.
            if (!File.Exists(Game1.CLASSEMENT_PATH))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(Game1.CLASSEMENT_PATH))
                {
                    sw.WriteLine(tbxPseudo.Text + ":" + score);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(Game1.CLASSEMENT_PATH))
                {
                    sw.WriteLine(tbxPseudo.Text + ":" + score);
                }
            }
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void FormArcade_Load(object sender, EventArgs e)
        {
        }
    }
}
