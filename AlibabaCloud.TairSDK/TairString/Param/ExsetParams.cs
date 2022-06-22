using System.Text;
using System.Collections.Generic;

namespace AlibabaCloud.TairSDK.TairString.Param
{
    public class ExsetParams : TairSDK.Param
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
        private const string FLAGS = "flags";

        public ExsetParams flags(int flags)
        {
            addParam(FLAGS, flags);
            return this;
        }

        public ExsetParams xx()
        {
            addParam(XX);
            return this;
        }

        public ExsetParams nx()
        {
            addParam(NX);
            return this;
        }

        public ExsetParams ex(int secondsToExpire)
        {
            addParam(EX, secondsToExpire);
            return this;
        }

        public ExsetParams px(long millisecondsToExpire)
        {
            addParam(PX, millisecondsToExpire);
            return this;
        }

        public ExsetParams exat(int secondsToExpire)
        {
            addParam(EXAT, secondsToExpire);
            return this;
        }

        public ExsetParams pxat(long millisecondsToExpire)
        {
            addParam(PXAT, millisecondsToExpire);
            return this;
        }

        public ExsetParams ver(long version)
        {
            addParam(VER, version);
            return this;
        }

        public ExsetParams abs(long absoluterVersion)
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

        public byte[][] getByteParams(params string[] args)
        {
            var byteParams = new List<byte[]>();
            foreach (var arg in args) byteParams.Add(Encoding.UTF8.GetBytes(arg));

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

            addParamWithValue(byteParams, FLAGS);
            return byteParams.ToArray();
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

            addParamWithValue(byteParams, FLAGS);
            return byteParams.ToArray();
        }
    }
}