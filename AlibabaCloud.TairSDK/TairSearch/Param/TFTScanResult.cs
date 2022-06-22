using System.Collections.Generic;

namespace AlibabaCloud.TairSDK.TairSearch.Param
{
    public class TFTScanResult<T>
    {
        private T _cursor;
        private List<T> _results;

        public TFTScanResult()
        {
        }

        public TFTScanResult(T cursor, List<T> results)
        {
            _cursor = cursor;
            _results = results;
        }

        public T getCursor()
        {
            return _cursor;
        }

        public List<T> getResult()
        {
            return _results;
        }

        public void setCursor(T cursor)
        {
            _cursor = cursor;
        }

        public void setResult(List<T> results)
        {
            _results = results;
        }
    }
}