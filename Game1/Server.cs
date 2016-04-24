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
        private List<string> listPseudo;
        private Multi parent;
        Random rdm;
        bool stop = false;
        bool quit = false;
        int nbJoueur = 0;
        const int NB_JOUEUR_MAX = 8;
        bool finishGame = false;
        bool bStartGame = false;
        int[] sleepTime;
        int cptSecondeElapsed = 0;
        System.Timers.Timer timerGame;

        public Server(string ip, Multi parent)
        {
            this.parent = parent;
            clientTCP = new Dictionary<string, Joueur>();
            IPAddress localIPAddress = IPAddress.Parse(ip);
            IPEndPoint ipLocal = new IPEndPoint(localIPAddress, 8888);
            listener = new TcpListener(ipLocal);
            listener.Start();
            rdm = new Random();
            sleepTime = new int[4] { 200, 7000, 10000, 3000 };

            timerGame = new System.Timers.Timer();
            timerGame.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timerGame.Interval = 1000;

            WaitForClientConnect();

        }

        // Specify what you want to happen when the Elapsed event is raised.
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            cptSecondeElapsed++;

            if (cptSecondeElapsed == 20)
            {
                //sleepTime[0] = 3000;
                sleepTime[1] = 1000;
                sleepTime[3] = 3000;
            }

            if (cptSecondeElapsed == 40)
            {
                sleepTime[2] = 2000;
            }

            if (cptSecondeElapsed == 70)
            {
                //sleepTime[0] = 2000;
                sleepTime[1] = 3000;
                sleepTime[2] = 2000;
                sleepTime[3] = 1000;
            }

            if (cptSecondeElapsed == 100)
            {
                //sleepTime[0] = 2000;
                sleepTime[1] = 1000;
                sleepTime[2] = 2000;
                sleepTime[3] = 1000;

                timerGame.Stop();
            }
        }

        public void addJoueur(string pseudo, Joueur joueur)
        {
            nbJoueur++;
            foreach (KeyValuePair<string, Joueur> j in clientTCP)
            {
                if (j.Key == pseudo) { 
                    pseudo = pseudo + nbJoueur;
                    break;
                }
            }
            clientTCP.Add(pseudo, joueur);
            parent.setConnection(pseudo);
        }

        public void startGame()
        {
            bStartGame = false;
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
                    NetworkStream serverStream = joueur.Value.client.GetStream();
                    byte[] outStream = Encoding.ASCII.GetBytes("4:" + txtCpt + ";");
                    serverStream.Write(outStream, 0, outStream.Length);
                    // serverStream.Flush();
                }

                Thread.Sleep(1000);
                timer--;
            }
            bStartGame = true;
            string dataGameString = "0:";
            for (int i = 0; i < 1000; i++)
            {
                dataGameString += generateBoule();
            }
            dataGameString += ";";
            //Debug.WriteLine(dataGameString);
            foreach (KeyValuePair<string, Joueur> joueur in clientTCP)
            {
                    NetworkStream serverStream = joueur.Value.client.GetStream();
                    byte[] outStream = Encoding.ASCII.GetBytes(dataGameString);
                    serverStream.Write(outStream, 0, outStream.Length);
                    //serverStream.Flush();
            }
            /*Thread thCanon0 = new Thread(SendData);
            thCanon0.Start(0);*/

            /* Thread thCanon1 = new Thread(SendData);
             thCanon1.Start(1);

             Thread thCanon2 = new Thread(SendData);
             thCanon2.Start(2);

             Thread thCanon3 = new Thread(SendData);
             thCanon3.Start(3);*/

            // timerGame.Start();

        }

        public void SendData(object ms)
        {
            int canon = (int)ms;
            int direction, H, W;
            int idCanon = canon;

            /*int timer = 4;
            string txtCpt = "";
            while (timer >= 0)
            {
                if(timer == 1)
                    txtCpt = "GO!";
                else
                    txtCpt = "" + (timer-1);

                foreach (KeyValuePair<string, Joueur> joueur in clientTCP)
                {
                    NetworkStream serverStream = joueur.Value.client.GetStream();
                    byte[] outStream = Encoding.ASCII.GetBytes("4:" + txtCpt +";");
                    serverStream.Write(outStream, 0, outStream.Length);
                   // serverStream.Flush();
                }

                Thread.Sleep(1000);
                timer--;
            }*/

            while (!finishGame)
            {
                int idBoule = rdm.Next(8);
             
                direction = idCanon > 1 ? -1 : 1;
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
                
                foreach (KeyValuePair<string, Joueur> joueur in clientTCP)
                {
                    if (!joueur.Value.finish)
                    {
                        NetworkStream serverStream = joueur.Value.client.GetStream();
                        byte[] outStream = Encoding.ASCII.GetBytes("0:" + idCanon + "#" + idBoule + "#" + direction + "#" + W + "#" + H + ";");
                        serverStream.Write(outStream, 0, outStream.Length);
                        //serverStream.Flush();
                    }
                }

                Thread.Sleep(sleepTime[idCanon]);
            }
        }

        public void joueurWin(string pseudo)
        {

            finishGame = true;
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
                finishGame = true;
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
                NetworkStream serverStream = joueur.Value.client.GetStream();
                byte[] outStream = Encoding.ASCII.GetBytes(info);
                serverStream.Write(outStream, 0, outStream.Length);
               // serverStream.Flush();
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

            bStartGame = false;
            finishGame = true;
            stop = true;
            quit = true;
            timerGame.Stop();
            timerGame.Close();
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
