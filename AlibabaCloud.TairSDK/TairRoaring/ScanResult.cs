using System.Collections.Generic;

namespace AlibabaCloud.TairSDK.TairRoaring
{
    public class ScanResult<T>
    {
        private T cursor;
        private List<T> results;

        public ScanResult(T cursor, List<T> results)
        {
            this.cursor = cursor;
            this.results = results;
        }

        public T getCursor()
        {
            return cursor;
        }

        public List<T> getResult()
        {
            return results;
        }
    }
}