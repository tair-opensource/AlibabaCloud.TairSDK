using AlibabaCloud.TairSDK.TairCpc;
using StackExchange.Redis;

namespace Example;

public class FraudPrevention
{
    private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
    private static readonly TairCpc tairCpc = new(connDC, 0);

    /// <summary>
    /// update item to key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static bool cpcAdd(string key, string item)
    {
        try
        {
            string ret = tairCpc.cpcUpdate(key, item);
            return ret.Equals("OK");
        }
        catch (Exception e)
        {
            // logger.error(e);
            return false;
        }
    }

    /// <summary>
    /// Estimate all quantities in cpc.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static double cpcEstimate(string key)
    {
        try
        {
            return tairCpc.cpcEstimate(key);
        }
        catch (Exception e)
        {
            // logger.error(e);
            return -1;
        }
    }

    static void Main(string[] args)
    {
        String key = "FraudPrevention";
        cpcAdd(key, "a");
        cpcAdd(key, "b");
        cpcAdd(key, "c");
        Console.WriteLine(cpcEstimate(key));
        cpcAdd(key, "d");
        Console.WriteLine(cpcEstimate(key));
    }
}