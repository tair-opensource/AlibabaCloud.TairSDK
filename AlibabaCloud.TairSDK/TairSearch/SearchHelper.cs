using System.Collections.Generic;
using System.Text;
using AlibabaCloud.TairSDK.TairSearch.Param;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairSearch
{
    public class SearchHelper
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

        public static TFTScanResult<string> ScanResultString(RedisResult obj)
        {
            var rawResult = (RedisResult[]) obj;
            if (rawResult.Length == 0) return null;

            var res = new TFTScanResult<string>();
            res.setCursor(rawResult[0].ToString());
            var res_obj = (RedisResult[]) rawResult[1];
            var ret = new List<string>();
            foreach (var entry in res_obj)
            {
                ret.Add(entry.ToString());
            }

            res.setResult(ret);
            return res;
        }

        public static TFTScanResult<byte[]> ScanResultByte(RedisResult obj)
        {
            var rawResult = (RedisResult[]) obj;
            if (rawResult.Length == 0) return null;

            var res = new TFTScanResult<byte[]>();
            res.setCursor(Encoding.UTF8.GetBytes(rawResult[0].ToString()));
            var res_obj = (RedisResult[]) rawResult[1];
            var ret = new List<byte[]>();
            foreach (var entry in res_obj)
            {
                ret.Add(Encoding.UTF8.GetBytes(entry.ToString()));
            }

            res.setResult(ret);
            return res;
        }
    }
}