using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class ServerGameListener : Listener
    {
        Server server;
        public ServerGameListener(TcpClient client, bool loop, Server server) : base(client, loop)
        {
            this.server = server;
        }
        public override void actionResult(string data)
        {
            //Debug.WriteLine(data);
            string[] values = data.Split(';');           
            for (int i = 0; i < values.Length; i++)
            {
                string[] message = values[i].Split(':');
                if (message[0] == "2")
                {
                    server.sendInfo(values[i]+";");
                }
                else if (message[0] == "3")
                {
                    server.sendBonus(values[i] + ";");
                }
                else if (message[0] == "5")
                {
                    server.joueurFinish(message[1]);
                }
                else if (message[0] == "7")
                {
                    server.joueurWin(message[1]);
                }
            }
        }
    }
}
