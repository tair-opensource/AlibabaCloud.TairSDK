using System.Text;
using System.Collections.Generic;

namespace AlibabaCloud.TairSDK.TairDoc.Param
{
    public class JsonsetParams : TairSDK.Param
    {
        private static string XX = "xx";
        private static string NX = "nx";

        public static JsonsetParams jsonsetParams()
        {
            return new JsonsetParams();
        }

        public JsonsetParams xx()
        {
            addParam(XX);
            return this;
        }

        public JsonsetParams nx()
        {
            addParam(NX);
            return this;
        }

        private void addParamWithValue(List<byte[]> byteParams, string option)
        {
            if (contains(option))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(option));
                byteParams.Add(Encoding.UTF8.GetBytes(getParams(option).ToString()));
            }
        }

        public byte[][] getByteParams(params byte[][] args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            foreach (var arg in args)
            {
                byteParams.Add(arg);
            }

            if (contains(XX))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(XX));
            }

            if (contains(NX))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(NX));
            }

            return byteParams.ToArray();
        }
    }
}