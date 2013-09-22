using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApp;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //==============测试汉字转成字节==================
            //var bc = CTOC();
            //byte[] bb = { 73,79,58,35,53,88,71,120,76,108,70,120,84,36,35,40};
            //var we = TransToChinese(bb);
            //var cc = we;
            ////string str = "liuyanjia刘艳佳2222333sss李四sss王五";
            //============这个是组装字节的==================================
            //string test2 = "1 0 1 178 0 0 0 1 39 81 112 2 27 11 40 32 15 27 77 27 14 73 79 58 35 53 88 71 120 76 108 70 120 84 36 49 40 27 15 58 32 32 32 13 32 32 32 32 32 57 27 14 84 66 27 15 50 50 27 14 72 85 27 15 32 27 14 80 71 70 90 72 85 27 15 58 13 32 32 32 32 32 32 32 32 27 14 87 110 53 77 70 120 78 66 27 15 32 50 53 67 40 55 55 70 41 44 32 27 14 87 110 56 95 70 120 78 66 27 15 32 50 57 67 40 56 52 70 41 44 32 27 14 85 115 83 106 27 15 44 32 27 14 54 43 55 103 27 15 52 45 53 27 14 60 54 27 15 32 13 32 32 32 32 32 32 57 27 14 84 66 27 15 50 51 27 14 72 85 27 15 32 27 14 80 71 70 90 82 59 27 15 58 13 32 32 32 32 32 32 32 32 27 14 87 110 53 77 70 120 78 66 27 15 32 50 52 67 40 55 53 70 41 44 32 27 14 87 110 56 95 70 120 78 66 27 15 32 51 48 67 40 56 54 70 41 44 32 27 14 54 96 84 70 27 15 44 32 27 14 54 43 68 79 55 103 27 15 52 45 53 27 14 60 54 27 15 32 13 32 32 32 32 32 32 32 32 57 27 14 84 66 27 15 50 52 27 14 72 85 27 15 32 27 14 80 71 70 90 54 126 27 15 58 13 32 32 32 32 32 32 32 32 27 14 87 110 53 77 70 120 78 66 27 15 32 50 49 67 40 55 48 70 41 44 32 27 14 87 110 56 95 70 120 78 66 27 15 32 51 48 67 40 56 54 70 41 44 32 27 14 85 115 83 106 87 42 86 80 83 106 27 15 44 32 27 14 68 79 55 103 27 15 52 45 53 27 14 60 54 87 42 54 43 39 35 55 103 27 15 52 45 53 27 14 60 54 27 15 32 13 32 32 32 32 32 32 27 98 13 30 27 98 3";
            //var test2a = GetNewStr(test2);
            //var sssss = test2a;
            //============================================================
            byte[] test1 = //这个是avshacgo
            {
                1 ,0 ,3, 103, 0, 0, 0, 1, 51, 81, 112, 2, 27, 11, 32, 32, 15 ,27 ,77, 32, 50, 49, 83, 69, 80, 40, 83, 65, 84,
                41, 32 ,83 ,72, 65, 67 ,71 ,79 ,32 ,13 ,49, 45, 32 ,42 ,77, 85, 57, 51, 50 ,51 ,32 ,32, 83 ,72 ,65, 67 ,71 ,79, 
                32 ,49, 55, 53 ,53, 32 ,32 ,32,49, 57, 52 ,53, 32, 32 ,32 ,55 ,51 ,55 ,32 ,48, 94, 68, 32, 32, 69, 32, 32, 28,
                65, 83, 35, 29, 70 ,76 ,32 ,80, 81, 32, 65 ,76, 32, 89, 76, 32, 66, 81 ,32 ,77 ,81, 32, 69, 81 ,32, 72, 81, 32,
                75 ,81 ,32 ,76 ,81 ,42 ,50 ,32 ,32 ,32 ,70 ,77 ,57 ,51 ,50, 51, 32, 32 ,83, 72, 65, 67, 71, 79, 32, 49, 55, 53,
                53 ,32 ,32 ,32 ,49 ,57 ,52 ,53 ,32 ,32 ,32, 55, 51, 55, 32, 48, 94, 68, 32, 32, 69, 32, 32, 28, 65, 83, 35, 29,
                70 ,76, 32, 80, 81 ,32 ,89 ,76 ,32 ,66 ,81 ,32 ,77 ,81 ,32 ,69 ,81 ,32 ,72 ,81 ,32 ,75, 81 ,32 ,76 ,81 ,32 ,78,
                81 ,42 ,51 ,32 ,32 ,32 ,67 ,90 ,51 ,53 ,57 ,56 ,32 ,32 ,83 ,72 ,65 ,67 ,71 ,79 ,32 ,50 ,48 ,51 ,48 ,32 ,32 ,32,
                50 ,50 ,50 ,48 ,32 ,32 ,32 ,55 ,51 ,56 ,32 ,48 ,94 ,67 ,32 ,32 ,69 ,32 ,32 ,28 ,65 ,83 ,35 ,29 ,70 ,50 ,32 ,80 ,
                81 ,32 ,87 ,76 ,32 ,90 ,81 ,32 ,89 ,76 ,32 ,84 ,81 ,32 ,75 ,81 ,32 ,72 ,81 ,32 ,77 ,81 ,32 ,71 ,81 ,42 ,52 ,32 ,
                32 ,32 ,70 ,77 ,56 ,50 ,50 ,32 ,32 ,32 ,80 ,86 ,71 ,67 ,71 ,79 ,32 ,50 ,49 ,52 ,48 ,32 ,32 ,32 ,50 ,51 ,51 ,48 ,
                32 ,32 ,32 ,55 ,51 ,56 ,32 ,48 ,94, 32, 32 ,32 ,69 ,32 ,32 ,32 ,68 ,83 ,35 ,32 ,74 ,50 ,32 ,67 ,81 ,32 ,68 ,81 ,
                32 ,73, 81, 32, 89, 76 ,32 ,66 ,81 ,32 ,77 ,81 ,32 ,69 ,81 ,32 ,72 ,81 ,32 ,75 ,81 ,42 ,53 ,32 ,32 ,42 ,77 ,85 ,
                51 ,51 ,50 ,49 ,32 ,32 ,80 ,86 ,71 ,67 ,71 ,79 ,32 ,50 ,50 ,49 ,48 ,32 ,32 ,32 ,50 ,51, 53, 53, 32 ,32 ,32 ,55 ,
                51 ,56 ,32 ,48 ,94 ,67 ,32 ,32 ,69 ,32 ,32 ,28 ,65 ,83 ,35 ,29 ,89 ,65 ,32 ,66 ,65 ,32 ,77 ,65 ,32 ,69 ,65 ,32 ,
                72 ,81 ,32 ,75 ,81 ,32 ,76 ,81 ,32 ,78 ,81 ,32 ,82 ,81 ,32 ,83 ,81 ,42 ,54 ,32 ,32, 32, 67, 90, 51, 57, 52, 56,
                32 ,32 ,80 ,86 ,71 ,67 ,71 ,79 ,32 ,50 ,50 ,49 ,48 ,32 ,32 ,32 ,50 ,51 ,53 ,53 ,32 ,32 ,32 ,55 ,51 ,56 ,32 ,48 ,
                94 ,67, 32, 32 ,69 ,32 ,32 ,28 ,65 ,83 ,35 ,29 ,70 ,49 ,32 ,80 ,49 ,32 ,87 ,53 ,32 ,90 ,53 ,32 ,89 ,65 ,32 ,84 ,
                65 ,32 ,75 ,65 ,32 ,72 ,65 ,32 ,77 ,81 ,32 ,71 ,81 ,42 ,55 ,43 ,32 ,32 ,67 ,90 ,51 ,53 ,50 ,54 ,32 ,32 ,83 ,72 ,
                65 ,67, 65 ,78 ,32 ,49 ,54 ,52 ,53 ,32 ,32 ,32 ,49 ,57 ,48 ,53 ,32 ,32 ,32 ,51 ,50 ,49 ,32 ,48 ,94 ,67 ,32 ,32 ,
                69 ,32, 32, 32, 68 ,83 ,35 ,32 ,70 ,52 ,32 ,80 ,83 ,32 ,87 ,65 ,32 ,90 ,81 ,32 ,89 ,65, 32, 84, 81, 32, 75, 81 ,
                32 ,72 ,81 ,32 ,77 ,81 ,32 ,71 ,81 ,42 ,32 ,32 ,32 ,32 ,67 ,90 ,51 ,49 ,57 ,54 ,32 ,32 ,32 ,32 ,32 ,67 ,71 ,79 ,
                32 ,50 ,50 ,48 ,48 ,32 ,32 ,32 ,48 ,48 ,49 ,53 ,43 ,49 ,32 ,55 ,51 ,56, 32, 48 ,94 ,67 ,32 ,32 ,69 ,32, 32, 28 ,
                65 ,83 ,35 ,29 ,70 ,76 ,32 ,80 ,76 ,32 ,87 ,76 ,32 ,90 ,81 ,32 ,89 ,76 ,32 ,84 ,81 ,32 ,75 ,81 ,32 ,72 ,81 ,32 ,
                77, 81 ,32 ,71, 81 ,42 ,28 ,42, 42, 32, 32, 65, 108 ,108, 32 ,115, 99, 104 ,101 ,100, 117 ,108 ,101 ,100 ,32 ,77,
                85 ,32, 111, 114, 32 ,70, 77,
            32, 102, 108 ,105, 103, 104, 116, 115, 32 ,111, 112, 101, 114 ,97 ,116, 101, 100 ,32 ,98 ,121, 32, 77 ,85 ,32, 111, 114,
            32 ,70, 77, 32 ,97 ,114,
            101, 32 ,34 ,69 ,97 ,115 ,116 ,101 ,114 ,110 ,32 ,69 ,120 ,112 ,114, 101, 115 ,115 ,34 ,29 ,13 ,28 ,42 ,42 ,32 ,32, 72 ,
            79 ,32, 70 ,76 ,73 ,71, 
            72 ,84 ,32 ,68, 69, 80, 65 ,82 ,84 ,85, 82 ,69, 47, 65, 82, 82, 73 ,86, 65, 76 ,32, 65, 84 ,32 ,80, 86 ,71, 32 ,84, 50 ,
            32, 70, 82 ,79 ,77 ,32, 49 ,
            56, 68, 69, 67, 49 ,50, 44, 29, 13 ,28 ,42, 42, 32 ,32 ,72 ,79, 32 ,80, 86, 71 ,32, 67 ,72 ,69 ,67, 75, 32 ,73, 78 ,32 ,
            52 ,53 ,32 ,77 ,73 ,78, 85,
            84, 69 ,83 ,32 ,66 ,69, 70 ,79 ,82, 69 ,32 ,68 ,69, 80 ,65, 82, 84, 85, 82, 69 ,29, 13, 30, 27, 98, 3            
            };
            byte[] test =//天气预报
            {
               1,0,1,178,0,0,0,1,39,81,112,2,27,11,40,32,15,27,77,27,14,73,79,58,35,53,88,71,120,76,108,70,120,84,36,49,40,27,15,58,32,32,32,13,32,32,32,32,32,57,27,14,84,66,27,15,50,50,27,14,72,85,27,15,32,27,14,80,71,70,90,72,85,27,15,58,13,32,32,32,32,32,32,32,32,27,14,87,110,53,77,70,120,78,66,27,15,32,50,53,67,40,55,55,70,41,44,32,27,14,87,110,56,95,70,120,78,66,27,15,32,50,57,67,40,56,52,70,41,44,32,27,14,85,115,83,106,27,15,44,32,27,14,54,43,55,103,27,15,52,45,53,27,14,60,54,27,15,32,13,32,32,32,32,32,32,57,27,14,84,66,27,15,50,51,27,14,72,85,27,15,32,27,14,80,71,70,90,82,59,27,15,58,13,32,32,32,32,32,32,32,32,27,14,87,110,53,77,70,120,78,66,27,15,32,50,52,67,40,55,53,70,41,44,32,27,14,87,110,56,95,70,120,78,66,27,15,32,51,48,67,40,56,54,70,41,44,32,27,14,54,96,84,70,27,15,44,32,27,14,54,43,68,79,55,103,27,15,52,45,53,27,14,60,54,27,15,32,13,32,32,32,32,32,32,32,32,57,27,14,84,66,27,15,50,52,27,14,72,85,27,15,32,27,14,80,71,70,90,54,126,27,15,58,13,32,32,32,32,32,32,32,32,27,14,87,110,53,77,70,120,78,66,27,15,32,50,49,67,40,55,48,70,41,44,32,27,14,87,110,56,95,70,120,78,66,27,15,32,51,48,67,40,56,54,70,41,44,32,27,14,85,115,83,106,87,42,86,80,83,106,27,15,44,32,27,14,68,79,55,103,27,15,52,45,53,27,14,60,54,87,42,54,43,39,35,55,103,27,15,52,45,53,27,14,60,54,27,15,32,13,32,32,32,32,32,32,27,98,13,30,27,98,3
            };
            string stra = OrderService(test);
            string[] testa = stra.Split('~');
            foreach(var item in testa)
            {
                int a = item.IndexOf("\0");
                string q2 = item.Substring(0,a+1);//那就在这个地方处理情况了
                Console.WriteLine(q2);
            }

            string str = "detr:nm/ 刘艳佳1";
            //System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(?<UserName>[\u4E00-\u9FA5]{2,9})");
            //var matches = regex.Matches(str);
            //List<string> strName = new List<string>();
            //List<int> location = new List<int>();//起始位置
            //List<string> strPin = new List<string>(); //表示汉字转拼音
            //foreach(System.Text.RegularExpressions.Match item in matches)
            //{
            //    string fir = item.Groups["UserName"].Value;
            //    int first = str.IndexOf(fir);
            //    location.Add(first);//记录这个名字的地址
            //    int strlenth = fir.Length;
            //    location.Add(strlenth);
            //    strName.Add(fir);
            //}//上面的数组里面全是有汉字的

            ////把这个字符串截取一段一段：
            //List<string> sub = new List<string>();
            //string sec = str.Substring(0, location[0]);
            //sub.Add(sec);
            //for (int i = 0; i < location.Count();i+=2 )
            //{
            //    string stt = str.Substring(location[i], location[i + 1]);
            //    sub.Add(stt);
            //}//sub把所有的都截取出来了。

            //int m=0;
            //List<byte[]> list = new List<byte[]>();
            //List<int> leng = new List<int>();
            //for (int i = 0; i < sub.Count(); i++) 
            //{
            //    if (sub[i] != strName[m])
            //    {
            //        byte[] first = Encoding.ASCII.GetBytes(sub[i]);
            //        int count = first.Length;
            //        list.Add(first);
            //        leng.Add(count);
            //    }
            //    else //汉字
            //    {
            //        byte[] first = ChineseToCode(strName[m]);
            //        m++;
            //        int count = first.Length;
            //        list.Add(first);
            //        leng.Add(count);
            //    }
            //}//进行字符串转字节码
            
            //int allleng = 0;
            //foreach(var item in leng)
            //{
            //    allleng += item;
            //}//算出最终要申请的存储空间

            //byte[] rtn=new byte[allleng];
            //int t = 0;
            //foreach(var item in list)
            //{
            //    for (int i = 0; i < item.Length;i++ )
            //    {
            //        rtn[t] = item[i];
            //        t++;
            //    }
            //}
            //var aa = rtn;
          
        }
        //做这样一个原始的东西感觉上挺好的。
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
        #region 根据gb2312转汉字
        private static string TransToChinese(byte[] toChinese)
        {
            string str = "";
            byte[] arry = new byte[2];
            for (int i = 0; i < toChinese.Length; i++)
            {
                toChinese[i] = (byte)(toChinese[i] + 128);
            }
            for (int i = 0; i < toChinese.Length-1; i += 2)
            {
                arry[0] = toChinese[i];
                arry[1] = toChinese[i + 1];
                str += System.Text.Encoding.GetEncoding("gb2312").GetString(arry);
            }
            return str;
        }

        private static string TransToCode(byte[] toCode)
        {
            string str = System.Text.Encoding.Default.GetString(toCode);
            return str;
        }
        public static byte[] CTOC() 
        {
            string name="上海地区天气预报";
            byte[] aa = System.Text.Encoding.GetEncoding("gb2312").GetBytes(name);
            for (int i = 0; i < aa.Length;i++ )
            {
                aa[i] = (byte)(aa[i] - 128);
            }
            return aa;
        }
        #region 表示解析普通命令
        public static string OrderService(byte[] receiveService)
        {
            string rtn=""; int max=0;
            Byte[] chineseCode = new byte[100];
            int start = 0;//标记解析的开始
            for (int i = 0; i < receiveService.Length;i++ )
            {
                if (receiveService[i] == 77 && receiveService[i-1] == 27) 
                {
                    start = i;
                    break;
                }
            }
            for (int i = start+1; i < receiveService.Length; i++) //控制长度
            {
                if (receiveService[i] == 13) 
                {
                    max = i;
                    break;
                }
            }
            Byte[] code = new byte[max];
            bool value = false;
            int m = 0;//相当于chineseCode的指针
            int n = 0;//相当于code的指针
            for (int i = 19; i < receiveService.Length; i++)
            {
                
                if (receiveService[i] < 32)
                {
                    //表示特殊字符
                    if (receiveService[i] == 13)
                    {
                        rtn += TransToCode(code) + "~";
                        int longth =0;
                        for(int j=i+1;j<receiveService.Length;j++)
                        {
                            if (receiveService[j] == 13)//receiveService[j]==28||这个地方弄地是有问题的
                            {
                                longth = j;
                                break;
                            }
                        }
                        code = new byte[longth];
                        n = 0;
                    }
                    //if (receiveService[i] == 28) //带有红色标记的地方是有问题的
                    //{
                    //    //表示红色标记开始
                    //    rtn += TransToCode(code) + "@";
                    //    int longth = 0;
                    //    for (int j = i; j < receiveService.Length; j++)
                    //    {
                    //        if (receiveService[j] == 29)
                    //        {
                    //            longth = j - i + 1;
                    //            break;
                    //        }
                    //    }
                    //    code = new byte[longth];
                    //    n = 0;
                    //}
                    //if (receiveService[i] == 29)
                    //{
                    //    //表示红色标记结束
                    //    int longth = 0;
                    //    rtn += TransToCode(code) + "@";
                    //    for (int j = i; j < receiveService.Length; j++)
                    //    {
                    //        if (receiveService[j] == 13)
                    //        {
                    //            longth = j - i + 1;
                    //            break;
                    //        }
                    //    }
                    //    code = new byte[longth];
                    //    n = 0;
                    //}
                    if (receiveService[i] == 14 && receiveService[i - 1] == 27)//表示汉字的开始
                    {
                        if (code[0]!=0) 
                        {
                            rtn += TransToCode(code);//这个地方是有问题的
                        }
                        int longth = 0;
                        for (int j = i; j < receiveService.Length; j++) 
                        {
                            if (receiveService[j] == 15 && receiveService[j - 1] == 27) 
                            {
                                longth = j;
                                longth = longth - i-1;
                                break;
                            }
                        }
                        chineseCode = new byte[longth];//似乎只要一遇到这个地方就有问题
                        m = 0;
                        value = true;
                    }
                    if (receiveService[i] == 15 && receiveService[i - 1] == 27)//这个地方表示汉字的地方结束
                    {
                        value = false;
                        rtn += TransToChinese(chineseCode);
                        int longth = 0;
                        for (int j = i; j < receiveService.Length; j++)
                        {
                            if (receiveService[j] == 13 || receiveService[j] == 28 || (receiveService[j] == 14 && receiveService[j - 1] == 27))
                            {
                                longth = j;
                                break;
                            }
                        }
                        code = new byte[longth];
                        n = 0;
                    }
                    if (receiveService[i] == 30)
                    {
                        //表示是【光标的（三角）开始】
                        rtn += "-=-";//这个解析的图像是有小问题的
                    }
                    if (receiveService[i] == 9 && receiveService[i - 1] == 27)
                    {
                        //表示是个结束表示【四方小点儿】
                        rtn += "☉";//这个地方算是没有解析成功
                    }
                    if (receiveService[i] == 11 && receiveService[i - 1] == 27 && receiveService[i + 3] == 15 && receiveService[i + 4] == 3)
                    {
                        break;//格式错误的情况
                    }
                    if (receiveService[i] == 27)
                    {
                        continue;
                    }
                    if (receiveService[i] == 3)
                    {
                        break;//最终结束
                    }
                }
                else if (receiveService[i] >= 32)
                {
                    if (receiveService[i] == 43)
                    {
                       // rtn += "当前的内容没有显示完";
                    }
                    if (receiveService[i] == 98 && receiveService[i - 1] == 27)
                    {
                        continue;//这个是显示不出来内容的。
                    }
                    if (value == true)
                    {
                        chineseCode[m] = receiveService[i];
                        m++;
                    }
                    else if (value == false)
                    {
                        code[n] = receiveService[i];
                        n++;
                    }
                }

            }
            return rtn;
        }
        #endregion
        #endregion
        #region 将字符串添加逗号
        public static string GetNewStr(string str) 
        {
            string aa = str.Replace(" ",",");
            return aa;
        }
        #endregion

        #region 显示是否可以重新new出一个空间
        public static void Test() 
        {
            byte[] aa = new byte[20];
            string test = "ttt";
            string test1 = "aaa";
            aa = System.Text.Encoding.Default.GetBytes(test);
            foreach (var item in aa) 
            {
                Console.WriteLine(item);
            }
            aa=new byte[10];
            aa = System.Text.Encoding.Default.GetBytes(test1);
            foreach(var item in aa)
            {
                Console.WriteLine(item);
            }
        }
        #endregion
    }
}
