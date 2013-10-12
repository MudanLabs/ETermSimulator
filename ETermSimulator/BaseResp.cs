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
        private const byte FuncTextContentStartFlag =77;


        public BaseResp(byte[] receiveBuffer)
        {
            Buffer = receiveBuffer;
        }
        public List<byte[]> SplitSegments(byte[] buffer)
        {

            int contentStartIdx = Array.LastIndexOf(buffer, RespContentStartFlag);//因为前边表示长度的部分可能会存在2或者3 所以从后往前搜索
            int contentEndIdx = Array.LastIndexOf(buffer, RespContentEndFlag);
            List<byte[]> RespBufferSegments = new List<byte[]>();
            var content = buffer.Skip(contentStartIdx + 1).Take(contentEndIdx - contentStartIdx - 1);
            List<int> RespBufferSegmentStartIdxs = new List<int>();
            for (int i = contentStartIdx + 1; i <= contentStartIdx+1+content.Count(); i++)
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
            RespBufferSegments.Add(buffer.Skip(RespBufferSegmentStartIdxs[RespBufferSegmentStartIdxs.Count-1]).Take(contentEndIdx - RespBufferSegmentStartIdxs[RespBufferSegmentStartIdxs.Count-1]).ToArray());
            return RespBufferSegments;
        }
       

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
