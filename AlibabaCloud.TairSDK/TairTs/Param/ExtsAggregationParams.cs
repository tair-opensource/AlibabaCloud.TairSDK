using System.Collections.Generic;
using System.Text;

namespace AlibabaCloud.TairSDK.TairTs.Param
{
    public class ExtsAggregationParams : TairSDK.Param
    {
        private static string MAXCOUNT = "MAXCOUNT";
        private static string WITHLABELS = "WITHLABELS";
        private static string REVERSE = "REVERSE";
        private static string FILTER = "FILTER";
        private static string AGGREGATION = "AGGREGATION";
        private static string MIN = "MIN";
        private static string MAX = "MAX";
        private static string SUM = "SUM";
        private static string AVG = "AVG";
        private static string STDP = "STD.P";
        private static string STDS = "STD.S";
        private static string COUNT = "COUNT";
        private static string FIRST = "FIRST";
        private static string LAST = "LAST";
        private static string RANGE = "RANGE";

        private static List<string> MENUS = new List<string>
            {MIN, MAX, SUM, AVG, STDP, STDS, COUNT, FIRST, LAST, RANGE};


        public ExtsAggregationParams withLabels()
        {
            addParam(WITHLABELS);
            return this;
        }

        public ExtsAggregationParams reverse()
        {
            addParam(REVERSE);
            return this;
        }

        public ExtsAggregationParams maxCountSize(long count)
        {
            addParam(MAXCOUNT, count);
            return this;
        }

        public ExtsAggregationParams aggMin(long timeBucket)
        {
            addParam(MIN, timeBucket);
            return this;
        }

        public ExtsAggregationParams aggMax(long timeBucket)
        {
            addParam(MAX, timeBucket);
            return this;
        }

        public ExtsAggregationParams aggSum(long timeBucket)
        {
            addParam(SUM, timeBucket);
            return this;
        }

        public ExtsAggregationParams aggAvg(long timeBucket)
        {
            addParam(AVG, timeBucket);
            return this;
        }

        public ExtsAggregationParams aggStdP(long timeBucket)
        {
            addParam(STDP, timeBucket);
            return this;
        }

        public ExtsAggregationParams aggStdS(long timeBucket)
        {
            addParam(STDS, timeBucket);
            return this;
        }

        public ExtsAggregationParams aggCount(long timeBucket)
        {
            addParam(COUNT, timeBucket);
            return this;
        }

        public ExtsAggregationParams aggFirst(long timeBucket)
        {
            addParam(FIRST, timeBucket);
            return this;
        }

        public ExtsAggregationParams aggLast(long timeBucket)
        {
            addParam(LAST, timeBucket);
            return this;
        }

        public ExtsAggregationParams aggRange(long timeBucket)
        {
            addParam(RANGE, timeBucket);
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

        public byte[][] getByteRangeParams(params string[] args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            foreach (string arg in args)
            {
                byteParams.Add(Encoding.UTF8.GetBytes(arg));
            }

            if (contains(MAXCOUNT))
            {
                addParamWithValue(byteParams, MAXCOUNT);
            }

            if (contains(REVERSE))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(REVERSE));
            }


            foreach (string menu in MENUS)
            {
                if (contains(menu))
                {
                    byteParams.Add(Encoding.UTF8.GetBytes(AGGREGATION));
                    addParamWithValue(byteParams, menu);
                    break;
                }
            }

            return byteParams.ToArray();
        }

        public byte[][] getByteRangeParams(params byte[][] args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            foreach (byte[] arg in args)
            {
                byteParams.Add(arg);
            }

            if (contains(MAXCOUNT))
            {
                addParamWithValue(byteParams, MAXCOUNT);
            }

            if (contains(REVERSE))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(REVERSE));
            }

            foreach (string menu in MENUS)
            {
                if (contains(menu))
                {
                    byteParams.Add(Encoding.UTF8.GetBytes(AGGREGATION));
                    addParamWithValue(byteParams, menu);
                    break;
                }
            }

            return byteParams.ToArray();
        }

        public byte[][] getByteRangeParams(string pkey, List<string> skeys, string startTs, string endTs)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(Encoding.UTF8.GetBytes(pkey));
            byteParams.Add(Encoding.UTF8.GetBytes(skeys.Count.ToString()));
            foreach (string arg in skeys)
            {
                byteParams.Add(Encoding.UTF8.GetBytes(arg));
            }

            byteParams.Add(Encoding.UTF8.GetBytes(startTs));
            byteParams.Add(Encoding.UTF8.GetBytes(endTs));

            if (contains(MAXCOUNT))
            {
                addParamWithValue(byteParams, MAXCOUNT);
            }

            if (contains(WITHLABELS))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(WITHLABELS));
            }

            if (contains(REVERSE))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(REVERSE));
            }

            foreach (string menu in MENUS)
            {
                if (contains(menu))
                {
                    byteParams.Add(Encoding.UTF8.GetBytes(AGGREGATION));
                    addParamWithValue(byteParams, menu);
                    break;
                }
            }

            return byteParams.ToArray();
        }

        public byte[][] getByteRangeParams(byte[] pkey, List<byte[]> skeys, byte[] startTs, byte[] endTs)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(pkey);
            byteParams.Add(Encoding.UTF8.GetBytes(skeys.Count.ToString()));
            foreach (byte[] arg in skeys)
            {
                byteParams.Add(arg);
            }

            byteParams.Add(startTs);
            byteParams.Add(endTs);

            if (contains(MAXCOUNT))
            {
                addParamWithValue(byteParams, MAXCOUNT);
            }

            if (contains(WITHLABELS))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(WITHLABELS));
            }

            if (contains(REVERSE))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(REVERSE));
            }

            foreach (string menu in MENUS)
            {
                if (contains(menu))
                {
                    byteParams.Add(Encoding.UTF8.GetBytes(AGGREGATION));
                    addParamWithValue(byteParams, menu);
                    break;
                }
            }

            return byteParams.ToArray();
        }

        public byte[][] getByteMrangeParams(string pkey, string startTs, string endTs, List<ExtsFilter<string>> args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(Encoding.UTF8.GetBytes(pkey));
            byteParams.Add(Encoding.UTF8.GetBytes(startTs));
            byteParams.Add(Encoding.UTF8.GetBytes(endTs));

            if (contains(MAXCOUNT))
            {
                addParamWithValue(byteParams, MAXCOUNT);
            }

            foreach (string menu in MENUS)
            {
                if (contains(menu))
                {
                    byteParams.Add(Encoding.UTF8.GetBytes(AGGREGATION));
                    addParamWithValue(byteParams, menu);
                    break;
                }
            }

            if (contains(WITHLABELS))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(WITHLABELS));
            }

            if (contains(REVERSE))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(REVERSE));
            }

            byteParams.Add(Encoding.UTF8.GetBytes(FILTER));
            foreach (ExtsFilter<string> arg in args)
            {
                byteParams.Add(Encoding.UTF8.GetBytes(arg.getFilter()));
            }

            return byteParams.ToArray();
        }

        public byte[][] getByteMrangeParams(byte[] pkey, byte[] startTs, byte[] endTs, List<ExtsFilter<byte[]>> args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(pkey);
            byteParams.Add(startTs);
            byteParams.Add(endTs);

            if (contains(MAXCOUNT))
            {
                addParamWithValue(byteParams, MAXCOUNT);
            }

            foreach (string menu in MENUS)
            {
                if (contains(menu))
                {
                    byteParams.Add(Encoding.UTF8.GetBytes(AGGREGATION));
                    addParamWithValue(byteParams, menu);
                    break;
                }
            }

            if (contains(WITHLABELS))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(WITHLABELS));
            }

            if (contains(REVERSE))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(REVERSE));
            }

            byteParams.Add(Encoding.UTF8.GetBytes(FILTER));
            foreach (ExtsFilter<byte[]> arg in args)
            {
                byteParams.Add(arg.getFilter());
            }

            return byteParams.ToArray();
        }


        public byte[][] getBytePrangeParams(string pkey, string startTs, string endTs, string pkeyAggregationType,
            long pkeyTimeBucket, List<ExtsFilter<string>> args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(Encoding.UTF8.GetBytes(pkey));
            byteParams.Add(Encoding.UTF8.GetBytes(startTs));
            byteParams.Add(Encoding.UTF8.GetBytes(endTs));
            byteParams.Add(Encoding.UTF8.GetBytes(pkeyAggregationType));
            byteParams.Add(Encoding.UTF8.GetBytes(pkeyTimeBucket.ToString()));

            if (contains(MAXCOUNT))
            {
                addParamWithValue(byteParams, MAXCOUNT);
            }

            foreach (string menu in MENUS)
            {
                if (contains(menu))
                {
                    byteParams.Add(Encoding.UTF8.GetBytes(AGGREGATION));
                    addParamWithValue(byteParams, menu);
                    break;
                }
            }

            if (contains(WITHLABELS))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(WITHLABELS));
            }

            if (contains(REVERSE))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(REVERSE));
            }

            byteParams.Add(Encoding.UTF8.GetBytes(FILTER));
            foreach (ExtsFilter<string> arg in args)
            {
                byteParams.Add(Encoding.UTF8.GetBytes(arg.getFilter()));
            }

            return byteParams.ToArray();
        }

        public byte[][] getBytePrangeParams(byte[] pkey, byte[] startTs, byte[] endTs, byte[] pkeyAggregationType,
            long pkeyTimeBucket, List<ExtsFilter<byte[]>> args)
        {
            List<byte[]> byteParams = new List<byte[]>();
            byteParams.Add(pkey);
            byteParams.Add(startTs);
            byteParams.Add(endTs);
            byteParams.Add(pkeyAggregationType);
            byteParams.Add(Encoding.UTF8.GetBytes(pkeyTimeBucket.ToString()));

            if (contains(MAXCOUNT))
            {
                addParamWithValue(byteParams, MAXCOUNT);
            }

            foreach (string menu in MENUS)
            {
                if (contains(menu))
                {
                    byteParams.Add(Encoding.UTF8.GetBytes(AGGREGATION));
                    addParamWithValue(byteParams, menu);
                    break;
                }
            }

            if (contains(WITHLABELS))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(WITHLABELS));
            }

            if (contains(REVERSE))
            {
                byteParams.Add(Encoding.UTF8.GetBytes(REVERSE));
            }

            byteParams.Add(Encoding.UTF8.GetBytes(FILTER));
            foreach (ExtsFilter<byte[]> arg in args)
            {
                byteParams.Add(arg.getFilter());
            }

            return byteParams.ToArray();
        }
    }
}