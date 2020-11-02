using System;
using System.Net;
using System.Threading;
using Serwer_TCPcmd;

namespace IO_Lab1_Serwer
{
    class TCPcmd
    {
        static void Main(string[] args)
        {
            Serwer_TCPcmdAPM serwer = new Serwer_TCPcmdAPM(IPAddress.Any, 2048);


        }

    }
}