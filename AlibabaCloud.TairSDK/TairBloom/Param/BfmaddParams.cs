using System.Collections.Generic;

namespace AlibabaCloud.TairSDK.TairBloom.Param
{
    public class BfmaddParams : TairSDK.Param
    {
        public byte[][] getByteParams(byte[] key, params byte[][] items)
        {
            List<byte[]> byteParam = new List<byte[]>();
            byteParam.Add(key);

            foreach (var item in items)
            {
                byteParam.Add(item);
            }

            return byteParam.ToArray();
        }
    }
}