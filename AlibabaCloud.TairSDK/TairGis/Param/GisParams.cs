using System.Collections.Generic;
using System.Text;

public class GisParams : AlibabaCloud.TairSDK.Param
{
    public static string RAIDUS = "radius";
    public static string MEMBER = "member";

    private static string WITHOUTWKT = "withoutwkt";
    private static string WITHVALUE = "withvalue";
    private static string WITHOUTVALUE = "withoutvalue";
    private static string WITHDIST = "withdist";

    private static string ASC = "asc";
    private static string DESC = "desc";
    private static string COUNT = "count";

    public GisParams()
    {
    }

    public static GisParams gisParams()
    {
        return new GisParams();
    }

    public GisParams withoutWkt()
    {
        addParam(WITHOUTWKT);
        return this;
    }

    public GisParams withValue()
    {
        addParam(WITHVALUE);
        return this;
    }

    public GisParams withoutValue()
    {
        addParam(WITHOUTVALUE);
        return this;
    }

    public GisParams withDist()
    {
        addParam(WITHDIST);
        return this;
    }

    public GisParams sortAscending()
    {
        addParam(ASC);
        return this;
    }

    public GisParams sortDescending()
    {
        addParam(DESC);
        return this;
    }

    public GisParams count(int count)
    {
        if (count > 0)
        {
            addParam(COUNT, count);
        }

        return this;
    }

    public byte[][] getByteParams(params byte[][] args)
    {
        List<byte[]> byteParams = new List<byte[]>();
        foreach (byte[] arg in args)
        {
            byteParams.Add(arg);
        }

        if (contains(WITHOUTWKT))
        {
            byteParams.Add(Encoding.UTF8.GetBytes(WITHOUTWKT));
        }

        if (contains(WITHVALUE))
        {
            byteParams.Add(Encoding.UTF8.GetBytes(WITHVALUE));
        }

        if (contains(WITHOUTVALUE))
        {
            byteParams.Add(Encoding.UTF8.GetBytes(WITHOUTVALUE));
        }

        if (contains(WITHDIST))
        {
            byteParams.Add(Encoding.UTF8.GetBytes(WITHDIST));
        }

        if (contains(COUNT))
        {
            byteParams.Add(Encoding.UTF8.GetBytes(COUNT));
            byteParams.Add(Encoding.UTF8.GetBytes(getParams(COUNT).ToString()));
        }

        if (contains(ASC))
        {
            byteParams.Add(Encoding.UTF8.GetBytes(ASC));
        }
        else if (contains(DESC))
        {
            byteParams.Add(Encoding.UTF8.GetBytes(DESC));
        }

        return byteParams.ToArray();
    }
}