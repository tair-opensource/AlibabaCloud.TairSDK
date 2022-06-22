namespace AlibabaCloud.TairSDK.TairHash.Param
{
    public class ExhmsetwithopsParams<T>
    {
        private long _exp;
        private T _field;
        private T _value;
        private long _ver;

        public ExhmsetwithopsParams(T field, T value, long ver, long exp)
        {
            _field = field;
            _value = value;
            _ver = ver;
            _exp = exp;
        }

        public T getField()
        {
            return _field;
        }

        public void setField(T field)
        {
            _field = field;
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

        public long getExp()
        {
            return _exp;
        }

        public void setExp(long exp)
        {
            _exp = exp;
        }
    }
}