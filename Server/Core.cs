using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Console = Colorful.Console;

namespace Server
{
    class Core
    {
        
        static void Main(string[] args)
        {
            try
            {
                Server.Start();
                Log($"Started Server on Port, {Port}");
                Log("Waiting For Incoming Connections");
                while (true)
                {
                    TcpClient Client = Server.AcceptTcpClient();
                    Log($"Client Connected From, {Client.Client.RemoteEndPoint}");
                    new Thread(()=>RecieveMessages(Client)).Start();

                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
            Console.ReadLine();
        }

        static void RecieveMessages(TcpClient Client)
        {
            while (Client.Connected)
            {
                try
                {
                    NetworkStream Stream = Client.GetStream();
                    byte[] Buffer = new byte[Client.ReceiveBufferSize];
                    Stream.Read(Buffer, 0, Buffer.Length);
                    string Message = Encoding.ASCII.GetString(Buffer);
                    Log($"Recieved Message, \"{Message.Substring(0, Message.IndexOf(">_<"))}\" , From Client");
                }
                catch
                {
                    Log($"Client Disconnected From, {Client.Client.RemoteEndPoint}");
                }
            }
        }

        static void Log(string message)
        {
            Console.Write("» ", Color.Crimson);
            Console.WriteLine(message,Color.LightGray);
        }

        public static int Port = 3621;
        public static TcpListener Server = new TcpListener(IPAddress.Any,Port);
    }
}
