using System;
using System.Text;
using AlibabaCloud.TairSDK.TairString.Result;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairString
{
    public class StringHelper
    {
        public static ExgetResult<string> GetResultString(RedisResult obj)
        {
            if (obj.IsNull) return null;

            var rawResult = (RedisResult[]) obj;
            if (rawResult.Length == 3)
                return new ExgetResult<string>(rawResult[0].ToString(), Convert.ToInt64(rawResult[1]),
                    Convert.ToInt64(rawResult[2]));

            return new ExgetResult<string>(new string(rawResult[0].ToString()), Convert.ToInt64(rawResult[1]));
        }

        public static ExgetResult<byte[]> GetResultByte(RedisResult obj)
        {
            if (obj.IsNull) return null;

            var rawResult = (RedisResult[]) obj;
            if (rawResult.Length == 3)
                return new ExgetResult<byte[]>(Encoding.UTF8.GetBytes(rawResult[0].ToString()),
                    Convert.ToInt64(rawResult[1]),
                    Convert.ToInt64(rawResult[2]));

            return new ExgetResult<byte[]>(Encoding.UTF8.GetBytes(rawResult[0].ToString()),
                Convert.ToInt64(rawResult[1]));
        }

        public static ExgetResult<string> GetFlagString(RedisResult obj)
        {
            if (obj.Type == ResultType.BulkString) return null;

            var rawResult = (RedisResult[]) obj;

            return new ExgetResult<string>(Convert.ToString(rawResult[0]), Convert.ToInt64(rawResult[1]),
                Convert.ToInt64(rawResult[2]));
        }

        public static ExgetResult<byte[]> GetFlagByte(RedisResult obj)
        {
            if (obj.Type == ResultType.BulkString) return null;

            var rawResult = (RedisResult[]) obj;
            return new ExgetResult<byte[]>(Encoding.UTF8.GetBytes(rawResult[0].ToString()),
                Convert.ToInt64(rawResult[1]),
                Convert.ToInt64(rawResult[2]));
        }


        public static ExincrbyVersionResult IncrbyVersionResult(RedisResult obj)
        {
            if (obj.IsNull) return null;

            var rawResult = (RedisResult[]) obj;
            return new ExincrbyVersionResult(Convert.ToInt64(rawResult[0]), Convert.ToInt64(rawResult[1]));
        }


        public static ExcasResult<string> CasResultString(RedisResult obj)
        {
            if (obj.Type == ResultType.Integer && (long) obj == -1d) return null;

            var rawResult = (RedisResult[]) obj;
            return new ExcasResult<string>(Convert.ToString(rawResult[0]), Convert.ToString(rawResult[1]),
                Convert.ToInt64(rawResult[2]));
        }

        public static ExcasResult<byte[]> CasResultByte(RedisResult obj)
        {
            if (obj.Type == ResultType.Integer && (long) obj == -1d) return null;

            var rawResult = (RedisResult[]) obj;
            return new ExcasResult<byte[]>(Encoding.UTF8.GetBytes(rawResult[0].ToString()),
                Encoding.UTF8.GetBytes(rawResult[1].ToString()),
                Convert.ToInt64(rawResult[2]));
        }
    }
}