using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairRoaring
{
    public static class RoaringHelper
    {
        public static byte[][] longTobyte(params long[] values)
        {
            List<byte[]> args = new List<byte[]>();
            foreach (var val in values)
            {
                args.Add(Encoding.UTF8.GetBytes(val.ToString()));
            }

            return args.ToArray();
        }

        public static byte[][] encodemany(params string[] values)
        {
            List<byte[]> args = new List<byte[]>();
            foreach (var val in values)
            {
                args.Add(Encoding.UTF8.GetBytes(val));
            }

            return args.ToArray();
        }

        public static ScanResult<long> ScanResultLong(RedisResult obj)
        {
            if (obj.IsNull)
            {
                return null;
            }

            var ret_obj = (RedisResult[]) obj;
            List<long> result = new List<long>();
            long cursor = Convert.ToInt64(ret_obj[0]);
            var rawResult = (RedisResult[]) ret_obj[1];
            foreach (var val in rawResult)
            {
                result.Add(Convert.ToInt64(val));
            }

            return new ScanResult<long>(cursor, result);
        }

        public static ScanResult<byte[]> ScanResultByte(RedisResult obj)
        {
            if (obj.IsNull)
            {
                return null;
            }

            var ret_obj = (RedisResult[]) obj;
            List<byte[]> result = new List<byte[]>();
            byte[] cursor = Encoding.UTF8.GetBytes(ret_obj[0].ToString());
            var rawResult = (RedisResult[]) ret_obj[1];
            foreach (var val in rawResult)
            {
                result.Add(Encoding.UTF8.GetBytes(val.ToString()));
            }

            return new ScanResult<byte[]>(cursor, result);
        }
    }
}