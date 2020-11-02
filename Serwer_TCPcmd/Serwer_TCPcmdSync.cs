using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Serwer_TCPcmd
{
    public class Serwer_TCPcmdSync : Serwer_TCPcmd
    {
        public Serwer_TCPcmdSync(IPAddress adresIP, int port) : base(adresIP, port)
        {

        }

        public new void ExecuteCommand() => base.ExecuteCommand();

        public new void ReadStream() => base.ReadStream();

        public new void AcceptClient() => base.AcceptClient();
    }
}
