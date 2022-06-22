using System.Collections.Generic;
using System.Text;

namespace AlibabaCloud.TairSDK.TairHash.Param
{
    public class ExhsetParams : TairSDK.Param
    {
        private const string XX = "xx";
        private const string NX = "nx";
        private const string PX = "px";
        private const string EX = "ex";
        private const string EXAT = "exat";
        private const string PXAT = "pxat";

        private const string VER = "ver";
        private const string ABS = "abs";

        public static ExhsetParams exhsetParams()
        {
            return new ExhsetParams();
        }

        public ExhsetParams xx()
        {
            addParam(XX);
            return this;
        }

        public ExhsetParams nx()
        {
            addParam(NX);
            return this;
        }

        public ExhsetParams ex(int secondsToExpire)
        {
            addParam(EX, secondsToExpire);
            return this;
        }

        public ExhsetParams px(long millisecondsToExpire)
        {
            addParam(PX, millisecondsToExpire);
            return this;
        }

        public ExhsetParams exat(int secondsToExpire)
        {
            addParam(EXAT, secondsToExpire);
            return this;
        }

        public ExhsetParams pxat(long millisecondsToExpire)
        {
            addParam(PXAT, millisecondsToExpire);
            return this;
        }

        public ExhsetParams ver(long version)
        {
            addParam(VER, version);
            return this;
        }

        public ExhsetParams abs(long absoluterVersion)
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

            return byteParams.ToArray();
        }
    }
}