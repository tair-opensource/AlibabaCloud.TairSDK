using System.Text;
using System.Collections.Generic;
using AlibabaCloud.TairSDK.TairString.Param;

namespace AlibabaCloud.TairSDK.TairHash.Param
{
    public class ExhincrByParams : TairSDK.Param
    {
        private const string PX = "px";
        private const string EX = "ex";
        private const string EXAT = "exat";
        private const string PXAT = "pxat";

        private const string VER = "ver";
        private const string ABS = "abs";
        private const string MIN = "min";
        private const string MAX = "max";

        public static ExincrbyParams exincrbyParams()
        {
            return new ExincrbyParams();
        }

        public ExhincrByParams ex(int secondsToExpire)
        {
            if (!contains(EXAT)) addParam(EX, secondsToExpire);

            return this;
        }

        public ExhincrByParams exat(long unixTime)
        {
            if (!contains(EX)) addParam(EXAT, unixTime);

            return this;
        }

        public ExhincrByParams px(int millisecondsToExpire)
        {
            if (!contains(PXAT)) addParam(PX, millisecondsToExpire);

            return this;
        }

        public ExhincrByParams pxat(long millisecondsToExpire)
        {
            if (!contains(PX)) addParam(PXAT, millisecondsToExpire);

            return this;
        }

        public ExhincrByParams ver(long version)
        {
            addParam(VER, version);
            return this;
        }

        public ExhincrByParams abs(long absoluterVersion)
        {
            addParam(ABS, absoluterVersion);
            return this;
        }

        public ExhincrByParams max(long max)
        {
            addParam(MAX, max);
            return this;
        }

        public ExhincrByParams min(long min)
        {
            addParam(MIN, min);
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


            addParamWithValue(byteParams, EX);
            addParamWithValue(byteParams, PX);
            addParamWithValue(byteParams, EXAT);
            addParamWithValue(byteParams, PXAT);
            addParamWithValue(byteParams, MAX);
            addParamWithValue(byteParams, MIN);
            addParamWithValue(byteParams, VER);
            addParamWithValue(byteParams, ABS);

            return byteParams.ToArray();
        }
    }
}