using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class ClientListener : Listener
    {
        Multi parent;
        public ClientListener(TcpClient client, bool loop, Multi parent) : base(client, loop)
        {
            this.parent = parent;
        }

        public override void actionResult(string data)
        {
            Debug.WriteLine(data);
            string[] values = data.Split(';');
            for (int i = 0; i < values.Length; i++)
            {
                string[] message = values[i].Split(':');
                if (message[0] == "0")
                {
                    //game.launchBoule(int.Parse(value[0]), int.Parse(value[1]), int.Parse(value[2]), int.Parse(value[3]), int.Parse(value[4]));
                    parent.setGame(message[1]);
                }
                else if (message[0] == "1")
                {
                    parent.initPlayer(message[1]);
                }
                else if (message[0] == "2")
                {
                    parent.setDataPlayer(message[1]);
                }
                else if (message[0] == "3")
                {
                    int idBonus = int.Parse(message[1].Split('#')[1]);
                    parent.receiveBonus(idBonus);
                }
                else if (message[0] == "4")
                {
                    parent.decompte(message[1]);
                }
                else if (message[0] == "6")
                {
                    parent.finishGame(message[1]);
                }
                else if (message[0] == "7")
                {
                    parent.finishGameWinner(message[1]);
                }
                else if(message[0] == "8")
                {
                    parent.serverDown();
                }
                else if (message[0] == "9")
                {
                    parent.changePseudo(message[1]);
                }
            }
        }
    }
}
