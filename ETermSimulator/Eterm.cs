using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace ETermSimulator
{
    public class Eterm
    {
        private string _ClientIPAddress { get; set; }
        private string _ClientMacAddress { get; set; }
        private string _EtermVersion { get; set; }
        private string _EtermUserName { get; set; }
        private string _EtermPassword { get; set; }
        private string _JobNo{ get; set; }
        private string _JobNoPassword { get; set; }
        private TcpClient _TcpClient;
        private byte[] _Session=new byte[2];
        
        string strworkport = "60a44cac46ab";//这个地方时登陆人的mac地址（其中的大写一律转换成小写，中间的分隔符去掉）
        string strport = "192.168.80.21";//这个地方可能会不一样（登陆人的ip地址）
        
        
        public bool Connection(string etermServerIP,int etermServerPort)
        {
            _TcpClient = new TcpClient();
            _TcpClient.Connect(etermServerIP, etermServerPort);
            if (_TcpClient.Connected)
            {
                Console.Write("Eterm User Name:");
                _EtermUserName = Console.ReadLine();
                Console.Write("Eterm Password:");
                _EtermPassword = Console.ReadLine();
                Send(Loing());
                byte[] receiveBuffer = new byte[_TcpClient.ReceiveBufferSize];
                int receiveLength =_TcpClient.GetStream().Read(receiveBuffer, 0, receiveBuffer.Length);
                if (receiveLength > 0)
                {
                    if (receiveBuffer[2] == 1)
                    {
                        Console.WriteLine("登陆成功！");
                        Array.Copy(receiveBuffer,8,_Session,0,2);
                        
                    }
                    else
                    {
                        Console.WriteLine("登录失败!");
                        return false;
                    }

                }
                System.Threading.Thread t = new System.Threading.Thread(Receive);
                t.Start();

                EncodeMachine machine = new EncodeMachine();
                Send(machine.TestA());
                Send(machine.TestB());

                while (true)
                {
                    string str=Console.ReadLine();
                    Send(machine.MakeInstruct(_Session, str));
                }
            }
            return true;
            //_TcpClient.GetStream().Write();
        }
        public void Receive()
        {
            byte[] receiveBuffer = new byte[_TcpClient.ReceiveBufferSize];
            while (true)
            {
                int receiveLength = _TcpClient.GetStream().Read(receiveBuffer, 0, receiveBuffer.Length);
                if (receiveLength > 0)
                {
                    Console.Write("接收:");
                    for (int i = 0; i < receiveLength; i++)
                    {
                        Console.Write("{0},", receiveBuffer[i]);
                    }
                    Console.WriteLine();
                    var resp = new BaseResp(receiveBuffer);
                    Console.Write(resp.Text);
                }
            }
        }
        private void Send(byte[] bytes)
        {
            _TcpClient.GetStream().Write(bytes, 0, bytes.Length);
        }
        public byte[] Loing() //string userName,string password(这个地方暂时先不传)
        {
            string strworkport = "60a44cac46ab";//这个地方时登陆人的mac地址（其中的大写一律转换成小写，中间的分隔符去掉）
            string strport = "192.168.80.21";//这个地方可能会不一样（登陆人的ip地址）
            EncodeMachine machine = new EncodeMachine();
            byte[] Send = machine.LogEncode(_EtermUserName, _EtermPassword, strworkport, strport, "3847010");
            return Send;
        }
    }
}
