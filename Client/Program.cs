using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new TcpClient("localhost", 5678);
            using (var writer = new StreamWriter(client.GetStream()))
            {
                writer.WriteLine(
                    "{\"name\":\"JobName\",\"url\":\"JobUrl\",\"build\":{\"number\":1,\"phase\":\"COMPLETED\",\"status\":\"SUCCESS\"}}");

            }
            client.Close();

        }
    }
}
