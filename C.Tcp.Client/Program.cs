using System.Net.Sockets;

namespace C.Tcp.Client
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
    internal class Program
    {
        internal static void Main()
        {
            Console.WriteLine("Client started.");

            Console.Write("Enter a name: ");
            string name = Console.ReadLine()!;

            TcpClient client = new TcpClient("localhost", 4000);

            NetworkStream stream = client.GetStream();

            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            writer.AutoFlush = true;

            writer.WriteLine(name);

            while (true)
            {
                string message = reader.ReadLine()!;
                Console.WriteLine("Message sent from server: {0}", message);
            }
        }
    }
}
