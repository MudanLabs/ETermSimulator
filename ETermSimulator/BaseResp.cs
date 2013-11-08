using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETermSimulator
{
    public class BaseResp
    {
        private const byte RespContentStartFlag = 2;
        private const byte RespContentEndFlag = 3;
        private const byte RespContentFuncFlag = 27;
        private const byte FuncPointFlag = 11;
        private const byte FuncGBKFlag = 14;//include(GB2312 and infrequently used character)
        private const byte FuncGBKInfrequentFlag = 120;//flag of infrequently used character
        private const byte FuncASCIIFlag = 15;
        private const byte FuncASCIIFlag2 = 98;
        private const byte FuncTextContentStartFlag = 77;
        private const byte FuncTabStopFlag = 9;//TabStop

        private const byte FuncSOEFlag = 30;//SOE



        public BaseResp(byte[] receiveBuffer)
        {
            if (receiveBuffer != null && receiveBuffer.Length > 13 && receiveBuffer[0] == 1 && receiveBuffer[1] == 0)
            {
                int respLength = receiveBuffer[2] * 256 + receiveBuffer[3];
                Buffer = new byte[respLength];
                Array.Copy(receiveBuffer,Buffer,respLength);
                List<byte[]> segments = SplitSegments(Buffer);
                if (Text == null)
                    Text = "";
                foreach (var segment in segments)
                {
                    switch (segment[1])
                    {
                        case FuncPointFlag:
                            StartPoint = DealFuncPointFlag(segment);
                            break;
                        case FuncGBKFlag:
                            Text += DealFuncGBKFlag(segment);
                            break;
                        case FuncASCIIFlag:
                        case FuncASCIIFlag2:
                        case FuncTabStopFlag:
                        case FuncTextContentStartFlag:
                            Text += DealFuncASCIIFlag(segment);
                            break;
                        default:
                            continue;
                    }
                }
            }
        }

        #region deal PointFlag
        private Point DealFuncPointFlag(byte[] segment)
        {
            return new Point((System.Int16)(segment[3] - 32), (System.Int16)(segment[4] - 32));
        }
        #endregion

        #region deal GBKFlag
        private string DealFuncGBKFlag(byte[] segment)
        {
            List<byte> GBKList = new List<byte>();
            for (int i = 0; i < segment.Length; i++)
            {
                if (i < 2)//head
                    continue;
                if (segment[i] != FuncGBKInfrequentFlag)//frequently used character
                {

                    var judge = 0x2f <= segment[i] + 0xa && segment[i] + 0xa <= 0x32;
                    var first = judge ? segment[i+1] : segment[i];
                    var second = judge ? segment[i] : segment[i+1];
                    first = (byte)first - 0xe >= 0x24 ? (byte)(first - 0xe) : first;
                    first = (byte)(first - 0x72);
                    second = judge ? (byte)(second + 0xa) : second;
                    second = (byte)(second - 0x80);
                    GBKList.Add(first);
                    GBKList.Add(second);
                    //if (segment[i] < 37)
                    //    GBKList.Add((byte)(segment[i] + 142));
                    //else
                    //    GBKList.Add((byte)(segment[i] + 128));
                    //GBKList.Add((byte)(segment[i + 1] + 128));
                    i++;
                }
                else//infrequently used character
                {
                    byte a = (byte)((((segment[i + 1] + 0x1) << 4) & 0xF0) | ((segment[i + 2] >> 2) & 0xF));
                    byte b = (byte)((segment[i + 3] + 0x1) + ((segment[i + 2] & 0x2) << 6));
                    GBKList.Add(a);
                    GBKList.Add(b);
                    i = i + 3;
                }
            }
            Encoding gbkEncoding = System.Text.Encoding.GetEncoding("GBK");
            return gbkEncoding.GetString(GBKList.ToArray());
        }
        #endregion

        #region deal ASCIIFlag
        private string DealFuncASCIIFlag(byte[] segment)
        {
            List<byte> AssciiList = new List<byte>();
            for (int i = 0; i < segment.Length; i++)
            {
                if (i < 2)//head
                    continue;
                if (segment[i] != FuncSOEFlag)
                    AssciiList.Add(segment[i]);
            }
            Encoding assciiEncoding = new System.Text.ASCIIEncoding();
            return assciiEncoding.GetString(AssciiList.ToArray());
        }
        #endregion

        #region Split buffer with RespContentFuncFlag
        private List<byte[]> SplitSegments(byte[] buffer)
        {

            int contentStartIdx = Array.LastIndexOf(buffer, RespContentStartFlag);//因为前边表示长度的部分可能会存在2或者3 所以从后往前搜索
            int contentEndIdx = Array.LastIndexOf(buffer, RespContentEndFlag);
            List<byte[]> RespBufferSegments = new List<byte[]>();
            var content = buffer.Skip(contentStartIdx + 1).Take(contentEndIdx - contentStartIdx - 1);
            List<int> RespBufferSegmentStartIdxs = new List<int>();
            for (int i = contentStartIdx + 1; i <= contentStartIdx + 1 + content.Count(); i++)
            {
                if (buffer[i] == RespContentFuncFlag)
                {
                    RespBufferSegmentStartIdxs.Add(i);
                }
            }
            for (int i = 0; i < RespBufferSegmentStartIdxs.Count - 1; i++)
            {
                RespBufferSegments.Add(buffer.Skip(RespBufferSegmentStartIdxs[i]).Take(RespBufferSegmentStartIdxs[i + 1] - RespBufferSegmentStartIdxs[i]).ToArray());
            }
            RespBufferSegments.Add(buffer.Skip(RespBufferSegmentStartIdxs[RespBufferSegmentStartIdxs.Count - 1]).Take(contentEndIdx - RespBufferSegmentStartIdxs[RespBufferSegmentStartIdxs.Count - 1]).ToArray());
            return RespBufferSegments;
        }
        #endregion

        public byte[] Buffer { get; private set; }//服务器返回的数据

        public string Text
        {
            get;
            private set;
        }

        public Point StartPoint { get; private set; }
        public Point? EndPoint { get; private set; }

    }
}
