using System.Configuration;
using System.Net;
using System.Net.Sockets;
using SocketAgent;

namespace MDLabs.EtermSimulator.SocketAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener agentServer = GetTcpListener();
            agentServer.Start();
            int collectionId = 0;
            while (true)
            {
                new AgentServer2(agentServer.AcceptTcpClient(), collectionId);
                collectionId++;
            }
        }
        private static TcpListener GetTcpListener()
        {
            string ip = ConfigurationManager.AppSettings["ListenerIP"];
            int port = int.Parse(ConfigurationManager.AppSettings["ListenerPort"]);
            if (string.IsNullOrWhiteSpace(ip))
            {
                return new TcpListener(IPAddress.Any, port);
            }
            else
            {
                return new TcpListener(IPAddress.Parse(ip), port);
            }
        }

        
    }
}
