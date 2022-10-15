using AlibabaCloud.TairSDK.TairString;
using StackExchange.Redis;

namespace Example;

public class DistributedLock
{
    private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
    private static readonly TairString tair = new(connDC, 0);
    private static readonly IDatabase db = connDC.GetDatabase(0);

    /// <summary>
    /// locks atomically via set with NX flag.
    /// </summary>
    /// <param name="lockkey"></param>
    /// <param name="requestId"></param>
    /// <param name="expireTime"></param>
    /// <returns></returns>
    public static bool tryGetDistributedLock(string lockkey, string requestId, int expireTime)
    {
        try
        {
            var ret = db.StringSet(lockkey, requestId, TimeSpan.FromSeconds(expireTime), When.NotExists,
                CommandFlags.None);
            if (ret)
            {
                return ret;
            }
        }
        catch (Exception e)
        {
            // logger.error(e);
        }

        return false;
    }

    /// <summary>
    /// atomically releases the lock via the CAD command.
    /// </summary>
    /// <param name="lockkey"></param>
    /// <param name="requestId"></param>
    /// <returns></returns>
    public static bool releaseDistributedLock(string lockkey, string requestId)
    {
        try
        {
            var ret = tair.cad(lockkey, requestId);
            return ret == 1;
        }
        catch (Exception e)
        {
            // logger.error(e);
            return false;
        }
    }

    static void Main(string[] args)
    {
        string key = "distributedkey";
        string requsetid = "requestid";
        int expire = 3;
        Console.WriteLine(tryGetDistributedLock(key, requsetid, expire));
        Thread.Sleep(4000);
        Console.WriteLine(releaseDistributedLock(key, requsetid));
    }
}