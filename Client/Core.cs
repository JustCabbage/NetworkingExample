using System;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using Console = Colorful.Console;

namespace Client
{
    class Core
    {
        static void Main(string[] args)
        {
            try
            {
                Client.Connect(HostName, Port);
                Log($"Connected to server, {HostName}:{Port}");
                while (true)
                {
                    Input();
                    string Message = Console.ReadLine();
                    NetworkStream Stream = Client.GetStream();
                    byte[] Buffer = Encoding.ASCII.GetBytes(Message+">_<"); // To Remove Ending Whitespace Characters Add >_<
                    Stream.Write(Buffer,0, Buffer.Length);
                    Stream.Flush();
                }
            }
            catch(Exception ex)
            {
                Log(ex.Message);
            }
            Console.ReadLine();
            Client.Dispose();
        }
        static void Input()
        {
            Console.Write("» ", Color.Crimson);
            Console.Write("Message ", Color.LightGray);
            Console.Write("» ", Color.Crimson);
        }
        static void Log(string message)
        {
            Console.Write("» ", Color.Crimson);
            Console.WriteLine(message, Color.LightGray);
        }


        public static string HostName = "127.0.0.1";
        public static int Port = 3621;
        public static TcpClient Client = new TcpClient();
    }
}
