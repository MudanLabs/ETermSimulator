using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient
{
    public class AnalysisMachine
    {
        #region 解析登陆数据
        public void LoginAnalysis(byte[] logReceive) 
        {
            if (logReceive.Length > 8)//这个表示要么是命令，要么是服务器返回的是正确的数据
            {
                if (logReceive[5] == logReceive[6]&&logReceive[6]==logReceive[7]&&logReceive[7]==0) //表示登陆服务器首次返回
                {
                    var userid=LogService(logReceive);//登陆的账号
                }
                if(logReceive[4]==logReceive[5]&&logReceive[5]==logReceive[6]&&logReceive[7]==1)//这个表示发送的指令和命令
                {
                    OrderService(logReceive);
                }
                //其他错误的还没有找到规律（错误的情况）
            }
            else //这个表示是测试返回的字节
            {
                
            }
        }
        #endregion

        #region 解析登陆命令
        public byte LogService(byte [] receiveService) 
        {//取登陆号：这个地方需要研究一下(里面可能会包含权限问题)
            return receiveService[8]; //表示需要返回的账号标志           
        }
        #endregion
        //下午的目标就是：把这个服务器上返回回来的数据进行解析，解析成字符串，然后输出出来。能把一般的命令解析出来，这就算完成了。
        #region 表示解析普通命令
        public string OrderService(byte [] receiveService) 
        {
            string rtn = "";
            Byte[] chineseCode=new byte[256];
            Byte[] code=new byte[receiveService.Length];//该地方申请的长度与输出是有关系的，主意
            bool value = false;
            int m=0;//相当于chineseCode的指针
            int n=0;//相当于code的指针
            for (int i = 19; i < receiveService.Length; i++) 
            {
                //出现的问题就是：1.遇到特殊标记的，如何处理
                //2.遇到汉字的情况如何处理？
                if (receiveService[i] < 32) 
                {
                    //表示特殊字符
                    if (receiveService[i] == 13) 
                    {
                        rtn+=TransToCode(code)+"~";
                        code=new byte[1024];
                        n = 0;
                    }
                    if (receiveService[i] == 28) 
                    {
                        //表示红色标记开始
                        rtn += "@";
                    }
                    if (receiveService[i] == 29) 
                    {
                        //表示红色标记结束
                        rtn+="@";
                    }
                    if (receiveService[i] == 14 && receiveService[i-1] == 27) 
                    {
                        value = true;
                    }
                    if (receiveService[i] == 15 && receiveService[i - 1] == 27) 
                    {
                        value = false;
                        rtn += TransToChinese(chineseCode);
                    }
                    if (receiveService[i] == 30) 
                    {
                        //表示是【光标的（三角）开始】
                        rtn += "△";
                    }
                    if (receiveService[i] == 9 && receiveService[i - 1] == 27) 
                    {
                        //表示是个结束表示【四方小点儿】
                        rtn += "□";
                    }
                    if (receiveService[i] == 11 && receiveService[i - 1] == 27&&receiveService[i+3]==15&&receiveService[i+4]==3) 
                    {
                        break;
                    }
                    if (receiveService[i] == 27) 
                    {
                        continue;
                    }
                    if (receiveService[i] == 3) 
                    {
                        break;
                    }
                }
                else if (receiveService[i] >= 32) 
                {
                    if (receiveService[i] == 43)
                    {
                        rtn += "当前的内容没有显示完";
                    }
                    if (receiveService[i] == 98 && receiveService[i - 1] == 27) 
                    {
                        continue;//这个是显示不出来内容的。
                    }
                    if (value == true) 
                    {
                        chineseCode[m] = receiveService[i];
                        m++;
                    }else if(value==false)
                    {
                        code[n]=receiveService[i];
                    }
                }

            }
            return rtn;
        }
        #endregion
        #region 解析错误指令
        public byte[] FaultService(byte[] receiveService) 
        {
            return null;
        }
        #endregion
        #region 根据gb2312转汉字
        private string TransToChinese(byte[] toChinese)
        {
            string str = "";
            byte[] arry = new byte[2];
            for (int i = 0; i < toChinese.Length; i++)
            {
                toChinese[i] = (byte)(toChinese[i] + 128);
            }
            for (int i = 0; i < toChinese.Length; i += 2)
            {
                arry[0] = toChinese[i];
                arry[1] = toChinese[i + 1];
                str += System.Text.Encoding.GetEncoding("gb2312").GetString(arry);
            }
            return str;
        }

        private string TransToCode(byte[] toCode) 
        {
            string str = System.Text.Encoding.Default.GetString(toCode);
            return str;
        }
        #endregion
    }
}
