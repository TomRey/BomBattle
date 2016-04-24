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
    public partial class FormSession : Form
    {
        delegate void SetTextCallback(string data);
        int cpt = 0;
        public FormSession()
        {
            InitializeComponent();
        }

        private void btnBackToConnection_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
        }

        public void setConnection(string data)
        {          
            if (this.tbxJoueur.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(setConnection);
                this.Invoke(d, new object[] { data });
            }
            else
            {
                cpt++;
                if (cpt >= 1)
                    btnStart.Enabled = true;
                else
                    btnStart.Enabled = false;
                tbxJoueur.Text += data + Environment.NewLine;
                lblJoueur.Text = "NOMBRE DE JOUEUR : " + cpt + " / 8";
            }
        }
    }
}
