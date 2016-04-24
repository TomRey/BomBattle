using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    abstract class Listener
    {
        TcpClient _clientSocket;
        NetworkStream _networkStream = null;
        bool loop;

        public abstract void actionResult(string data);

        public Listener(TcpClient clientConnected, bool loop)
        {
            this.loop = loop;
            this._clientSocket = clientConnected;
            _networkStream = _clientSocket.GetStream();
            WaitForRequest();
        }

        public void WaitForRequest()
        {         
            if (_clientSocket.Connected)
            {
                byte[] buffer = new byte[_clientSocket.ReceiveBufferSize];
                try
                {
                    _networkStream.BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);
                }
                catch
                {
                    _clientSocket.Close();
                    loop = false;
                }
            }
            else
            {
                loop = false;
            }
        }

        private void ReadCallback(IAsyncResult result)
        {
            try
            {
                NetworkStream networkStream = _clientSocket.GetStream();
                int read = networkStream.EndRead(result);
                if (read != 0)
                {

                    byte[] buffer = result.AsyncState as byte[];
                    string data = Encoding.Default.GetString(buffer, 0, read);
                    actionResult(data);
                }
            }
            catch (Exception ex)
            {
            }

            if (loop)
                this.WaitForRequest();
        }
    }
}
