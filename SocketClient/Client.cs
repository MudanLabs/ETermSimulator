using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketClient
{
    public class Client
    {
        public Socket client { get; set; }
        public void Connect() 
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//建立流式套接字
            IPAddress address = IPAddress.Parse("192.168.80.21");
            int port = 350;
            IPEndPoint ipend = new IPEndPoint(address, port);
            try
            {
                client.Connect(ipend);
                SendOrReceive();
                Console.WriteLine("连接成功！");
            }
            catch (SocketException EX) 
            {
                Console.WriteLine("未连接上");
            }   
            
        }

        public void SendOrReceive()
        {
            string wantsend = "avshacgo";
            var aa=Encoding.ASCII.GetBytes(wantsend);
            client.Send(aa,aa.Length,SocketFlags.None);
            Console.WriteLine("发送信息");//发送出去的东西不知道是否已经发上上去，并且获取不到返回回来的信息。
            string strdata="";
            
            while(true)
            {
                byte[] data = new Byte[2048];
                int recv = client.Receive(data);//这个返回的是长度
                if(recv<=0)
                {
                    Console.WriteLine("失败");
                    break;
                    strdata = Encoding.ASCII.GetString(data, 0, recv); //这个地方获取不到信息
                    Console.WriteLine(strdata);
                }
                
            }
            //收不到信息（是那个环节出现了问题）
        }
    }
}
