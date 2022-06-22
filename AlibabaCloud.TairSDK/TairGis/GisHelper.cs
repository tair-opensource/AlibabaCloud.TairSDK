using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairGis
{
    public class GisHelper
    {
        public static string geoUnitToString(GeoUnit unit)
        {
            string ret = "km";
            switch (unit)
            {
                case GeoUnit.Kilometers:
                    ret = "km";
                    break;
                case GeoUnit.Feet:
                    ret = "ft";
                    break;
                case GeoUnit.Meters:
                    ret = "m";
                    break;
                case GeoUnit.Miles:
                    ret = "mi";
                    break;
            }

            return ret;
        }

        public static bool canCovertToDouble(string str)
        {
            if (str == null) return false;
            double retval = Double.NaN;
            if (double.TryParse(str, out retval))
            {
                return true;
            }
            else if (str.Equals("inf") || str.Equals("+inf") || str.Equals("-inf"))
            {
                return true;
            }

            return false;
        }


        public static List<GisSearchResponse> SearchListResponse(RedisResult obj)
        {
            if (obj.IsNull)
            {
                return null;
            }

            var ret_obj = (RedisResult[]) obj;
            long number = (long) ret_obj[0];
            var result_raw = (RedisResult[]) ret_obj[1];
            List<GisSearchResponse> responses = new List<GisSearchResponse>();
            if (number == 0 || result_raw.Length == 0)
            {
                return responses;
            }

            int size = result_raw.Length / (int) number;
            for (int i = 0; i < number; i++)
            {
                GisSearchResponse resp = new GisSearchResponse();
                resp.setField(Encoding.UTF8.GetBytes(result_raw[i * size].ToString()));
                for (int j = i * size + 1; j < (i + 1) * size; j++)
                {
                    var tmp = (string) result_raw[j];
                    if (canCovertToDouble(tmp))
                    {
                        resp.setDistance(Convert.ToDouble(tmp));
                    }
                    else
                    {
                        resp.setValue(Encoding.UTF8.GetBytes(tmp));
                    }
                }

                responses.Add(resp);
            }

            return responses;
        }

        public static Dictionary<string, string> DictResultString(RedisResult obj)
        {
            var tmp_obj = (RedisResult[]) obj;
            Dictionary<string, string> res = new Dictionary<string, string>();
            if (tmp_obj == null || tmp_obj.Length == 0)
            {
                return res;
            }

            var rawResult = (RedisResult[]) tmp_obj[1];
            for (int i = 0; i < rawResult.Length - 1; i = i + 2)
            {
                res.Add(rawResult[i].ToString(), rawResult[i + 1].ToString());
            }

            return res;
        }

        public static Dictionary<byte[], byte[]> DictResultByte(RedisResult obj)
        {
            var tmp_obj = (RedisResult[]) obj;
            Dictionary<byte[], byte[]> res = new Dictionary<byte[], byte[]>();
            if (tmp_obj == null || tmp_obj.Length == 0)
            {
                return res;
            }

            var rawResult = (RedisResult[]) tmp_obj[1];
            for (int i = 0; i < rawResult.Length - 1; i = i + 2)
            {
                res.Add(Encoding.UTF8.GetBytes(rawResult[i].ToString()),
                    Encoding.UTF8.GetBytes(rawResult[i + 1].ToString()));
            }

            return res;
        }

        public static List<string> ResultListString(RedisResult obj)
        {
            var tmp_obj = (RedisResult[]) obj;
            List<string> res = new List<string>();
            if (tmp_obj == null || tmp_obj.Length == 0)
            {
                return res;
            }

            var rawResult = (RedisResult[]) tmp_obj[1];
            foreach (var entry in rawResult)
            {
                res.Add(entry.ToString());
            }

            return res;
        }

        public static List<byte[]> ResultListByte(RedisResult obj)
        {
            var tmp_obj = (RedisResult[]) obj;
            List<byte[]> res = new List<byte[]>();
            if (tmp_obj == null || tmp_obj.Length == 0)
            {
                return res;
            }

            var rawResult = (RedisResult[]) tmp_obj[1];
            foreach (var entry in rawResult)
            {
                res.Add(Encoding.UTF8.GetBytes(entry.ToString()));
            }

            return res;
        }
    }
}