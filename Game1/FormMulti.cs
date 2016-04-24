using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
        public bool Erreur { get; set; }

        public FormMulti(string message, string pseudo, string ip, int type)
        {
            InitializeComponent();
            Erreur = false;
            lblErreur.Text = message;

            tbxPseudo.Text = pseudo;

            IP = ip;
            tbxIp.Text = ip;

            if (type == 0)
                rdbHote.Checked = true;
            else
                rdbClient.Checked = true;

        }

        private void btnCreer_Click(object sender, EventArgs e)
        {
            if (tbxPseudo.Text.Trim().Length > 0 && tbxIp.Text.Trim().Length > 0)
            {
                Debug.WriteLine(tbxPseudo.Text.Trim().Length+"");
                Pseudo = tbxPseudo.Text;
                IP = tbxIp.Text;
                Type = rdbHote.Checked ? 0 : 1;
                Erreur = false;
                DialogResult = DialogResult.OK;
                Dispose();
            }
            else
            {
                setErreur("Remplissez tous les champs!");
            }
        }

        public void setErreur(string message)
        {
            Erreur = true;
            lblErreur.Text = message;
        }

        private void btnRetour_Click(object sender, EventArgs e)
        {
            Erreur = false;
            Dispose();
        }

        private void FormMulti_Load(object sender, EventArgs e)
        {
            if (IP.Length < 1)
            {
                string localIP;
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("10.0.2.4", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    localIP = endPoint.Address.ToString();
                }
                tbxIp.Text = localIP;
            }
        }
    }
}
