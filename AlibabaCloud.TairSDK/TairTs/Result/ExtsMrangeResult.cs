using System;
using System.Collections.Generic;

namespace AlibabaCloud.TairSDK.TairTs.Result
{
    public class ExtsMrangeResult
    {
        private string skey;
        private List<ExtsLabelResult> labels = new List<ExtsLabelResult>();
        private List<ExtsDataPointResult> dataPoints = new List<ExtsDataPointResult>();
        private long token;

        public ExtsMrangeResult(string skey, List<ExtsLabelResult> labels, List<ExtsDataPointResult> dataPoints,
            long token)
        {
            this.skey = skey;
            this.token = token;

            int labelsNum = labels.Count;
            for (int i = 0; i < labelsNum; i++)
            {
                var subl = labels[i];
                this.labels.Add(subl);
            }

            int dataPointsNum = dataPoints.Count;
            for (int i = 0; i < dataPointsNum; i++)
            {
                var subl = dataPoints[i];
                this.dataPoints.Add(subl);
            }
        }

        public String getSkey()
        {
            return skey;
        }

        public void setSkey(String skey)
        {
            this.skey = skey;
        }

        public List<ExtsLabelResult> getLabels()
        {
            return labels;
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