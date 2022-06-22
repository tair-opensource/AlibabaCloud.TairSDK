using System.Collections.Generic;
using System.Text;

namespace AlibabaCloud.TairSDK.TairZset.Param
{
    public class ExzaddParams : TairSDK.Param
    {
        private static string XX = "xx";
        private static string NX = "nx";
        private static string CH = "ch";
        private static string INCR = "incr";

        public ExzaddParams()
        {
        }


        public ExzaddParams nx()
        {
            addParam(NX);
            return this;
        }

        public ExzaddParams xx()
        {
            addParam(XX);
            return this;
        }

        public ExzaddParams ch()
        {
            addParam(CH);
            return this;
        }

        public ExzaddParams incr()
        {
            addParam(INCR);
            return this;
        }

        public byte[][] getByteParams(byte[] key, params byte[][] args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(key);

            if (contains(NX))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(NX));
            }

            if (contains(XX))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(XX));
            }

            if (contains(CH))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(CH));
            }

            if (contains(INCR))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(INCR));
            }

            foreach (var arg in args)
            {
                byteParams.Add(arg);
            }

            return byteParams.ToArray();
        }
    }
}