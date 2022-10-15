using AlibabaCloud.TairSDK.TairGis;
using StackExchange.Redis;

namespace Example;

public class LbsBuy
{
    private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
    private static readonly TairGis tairGis = new(connDC, 0);

    /// <summary>
    /// Add a service store geographical scope.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="storeName"></param>
    /// <param name="storeWkt"></param>
    /// <returns></returns>
    public static bool addPolygon(string key, string storeName, String storeWkt)
    {
        try
        {
            long ret = tairGis.gisadd(key, storeName, storeWkt);
            return ret == 1;
        }
        catch (Exception e)
        {
            // logger.error(e);
        }

        return false;
    }

    /// <summary>
    /// Determine whether the user's location is within the service range of the store.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="userLocation"></param>
    /// <returns></returns>
    public static Dictionary<string, string> getServiceStore(string key, string userLocation)
    {
        try
        {
            return tairGis.giscontains(key, userLocation);
        }
        catch (Exception e)
        {
            // logger.error(e);
            return null;
        }
    }

    static void Main(string[] args)
    {
        String key = "LbsBuy";
        addPolygon(key, "store-1",
            "POLYGON ((120.058897 30.283681, 120.093033 30.286363, 120.097632 30.269147, 120.050705 30.252863))");
        addPolygon(key, "store-2",
            "POLYGON ((120.026343 30.285739, 120.029289 30.280749, 120.0382 30.281997, 120.037051 30.288109))");

        Console.WriteLine(getServiceStore(key, "POINT(120.072264 30.27501)"));
    }
}