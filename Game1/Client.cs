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
        string pseudo;
        Form1 parent;
        ClientListener clientReq;

        public Client(string ip, string name, Form1 parent, Game1 game)
        {
            pseudo = name;
            this.parent = parent;
            clientSocket.Connect(ip, 8888);
            SendData(name);
            clientReq = new ClientListener(clientSocket, true, parent, game);
            clientReq.StartClient();
        }

        public void finish(string message)
        {
            SendData(message);
        }

        public void SendData(string dataTosend)
        {
            if (!clientSocket.Connected)
                return;
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(dataTosend);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }

        public void CloseConnection()
        {
            clientReq.stopLoop();
        }
    }
}
