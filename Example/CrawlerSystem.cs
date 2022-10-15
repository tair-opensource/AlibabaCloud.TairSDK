using AlibabaCloud.TairSDK.TairBloom;
using StackExchange.Redis;

namespace Example;

public class CrawlerSystem
{
    private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
    private static readonly TairBloom tairBloom = new(connDC, 0);

    /// <summary>
    /// Determine if the URL has been crawled.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="urls"></param>
    /// <returns></returns>
    public static bool[] bfMexists(string key, params string[] urls)
    {
        try
        {
            return tairBloom.bfmexists(key, urls);
        }
        catch (Exception e)
        {
            // logger.error(e);
            return null;
        }
    }

    static void Main(string[] args)
    {
        String key = "CrawlerSystem";
        tairBloom.bfadd(key, "abc");
        tairBloom.bfadd(key, "def");
        tairBloom.bfadd(key, "ghi");
        Console.WriteLine(bfMexists(key, "abc", "def", "xxx"));
    }
}