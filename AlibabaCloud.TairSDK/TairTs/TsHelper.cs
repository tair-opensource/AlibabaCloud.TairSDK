using System;
using System.Collections.Generic;
using AlibabaCloud.TairSDK.TairTs.Result;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairTs
{
    public class TsHelper
    {
        public static ExtsDataPointResult GetResult(RedisResult obj)
        {
            if (obj.IsNull)
            {
                return null;
            }

            var rawResult = (RedisResult[]) obj;
            return new ExtsDataPointResult(Convert.ToInt64(rawResult[0]), Convert.ToString(rawResult[1]));
        }

        public static ExtsLabelResult LabelResult(RedisResult obj)
        {
            if (obj.IsNull)
            {
                return null;
            }

            var rawResult = (RedisResult[]) obj;
            return new ExtsLabelResult(rawResult[0].ToString(), rawResult[1].ToString());
        }

        public static ExtsRangeResult RangeResult(RedisResult obj)
        {
            if (obj.IsNull)
            {
                return null;
            }

            var rawResult = (RedisResult[]) obj;
            List<ExtsDataPointResult> datapointList = new List<ExtsDataPointResult>();
            var rawPoint = (RedisResult[]) rawResult[0];
            foreach (var entry in rawPoint)
            {
                datapointList.Add(GetResult(entry));
            }

            return new ExtsRangeResult(datapointList, Convert.ToInt64(rawResult[1]));
        }

        public static List<ExtsMrangeResult> MrangeResult(RedisResult obj)
        {
            if (obj.IsNull)
            {
                return null;
            }

            var rawResult = (RedisResult[]) obj;
            List<ExtsMrangeResult> results = new List<ExtsMrangeResult>();
            for (int i = 0; i < rawResult.Length; i++)
            {
                var subl = (RedisResult[]) rawResult[i];
                var rawLabel = (RedisResult[]) subl[1];
                var rawPoint = (RedisResult[]) subl[2];
                List<ExtsDataPointResult> datapointList = new List<ExtsDataPointResult>();
                List<ExtsLabelResult> labelList = new List<ExtsLabelResult>();
                foreach (var entry in rawLabel)
                {
                    labelList.Add(LabelResult(entry));
                }

                foreach (var entry in rawPoint)
                {
                    datapointList.Add(GetResult(entry));
                }

                results.Add(
                    new ExtsMrangeResult(subl[0].ToString(), labelList, datapointList, Convert.ToInt64(subl[3])));
            }

            return results;
        }
    }
}