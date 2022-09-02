using System;
using System.Collections.Generic;
using System.Text;

namespace AlibabaCloud.TairSDK.TairTs.Param
{
    public class ExtsQueryParams : AlibabaCloud.TairSDK.Param
    {
        public byte[][] getByteParams(string pkey, List<ExtsFilter<string>> args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(Encoding.UTF8.GetBytes(pkey));
            foreach (ExtsFilter<String> arg in args)
            {
                byteParams.Add(Encoding.UTF8.GetBytes(arg.getFilter()));
            }

            return byteParams.ToArray();
        }

        public byte[][] getByteParams(byte[] pkey, List<ExtsFilter<byte[]>> args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(pkey);
            foreach (ExtsFilter<byte[]> arg in args)
            {
                byteParams.Add(arg.getFilter());
            }

            return byteParams.ToArray();
        }
    }
}