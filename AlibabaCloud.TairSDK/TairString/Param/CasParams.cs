using System.Collections.Generic;
using System.Text;

namespace AlibabaCloud.TairSDK.TairString.Param
{
    public class CasParams : TairSDK.Param
    {
        private static string PX = "px";
        private static string EX = "ex";
        private static string EXAT = "exat";
        private static string PXAT = "pxat";

        public CasParams ex(int secondToExpire)
        {
            addParam(EX, secondToExpire);
            return this;
        }

        public CasParams px(long millisecondsToExpire)
        {
            addParam(PX, millisecondsToExpire);
            return this;
        }

        public CasParams exat(int secondsToExpire)
        {
            addParam(EXAT, secondsToExpire);
            return this;
        }

        public CasParams pxat(long millisecondsToExpire)
        {
            addParam(PXAT, millisecondsToExpire);
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

        public byte[][] getByteParams(params string[] args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            foreach (var arg in args)
            {
                byteParams.Add(Encoding.UTF8.GetBytes(arg));
            }

            addParamWithValue(byteParams, EX);
            addParamWithValue(byteParams, PX);
            addParamWithValue(byteParams, EXAT);
            addParamWithValue(byteParams, PXAT);

            return byteParams.ToArray();
        }

        public byte[][] getByteParams(params byte[][] args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            foreach (var arg in args)
            {
                byteParams.Add(arg);
            }

            addParamWithValue(byteParams, EX);
            addParamWithValue(byteParams, PX);
            addParamWithValue(byteParams, EXAT);
            addParamWithValue(byteParams, PXAT);
            return byteParams.ToArray();
        }
    }
}