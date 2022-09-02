using System;
using System.Collections.Generic;
using System.Text;

namespace AlibabaCloud.TairSDK.TairTs.Param
{
    public class ExtsAttributesParams : AlibabaCloud.TairSDK.Param
    {
        private static string UNCOMPRESSED = "UNCOMPRESSED";
        private static string DATA_ET = "DATA_ET";
        private static string CHUNK_SIZE = "CHUNK_SIZE";
        private static string LABELS = "LABELS";

        public ExtsAttributesParams uncompressed()
        {
            addParam(UNCOMPRESSED);
            return this;
        }

        public ExtsAttributesParams dataEt(long millisecondsToExpire)
        {
            addParam(DATA_ET, millisecondsToExpire);
            return this;
        }

        public ExtsAttributesParams chunkSize(long maxDataPointsPerChunk)
        {
            addParam(CHUNK_SIZE, maxDataPointsPerChunk);
            return this;
        }

        public ExtsAttributesParams labels(List<string> labels)
        {
            addParam(LABELS, labels);
            return this;
        }

        private void addParamWithValue(List<byte[]> byteParams, String option)
        {
            if (contains(option))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(option));
                byteParams.Add(Encoding.UTF8.GetBytes(getParams(option).ToString()));
            }
        }

        private void addParamWithLabel(List<byte[]> byteParams, String option)
        {
            if (contains(option))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(option));
                List<string> labels = (List<string>) getParams(option);
                foreach (String label in labels)
                {
                    byteParams.Add(Encoding.UTF8.GetBytes(label));
                }
            }
        }

        public byte[][] getByteParams(params string[] args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            foreach (string arg in args)
            {
                byteParams.Add(Encoding.UTF8.GetBytes(arg));
            }

            if (contains(UNCOMPRESSED))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(UNCOMPRESSED));
            }

            addParamWithValue(byteParams, DATA_ET);
            addParamWithValue(byteParams, CHUNK_SIZE);
            addParamWithLabel(byteParams, LABELS);

            return byteParams.ToArray();
        }

        public byte[][] getByteParams(params byte[][] args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            foreach (byte[] arg in args)
            {
                byteParams.Add(arg);
            }

            if (contains(UNCOMPRESSED))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(UNCOMPRESSED));
            }

            addParamWithValue(byteParams, DATA_ET);
            addParamWithValue(byteParams, CHUNK_SIZE);
            addParamWithLabel(byteParams, LABELS);

            return byteParams.ToArray();
        }

        public byte[][] getByteParams(String pkey, List<ExtsDataPoint<string>> args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(Encoding.UTF8.GetBytes(pkey));
            byteParams.Add(Encoding.UTF8.GetBytes((args.Count).ToString()));
            foreach (ExtsDataPoint<string> arg in args)
            {
                byteParams.Add(Encoding.UTF8.GetBytes(arg.getSkey()));
                byteParams.Add(Encoding.UTF8.GetBytes(arg.getTs()));
                byteParams.Add(Encoding.UTF8.GetBytes(arg.getValue().ToString()));
            }

            if (contains(UNCOMPRESSED))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(UNCOMPRESSED));
            }

            addParamWithValue(byteParams, DATA_ET);
            addParamWithValue(byteParams, CHUNK_SIZE);
            addParamWithLabel(byteParams, LABELS);

            return byteParams.ToArray();
        }

        public byte[][] getByteParams(byte[] pkey, List<ExtsDataPoint<byte[]>> args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(pkey);
            byteParams.Add(Encoding.UTF8.GetBytes(args.Count.ToString()));
            foreach (ExtsDataPoint<byte[]> arg in args)
            {
                byteParams.Add(arg.getSkey());
                byteParams.Add(arg.getTs());
                byteParams.Add(Encoding.UTF8.GetBytes(arg.getValue().ToString()));
            }

            if (contains(UNCOMPRESSED))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(UNCOMPRESSED));
            }

            addParamWithValue(byteParams, DATA_ET);
            addParamWithValue(byteParams, CHUNK_SIZE);
            addParamWithLabel(byteParams, LABELS);

            return byteParams.ToArray();
        }
    }
}