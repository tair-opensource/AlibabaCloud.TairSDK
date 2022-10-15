using AlibabaCloud.TairSDK.TairSearch;
using StackExchange.Redis;

namespace Example;

public class FullTextSearch
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
    /// Add doc to index, doc is JSON format.
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
        String key = "FullTextSearch";
        // create index
        createIndex(key, "{\"mappings\":{\"properties\":{\"title\":{\"type\":\"keyword\"},"
                         + "\"content\":{\"type\":\"text\",\"analyzer\":\"jieba\"},\"time\":{\"type\":\"long\"},"
                         + "\"author\":{\"type\":\"keyword\"},\"heat\":{\"type\":\"integer\"}}}}");
        // add doc
        addDoc(key, "{\"title\":\"Does not work\",\"content\":\"It was removed from the beta a while ago. You should "
                    + "have expected it was going to be removed from the stable client as well at some point.\","
                    + "\"time\":1541713787,\"author\":\"cSg|mc\",\"heat\":10}");
        addDoc(key, "{\"title\":\"paypal no longer launches to purchase\",\"content\":\"Since the last update, I "
                    + "cannot purchase anything via the app. I just keep getting a screen that says\",\"time\":1551476987,"
                    + "\"author\":\"disasterpeac\",\"heat\":2}");
        addDoc(key, "{\"title\":\"cat not login\",\"content\":\"Hey! I am trying to login to steam beta client via qr"
                    + " code / steam guard code but both methods does not work for me\",\"time\":1664488187,"
                    + "\"author\":\"7xx\",\"heat\":100}");
        // search index
        String request = "{\"sort\":[{\"heat\":{\"order\":\"desc\"}}],\"query\":{\"match\":{\"content\":\"paypal work"
                         + " code\"}}}";
        Console.WriteLine(searchIndex(key, request));
    }
}