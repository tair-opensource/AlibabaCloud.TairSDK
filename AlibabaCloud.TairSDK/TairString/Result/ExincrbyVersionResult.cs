namespace AlibabaCloud.TairSDK.TairString.Result
{
    public class ExincrbyVersionResult
    {
        private long _value;
        private long _version;

        public ExincrbyVersionResult(long value, long version)
        {
            _value = value;
            _version = version;
        }

        public long getValue()
        {
            return _value;
        }

        public void setValue(long value)
        {
            _value = value;
        }

        public long getVersion()
        {
            return _version;
        }

        public void setVersion(long version)
        {
            _version = version;
        }
    }
}