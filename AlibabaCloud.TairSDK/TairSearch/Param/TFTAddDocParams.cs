using System.Text;
using System.Collections.Generic;

namespace AlibabaCloud.TairSDK.TairSearch.Param
{
    public class TFTAddDocParams
    {
        public byte[][] getbyteParams(string key, Dictionary<string, string> docs)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(Encoding.UTF8.GetBytes(key));

            foreach (var entry in docs)
            {
                byteParams.Add(Encoding.UTF8.GetBytes(entry.Key));
                byteParams.Add(Encoding.UTF8.GetBytes(entry.Value));
            }

            return byteParams.ToArray();
        }

        public byte[][] getbyteParams(byte[] key, Dictionary<byte[], byte[]> docs)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(key);
            foreach (var entry in docs)
            {
                byteParams.Add(entry.Key);
                byteParams.Add(entry.Value);
            }

            return byteParams.ToArray();
        }
    }
}