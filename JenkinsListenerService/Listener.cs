using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace JenkinsListenerService
{
    public class Listener : IDisposable
    {
        private TcpListener _tcpListener;

        public void Start()
        {
            _tcpListener = new TcpListener(IPAddress.Any, Configuration.ListenerPort);
            Trace.TraceInformation("Starting on {0}:{1}", IPAddress.Any, Configuration.ListenerPort);
            Debug.Write(string.Format("Starting on {0}:{1}", IPAddress.Any, Configuration.ListenerPort));

            _tcpListener.Start();
            Trace.TraceInformation("Started");

            _tcpListener.BeginAcceptTcpClient(AcceptClient, _tcpListener);
        }

        public void Stop()
        {
            _tcpListener.Stop();
            _tcpListener = null;
        }

        protected void AcceptClient(IAsyncResult result)
        {
            var listener = (TcpListener) result.AsyncState;

            var client = listener.EndAcceptTcpClient(result);

            ProcessClientConnection(client);

            // deliberately done after to ensure synchronous operation and avoid script run collisions
            listener.BeginAcceptTcpClient(AcceptClient, listener);

        }

        protected void ProcessClientConnection(TcpClient tcpClient)
        {
            var clientStream = tcpClient.GetStream();

            var message = new byte[4096];

            while (true)
            {
                var bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }

                //message has successfully been received
                var payload = new ASCIIEncoding().GetString(message, 0, bytesRead);
                
                try
                {
                    var notification = Notification.New(payload);
                    notification.Execute();
                }
                catch (Exception ex)
                {
                    Trace.TraceError("{0} with payload {1}", ex.Message, payload);
                }
            }

            tcpClient.Close();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
