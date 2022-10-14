using AlibabaCloud.TairSDK.TairString;
using AlibabaCloud.TairSDK.TairString.Param;
using StackExchange.Redis;

namespace example;

public class BargainRush
{
    private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
    private static readonly TairString tairString = new(connDC, 0);

    /// <summary>
    /// bargainRush decrements the value of key from upperBound by 1 until lowerBound.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="upperBound"></param>
    /// <param name="lowerBound"></param>
    /// <returns></returns>
    public static Boolean bargainRush(string key, int upperBound, int lowerBound)
    {
        var param = new ExincrbyParams();
        param.min(lowerBound);
        param.max(upperBound);

        try
        {
            tairString.exincrBy(key, -1, param);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    static void Main(string[] args)
    {
        string key = "bargainRush";
        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine("attempt {0}, result: {1}", i, bargainRush(key, 10, 0));
        }
    }
}