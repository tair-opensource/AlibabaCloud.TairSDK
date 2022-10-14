using AlibabaCloud.TairSDK.TairZset;
using StackExchange.Redis;

namespace Example;

public class LeaderBoard
{
    private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
    private static readonly TairZset tairzset = new(connDC, 0);

    /// <summary>
    /// Add User with Multi scores.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="member"></param>
    /// <param name="scores"></param>
    /// <returns></returns>
    public static bool addUser(string key, string member, params double[] scores)
    {
        try
        {
            tairzset.exzadd(key, member, scores);
            return true;
        }
        catch (Exception e)
        {
            // logger.error(e);
            return false;
        }
    }

    /// <summary>
    /// Get the top element of the leaderboard.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="startOffset"></param>
    /// <param name="endOffset"></param>
    /// <returns></returns>
    public static List<string> top(string key, long startOffset, long endOffset)
    {
        try
        {
            return tairzset.exzrevrange(key, startOffset, endOffset);
        }
        catch (Exception e)
        {
            // logger.error(e);
            return new List<string>();
        }
    }

    static void Main(string[] args)
    {
        String key = "LeaderBoard";
        // add three user
        addUser(key, "user1", 20, 10, 30);
        addUser(key, "user2", 20, 15, 10);
        addUser(key, "user3", 30, 10, 10);
        // get top 2
        Console.WriteLine(top(key, 0, 1));
    }
}