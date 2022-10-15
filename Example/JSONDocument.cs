using AlibabaCloud.TairSDK.TairDoc;
using StackExchange.Redis;

namespace Example;

public class JSONDocument
{
    private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
    private static readonly TairDoc tairDoc = new(connDC, 0);

    /// <summary>
    /// Save JSON in key at path.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="path"></param>
    /// <param name="json"></param>
    /// <returns></returns>
    public static bool jsonSave(string key, string path, string json)
    {
        try
        {
            var result = tairDoc.jsonset(key, path, json);
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
    /// Get JSON elements from path
    /// </summary>
    /// <param name="key"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string jsonGet(string key, string path)
    {
        try
        {
            return tairDoc.jsonget(key, path);
        }
        catch (Exception e)
        {
            // logger.error(e);
            return null;
        }
    }

    static void Main(string[] args)
    {
        String key = "JSONDocument";
        jsonSave(key, ".", "{\"name\":\"tom\",\"age\":22,\"description\":\"A man with a blue lightsaber\","
                           + "\"friends\":[]}");
        Console.WriteLine(jsonGet(key, ".description"));
    }
}