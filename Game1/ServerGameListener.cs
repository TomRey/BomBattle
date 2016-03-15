using System;
using System.Collections.Generic;
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
            string[] message = data.Split('-');
            if(message[1].Equals("0"))
            {
                server.gameOver(message[0]);
            }
            else
            {
                server.win(message[0]);
            }
            server.sendInfo(message[2]);
        }
    }
}
