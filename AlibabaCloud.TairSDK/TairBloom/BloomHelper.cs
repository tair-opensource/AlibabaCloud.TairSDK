using System.Collections.Generic;
using System.Text;

namespace AlibabaCloud.TairSDK.TairBloom
{
    public class BloomHelper
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
    }
}