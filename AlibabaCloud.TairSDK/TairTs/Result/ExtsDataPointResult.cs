using System;

namespace AlibabaCloud.TairSDK.TairTs.Result
{
    public class ExtsDataPointResult
    {
        private long ts;
        private string value;

        public ExtsDataPointResult(long ts, string value)
        {
            this.ts = ts;
            this.value = value;
        }

        public long getTs()
        {
            return ts;
        }

        public void setTs(long ts)
        {
            this.ts = ts;
        }

        public double getDoubleValue()
        {
            return Convert.ToDouble(value);
        }

        public void setValue(string value)
        {
            this.value = value;
        }
    }
}