using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game1
{
    class Client
    {
        TcpClient clientSocket = new TcpClient();
        public string Pseudo{get; set;}
        Multi parent;
        ClientListener clientReq;
        bool stop = false;

        public Client(string ip, string name, Multi parent)
        {
            Pseudo = name;
            this.parent = parent;
            clientSocket.Connect(ip, 8888);
            clientReq = new ClientListener(clientSocket, true, parent);
            SendData(name);
        }

        public void start()
        {
            Thread th = new Thread(SendDataPlayer);
            th.Start();
        }

        public void close()
        {
            stop = true;
            clientSocket.Close();
        }

        public void SendDataPlayer()
        {
            while (!stop)
            {
                if (!clientSocket.Connected)
                    return;
                NetworkStream serverStream = clientSocket.GetStream();
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes("2:" + Pseudo + "#" + parent.getPlayerData() + ";");
                serverStream.Write(outStream, 0, outStream.Length);
                //serverStream.Flush();
                Thread.Sleep(2000);
            }
        }

        public void SendData(string dataTosend)
        {
            if (!clientSocket.Connected)
                return;
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(dataTosend);
            serverStream.Write(outStream, 0, outStream.Length);
            //serverStream.Flush();
        }
    }
}
