using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Serwer_TCPcmd
{
    public class Serwer_TCPcmdAPM : Serwer_TCPcmd
    {

        public delegate void TransmissionStreamDelegate(NetworkStream stream);


        public Serwer_TCPcmdAPM(IPAddress ip, int port) : base()
        {
            server = new TcpListener(ip, port);
            server.Start();

            AcceptClient();
        }

        protected new void AcceptClient()
        {
            while (true)
            {
                TcpClient tcpClient = server.AcceptTcpClient();
                stream = tcpClient.GetStream();

                TransmissionStreamDelegate transmissionDelegate = new TransmissionStreamDelegate(ReadStream);

                //callback style
                transmissionDelegate.BeginInvoke(stream, TransmissionCallback, tcpClient);

                // async result style

                //IAsyncResult result = transmissionDelegate.BeginInvoke(Stream, null, null);

                ////operacje......

                //while (!result.IsCompleted) ;

                ////sprzątanie

            }

        }

        protected void ReadStream(NetworkStream stream)
        {

            byte[] buffer = new byte[2048];
            System.Diagnostics.Process process;
            System.Diagnostics.ProcessStartInfo startInfo;
            process = new System.Diagnostics.Process();
            startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = ""
            };

            process.StartInfo = startInfo;


            while (true)
            {
                try
                {
                    int meassage_length = stream.Read(buffer, 0, 2048);
                    startInfo.Arguments = System.Text.Encoding.UTF8.GetString(buffer, 0, meassage_length);
                    process.StartInfo = startInfo;

                    process.Start();
                    process.Close();
                    stream.Write(Encoding.UTF8.GetBytes("executed" + System.Environment.NewLine), 0,
                        Encoding.UTF8.GetBytes("executed" + System.Environment.NewLine).Length);

                }
                catch (Exception error)
                {
                    break;
                }
            }
        }

        private void TransmissionCallback(IAsyncResult ar)
        {
            TcpClient tcpClient = ar.AsyncState as TcpClient;
            tcpClient.Close();

        }

    }
}
