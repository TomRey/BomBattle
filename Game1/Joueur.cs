using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Joueur
    {
        public TcpClient client{ get; set; }
        public bool finish { get; set; }

        public Joueur(TcpClient tcpClient)
        {
            client = tcpClient;
            finish = false;
        }

    }
}
