using System.Text;
using System.Collections.Generic;

namespace AlibabaCloud.TairSDK.TairSearch.Param
{
    public class TFTExplainScoreParams
    {
        public byte[][] getbyteParams(string key, string request, string[] docIds)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(Encoding.UTF8.GetBytes(key));
            byteParams.Add(Encoding.UTF8.GetBytes(request));

            foreach (var id in docIds)
            {
                byteParams.Add(Encoding.UTF8.GetBytes(id));
            }

            return byteParams.ToArray();
        }

        public byte[][] getbyteParams(byte[] key, byte[] request, byte[][] docIds)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(key);
            byteParams.Add(request);
            foreach (var id in docIds)
            {
                byteParams.Add(id);    
            }

            return byteParams.ToArray();
        }
    }
}