using System.Collections.Generic;
using System.Text;
using AlibabaCloud.TairSDK.TairHash.Param;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairHash
{
    public class HashHepler
    {
        public static byte[][] encodemany(params string[] values)
        {
            List<byte[]> args = new List<byte[]>();
            foreach (var val in values)
            {
                args.Add(Encoding.UTF8.GetBytes(val));
            }

            return args.ToArray();
        }

        public static ExhgetwithverResult<string> GetVerResultString(RedisResult obj)
        {
            if (obj.IsNull) return null;

            var rawResult = (RedisResult[]) obj;
            if (rawResult.Length == 0) return null;

            var value = rawResult[0].ToString();
            var version = (long) rawResult[1];
            return new ExhgetwithverResult<string>(value, version);
        }

        public static ExhgetwithverResult<byte[]> GetVerResultByte(RedisResult obj)
        {
            if (obj.IsNull) return null;

            var rawResult = (RedisResult[]) obj;
            if (rawResult.Length == 0) return null;

            var value = Encoding.UTF8.GetBytes(rawResult[0].ToString());
            var version = (long) rawResult[1];
            return new ExhgetwithverResult<byte[]>(value, version);
        }

        public static List<ExhgetwithverResult<string>> HmgetVerResultString(RedisResult obj)
        {
            if (obj.IsNull) return null;

            var raw_result = (RedisResult[]) obj;
            var results = new List<ExhgetwithverResult<string>>();
            foreach (var entry in raw_result)
            {
                if (entry.IsNull)
                {
                    results.Add(new ExhgetwithverResult<string>());
                }
                else
                {
                    var lo = (RedisResult[]) entry;
                    var value = lo[0].ToString();
                    var version = (long) lo[1];
                    results.Add(new ExhgetwithverResult<string>(value, version));
                }
            }

            return results;
        }


        public static List<ExhgetwithverResult<byte[]>> HmgetVerResultByte(RedisResult obj)
        {
            if (obj.IsNull) return null;

            var rawResult = (RedisResult[]) obj;
            var results = new List<ExhgetwithverResult<byte[]>>();
            foreach (var entry in rawResult)
                if (entry.IsNull)
                {
                    results.Add(new ExhgetwithverResult<byte[]>());
                }
                else
                {
                    var lo = (RedisResult[]) entry;
                    var value = (byte[]) lo[0];
                    var version = (long) lo[1];
                    results.Add(new ExhgetwithverResult<byte[]>(value, version));
                }

            return results;
        }

        public static HashSet<string> HkeysResultString(RedisResult obj)
        {
            if (obj.IsNull) return null;

            var rawResult = (RedisResult[]) obj;

            var res = new HashSet<string>();
            foreach (var tmp in rawResult) res.Add(tmp.ToString());

            return res;
        }

        public static HashSet<byte[]> HkeysResultByte(RedisResult obj)
        {
            if (obj.IsNull) return null;

            var rawResult = (RedisResult[]) obj;
            var res = new HashSet<byte[]>();
            foreach (var tmp in rawResult) res.Add((byte[]) tmp);

            return res;
        }

        public static Dictionary<string, string> HgetallResultString(RedisResult obj)
        {
            var rawResult = (RedisResult[]) obj;
            var res = new Dictionary<string, string>();
            if (rawResult.Length == 0) return res;

            for (var i = 0; i < rawResult.Length - 1; i = i + 2)
                res.Add(rawResult[i].ToString(), rawResult[i + 1].ToString());

            return res;
        }

        public static Dictionary<byte[], byte[]> HgetallResultByte(RedisResult obj)
        {
            var rawResult = (RedisResult[]) obj;
            var res = new Dictionary<byte[], byte[]>();
            if (rawResult.Length == 0) return res;

            for (var i = 0; i < rawResult.Length - 1; i = i + 2)
                res.Add((byte[]) rawResult[i], (byte[]) rawResult[i + 1]);

            return res;
        }

        public static ExhscanResult<string> ScanResultString(RedisResult obj)
        {
            var rawResult = (RedisResult[]) obj;
            if (rawResult.Length == 0) return null;

            var res = new ExhscanResult<string>();
            res.setCursor(rawResult[0].ToString());
            var res_obj = rawResult[1].ToDictionary();
            var ret = new Dictionary<string, string>(res_obj.Count);
            foreach (var entry in res_obj) ret.Add(entry.Key, entry.Value.ToString());

            res.setResult(ret);

            return res;
        }

        public static ExhscanResult<byte[]> ScanResultByte(RedisResult obj)
        {
            var rawResult = (RedisResult[]) obj;
            if (rawResult.Length == 0) return null;

            var res = new ExhscanResult<byte[]>();
            res.setCursor(Encoding.UTF8.GetBytes(rawResult[0].ToString()));
            var res_obj = rawResult[1].ToDictionary();
            var ret = new Dictionary<byte[], byte[]>(res_obj.Count);
            foreach (var entry in res_obj)
                ret.Add(Encoding.UTF8.GetBytes(entry.Key), Encoding.UTF8.GetBytes(entry.Value.ToString()));

            res.setResult(ret);

            return res;
        }
    }
}