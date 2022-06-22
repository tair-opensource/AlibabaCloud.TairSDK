using System.Collections.Generic;

namespace AlibabaCloud.TairSDK.TairHash.Param
{
    public class ExhscanResult<T>
    {
        private T _cursor;
        private Dictionary<T, T> _results;

        public ExhscanResult()
        {
        }

        public ExhscanResult(T cursor, Dictionary<T, T> results)
        {
            _cursor = cursor;
            _results = results;
        }

        public T getCursor()
        {
            return _cursor;
        }

        public Dictionary<T, T> getResult()
        {
            return _results;
        }

        public void setCursor(T cursor)
        {
            _cursor = cursor;
        }

        public void setResult(Dictionary<T, T> results)
        {
            _results = results;
        }
    }
}