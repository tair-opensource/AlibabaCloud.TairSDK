using AlibabaCloud.TairSDK.TairBloom;
using StackExchange.Redis;

namespace Example;

public class BloomFilter
{
    private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
    private static readonly TairBloom tairBloom = new(connDC, 0);

    /// <summary>
    /// Recommend the doc to the user, ignore it if it has been recommended, otherwise recommend it and mark it.
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="docid"></param>
    public static void recommendedSystem(string userid, string docid)
    {
        if (tairBloom.bfexists(userid, docid))
        {
            //do nothing
        }
        else
        {
            // recommend to user sendRecommendMsg(docid);
            // add userid with docid
            tairBloom.bfadd(userid, docid);
        }
    }

    static void Main(string[] args)
    {
        string key = "BloomFilter";
        recommendedSystem(key, Guid.NewGuid().ToString("N"));
        recommendedSystem(key, Guid.NewGuid().ToString("N"));
        recommendedSystem(key, Guid.NewGuid().ToString("N"));
    }
}