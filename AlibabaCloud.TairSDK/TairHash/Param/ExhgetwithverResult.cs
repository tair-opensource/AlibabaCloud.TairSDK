namespace AlibabaCloud.TairSDK.TairHash.Param
{
    public class ExhgetwithverResult<T>
    {
        private T _value;
        private long _ver;

        public ExhgetwithverResult()
        {
        }

        public ExhgetwithverResult(T value, long ver)
        {
            _value = value;
            _ver = ver;
        }

        public T getValue()
        {
            return _value;
        }

        public void setValue(T value)
        {
            _value = value;
        }

        public long getVer()
        {
            return _ver;
        }

        public void setVer(long ver)
        {
            _ver = ver;
        }
    }
}