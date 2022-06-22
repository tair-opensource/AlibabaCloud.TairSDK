namespace AlibabaCloud.TairSDK.TairString.Result
{
    public class ExcasResult<T>
    {
        private T _msg;
        private T _value;
        private long _version;

        public ExcasResult(T msg, T value, long version)
        {
            _msg = msg;
            _value = value;
            _version = version;
        }

        public T getMsg()
        {
            return _msg;
        }

        public void setMsg(T msg)
        {
            _msg = msg;
        }

        public void setVersion(long version)
        {
            _version = version;
        }

        public long getVersion()
        {
            return _version;
        }

        public T getValue()
        {
            return _value;
        }

        public void setValue(T value)
        {
            _value = value;
        }
    }
}