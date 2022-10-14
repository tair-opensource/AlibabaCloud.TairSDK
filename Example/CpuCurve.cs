using AlibabaCloud.TairSDK.TairTs;
using AlibabaCloud.TairSDK.TairTs.Result;
using StackExchange.Redis;

namespace Example;

public class CpuCurve
{
    private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
    private static readonly TairTs tairTs = new(connDC, 0);

    /// <summary>
    /// dd point to CPU_LOAD series.
    /// </summary>
    /// <param name="ip">mechine ip</param>
    /// <param name="ts">the timestamp</param>
    /// <param name="value">the value</param>
    /// <returns></returns>
    public static bool addPoint(string ip, string ts, double value)
    {
        try
        {
            var result = tairTs.extsadd("CPU_LOAD", ip, ts, value);
            if (result.Equals("OK"))
            {
                return true;
            }
        }
        catch (Exception e)
        {
            // logger.error(e);
        }

        return false;
    }

    /// <summary>
    /// Range all data in a certain time series.
    /// </summary>
    /// <param name="ip">machine ip</param>
    /// <param name="startTs">start timestamp</param>
    /// <param name="endTs">end timestamp</param>
    /// <returns></returns>
    public static ExtsRangeResult rangePoint(string ip, string startTs, string endTs)
    {
        try
        {
            return tairTs.extsrange("CPU_LOAD", ip, startTs, endTs);
        }
        catch (Exception e)
        {
            // logger.error(e);
        }

        return null;
    }

    static void Main(string[] args)
    {
        addPoint("127.0.0.1", "*", 10);
        addPoint("127.0.0.1", "*", 20);
        addPoint("127.0.0.1", "*", 30);

        rangePoint("127.0.0.1", "1587889046161", "*");
    }
}