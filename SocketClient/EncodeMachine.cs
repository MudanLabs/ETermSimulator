using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketClient;


namespace SocketClient
{
    public class EncodeMachine
    {
        //1.登陆编码
        //2.指令登陆
        #region 登陆编码
        public byte[] LogEncode(string userName,string password,string strworkport,string strport ) 
        {
            string fixstr = "3847010";
            byte[] LogSend = new byte[162];
            byte[] user = System.Text.Encoding.Default.GetBytes(userName);
            byte[] pass = System.Text.Encoding.Default.GetBytes(password);
            byte[] port = System.Text.Encoding.Default.GetBytes(strport);//这个后
            byte[] workport = System.Text.Encoding.Default.GetBytes(strworkport);//这个先
            byte[] fixedstr = System.Text.Encoding.Default.GetBytes(fixstr);//
            for (int i = 0; i < 162; i++) 
            {
                if (i == 0) LogSend[i] = 1;
                if (i == 1) LogSend[i] = 162;
                if (i > 1 && i < 2 + user.Length) //填充密码
                {
                    LogSend[i] = user[i - 2];
                }
                if (i >= user.Length + 2 && i < 2 + user.Length + 8) //填充8个0
                {
                    LogSend[i] = 0;
                }
                if (i >= 10 + user.Length && i < 10 + user.Length + pass.Length)//填充密码 
                {
                    LogSend[i] = pass[i - user.Length - 10];
                }
                if (i >= 10 + user.Length + pass.Length && i < 35 + user.Length + pass.Length) //填充后面的25个0
                {
                    LogSend[i] = 0;
                }
                if (i >= 35 + user.Length + pass.Length && i < 35 + user.Length + pass.Length + workport.Length)//填充端口号前面的 
                {
                    LogSend[i] = workport[i - 35 - user.Length - pass.Length];
                }
                if (i >= 35 + user.Length + pass.Length + workport.Length && i < 35 + user.Length + pass.Length + workport.Length + port.Length) //填充端口号
                {
                    LogSend[i] = port[i - 35 - user.Length - pass.Length - workport.Length];
                }
                if (i >= 35 + user.Length + pass.Length + workport.Length + port.Length && i < 37 + user.Length + pass.Length + workport.Length + port.Length) //后面添加两个空格
                {
                    LogSend[i] = 32;
                }
                if (i >= 37 + user.Length + pass.Length + workport.Length + port.Length && i < fixedstr.Length + 37 + user.Length + pass.Length + workport.Length + port.Length)//添加3847010 
                {
                    LogSend[i] = fixedstr[i - 37 - user.Length - pass.Length - workport.Length - port.Length];
                }//还要填写6个0
                if (i >= fixedstr.Length + 37 + user.Length + pass.Length + workport.Length + port.Length && i < fixedstr.Length + 44 + user.Length + pass.Length + workport.Length + port.Length)
                {
                    if (i == fixedstr.Length + 37 + user.Length + pass.Length + workport.Length + port.Length) LogSend[i] = 0;
                    else LogSend[i] = 48;
                }
                if (i >= fixedstr.Length + 44 + user.Length + pass.Length + workport.Length + port.Length && i < 162) 
                {
                    LogSend[i] = 0;
                }
            }
            return LogSend;
        }
        #endregion

        #region 发送指令编码
        //string userid//表示服务器返回回来的客户端应该：比如51,81//strinstruct指的是命令
        public byte[] MakeInstruct(byte[] userid, string strinstruct) 
        {
            byte[] order = MiddleToCode(strinstruct);
            int leng = order.Length + 21;
            byte[] instruct = new byte[leng];
            instruct = StartAndEnd(instruct, leng, userid);
            for (int i = 19; i < leng-2; i++)
            {
                instruct[i] = order[i - 19];
            }
            return instruct;
        }
        #region 命令的开头和结尾
        private byte[] StartAndEnd(byte[] instruct,int length,byte[] userid) 
        {
            instruct[0] = 1;
            instruct[1] = 0;
            instruct[2] = 0;
            instruct[3] = (byte)length;
            instruct[4] = 0;
            instruct[5] = 0;
            instruct[6] = 0;
            instruct[7] = 1;
            instruct[8] = userid[0];
            instruct[9] = userid[1];
            instruct[10] = 112;
            instruct[11] = 2;
            instruct[12] = 27;
            instruct[13] = 11;
            instruct[14] = 44;//光标的位置，随便写吧
            instruct[15] = 32;//同上
            instruct[16] = 0;
            instruct[17] = 15;
            instruct[18] = 30;//表示内容的开始
            instruct[length - 2] = 32;
            instruct[length - 1] = 3;
            return instruct;
        }
        #endregion

        #region 汉字编码
        public static byte[] ChineseToCode(string name)
        {
            string pinyin = ChineseToPinyin.ToPinYin(name);
            byte[] pinbyte = Encoding.ASCII.GetBytes(pinyin);
            byte[] middle = Encoding.GetEncoding("gb2312").GetBytes(name);
            byte[] chinesebyte = new byte[middle.Length];
            byte[] chinesecode = new byte[pinbyte.Length + middle.Length + 4];
            for (int i = 0; i < middle.Length; i++)
            {
                chinesebyte[i] = (byte)(middle[i] - 128);
            }
            for (int i = 0; i < pinbyte.Length + 2; i++)
            {
                if (i == pinbyte.Length) chinesecode[i] = 27;
                else if (i == pinbyte.Length + 1) chinesecode[i] = 14;
                else
                    chinesecode[i] = pinbyte[i];
            }
            for (int j = pinbyte.Length + 2; j < pinbyte.Length + chinesebyte.Length + 4; j++)
            {
                if (j == pinbyte.Length + chinesebyte.Length + 2) chinesecode[j] = 27;
                else if (j == pinbyte.Length + chinesebyte.Length + 3) chinesecode[j] = 15;
                else
                    chinesecode[j] = chinesebyte[j - pinbyte.Length - 2];
            }
            return chinesecode;
        }
        #endregion

        #region 中间部分的命令编码(其中是可以有字母加汉字的)
        public byte[] MiddleToCode(string str) 
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(?<UserName>[\u4E00-\u9FA5]{2,9})");
            var matches = regex.Matches(str);
            List<string> strChinese = new List<string>();
            List<int> location = new List<int>();//起始位置
            List<string> strPin = new List<string>(); //表示汉字转拼音
            foreach(System.Text.RegularExpressions.Match item in matches)
            {
                string fir = item.Groups["UserName"].Value;
                int first = str.IndexOf(fir);
                location.Add(first);//记录这个名字的地址
                int strlenth = fir.Length;
                location.Add(first+strlenth);
                strChinese.Add(fir);
            }//找出有汉字的部分

            List<string> subStr = new List<string>();
            if (location.Count() > 0)//表示有汉字
            {
                string sec = str.Substring(0, location[0]);
                subStr.Add(sec);
                for (int i = 0; i < location.Count; i ++)
                {
                    if (i + 1 >= location.Count)
                    {
                        if (location[i]== str.Length) break;
                        else 
                        {
                            string stt = str.Substring(location[i]);
                            subStr.Add(stt);
                        }
                    }
                    else 
                    {
                        string stt = str.Substring(location[i], location[i + 1] - location[i]);//
                        subStr.Add(stt);
                    }
                }
            }
            else //表示没有汉字
            {
                subStr.Add(str);
            }
            int m=0;
            List<byte[]> list = new List<byte[]>();
            List<int> leng = new List<int>();
            for (int i = 0; i < subStr.Count(); i++) 
            {
                if (strChinese.Count() > 0)//表示有汉字
                {
                    if (m >= strChinese.Count) m--;
                    if (subStr[i] != strChinese[m])
                    {
                        byte[] first = Encoding.ASCII.GetBytes(subStr[i]);
                        int count = first.Length;
                        list.Add(first);
                        leng.Add(count);
                    }
                    else //汉字
                    {
                        byte[] first = ChineseToCode(strChinese[m]);
                        m++;
                        int count = first.Length;
                        list.Add(first);
                        leng.Add(count);
                    }
                }
                else //表示没有汉字
                {
                    byte[] first = Encoding.ASCII.GetBytes(subStr[i]);
                    int count = first.Length;
                    list.Add(first);
                    leng.Add(count);
                }
                
            }//进行字符串转字节码
            
            int allleng = 0;
            foreach(var item in leng)
            {
                allleng += item;
            }//算出最终要申请的存储空间

            byte[] rtn=new byte[allleng];
            int t = 0;
            foreach(var item in list)
            {
                for (int i = 0; i < item.Length;i++ )
                {
                    rtn[t] = item[i];
                    t++;
                }
            }
            return rtn;
        }
        #endregion
        #endregion
    }
}
