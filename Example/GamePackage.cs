using AlibabaCloud.TairSDK.TairDoc;
using StackExchange.Redis;

namespace Example;

public class GamePackage
{
    private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
    private static readonly TairDoc tairDoc = new(connDC, 0);

    /// <summary>
    /// dd equipment to package.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="packetPath"></param>
    /// <param name="equipment"></param>
    /// <returns></returns>
    public static long addEquipment(string key, string packetPath, string equipment)
    {
        try
        {
            return tairDoc.jsonarrAppend(key, packetPath, equipment);
        }
        catch (Exception e)
        {
            // logger.error(e);
            return -1;
        }
    }

    static void Main(string[] args)
    {
        String key = "GamePackage";
        tairDoc.jsonset(key, ".", "[]");
        Console.WriteLine(addEquipment(key, ".", "\"lightsaber\""));
        Console.WriteLine(addEquipment(key, ".", "\"howitzer\""));
        Console.WriteLine(addEquipment(key, ".", "\"gun\""));
    }
}