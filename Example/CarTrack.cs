using AlibabaCloud.TairSDK.TairGis;
using StackExchange.Redis;

namespace Example;

public class CarTrack
{
    private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
    private static readonly TairGis tairGis = new(connDC, 0);

    /// <summary>
    /// add longitude/latitude to key, timestamp represents the current moment.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ts"></param>
    /// <param name="longitude"></param>
    /// <param name="latitude"></param>
    /// <returns></returns>
    public static Boolean addCoordinate(string key, string ts, double longitude, double latitude)
    {
        try
        {
            long ret = tairGis.gisadd(key, ts, "POINT (" + longitude + " " + latitude + ")");
            if (ret == 1)
            {
                return true;
            }
        }
        catch (Exception e)
        {
            // 
        }

        return false;
    }

    /// <summary>
    /// Get all points under a key.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static Dictionary<string, string> getAllCoordinate(string key)
    {
        try
        {
            return tairGis.gisgetall(key);
        }
        catch (Exception e)
        {
            return null;
        }
    }

    private static readonly DateTime Jan1st1970 = new DateTime
        (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static long CurrentTimeMillis()
    {
        return (long) (DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
    }

    static void Main(string[] args)
    {
        string key = "CarTrack";
        addCoordinate(key, CurrentTimeMillis().ToString(), 120.036188, 30.287922);
        Thread.Sleep(1);
        addCoordinate(key, CurrentTimeMillis().ToString(), 120.037625, 30.292225);
        Thread.Sleep(1);
        addCoordinate(key, CurrentTimeMillis().ToString(), 120.034435, 30.303303);

        Console.WriteLine(getAllCoordinate(key));
    }
}