using System.Net;
using System.Net.Sockets;
using System.Text;

namespace C.Udp.Server
{
    /// <summary>
    /// <para>
    ///     Crie a mesma aplicação do Exercício 1, mas utilizando comunicação em rede através do
    ///     protocolo UDP/IP.
    /// </para>
    /// </summary>
    internal static class Program
    {
        internal static void Main()
        {
            IpServer server = new IpServer(5000);
            server.Start();
        }
    }

    internal class IpServer
    {
        private readonly List<IpClient> clients = new List<IpClient>();
        private readonly UdpClient listener;

        public IpServer(int port)
        {
            listener = new UdpClient(port);
        }

        public void Start()
        {
            Console.WriteLine("Server started.");

            Task.Factory.StartNew(ConectClients);

            SendMessages();
        }

        private void ConectClients()
        {
            while (true)
            {
                IPEndPoint endPoint = new IPEndPoint(0, 0);
                byte[] bytes = listener.Receive(ref endPoint);

                lock (clients)
                {
                    string name = Encoding.Default.GetString(bytes);
                    IpClient known = new IpClient(endPoint, name);
                    clients.Add(known);

                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.WriteLine("The {0} client just connected to the server.", known.Name);
                    Console.Write("Enter a message: ");
                }
            }
        }

        private void SendMessages()
        {
            while (true)
            {
                Console.Write("Enter a message: ");
                string message = Console.ReadLine()!;

                lock (clients)
                {
                    foreach (IpClient client in clients)
                    {
                        byte[] bytes = Encoding.Default.GetBytes(message);
                        listener.Send(bytes, bytes.Length, client.EndPoint);
                    }
                }
            }
        }
    }

    internal class IpClient
    {
        public IpClient(IPEndPoint endPoint, string name)
        {
            EndPoint = endPoint;
            Name = name;
        }

        public IPEndPoint EndPoint { get; private set; }
        public string Name { get; private set; }
    }
}
