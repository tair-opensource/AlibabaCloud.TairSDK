using AlibabaCloud.TairSDK.TairRoaring;
using StackExchange.Redis;

namespace example;

public class BitCount
{
    private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
    private static readonly TairRoaring tairRoaring = new(connDC, 0);

    /// <summary>
    /// Set key offset value, value can be 0 or 1.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="offset"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Boolean setBit(string key, long offset, long value)
    {
        try
        {
            tairRoaring.trsetbit(key, offset, value);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    /// <summary>
    /// Count the number of elements in the bitmap.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static long bitCount(string key)
    {
        try
        {
            return tairRoaring.trbitcount(key);
        }
        catch (Exception e)
        {
            return -1;
        }
    }

    static void Main(string[] args)
    {
        string key1 = "BitCount";
        setBit(key1, 0, 1);
        setBit(key1, 1, 1);
        setBit(key1, 2, 1);
        Console.WriteLine(bitCount(key1));
    }
}