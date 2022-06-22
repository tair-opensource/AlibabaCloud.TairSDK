using System.Text;
using System.Collections.Generic;

namespace AlibabaCloud.TairSDK.TairString.Param
{
    public class ExincrbyFloatParams : TairSDK.Param
    {
        private const string XX = "xx";
        private const string NX = "nx";
        private const string PX = "px";
        private const string EX = "ex";
        private const string EXAT = "exat";
        private const string PXAT = "pxat";

        private const string VER = "ver";
        private const string ABS = "abs";
        private const string MIN = "min";
        private const string MAX = "max";


        private static ExincrbyFloatParams exincrbyFloatParam()
        {
            return new ExincrbyFloatParams();
        }

        public ExincrbyFloatParams max(double max)
        {
            addParam(MAX, max);
            return this;
        }

        public ExincrbyFloatParams min(double min)
        {
            addParam(MIN, min);
            return this;
        }

        public ExincrbyFloatParams xx()
        {
            addParam(XX);
            return this;
        }

        public ExincrbyFloatParams nx()
        {
            addParam(NX);
            return this;
        }

        public ExincrbyFloatParams ex(int secondsToExpire)
        {
            addParam(EX, secondsToExpire);
            return this;
        }

        public ExincrbyFloatParams px(long millisecondsToExpire)
        {
            addParam(PX, millisecondsToExpire);
            return this;
        }

        public ExincrbyFloatParams exat(int secondsToExpire)
        {
            addParam(EXAT, secondsToExpire);
            return this;
        }

        public ExincrbyFloatParams pxat(long millisecondsToExpire)
        {
            addParam(PXAT, millisecondsToExpire);
            return this;
        }

        public ExincrbyFloatParams ver(long version)
        {
            addParam(VER, version);
            return this;
        }

        public ExincrbyFloatParams abs(long absoluterVersion)
        {
            addParam(ABS, absoluterVersion);
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

            if (contains(XX)) byteParams.Add(Encoding.UTF8.GetBytes(XX));

            if (contains(NX)) byteParams.Add(Encoding.UTF8.GetBytes(NX));

            addParamWithValue(byteParams, EX);
            addParamWithValue(byteParams, PX);
            addParamWithValue(byteParams, EXAT);
            addParamWithValue(byteParams, PXAT);

            addParamWithValue(byteParams, VER);
            addParamWithValue(byteParams, ABS);

            addParamWithValue(byteParams, MIN);
            addParamWithValue(byteParams, MAX);

            return byteParams.ToArray();
        }
    }
}