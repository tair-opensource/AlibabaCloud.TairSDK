using AlibabaCloud.TairSDK.TairHash;
using AlibabaCloud.TairSDK.TairHash.Param;
using StackExchange.Redis;

namespace Example;

public class DeviceLogin
{
    private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
    private static readonly TairHash tairHash = new(connDC, 0);

    /// <summary>
    /// Record the login time and device name of the device, and set the login status expiration time.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="loginTime"></param>
    /// <param name="device"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public static bool deviceLogin(string key, string loginTime, string device, int timeout)
    {
        try
        {
            long ret = tairHash.exhset(key, loginTime, device, new ExhsetParams().ex(timeout));
            return ret == 1;
        }
        catch (Exception e)
        {
            // logger.error(e);
            return false;
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
        String key = "DeviceLogin";
        deviceLogin(key, CurrentTimeMillis().ToString(), "device1", 2);
        deviceLogin(key, CurrentTimeMillis().ToString(), "device2", 10);
        Thread.Sleep(5000);
        Console.WriteLine(tairHash.exhgetAll(key));
    }
}