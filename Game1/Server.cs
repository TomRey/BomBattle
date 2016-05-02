using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Game1
{
    class Server
    {
        private TcpListener listener;
        private Dictionary<string, Joueur> clientTCP;
        private Multi parent;
        Random rdm;
        bool stop = false;
        int nbJoueur = 0;
        const int NB_JOUEUR_MAX = 8;

        public Server(string ip, Multi parent)
        {
            this.parent = parent;
            clientTCP = new Dictionary<string, Joueur>();
            IPAddress localIPAddress = IPAddress.Parse(ip);
            IPEndPoint ipLocal = new IPEndPoint(localIPAddress, 8888);
            listener = new TcpListener(ipLocal);
            listener.Start();
            rdm = new Random();

            WaitForClientConnect();

        }

        public void addJoueur(string pseudo, Joueur joueur)
        {
            nbJoueur++;
            foreach (KeyValuePair<string, Joueur> j in clientTCP)
            {
                if (j.Key == pseudo) { 
                    pseudo = pseudo + nbJoueur;
                    NetworkStream serverStream = joueur.client.GetStream();
                    byte[] outStream = Encoding.ASCII.GetBytes("9:"+pseudo+";");
                    serverStream.Write(outStream, 0, outStream.Length);
                    break;
                }
            }
            clientTCP.Add(pseudo, joueur);
            parent.setConnection(pseudo);
        }

        public void startGame()
        {
            Thread thChrono = new Thread(sendChrono);
            thChrono.Start();

            string message = "";
            foreach (KeyValuePair<string, Joueur> j in clientTCP)
                message += j.Key + "#";
            message = message.Remove(message.Length - 1);
            foreach (KeyValuePair<string, Joueur> joueur in clientTCP)
            {
                ServerGameListener clientReq = new ServerGameListener(joueur.Value.client, true, this);
                
                sendInfo("1:" + message + ";");
            }
            
        }

        private string generateBoule()
        {
            int idBoule = rdm.Next(8);
            int idCanon = rdm.Next(4);
            int direction = idCanon > 1 ? -1 : 1;
            int H, W;
            if (idCanon == 0 || idCanon == 2)
            {
                W = rdm.Next(2, 8);
                H = rdm.Next(4, 8);
            }
            else
            {
                W = rdm.Next(2, 8);
                H = rdm.Next(6, 12);
            }
            return idCanon + "#" + idBoule + "#" + direction + "#" + W + "#" + H + "a";
        }

        public void sendChrono()
        {            
            int timer = 4;
            string txtCpt = "";
            while (timer >= 0)
            {
                if (timer == 1)
                    txtCpt = "GO!";
                else
                    txtCpt = "" + (timer - 1);

                foreach (KeyValuePair<string, Joueur> joueur in clientTCP)
                {
                    try
                    {
                        NetworkStream serverStream = joueur.Value.client.GetStream();
                        byte[] outStream = Encoding.ASCII.GetBytes("4:" + txtCpt + ";");
                        serverStream.Write(outStream, 0, outStream.Length);
                        // serverStream.Flush();
                    }
                    catch { }
                }

                Thread.Sleep(1000);
                timer--;
            }
            string dataGameString = "0:";
            for (int i = 0; i < 2000; i++)
            {
                dataGameString += generateBoule();
            }
            dataGameString += ";";
            foreach (KeyValuePair<string, Joueur> joueur in clientTCP)
            {
                try
                {
                    NetworkStream serverStream = joueur.Value.client.GetStream();
                    byte[] outStream = Encoding.ASCII.GetBytes(dataGameString);
                    serverStream.Write(outStream, 0, outStream.Length);
                    //serverStream.Flush();
                }
                catch { }
            }
        }

        public void serverDown()
        {
            sendInfo("8:down;");
        }

        public void joueurWin(string pseudo)
        {
            foreach (KeyValuePair<string, Joueur> joueur in clientTCP)
            {
                if (!joueur.Value.finish && joueur.Key != pseudo)
                {
                    NetworkStream serverStream = joueur.Value.client.GetStream();
                    byte[] outStream = Encoding.ASCII.GetBytes("7:" + pseudo + ";");
                    serverStream.Write(outStream, 0, outStream.Length);
                    //serverStream.Flush();
                }
                else
                {
                NetworkStream serverStream = joueur.Value.client.GetStream();
                byte[] outStream = Encoding.ASCII.GetBytes("6:" + pseudo + ";");
                serverStream.Write(outStream, 0, outStream.Length);
                //serverStream.Flush();
                }
            }

        }

        public void joueurFinish(string pseudo)
        {
            clientTCP[pseudo].finish = true;
            nbJoueur--;
            if(nbJoueur == 1)
            {
                string pseudoGagnant="";
                foreach (KeyValuePair<string, Joueur> joueur in clientTCP)
                {
                    if (!joueur.Value.finish)
                        pseudoGagnant = joueur.Key;
                }
                sendInfo("6:" + pseudoGagnant + ";");
            }
        }

        public void sendInfo(string info)
        {
            foreach (KeyValuePair<string, Joueur> joueur in clientTCP)
            {
                try
                {
                    NetworkStream serverStream = joueur.Value.client.GetStream();
                    byte[] outStream = Encoding.ASCII.GetBytes(info);
                    serverStream.Write(outStream, 0, outStream.Length);
                    // serverStream.Flush();
                }
                catch { }
            }
        }

        public void sendBonus(string info)
        {
            string pseudo = info.Split(':')[1].Split('#')[0];
            NetworkStream serverStream = clientTCP[pseudo].client.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes(info);
            serverStream.Write(outStream, 0, outStream.Length);
           // serverStream.Flush();
        }

        public void stopServer()
        {
            foreach(KeyValuePair<string, Joueur> joueur in clientTCP)
                joueur.Value.client.Close();

            stop = true;
            listener.Stop();
        }

        private void WaitForClientConnect()
        {
            object obj = new object();
            listener.BeginAcceptTcpClient(new AsyncCallback(OnClientConnect), obj);
        }

        private void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                ServerNameListener clientReq = new ServerNameListener(listener.EndAcceptTcpClient(asyn), false, this);
            }
            catch (Exception se)
            {
            }

            if (!stop)
            {
                if(nbJoueur < NB_JOUEUR_MAX-1)
                    WaitForClientConnect();
            }
        }
    }
}
