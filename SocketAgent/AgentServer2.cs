using System;
using System.Configuration;
using System.Net.Sockets;

namespace MDLabs.EtermSimulator.SocketAgent
{
    public class AgentServer2
    {
        private TcpClient _TcpClient;
        private TcpClient agentClient = GetTcpClient();
        private byte[] receiveBuffer;
        private int _CollectionID;

        public AgentServer2(TcpClient tcpClient, int collectionID)
        {
            _CollectionID = collectionID;
            _TcpClient = tcpClient;
            receiveBuffer = new byte[_TcpClient.ReceiveBufferSize];
            System.Threading.Thread t = new System.Threading.Thread(StartAgentClient);
            t.Start();
            while (true)
            {
                int i = _TcpClient.GetStream().Read(receiveBuffer, 0, _TcpClient.ReceiveBufferSize);//开始读取客户端发送的数据
                if (i > 0)
                {
                    //string str = System.Text.ASCIIEncoding.GetEncoding("gb2312").GetString(receiveBuffer, 0, i);
                    string str = System.Text.ASCIIEncoding.UTF8.GetString(receiveBuffer, 0, i);
                    agentClient.GetStream().Write(receiveBuffer, 0, i);//将客户端发送的数据 开始转发给 服务器
                    Output(_CollectionID, "客户端gb2312", str);
                    Output(_CollectionID, "客户端", receiveBuffer, i);
                }
            }
        }
        private void StartAgentClient()
        {
            while (true)
            {
                int j = agentClient.GetStream().Read(receiveBuffer, 0, agentClient.ReceiveBufferSize);//读取服务器返回的数据
                if (j > 0)
                {
                    //string str2 = System.Text.ASCIIEncoding.GetEncoding("gb2312").GetString(receiveBuffer, 0, j);
                    string str2 = System.Text.ASCIIEncoding.UTF8.GetString(receiveBuffer, 0, j);
                    _TcpClient.GetStream().Write(receiveBuffer, 0, j);//转发给客户端
                    Output(_CollectionID, "服务器gb2312", str2);
                    Output(_CollectionID, "服务器", receiveBuffer, j);
                }
            }
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
            System.IO.File.AppendAllText("Log.txt", log + System.Environment.NewLine);
        }
        #endregion
    }
}
