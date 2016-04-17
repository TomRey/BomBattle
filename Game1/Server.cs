using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game1
{
    class Server
    {
        private TcpListener listener;
        private Dictionary<string, Joueur> clientTCP;
        private Form1 parent;
        Random rdm;
        bool stop = false;
        bool quit = false;

        public Server(string ip, Form1 parent)
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
            clientTCP.Add(pseudo, joueur);
        }

        public void gameOver(string pseudo)
        {
            clientTCP[pseudo].finish = true;
            sendInfo(pseudo + " à perdu!");
        }

        public void win(string pseudo)
        {
            clientTCP[pseudo].finish = true;
            sendInfo(pseudo + " à gagner!");
        }

        public void startGame()
        {
            Thread th = new Thread(SendData);
            th.Start(new int[2] { 1000, 0 });

            Thread th2 = new Thread(SendData);
            th2.Start(new int[2] { 1250, 1 });

            Thread th3 = new Thread(SendData);
            th3.Start(new int[2] { 1000, 2 });

            Thread th4 = new Thread(SendData);
            th4.Start(new int[2] { 750, 3 });

            foreach (KeyValuePair<string, Joueur> joueur in clientTCP)
            {
                ServerGameListener clientReq = new ServerGameListener(joueur.Value.client, true, this);
                clientReq.StartClient();
            }
        }

        public void SendData(object ms)
        {
            int[] tab = ms as int[];
            int direction, H, W = 0;
            int idCanon = tab[1];
            while (quit != true)
            {
                int idBoule = rdm.Next(8);                
                direction = idCanon > 1 ? -1 : 1;
                if (idCanon == 0 || idCanon == 2)
                {
                    H = 3;
                    W = 5;
                }
                else
                {
                    H = 5;
                    W = 6;
                }

                int maxW = rdm.Next(1, W);
                int maxH = rdm.Next(1, H);
                System.Diagnostics.Debug.WriteLine("send: "+ maxH +"");
                foreach (KeyValuePair<string, Joueur> joueur in clientTCP)
                {
                    if (!joueur.Value.finish)
                    {
                        NetworkStream serverStream = joueur.Value.client.GetStream();
                        byte[] outStream = Encoding.ASCII.GetBytes(idCanon + "a" + idBoule + "a" + direction + "a" + maxW + "a" + maxH + "a");
                        serverStream.Write(outStream, 0, outStream.Length);
                        serverStream.Flush();
                    }
                }

                Thread.Sleep(tab[0]);
            }
        }

        public void sendInfo(string info)
        {
            foreach (KeyValuePair<string, Joueur> joueur in clientTCP)
            {
                NetworkStream serverStream = joueur.Value.client.GetStream();
                byte[] outStream = Encoding.ASCII.GetBytes(info);
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
            }
        }

        public void stopServer()
        {
            foreach(KeyValuePair<string, Joueur> joueur in clientTCP)
                joueur.Value.client.Close();

            stop = true;
            quit = true;
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
                ServerNameListener clientReq = new ServerNameListener(listener.EndAcceptTcpClient(asyn), false, parent, this);
                clientReq.StartClient();
            }
            catch (Exception se)
            {
            }

            if (!stop)
            {
                WaitForClientConnect();
            }
        }
    }
}
