﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        delegate void SetTextCallback(string ip);
        Client client;
        Server server;
        string pseudo;
        public int vie { get; set; }
        int type = 1;

        private void btnCreer_Click(object sender, EventArgs e)
        {
            vie = 0;
            type = 0;
            pseudo = tbxPseudo.Text;
            //server = new Server(tbxIP.Text, this);
            //client = new Client(tbxIP.Text, pseudo, this);
            btnStart.Visible = true;
        }

        private void btnRejoindre_Click(object sender, EventArgs e)
        {
            type = 1;
            pseudo = tbxPseudo.Text;
            //client = new Client(tbxIP.Text, pseudo, this);
            btnQuitter.Visible = true;
        }

        public void setConnection(string ip)
        {
            if (this.result.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(setConnection);
                this.Invoke(d, new object[] { ip });
            }
            else
            {
                    result.Text += ip + Environment.NewLine;
            }
        }

        public void setGame(string val)
        {
            if (this.game.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(setGame);
                this.Invoke(d, new object[] { val });
            }
            else
            {
                game.Text += val + Environment.NewLine;
            }
        }

        public void setDataPlayer(string val)
        {
            if (this.dataPlayer.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(setDataPlayer);
                this.Invoke(d, new object[] { val });
            }
            else
            {
                dataPlayer.Text += val + Environment.NewLine;
            }
        }

        public void initPlayer(string val)
        {
            if (this.dataPlayer.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(setDataPlayer);
                this.Invoke(d, new object[] { val });
            }
            else
            {
                dataPlayer.Text += "nbJoueur = " + val + Environment.NewLine;
            }
            client.start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("10.0.2.4", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            tbxIP.Text = localIP;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(type == 0)
                server.stopServer();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            server.startGame();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {

        }

        private void btnBombe_Click(object sender, EventArgs e)
        {
            //client.finish(pseudo + "-0-BOMBE");
            vie++;
        }

        private void btnGagner_Click(object sender, EventArgs e)
        {
            //client.finish(pseudo + "-1-GAGNER");
            vie--;
        }
    }
}
