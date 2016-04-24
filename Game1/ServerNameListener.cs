using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class ServerNameListener : Listener
    {
        Server server;
        TcpClient client;
        public ServerNameListener(TcpClient client, bool loop, Server server) : base(client, loop)
        {
            this.client = client;
            this.server = server;
        }
        public override void actionResult(string data)
        {
            server.addJoueur(data, new Joueur(client));

        }
    }
}
