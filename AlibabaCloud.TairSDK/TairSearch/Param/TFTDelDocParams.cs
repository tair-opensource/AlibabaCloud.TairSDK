using System.Text;
using System.Collections.Generic;

namespace AlibabaCloud.TairSDK.TairSearch.Param
{
    public class TFTDelDocParams
    {
        public byte[][] getByteParams(string key, params string[] args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(Encoding.UTF8.GetBytes(key));

            foreach (var s in args)
            {
                byteParams.Add(Encoding.UTF8.GetBytes(s));
            }

            return byteParams.ToArray();
        }

        public byte[][] getByteParams(byte[] key, params byte[][] args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(key);

            foreach (var s in args)
            {
                byteParams.Add(s);
            }

            return byteParams.ToArray();
        }
    }
}