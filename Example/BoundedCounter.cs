using StackExchange.Redis;

namespace Example;

public class BoundedCounter
{
    private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");

    /// <summary>
    /// tryAcquire is thread-safe and will increment the key from 0 to the upper bound within an interval of time.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="upperBound"></param>
    /// <param name="initerval"></param>
    /// <returns></returns>
    public static bool tryAcquire(string key, int upperBound, int initerval)
    {
        try
        {
            var Script = "if redis.call('exists', @KEYS) == 1 "
                         + "then return redis.call('EXINCRBY', @KEYS, '1', 'MAX', @ARGV1, 'KEEPTTL') "
                         + "else return redis.call('EXSET', @KEYS, 0, 'EX', @ARGV2) end";
            var db = connDC.GetDatabase(0);
            var prepard = LuaScript.Prepare(Script);
            var ret = db.ScriptEvaluate(prepard, new {KEYS = (RedisKey) key, ARGV1 = upperBound, ARGV2 = initerval});
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
        String key = "rateLimiter";
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine("attempt {0}, result: {1}", i, tryAcquire(key, 8, 10));
        }
    }
}