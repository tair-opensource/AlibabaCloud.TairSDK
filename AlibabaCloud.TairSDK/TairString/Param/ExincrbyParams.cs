using System.Text;
using System.Collections.Generic;

namespace AlibabaCloud.TairSDK.TairString.Param
{
    public class ExincrbyParams : TairSDK.Param
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


        private const string DEF = "def";
        private const string NONEGATIVE = "nonegative";


        private static ExincrbyParams ExincrbyParam()
        {
            return new ExincrbyParams();
        }

        public ExincrbyParams def(long defaultValue)
        {
            addParam(DEF, defaultValue);
            return this;
        }

        public ExincrbyParams nonegative()
        {
            addParam(NONEGATIVE);
            return this;
        }

        public ExincrbyParams max(long max)
        {
            addParam(MAX, max);
            return this;
        }

        public ExincrbyParams min(long min)
        {
            addParam(MIN, min);
            return this;
        }

        public ExincrbyParams xx()
        {
            addParam(XX);
            return this;
        }

        public ExincrbyParams nx()
        {
            addParam(NX);
            return this;
        }

        public ExincrbyParams ex(int secondsToExpire)
        {
            addParam(EX, secondsToExpire);
            return this;
        }

        public ExincrbyParams px(long millisecondsToExpire)
        {
            addParam(PX, millisecondsToExpire);
            return this;
        }

        public ExincrbyParams exat(int secondsToExpire)
        {
            addParam(EXAT, secondsToExpire);
            return this;
        }

        public ExincrbyParams pxat(long millisecondsToExpire)
        {
            addParam(PXAT, millisecondsToExpire);
            return this;
        }

        public ExincrbyParams ver(long version)
        {
            addParam(VER, version);
            return this;
        }

        public ExincrbyParams abs(long absoluterVersion)
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

            if (contains(NONEGATIVE)) byteParams.Add(Encoding.UTF8.GetBytes(NONEGATIVE));

            addParamWithValue(byteParams, EX);
            addParamWithValue(byteParams, PX);
            addParamWithValue(byteParams, EXAT);
            addParamWithValue(byteParams, PXAT);

            addParamWithValue(byteParams, VER);
            addParamWithValue(byteParams, ABS);

            addParamWithValue(byteParams, MIN);
            addParamWithValue(byteParams, MAX);

            addParamWithValue(byteParams, DEF);
            return byteParams.ToArray();
        }
    }
}