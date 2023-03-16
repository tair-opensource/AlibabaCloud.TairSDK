using System;
using System.Collections.Generic;
using System.Text;

namespace AlibabaCloud.TairSDK.TairSearch.Param
{
    public class TFTAnalyzerParams : TairSDK.Param
    {
        private const string INDEX = "index";
        private const string SHOW_TIME = "show_time";

        public TFTAnalyzerParams index(string index)
        {
            addParam(INDEX, index);
            return this;
        }

        public TFTAnalyzerParams index(byte[] index)
        {
            addParam(INDEX, index);
            return this;
        }

        public TFTAnalyzerParams showTime()
        {
            addParam(SHOW_TIME, "");
            return this;
        }

        public byte[] getByteParam(string name)
        {
            var obj = getParams(name);
            if (obj != null)
            {
                if (obj is string)
                    return Encoding.UTF8.GetBytes(obj.ToString());
                else
                    return (byte[]) obj;
            }

            return null;
        }

        public void addParamWithValue(List<byte[]> byteParams, string option)
        {
            if (contains(option))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(option));
                var key = getByteParam(option);
                if (key != null)
                {
                    byteParams.Add(key);
                }
                else
                    byteParams.Add(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
            }
        }

        public byte[][] getByteParams(params byte[][] args)
        {
            var byteParams = new List<byte[]>();
            foreach (var arg in args) byteParams.Add(arg);
            addParamWithValue(byteParams, INDEX);

            if (contains(SHOW_TIME))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(SHOW_TIME));
            }

            return byteParams.ToArray();
        }
    }
}