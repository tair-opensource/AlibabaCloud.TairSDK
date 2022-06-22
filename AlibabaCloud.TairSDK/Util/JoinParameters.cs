using System;

namespace AlibabaCloud.TairSDK.Util
{
    public class JoinParameters
    {
        public static byte[][] joinParameters(byte[] first, byte[][] rest)
        {
            var result = new byte[rest.Length + 1][];
            result[0] = first;
            Array.Copy(rest, 0, result, 1, rest.Length);
            return result;
        }

        public static string[] joinParameters(string first, string[] rest)
        {
            var result = new string[rest.Length + 1];
            result[0] = first;
            Array.Copy(rest, 0, result, 1, rest.Length);
            return result;
        }

        public static byte[][] joinParameters(byte[] first, byte[] second, byte[][] rest)
        {
            var result = new byte[rest.Length + 2][];
            result[0] = first;
            result[1] = second;
            Array.Copy(rest, 0, result, 2, rest.Length);
            return result;
        }

        public static string[] joinParameters(string first, string second, string[] rest)
        {
            var result = new string[rest.Length + 2];
            result[0] = first;
            result[1] = second;
            Array.Copy(rest, 0, result, 2, rest.Length);
            return result;
        }
    }
}