using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketAgent;

namespace SocketAgent
{
    public class Loging
    {
        public string logUserName = "o77777777";
        public string Password = "password";
        string strworkport = "60a44cac46ab";//这个地方时登陆人的mac地址（其中的大写一律转换成小写，中间的分隔符去掉）
        string strport = "192.168.80.21";//这个地方可能会不一样（登陆人的ip地址）
        //用户名，密码应该是传递过来的
        public byte[] Loing() //string userName,string password(这个地方暂时先不传)
        {
            EncodeMachine machine=new EncodeMachine ();
            byte[] Send = machine.LogEncode(logUserName, Password, strworkport, strport);
            return Send;
        }
        #region 第一组测试(猜测估计没错的话，应该是测试发送数据能不能正常接收，类似于三次握手)
        public byte[] TestA() 
        {
            byte[] firstTest = { 1, 254, 0, 17, 20, 16, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            return firstTest;
        }
        #endregion
        #region 第二组测试
        public byte[] TestB() 
        {
            byte[] secondTest = { 1, 254, 0, 17, 20, 16, 0, 2, 12, 0, 0, 0, 0, 0, 0, 0, 0, 1, 254, 0, 17, 20, 16, 0, 2, 41, 0, 0, 0, 0, 0, 0, 0, 0, 1, 254, 0, 17, 20, 16, 0, 2, 32, 0, 0, 0, 0, 0, 0, 0, 0 };
            return secondTest;
        }
        #endregion
    }
}
