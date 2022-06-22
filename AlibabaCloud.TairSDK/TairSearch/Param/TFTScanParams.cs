using System.Text;
using System.Collections.Generic;

namespace AlibabaCloud.TairSDK.TairSearch.Param
{
    public class TFTScanParams : TairSDK.Param
    {
        private const string MATCH = "match";
        private const string COUNT = "count";

        public static TFTScanParams exhscanParams()
        {
            return new TFTScanParams();
        }

        public TFTScanParams match(string pattern)
        {
            addParam(MATCH, pattern);
            return this;
        }

        public TFTScanParams count(int count)
        {
            addParam(COUNT, count);
            return this;
        }

        public void addParamWithValue(List<byte[]> byteParams, string option)
        {
            if (contains(option))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(option));
                byteParams.Add(Encoding.UTF8.GetBytes(getParams(option).ToString()));
            }
        }

        public byte[][] getByteParams(params byte[][] args)
        {
            var byteParams = new List<byte[]>();
            foreach (var arg in args) byteParams.Add(arg);

            addParamWithValue(byteParams, MATCH);
            addParamWithValue(byteParams, COUNT);

            return byteParams.ToArray();
        }
    }
}