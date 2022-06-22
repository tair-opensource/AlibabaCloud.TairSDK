using System.Text;
using System.Collections.Generic;

namespace AlibabaCloud.TairSDK.TairDoc.Param
{
    public class JsongetParams : TairSDK.Param
    {
        private static string FORMAT = "format";
        private static string ROOTNAME = "rootname";
        private static string ARRNAME = "arrname";

        public static JsongetParams jsongetParams()
        {
            return new JsongetParams();
        }

        public JsongetParams format(string format)
        {
            addParam(FORMAT, format);
            return this;
        }

        public JsongetParams rootname(string rootname)
        {
            addParam(ROOTNAME, rootname);
            return this;
        }

        public JsongetParams arrname(string arrname)
        {
            addParam(ARRNAME, arrname);
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

            addParamWithValue(byteParams, FORMAT);
            addParamWithValue(byteParams, ROOTNAME);
            addParamWithValue(byteParams, ARRNAME);
            return byteParams.ToArray();
        }
    }
}