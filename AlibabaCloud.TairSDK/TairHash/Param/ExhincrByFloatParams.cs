using System.Text;
using System.Collections.Generic;

namespace AlibabaCloud.TairSDK.TairHash.Param
{
    public class ExhincrByFloatParams : TairSDK.Param
    {
        private const string PX = "px";
        private const string EX = "ex";
        private const string EXAT = "exat";
        private const string PXAT = "pxat";

        private const string VER = "ver";
        private const string ABS = "abs";
        private const string MIN = "min";
        private const string MAX = "max";

        public static ExhincrByFloatParams exincrByFloatParams()
        {
            return new ExhincrByFloatParams();
        }

        public ExhincrByFloatParams ex(int secondsToExpire)
        {
            if (!contains(EXAT)) addParam(EX, secondsToExpire);

            return this;
        }

        public ExhincrByFloatParams exat(long unixTime)
        {
            if (!contains(EX)) addParam(EXAT, unixTime);

            return this;
        }

        public ExhincrByFloatParams px(int millisecondsToExpire)
        {
            if (!contains(PXAT)) addParam(PX, millisecondsToExpire);

            return this;
        }

        public ExhincrByFloatParams pxat(long millisecondsToExpire)
        {
            if (!contains(PX)) addParam(PXAT, millisecondsToExpire);

            return this;
        }

        public ExhincrByFloatParams ver(long version)
        {
            addParam(VER, version);
            return this;
        }

        public ExhincrByFloatParams abs(long absoluterVersion)
        {
            addParam(ABS, absoluterVersion);
            return this;
        }

        public ExhincrByFloatParams max(long max)
        {
            addParam(MAX, max);
            return this;
        }

        public ExhincrByFloatParams min(long min)
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