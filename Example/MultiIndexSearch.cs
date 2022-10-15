using AlibabaCloud.TairSDK.TairSearch;
using StackExchange.Redis;

namespace Example;

public class MultiIndexSearch
{
    private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
    private static readonly TairSearch tairSearch = new(connDC, 0);

    /// <summary>
    /// create index, The field of index is parsed according to the field corresponding to the text.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="schema"></param>
    /// <returns></returns>
    public static bool createIndex(string index, string schema)
    {
        try
        {
            tairSearch.tftcreateindex(index, schema);
            return true;
        }
        catch (Exception e)
        {
            // logger.error(e);
            return false;
        }
    }

    /// <summary>
    ///  Add doc to index, doc is JSON format.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="doc"></param>
    /// <returns></returns>
    public static string addDoc(string index, string doc)
    {
        try
        {
            return tairSearch.tftadddoc(index, doc);
        }
        catch (Exception e)
        {
            // logger.error(e);
            return null;
        }
    }

    /// <summary>
    /// search index by request.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public static string searchIndex(string index, string request)
    {
        try
        {
            return tairSearch.tftsearch(index, request);
        }
        catch (Exception e)
        {
            // logger.error(e);
            return null;
        }
    }

    static void Main(string[] args)
    {
        string key = "MultiIndexSearch";
        // create index
        createIndex(key, "{\"mappings\":{\"properties\":{\"departure\":{\"type\":\"keyword\"},"
                         + "\"destination\":{\"type\":\"keyword\"},\"date\":{\"type\":\"keyword\"},"
                         + "\"seat\":{\"type\":\"keyword\"},\"with\":{\"type\":\"keyword\"},\"flight_id\":{\"type\":\"keyword\"},"
                         + "\"price\":{\"type\":\"double\"},\"departure_time\":{\"type\":\"long\"},"
                         + "\"destination_time\":{\"type\":\"long\"}}}}");
        // add doc
        addDoc(key, "{\"departure\":\"zhuhai\",\"destination\":\"hangzhou\",\"date\":\"2022-09-01\","
                    + "\"seat\":\"first\",\"with\":\"baby\",\"flight_id\":\"CZ1000\",\"price\":986.1,"
                    + "\"departure_time\":1661991010,\"destination_time\":1661998210}");

        // search index
        String request = "{\"sort\":[\"departure_time\"],\"query\":{\"bool\":{\"must\":[{\"term\":{\"date\":\"2022-09"
                         + "-01\"}},{\"term\":{\"seat\":\"first\"}}]}}}";
        Console.WriteLine(searchIndex(key, request));
    }
}