using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketAgent
{
    public class NowClient
    {
        //这边与服务器连接，目前先做客户端来进行使用   步骤：（建立流式套接字，connect()将套接字与远程主机连接，send/receive()数据提交,closesocket()）
        public Socket client { get; set; }
        public void Connect() 
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//建立流式套接字
            IPAddress address = IPAddress.Parse("192.168.80.10");
            int port = 5009;
            IPEndPoint ipend = new IPEndPoint(address, port);
            try
            {
                client.Connect(ipend);
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
            //这个是没有发送上去还是个怎么回事呢？
            //一般的通信都是怎么互通的呢？（其中的问题是：只要我配对了端口号和ip地址的话，那么我想发送信息就可以发送了还是个怎么回事？用户名和密码什么的？
            //就算是使用代理了，怎么查看算是代理成功了呢？）
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
