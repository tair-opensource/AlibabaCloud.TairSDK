namespace AlibabaCloud.TairSDK.TairString.Result
{
    public class ExgetResult<T>
    {
        private long _flags;
        private T _value;
        private long _version;

        public ExgetResult(T value, long version)
        {
            _value = value;
            _version = version;
        }

        public ExgetResult(T value, long version, long flags)
        {
            _version = version;
            _value = value;
            _flags = flags;
        }

        public long getVersion()
        {
            return _version;
        }

        public void setVersion(long version)
        {
            _version = version;
        }

        public T getValue()
        {
            return _value;
        }

        public void setValue(T value)
        {
            _value = value;
        }

        public long getFlags()
        {
            return _flags;
        }

        public void setFlags(long flags)
        {
            _flags = flags;
        }
    }
}