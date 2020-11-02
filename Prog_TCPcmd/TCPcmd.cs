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
            Serwer_TCPcmdSync serwer = new Serwer_TCPcmdSync(IPAddress.Any, 2048);

            for (int i = 0; i < 100; i++)
            {
                try
                {
                    serwer.ReadStream();
                    Thread.Sleep(500);
                    serwer.ExecuteCommand();
                    Thread.Sleep(2000);
                }
                catch (Exception error)
                {
                    serwer.AcceptClient();
                }

                
            }



        }

    }
}