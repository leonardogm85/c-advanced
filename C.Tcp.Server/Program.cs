using System.Net;
using System.Net.Sockets;

namespace C.Tcp.Server
{
    /// <summary>
    /// <para>
    ///     Crie uma aplicação cliente-servidor que utiliza o protocolo TCP/IP para comunicação.
    /// </para>
    /// <para>
    ///     O servidor fica aguardando clientes ser conectarem. Cada cliente que se conecta envia o seu
    ///     nome e o servidor armazena a referência deste cliente. Toda vez que uma mensagem é
    ///     digitada no servidor, seguida de ENTER, ela é repassada a todos os clientes conectados.
    /// </para>
    /// <para>
    ///     O cliente deve ser capaz de se conectar a um servidor, em um host e porta conhecidos.
    ///     Quando a conexão é estabelecida, o cliente envia o seu nome (que deve ser digitado pelo
    ///     usuário via teclado) e fica aguardando o recebimento de mensagens do servidor. Quando uma
    ///     mensagem é recebida, o cliente deve mostrá-la na tela.
    /// </para>
    /// </summary>
    internal static class Program
    {
        internal static void Main()
        {
            IpServer server = new IpServer(4000);
            server.Start();
        }
    }

    internal class IpServer
    {
        private readonly List<IpClient> clients = new List<IpClient>();
        private readonly TcpListener listener;

        public IpServer(int port)
        {
            listener = new TcpListener(IPAddress.Loopback, port);
        }

        public void Start()
        {
            listener.Start();

            Console.WriteLine("Server started.");

            Task.Factory.StartNew(ConectClients);

            SendMessages();
        }

        private void ConectClients()
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                IpClient connected = new IpClient(client);

                lock (clients)
                {
                    clients.Add(connected);

                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.WriteLine("The {0} client just connected to the server.", connected.Name);
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
                        client.Writer.WriteLine(message);
                    }
                }
            }
        }
    }

    internal class IpClient
    {
        public IpClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            Reader = new StreamReader(stream);
            Writer = new StreamWriter(stream);

            Writer.AutoFlush = true;

            Name = Reader.ReadLine()!;
        }

        public StreamReader Reader { get; private set; }
        public StreamWriter Writer { get; private set; }
        public string Name { get; private set; }
    }
}
