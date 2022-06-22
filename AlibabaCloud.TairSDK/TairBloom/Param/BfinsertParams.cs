using System.Collections.Generic;
using System.Text;

namespace AlibabaCloud.TairSDK.TairBloom.Param
{
    public class BfinsertParams : TairSDK.Param
    {
        private static string CAPACITY = "CAPACITY";
        private static string ERROR = "ERROR";
        private static string NOCREATE = "NOCREATE";
        private static string ITEMS = "ITEMS";

        public BfinsertParams()
        {
        }

        public BfinsertParams bfinsertParams()
        {
            return new BfinsertParams();
        }

        public BfinsertParams capacity(long initCapacity)
        {
            addParam(CAPACITY, initCapacity);
            return this;
        }

        public BfinsertParams error(double errorRate)
        {
            addParam(ERROR, errorRate);
            return this;
        }

        public BfinsertParams nocreate()
        {
            addParam(NOCREATE);
            return this;
        }

        private void addParamWithValue(List<byte[]> bytesParams, string option)
        {
            if (contains(option))
            {
                bytesParams.Add(Encoding.UTF8.GetBytes(option));
                bytesParams.Add(Encoding.UTF8.GetBytes(getParams(option).ToString()));
            }
        }

        public byte[][] getByteParamsMeta(string key, params string[] args)
        {
            List<byte[]> byteParamsMeta = new List<byte[]>();
            byteParamsMeta.Add(Encoding.UTF8.GetBytes(key));
            foreach (var arg in args)
            {
                byteParamsMeta.Add(Encoding.UTF8.GetBytes(arg));
            }

            return byteParamsMeta.ToArray();
        }

        public byte[][] getByteParamsMeta(byte[] key, params byte[][] args)
        {
            List<byte[]> byteParamsMeta = new List<byte[]>();
            byteParamsMeta.Add(key);
            foreach (var arg in args)
            {
                byteParamsMeta.Add(arg);
            }

            return byteParamsMeta.ToArray();
        }

        public byte[][] getByteParams(byte[][] meta, params string[] args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            foreach (var bytes in meta)
            {
                byteParams.Add(bytes);
            }

            foreach (var arg in args)
            {
                byteParams.Add(Encoding.UTF8.GetBytes(arg));
            }

            return byteParams.ToArray();
        }

        public byte[][] getByteParams(byte[][] meta, params byte[][] args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            foreach (var barray in meta)
            {
                byteParams.Add(barray);
            }

            foreach (var barray in args)
            {
                byteParams.Add(barray);
            }

            return byteParams.ToArray();
        }

        public byte[][] getByteParams(byte[] key, params byte[][] args)
        {
            List<byte[]> bytesParams = new List<byte[]>();
            bytesParams.Add(key);
            if (contains(NOCREATE))
            {
                bytesParams.Add(Encoding.UTF8.GetBytes(NOCREATE));
            }

            addParamWithValue(bytesParams, CAPACITY);
            addParamWithValue(bytesParams, ERROR);
            bytesParams.Add(Encoding.UTF8.GetBytes(ITEMS));
            foreach (var arg in args)
            {
                bytesParams.Add(arg);
            }

            return bytesParams.ToArray();
        }
    }
}