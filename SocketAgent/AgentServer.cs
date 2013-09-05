using System;
using System.Configuration;
using System.Net.Sockets;

namespace MDLabs.EtermSimulator.SocketAgent
{
    public class AgentServer
    {
        private TcpClient _TcpClient;
        private TcpClient agentClient = GetTcpClient();
        private byte[] receiveBuffer;
        private int _CollectionID;

        public AgentServer(TcpClient tcpClient, int collectionID)
        {
            _CollectionID = collectionID;
            _TcpClient = tcpClient;
            receiveBuffer = new byte[_TcpClient.ReceiveBufferSize];
            _TcpClient.GetStream().BeginRead(receiveBuffer, 0, _TcpClient.ReceiveBufferSize, Receive, null);//开始读取客户端发送的数据

        }
        private void Receive(IAsyncResult ir)
        {
            int i = _TcpClient.GetStream().EndRead(ir);//接收到客户端发送的数据
            if (i > 0)
            {
                string str = System.Text.ASCIIEncoding.ASCII.GetString(receiveBuffer, 0, i);
                Output(_CollectionID, "CA", str);
                Output(_CollectionID, "CA", receiveBuffer, i);
                agentClient.GetStream().BeginWrite(receiveBuffer, 0, i, Send, null);//将客户端发送的数据 开始转发给 服务器
                Output(_CollectionID, "AS", "BEGIN");
            }
        }
        private void Send(IAsyncResult ir)
        {
            Output(_CollectionID, "AS", "END");
            agentClient.GetStream().EndWrite(ir);//转发结束
            agentClient.GetStream().BeginRead(receiveBuffer, 0, agentClient.ReceiveBufferSize, Receive2, null);//读取服务器返回的数据
        }
        private void Receive2(IAsyncResult ir)
        {
            int i = agentClient.GetStream().EndRead(ir);//读取服务器返回的数据
            if (i > 0)
            {
                string str = System.Text.ASCIIEncoding.ASCII.GetString(receiveBuffer, 0, i);
                Output(_CollectionID, "SA", str);
                Output(_CollectionID, "SA", receiveBuffer, i);
                _TcpClient.GetStream().BeginWrite(receiveBuffer, 0, i, Send2, null);//转发给客户端
                Output(_CollectionID, "AC", "BEGIN");
            }
        }
        private void Send2(IAsyncResult ir)
        {
            Output(_CollectionID, "AC", "END");
            _TcpClient.GetStream().EndWrite(ir);//转发结束
            _TcpClient.GetStream().BeginRead(receiveBuffer, 0, _TcpClient.ReceiveBufferSize, Receive, null);//读取客户端发送的数据
        }

        #region 获取监听及服务器地址端口
        private static TcpClient GetTcpClient()
        {
            string ip = ConfigurationManager.AppSettings["EtermServerIP"];
            int port = int.Parse(ConfigurationManager.AppSettings["EtermServerPort"]);
            return new TcpClient(ip, port);
        }
        #endregion

        #region 日志
        private static void Output(int id, string type, string str)
        {
            string log = string.Format("{0}|{1}|{2}", id, type, str);
            Console.WriteLine(log);
            //System.IO.File.AppendAllText("Log.txt", log + System.Environment.NewLine);
        }
        private static void Output(int id, string type, byte[] data, int length)
        {
            string log = id.ToString() + "|" + type + "|";

            Console.Write(id.ToString() + "|" + type + "|");
            for (int i = 0; i < length; i++)
            {
                log += string.Format("{0} ", data[i]);
            }
            Console.WriteLine(log);
            //System.IO.File.AppendAllText("Log.txt", log + System.Environment.NewLine);
        }
        #endregion
    }
}
