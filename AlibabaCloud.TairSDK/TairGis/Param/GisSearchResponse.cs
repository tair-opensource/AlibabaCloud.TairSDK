using System.Text;

public class GisSearchResponse
{
    private byte[] field;
    private byte[] value;
    private double distance;

    public GisSearchResponse()
    {
    }

    public byte[] getField()
    {
        return field;
    }

    public string getFieldByString()
    {
        if (field == null)
        {
            return null;
        }

        return Encoding.UTF8.GetString(field);
    }

    public void setField(byte[] field)
    {
        this.field = field;
    }

    public byte[] getValue()
    {
        return value;
    }

    public string getValueByString()
    {
        if (value == null)
        {
            return null;
        }

        return Encoding.UTF8.GetString(value);
    }

    public void setValue(byte[] value)
    {
        this.value = value;
    }

    public double getDistance()
    {
        return distance;
    }

    public void setDistance(double distance)
    {
        this.distance = distance;
    }

    public string toString()
    {
        return "GisSearchResponse" + "field=" + getFieldByString() + ",vlaue=" + getValueByString() + ",distance=" +
               distance + "}";
    }
}