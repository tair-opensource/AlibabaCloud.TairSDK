using System;
using System.Collections.Generic;
using AlibabaCloud.TairSDK.TairCpc.Result;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairCpc
{
    public class CpcHelper
    {
        public static Update2JudResult UpdateResult(RedisResult obj)
        {
            if (obj.IsNull)
            {
                return null;
            }

            var rawResult = (RedisResult[]) obj;
            string valueStr = rawResult[0].ToString();
            string diffValueStr = rawResult[1].ToString();
            return new Update2JudResult(Convert.ToDouble(valueStr), Convert.ToDouble(diffValueStr));
        }

        public static List<double> ListDouble(RedisResult obj)
        {
            if (obj.IsNull)
            {
                return null;
            }

            var rawResult = (RedisResult[]) obj;
            List<double> res = new List<double>();
            for (int i = 0; i < rawResult.Length; i++)
            {
                res.Add(Convert.ToDouble(rawResult[i]));
            }

            return res;
        }
    }
}