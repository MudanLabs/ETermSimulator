using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketClient;
using System.Net.Sockets;
using System.Configuration;
using System.Net;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient tcp = GetTcpService();
            
            byte[] receiveBuffer=new byte[tcp.ReceiveBufferSize];
            Loging log = new Loging();
            receiveBuffer = log.Loing();
            tcp.GetStream().Write(receiveBuffer, 0, receiveBuffer.Length);

            //receiveBuffer = log.TestA();
            //tcp.GetStream().Write(receiveBuffer, 0, receiveBuffer.Length);
            //receiveBuffer = log.TestB();
            //tcp.GetStream().Write(receiveBuffer, 0, receiveBuffer.Length);
            
            // 我都把这种事情看成常态了，看来这样还真不行，女人，真得有自己的一份职业，有自己的一席之地才行啊，要不，连个家都没有，在家里还有受气，这样可真不行

            //EncodeMachine machine = new EncodeMachine();
            //byte[] userid = { 40, 81 };
            //string strinstruct = "avshacgo";
            //byte[] send = machine.MakeInstruct(userid, strinstruct);
            //tcp.GetStream().Write(send, 0, send.Length);
            while (true) 
            {
                //EncodeMachine machine = new EncodeMachine();
                //byte[] userid = { 40, 81 };
                //string strinstruct = "avshacgo";//avshacgo
                //byte[] send = machine.MakeInstruct(userid, strinstruct);
                //tcp.GetStream().Write(send, 0, send.Length);
                //这个地方可以弄一个登陆信息发送过去
               // int j = tcp.GetStream().Read(receiveBuffer, 0, );
                byte[] receive = new byte[tcp.ReceiveBufferSize];
                int j = tcp.GetStream().Read(receive, 0, tcp.ReceiveBufferSize);
                //这个地方怎么（这个地方怎么解析服务器返回回来的数据呢?）
                //有返回信息,读取之后返回信息
                if (j > 0)
                {
                    for (int i = 0; i < j; i++) 
                    {
                        Console.Write(receive[i]);
                    }
                }
            }
            
        }
        //配置一个可以监听的客户端
        private static TcpClient GetTcpService()
        {
            string ip = "192.168.80.21";
            int port = 350;
            return new TcpClient(ip, port);
        }
    }
}
