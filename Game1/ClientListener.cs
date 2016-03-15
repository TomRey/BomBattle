using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class ClientListener : Listener
    {
        Form1 parent;
        Game1 game;
        public ClientListener(TcpClient client, bool loop, Form1 parent, Game1 game) : base(client, loop)
        {
            this.parent = parent;
            this.game = game;
        }
        public override void actionResult(string data)
        {
            parent.setGame(data);
            string[] value = data.Split('#');
            game.launchBoule(int.Parse(value[0]), int.Parse(value[1]), int.Parse(value[2]), int.Parse(value[3]), int.Parse(value[4]));
        }
    }
}
