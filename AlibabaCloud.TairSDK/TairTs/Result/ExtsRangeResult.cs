using System.Collections.Generic;

namespace AlibabaCloud.TairSDK.TairTs.Result
{
    public class ExtsRangeResult
    {
        private List<ExtsDataPointResult> dataPoints = new List<ExtsDataPointResult>();
        private long token;

        public ExtsRangeResult(List<ExtsDataPointResult> dataPoints, long token)
        {
            this.token = token;

            int dataPointsNum = dataPoints.Count;
            for (int i = 0; i < dataPointsNum; i++)
            {
                var subl = dataPoints[i];
                this.dataPoints.Add(subl);
            }
        }

        public List<ExtsDataPointResult> getDataPoints()
        {
            return dataPoints;
        }

        public long getToken()
        {
            return token;
        }

        public void setToken(long token)
        {
            this.token = token;
        }
    }
}