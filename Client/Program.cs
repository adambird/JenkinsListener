using System.IO;
using System.Net.Sockets;

namespace Client
{
    static class Program
    {
        static void Main()
        {
            var client = new TcpClient("localhost", 5678);
            using (var writer = new StreamWriter(client.GetStream()))
            {
                writer.WriteLine("{\"name\":\"JobName\",\"url\":\"JobUrl\",\"build\":{\"number\":1,\"phase\":\"COMPLETED\",\"status\":\"SUCCESS\"}}");
            }
            client.Close();

        }
    }
}
