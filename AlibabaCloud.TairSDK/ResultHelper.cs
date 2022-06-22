using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK
{
    public class ResultHelper
    {
        public static double Double(RedisResult obj)
        {
            return Convert.ToDouble(obj);
        }

        public static long Long(RedisResult obj)
        {
            return Convert.ToInt64(obj);
        }

        public static string String(RedisResult obj)
        {
            return obj.ToString();
        }

        public static byte[] ByteArray(RedisResult obj)
        {
            return Encoding.UTF8.GetBytes(obj.ToString());
        }

        public static bool Bool(RedisResult obj)
        {
            return Convert.ToBoolean(obj);
        }

        public static bool[] BoolArray(RedisResult obj)
        {
            if (obj.IsNull)
            {
                return null;
            }

            var rawResult = (RedisResult[]) obj;
            bool[] res = new bool[rawResult.Length];
            for (int i = 0; i < rawResult.Length; i++)
            {
                res[i] = (Convert.ToInt64(rawResult[i]) != 0);
            }

            return res;
        }

        public static List<string> ListString(RedisResult obj)
        {
            if (obj.IsNull)
            {
                return null;
            }

            var rawResult = (RedisResult[]) obj;
            List<string> res = new List<string>();
            foreach (var entry in rawResult)
            {
                res.Add(entry.ToString());
            }

            return res;
        }

        public static List<byte[]> ListByte(RedisResult obj)
        {
            if (obj.IsNull)
            {
                return null;
            }

            var rawResult = (RedisResult[]) obj;
            List<byte[]> res = new List<byte[]>();
            foreach (var entry in rawResult)
            {
                res.Add((byte[]) entry);
            }

            return res;
        }

        public static List<long> ListLong(RedisResult obj)
        {
            if (obj.IsNull)
            {
                return null;
            }

            var rawResult = (RedisResult[]) obj;
            List<long> res = new List<long>();
            foreach (var entry in rawResult)
            {
                res.Add(Convert.ToInt64(entry));
            }

            return res;
        }

        public static Dictionary<string, string> DictString(RedisResult obj)
        {
            if (obj.IsNull) return null;
            var rawResult = (RedisResult[]) obj;
            var res = new Dictionary<string, string>();
            if (rawResult.Length == 0) return res;

            for (var i = 0; i < rawResult.Length - 1; i = i + 2)
                res.Add(rawResult[i].ToString(), rawResult[i + 1].ToString());

            return res;
        }

        public static Dictionary<byte[], byte[]> DictByte(RedisResult obj)
        {
            if (obj.IsNull) return null;
            var rawResult = (RedisResult[]) obj;
            var res = new Dictionary<byte[], byte[]>();
            if (rawResult.Length == 0) return res;

            for (var i = 0; i < rawResult.Length - 1; i = i + 2)
                res.Add((byte[]) rawResult[i], (byte[]) rawResult[i + 1]);

            return res;
        }
    }
}