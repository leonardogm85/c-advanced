using System.Net;
using System.Net.Sockets;
using System.Text;

namespace C.Udp.Client
{
    /// <summary>
    /// <para>
    ///     Crie a mesma aplicação do Exercício 1, mas utilizando comunicação em rede através do
    ///     protocolo UDP/IP.
    /// </para>
    /// </summary>
    internal class Program
    {
        internal static void Main()
        {
            Console.WriteLine("Client started.");

            Console.Write("Enter a name: ");
            string name = Console.ReadLine()!;

            UdpClient client = new UdpClient();

            byte[] bName = Encoding.Default.GetBytes(name);
            client.Send(bName, bName.Length, "localhost", 5000);

            while (true)
            {
                IPEndPoint endPoint = new IPEndPoint(0, 0);
                byte[] bMessage = client.Receive(ref endPoint);
                string message = Encoding.Default.GetString(bMessage);

                Console.WriteLine("Message sent from server: {0}", message);
            }
        }
    }
}
