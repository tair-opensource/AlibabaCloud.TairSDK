using AlibabaCloud.TairSDK.TairHash;
using AlibabaCloud.TairSDK.TairHash.Param;
using StackExchange.Redis;

namespace Example;

public class PasswordExpire
{
    private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
    private static readonly TairHash tairHash = new(connDC, 0);

    /// <summary>
    /// Add a user and password with a timeout.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public static bool addUserPass(string key, string user, string password, int timeout)
    {
        try
        {
            var ret = tairHash.exhset(key, user, password, new ExhsetParams().ex(timeout));
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
        String key = "PasswordExpire";
        addUserPass(key, "user1", "password1", 5);
        addUserPass(key, "user2", "password2", 10);
        Thread.Sleep(5000);
        Console.WriteLine(tairHash.exhgetAll(key));
    }
}