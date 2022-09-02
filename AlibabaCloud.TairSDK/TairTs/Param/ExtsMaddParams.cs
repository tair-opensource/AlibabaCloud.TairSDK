using System;
using System.Collections.Generic;
using System.Text;

namespace AlibabaCloud.TairSDK.TairTs.Param
{
    public class ExtsMaddParams
    {
        public byte[][] getByteParams(string pkey, List<ExtsDataPoint<string>> args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(Encoding.UTF8.GetBytes(pkey));
            byteParams.Add(Encoding.UTF8.GetBytes(args.Count.ToString()));
            foreach (ExtsDataPoint<String> arg in args)
            {
                byteParams.Add(Encoding.UTF8.GetBytes(arg.getSkey()));
                byteParams.Add(Encoding.UTF8.GetBytes(arg.getTs()));
                byteParams.Add(Encoding.UTF8.GetBytes(arg.getValue().ToString()));
            }

            return byteParams.ToArray();
        }

        public byte[][] getByteParams(byte[] pkey, List<ExtsDataPoint<byte[]>> args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(pkey);
            byteParams.Add(Encoding.UTF8.GetBytes(args.Count.ToString()));
            foreach (ExtsDataPoint<byte[]> arg in args)
            {
                byteParams.Add(arg.getSkey());
                byteParams.Add(arg.getTs());
                byteParams.Add(Encoding.UTF8.GetBytes(arg.getValue().ToString()));
            }

            return byteParams.ToArray();
        }
    }
}