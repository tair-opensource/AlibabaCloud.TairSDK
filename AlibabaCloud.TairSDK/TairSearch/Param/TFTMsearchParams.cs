using System.Text;
using System.Collections.Generic;

namespace AlibabaCloud.TairSDK.TairSearch.Param
{
    public class TFTMSearchParams
    {
        public byte[][] getByteParams(int index_count, string[] indexs, string query)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(Encoding.UTF8.GetBytes(index_count.ToString()));
            foreach (var index in indexs)
            {
                byteParams.Add(Encoding.UTF8.GetBytes(index));
            }

            byteParams.Add(Encoding.UTF8.GetBytes(query));
            return byteParams.ToArray();
        }

        public byte[][] getByteParams(int index_count, byte[][] indexs, byte[] request)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(Encoding.UTF8.GetBytes(index_count.ToString()));
            foreach (var index in indexs)
            {
                byteParams.Add(index);
            }

            byteParams.Add(request);
            return byteParams.ToArray();
        }
    }
}