using System.Collections.Generic;
using System.Text;

namespace AlibabaCloud.TairSDK.TairZset.Param
{
    public class ExzrangeParams : TairSDK.Param
    {
        private static string WITHSCORES = "withscores";
        private static string LIMIT = "limit";
        private long offset;
        private long count;

        public ExzrangeParams()
        {
        }

        public ExzrangeParams limit(long offset, long count)
        {
            addParam(LIMIT);
            this.offset = offset;
            this.count = count;
            return this;
        }

        public ExzrangeParams withscore()
        {
            addParam(WITHSCORES);
            return this;
        }

        public byte[][] getByteParams(byte[] key, params byte[][] args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(key);
            foreach (var arg in args)
            {
                byteParams.Add(arg);
            }

            if (contains(WITHSCORES))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(WITHSCORES));
            }

            if (contains(LIMIT))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(LIMIT));
                byteParams.Add(Encoding.UTF8.GetBytes(offset.ToString()));
                byteParams.Add(Encoding.UTF8.GetBytes(count.ToString()));
            }

            return byteParams.ToArray();
        }
    }
}