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

        string strworkport = "60a44cac46ab";//这个地方时登陆人的mac地址（其中的大写一律转换成小写，中间的分隔符去掉）
        string strport = "192.168.80.21";//这个地方可能会不一样（登陆人的ip地址）

        public bool Connection(string etermServerIP,int etermServerPort,string etermUserName,string etermPassword,string jobNo,string jobNoPassword)
        {
            _TcpClient.Connect(etermServerIP, etermServerPort);
            return true;
            //_TcpClient.GetStream().Write();
        }
        public byte[] Loing() //string userName,string password(这个地方暂时先不传)
        {
            string strworkport = "60a44cac46ab";//这个地方时登陆人的mac地址（其中的大写一律转换成小写，中间的分隔符去掉）
            string strport = "192.168.80.21";//这个地方可能会不一样（登陆人的ip地址）
            EncodeMachine machine = new EncodeMachine();
            byte[] Send = machine.LogEncode(_EtermUserName, _EtermPassword, strworkport, strport);
            return Send;
        }
    }
}
