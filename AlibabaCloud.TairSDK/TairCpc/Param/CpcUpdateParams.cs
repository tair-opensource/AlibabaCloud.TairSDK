using System;
using System.Collections.Generic;
using System.Text;

namespace AlibabaCloud.TairSDK.TairCpc.Param
{
    public class CpcUpdateParams : AlibabaCloud.TairSDK.Param
    {
        private const string PX = "px";
        private const string EX = "ex";
        private const string EXAT = "exat";
        private const string PXAT = "pxat";
        private const string SIZE = "size";
        private const string WIN = "win";

        public CpcUpdateParams()
        {
        }

        public static CpcUpdateParams cpcUpdateParams()
        {
            return new CpcUpdateParams();
        }

        public CpcUpdateParams ex(long secondsToExpire)
        {
            addParam(EX, secondsToExpire);
            return this;
        }

        public CpcUpdateParams px(long millisecondsToExpire)
        {
            addParam(PX, millisecondsToExpire);
            return this;
        }

        public CpcUpdateParams exat(long secondsToExpire)
        {
            addParam(EXAT, secondsToExpire);
            return this;
        }

        public CpcUpdateParams pxat(long millisecondsToExpire)
        {
            addParam(PXAT, millisecondsToExpire);
            return this;
        }

        public CpcUpdateParams size(long size)
        {
            addParam(SIZE, size);
            return this;
        }

        public CpcUpdateParams win(long winsize)
        {
            addParam(WIN, winsize);
            return this;
        }

        private int addParamWithValue(List<byte[]> byteParams, string option)
        {
            if (contains(option))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(option));
                byteParams.Add(Encoding.UTF8.GetBytes(getParams(option).ToString()));
                return 1;
            }

            return 0;
        }

        public byte[][] getByteParams(params byte[][] args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            foreach (byte[] arg in args)
            {
                byteParams.Add(arg);
            }

            int ex = addParamWithValue(byteParams, EX);
            int px = addParamWithValue(byteParams, PX);
            int exat = addParamWithValue(byteParams, EXAT);
            int pxat = addParamWithValue(byteParams, PXAT);
            if (ex + px + exat + pxat > 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            addParamWithValue(byteParams, SIZE);
            addParamWithValue(byteParams, WIN);

            return byteParams.ToArray();
        }
    }
}