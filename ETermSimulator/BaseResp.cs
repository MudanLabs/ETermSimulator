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
        private const byte FuncGb2312Flag = 14;
        private const byte FuncASCIIFlag = 15;
        private const byte FuncTextContentStartFlag = 77;


        public BaseResp(byte[] receiveBuffer)
        {
            if (receiveBuffer != null && receiveBuffer.Length>2)
            {
                if (receiveBuffer[0] == 1 && receiveBuffer[1] == 0)
                {
                    Buffer = receiveBuffer;
                    List<byte[]> segments = SplitSegments(receiveBuffer);
                    if (Text == null)
                        Text = "";
                    foreach (var segment in segments)
                    {
                        switch (segment[1])
                        {
                            case FuncPointFlag:
                                StartPoint = DealFuncPointFlag(segment);
                                break;
                            case FuncGb2312Flag:
                                Text += DealFuncGb2312Flag(segment);
                                break;
                            case FuncASCIIFlag:
                            case FuncTextContentStartFlag:
                                Text += DealFuncASCIIFlag(segment);
                                break;
                            default:
                                continue;
                        }
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

        #region deal Gb2312Flag
        private string DealFuncGb2312Flag(byte[] segment)
        {
            Encoding gb2312Encoding = System.Text.Encoding.GetEncoding("gb2312");
            for (int i = 2; i <segment.Length; i++)
                segment[i] += 128;
            return gb2312Encoding.GetString(segment, 2, segment.Length - 2);
        }
        #endregion

        #region deal ASCIIFlag
        private string DealFuncASCIIFlag(byte[] segment)
        {
            Encoding assciiEncoding = new System.Text.ASCIIEncoding();
            return assciiEncoding.GetString(segment, 2, segment.Length - 2);
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
