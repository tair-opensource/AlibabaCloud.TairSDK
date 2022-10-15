using AlibabaCloud.TairSDK.TairRoaring;
using StackExchange.Redis;

namespace Example;

public class CrowdSelection
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
    public static bool setBit(string key, long offset, long value)
    {
        try
        {
            tairRoaring.trsetbit(key, offset, value);
            return true;
        }
        catch (Exception e)
        {
            // logger.error(e);
            return false;
        }
    }

    /// <summary>
    /// Get key offset value.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="offset"></param>
    /// <returns>he offset value, if not exists, return 0</returns>
    public static long getBit(string key, long offset)
    {
        try
        {
            return tairRoaring.trgetbit(key, offset);
        }
        catch (Exception e)
        {
            // logger.error(e);
            return -1;
        }
    }

    /// <summary>
    /// AND the two bitmaps and store the result in a new destkey.
    /// </summary>
    /// <param name="destkey"></param>
    /// <param name="keys"></param>
    /// <returns></returns>
    public static bool bitAnd(string destkey, params string[] keys)
    {
        try
        {
            tairRoaring.trbitop(destkey, "AND", keys);
            return true;
        }
        catch (Exception e)
        {
            // logger.error(e);
            return false;
        }
    }

    static void Main(string[] args)
    {
        String key1 = "CrowdSelection-1";
        String key2 = "CrowdSelection-2";
        String key3 = "CrowdSelection-destKey";
        setBit(key1, 0, 1);
        setBit(key1, 1, 1);
        setBit(key2, 1, 1);
        Console.WriteLine(getBit(key1, 0));
        bitAnd(key3, key1, key2);
        Console.WriteLine(getBit(key3, 0));
        Console.WriteLine(getBit(key3, 1));
    }
}