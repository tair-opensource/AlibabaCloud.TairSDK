using System;
using System.Collections.Generic;
using System.Threading;
using AlibabaCloud.TairSDK.TairTs;
using AlibabaCloud.TairSDK.TairTs.Param;
using AlibabaCloud.TairSDK.TairTs.Result;
using NUnit.Framework;
using StackExchange.Redis;

namespace TestTsTest
{
    public class TsTest
    {
        
        private static readonly DateTime Jan1st1970 = new DateTime
            (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static long CurrentTimeMillis()
        {
            return (long) (DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
        private readonly TairTs tairTs = new(connDC, 0);

        
        [Test]
        public void extsaddTest()
        {
            string randomPkey = "randomPkey_" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string randomSkey = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string randomSkey2 = "randomSkey2" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            long startTs = (CurrentTimeMillis() - 100000) / 1000 * 1000;
            long endTs = (CurrentTimeMillis()) / 1000 * 1000;
            for (int i = 0; i < 1; i++)
            {
                double val = i;
                long ts = startTs + i * 100;
                string tsStr = ts.ToString();
                ExtsAttributesParams param = new ExtsAttributesParams();
                param.dataEt(1000000000);
                param.chunkSize(1024);
                param.uncompressed();
                List<string> labels = new List<string>();
                labels.Add("label1");
                labels.Add("1");
                labels.Add("label2");
                labels.Add("2");
                param.labels(labels);

                string addRet = tairTs.extsadd(randomPkey, randomSkey, tsStr, val);
                Assert.AreEqual("OK", addRet);
                ts = ts + 1;
                tsStr = ts.ToString();
                addRet = tairTs.extsadd(randomPkey, randomSkey, tsStr, val, param);
                Assert.AreEqual("OK", addRet);
            }
        }

        [Test]
        public void extRawModifyTest()
        {
            string randomPkey = "randomPkey_" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string randomSkey = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string randomSkey2 = "randomSkey2" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            long startTs = (CurrentTimeMillis() - 100000) / 1000 * 1000;
            long endTs = (CurrentTimeMillis()) / 1000 * 1000;
            for (int i = 0; i < 1; i++)
            {
                double val = i;
                long ts = startTs + i * 1;
                String tsStr = ts.ToString();
                ExtsAttributesParams param = new ExtsAttributesParams();
                param.dataEt(1000000000);
                param.chunkSize(1024);
                param.uncompressed();
                List<string> labels = new List<string>();
                labels.Add("label1");
                labels.Add("1");
                labels.Add("label2");
                labels.Add("2");
                param.labels(labels);

                String addRet = tairTs.extsrawmodify(randomPkey, randomSkey, tsStr, val);
                Assert.AreEqual("OK", addRet);
                addRet = tairTs.extsrawmodify(randomPkey, randomSkey, tsStr, val);
                Assert.AreEqual("OK", addRet);
                ExtsDataPointResult getRet = tairTs.extsget(randomPkey, randomSkey);
                Assert.AreEqual((long) ts, getRet.getTs());
                Assert.AreEqual(i, getRet.getDoubleValue(), 0.0);
                ts = ts + 1;
                tsStr = ts.ToString();
                addRet = tairTs.extsrawmodify(randomPkey, randomSkey, tsStr, val, param);
                Assert.AreEqual("OK", addRet);
                addRet = tairTs.extsrawmodify(randomPkey, randomSkey, tsStr, val, param);
                Assert.AreEqual("OK", addRet);
                getRet = tairTs.extsget(randomPkey, randomSkey);
                Assert.AreEqual((long) ts, getRet.getTs());
                Assert.AreEqual(i, getRet.getDoubleValue(), 0.0);
            }
        }

        [Test]
        public void extsmaddTest()
        {
            string randomPkey = "randomPkey_" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string randomSkey = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string randomSkey2 = "randomSkey2" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            long startTs = (CurrentTimeMillis() - 100000) / 1000 * 1000;
            long endTs = (CurrentTimeMillis()) / 1000 * 1000;
            for (int i = 0; i < 1; i++)
            {
                long val = i;
                long ts = startTs + i * 100;
                String tsStr = ts.ToString();
                ExtsAttributesParams param = new ExtsAttributesParams();
                param.dataEt(1000000000);
                param.chunkSize(1024);
                param.uncompressed();
                List<String> labels = new List<String>();
                labels.Add("label1");
                labels.Add("1");
                param.labels(labels);

                List<ExtsDataPoint<String>> addList = new List<ExtsDataPoint<String>>();
                ExtsDataPoint<String> add1 = new ExtsDataPoint<String>(randomSkey, tsStr, val);
                ExtsDataPoint<String> add2 = new ExtsDataPoint<String>(randomSkey2, tsStr, val);
                addList.Add(add1);
                addList.Add(add2);
                List<String> maddRet = tairTs.extsmadd(randomPkey, addList);
                for (int j = 0; j < maddRet.Count; j++)
                {
                    Assert.AreEqual("OK", maddRet[j]);
                }

                String delRet = tairTs.extsdel(randomPkey, randomSkey);
                Assert.AreEqual("OK", delRet);
                delRet = tairTs.extsdel(randomPkey, randomSkey2);
                Assert.AreEqual("OK", delRet);

                maddRet = tairTs.extsmadd(randomPkey, addList, param);
                for (int j = 0; j < maddRet.Count; j++)
                {
                    Assert.AreEqual("OK", maddRet[j]);
                }

                delRet = tairTs.extsdel(randomPkey, randomSkey);
                Assert.AreEqual("OK", delRet);
                delRet = tairTs.extsdel(randomPkey, randomSkey2);
                Assert.AreEqual("OK", delRet);
            }
        }

        [Test]
        public void extsRawMModifyTest()
        {
            string randomPkey = "randomPkey_" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string randomSkey = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string randomSkey2 = "randomSkey2" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            long startTs = (CurrentTimeMillis() - 100000) / 1000 * 1000;
            long endTs = (CurrentTimeMillis()) / 1000 * 1000;
            for (int i = 0; i < 1; i++)
            {
                long val = i;
                long ts = startTs + i * 1;
                String tsStr = ts.ToString();
                ExtsAttributesParams param = new ExtsAttributesParams();
                param.dataEt(1000000000);
                param.chunkSize(1024);
                param.uncompressed();
                List<String> labels = new List<String>();
                labels.Add("label1");
                labels.Add("1");
                param.labels(labels);

                List<ExtsDataPoint<String>> addList = new List<ExtsDataPoint<String>>();
                ExtsDataPoint<String> add1 = new ExtsDataPoint<String>(randomSkey, tsStr, val);
                ExtsDataPoint<String> add2 = new ExtsDataPoint<String>(randomSkey2, tsStr, val);
                addList.Add(add1);
                addList.Add(add2);
                List<String> maddRet = tairTs.extsmrawmodify(randomPkey, addList);
                for (int j = 0; j < maddRet.Count; j++)
                {
                    Assert.AreEqual("OK", maddRet[j]);
                }

                ExtsDataPointResult getRet = tairTs.extsget(randomPkey, randomSkey);
                Assert.AreEqual((long) ts, getRet.getTs());
                Assert.AreEqual(i, getRet.getDoubleValue(), 0.0);

                getRet = tairTs.extsget(randomPkey, randomSkey2);
                Assert.AreEqual((long) ts, getRet.getTs());
                Assert.AreEqual(i, getRet.getDoubleValue(), 0.0);

                ts = ts + 1;
                tsStr = ts.ToString();
                List<ExtsDataPoint<String>> addList2 = new List<ExtsDataPoint<String>>();
                add1 = new ExtsDataPoint<String>(randomSkey, tsStr, val);
                add2 = new ExtsDataPoint<String>(randomSkey2, tsStr, val);
                addList2.Add(add1);
                addList2.Add(add2);

                maddRet = tairTs.extsmrawmodify(randomPkey, addList2, param);
                for (int j = 0; j < maddRet.Count; j++)
                {
                    Assert.AreEqual("OK", maddRet[j]);
                }

                getRet = tairTs.extsget(randomPkey, randomSkey);
                Assert.AreEqual((long) ts, getRet.getTs());
                Assert.AreEqual(i, getRet.getDoubleValue(), 0.0);

                getRet = tairTs.extsget(randomPkey, randomSkey2);
                Assert.AreEqual((long) ts, getRet.getTs());
                Assert.AreEqual(i, getRet.getDoubleValue(), 0.0);

                String delRet = tairTs.extsdel(randomPkey, randomSkey);
                Assert.AreEqual("OK", delRet);
                delRet = tairTs.extsdel(randomPkey, randomSkey2);
                Assert.AreEqual("OK", delRet);
            }
        }

        [Test]
        public void extsRawMincrTest()
        {
            string randomPkey = "randomPkey_" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string randomSkey = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string randomSkey2 = "randomSkey2" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            long startTs = (CurrentTimeMillis() - 100000) / 1000 * 1000;
            long endTs = (CurrentTimeMillis()) / 1000 * 1000;
            for (int i = 0; i < 1; i++)
            {
                long val = i;
                long ts = startTs + i * 1;
                String tsStr = ts.ToString();
                ExtsAttributesParams param = new ExtsAttributesParams();
                param.dataEt(1000000000);
                param.chunkSize(1024);
                param.uncompressed();
                List<String> labels = new List<String>();
                labels.Add("label1");
                labels.Add("1");
                param.labels(labels);

                List<ExtsDataPoint<String>> addList = new List<ExtsDataPoint<String>>();
                ExtsDataPoint<String> add1 = new ExtsDataPoint<String>(randomSkey, tsStr, val);
                ExtsDataPoint<String> add2 = new ExtsDataPoint<String>(randomSkey2, tsStr, val);
                addList.Add(add1);
                addList.Add(add2);
                List<String> maddRet = tairTs.extsmrawincr(randomPkey, addList);
                for (int j = 0; j < maddRet.Count; j++)
                {
                    Assert.AreEqual("OK", maddRet[j]);
                }

                ExtsDataPointResult getRet = tairTs.extsget(randomPkey, randomSkey);
                Assert.AreEqual((long) ts, getRet.getTs());
                Assert.AreEqual(val, getRet.getDoubleValue(), 0.0);

                getRet = tairTs.extsget(randomPkey, randomSkey2);
                Assert.AreEqual((long) ts, getRet.getTs());
                Assert.AreEqual(val, getRet.getDoubleValue(), 0.0);

                ts = ts + 1;
                val = val + 1;
                tsStr = ts.ToString();
                List<ExtsDataPoint<String>> addList2 = new List<ExtsDataPoint<String>>();
                add1 = new ExtsDataPoint<String>(randomSkey, tsStr, val);
                add2 = new ExtsDataPoint<String>(randomSkey2, tsStr, val);
                addList2.Add(add1);
                addList2.Add(add2);

                maddRet = tairTs.extsmrawincr(randomPkey, addList2, param);
                for (int j = 0; j < maddRet.Count; j++)
                {
                    Assert.AreEqual("OK", maddRet[j]);
                }

                getRet = tairTs.extsget(randomPkey, randomSkey);
                Assert.AreEqual((long) ts, getRet.getTs());
                Assert.AreEqual(val, getRet.getDoubleValue(), 0.0);

                getRet = tairTs.extsget(randomPkey, randomSkey2);
                Assert.AreEqual((long) ts, getRet.getTs());
                Assert.AreEqual(val, getRet.getDoubleValue(), 0.0);

                String delRet = tairTs.extsdel(randomPkey, randomSkey);
                Assert.AreEqual("OK", delRet);
                delRet = tairTs.extsdel(randomPkey, randomSkey2);
                Assert.AreEqual("OK", delRet);
            }
        }

        [Test]
        public void extsgetTest()
        {
            string randomPkey = "randomPkey_" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string randomSkey = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string randomSkey2 = "randomSkey2" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            long startTs = (CurrentTimeMillis() - 100000) / 1000 * 1000;
            long endTs = (CurrentTimeMillis()) / 1000 * 1000;
            for (int i = 0; i < 1; i++)
            {
                long val = i;
                long ts = startTs + i * 100;
                String tsStr = ts.ToString();
                ExtsAttributesParams param = new ExtsAttributesParams();
                param.dataEt(1000000000);
                param.chunkSize(1024);
                param.uncompressed();
                List<String> labels = new List<String>();
                labels.Add("label1");
                labels.Add("1");
                labels.Add("label2");
                labels.Add("2");
                param.labels(labels);

                String addRet = tairTs.extsadd(randomPkey, randomSkey, tsStr, val, param);
                Assert.AreEqual("OK", addRet);

                ExtsDataPointResult getRet = tairTs.extsget(randomPkey, randomSkey);
                Assert.AreEqual((long) ts, getRet.getTs());
                Assert.AreEqual(i, getRet.getDoubleValue(), 0.0);
            }
        }

        [Test]
        public void extsqueryTest()
        {
            string randomPkey = "randomPkey_" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string randomSkey = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string randomSkey2 = "randomSkey2" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            long startTs = (CurrentTimeMillis() - 100000) / 1000 * 1000;
            long endTs = (CurrentTimeMillis()) / 1000 * 1000;
            for (int i = 0; i < 1; i++)
            {
                long val = i;
                long ts = startTs + i * 100;
                String tsStr = ts.ToString();
                ExtsAttributesParams param = new ExtsAttributesParams();
                param.dataEt(1000000000);
                param.chunkSize(1024);
                param.uncompressed();
                List<String> labels = new List<String>();
                labels.Add("label1");
                labels.Add("1");
                labels.Add("label2");
                labels.Add("2");
                param.labels(labels);

                ExtsAttributesParams params2 = new ExtsAttributesParams();
                params2.dataEt(1000000000);
                params2.chunkSize(1024);
                params2.uncompressed();
                List<String> labels2 = new List<String>();
                labels2.Add("label1");
                labels2.Add("1");
                labels2.Add("label3");
                labels2.Add("3");
                params2.labels(labels2);

                String addRet = tairTs.extsadd(randomPkey, randomSkey, tsStr, val, param);
                Assert.AreEqual("OK", addRet);
                addRet = tairTs.extsadd(randomPkey, randomSkey2, tsStr, val, params2);
                Assert.AreEqual("OK", addRet);

                ExtsFilter<String> filter1 = new ExtsFilter<String>("label1=1");
                ExtsFilter<String> filter2 = new ExtsFilter<String>("label2=2");
                ExtsFilter<String> filter3 = new ExtsFilter<String>("label3=3");
                ExtsFilter<String> filter4 = new ExtsFilter<String>("label2=3");
                List<ExtsFilter<String>> filterList = new List<ExtsFilter<String>>();
                filterList.Add(filter1);
                filterList.Add(filter2);

                List<String> queryRet = tairTs.extsquery(randomPkey, filterList);
                Assert.AreEqual(1, queryRet.Count);
                Assert.AreEqual(randomSkey, queryRet[0]);


                List<ExtsFilter<String>> filterList2 = new List<ExtsFilter<String>>();
                filterList2.Add(filter1);
                filterList2.Add(filter3);

                queryRet = tairTs.extsquery(randomPkey, filterList2);
                Assert.AreEqual(1, queryRet.Count);
                Assert.AreEqual(randomSkey2, queryRet[0]);

                List<ExtsFilter<String>> filterList3 = new List<ExtsFilter<String>>();
                filterList3.Add(filter1);

                queryRet = tairTs.extsquery(randomPkey, filterList3);
                Assert.AreEqual(2, queryRet.Count);

                List<ExtsFilter<String>> filterList4 = new List<ExtsFilter<String>>();
                filterList4.Add(filter4);

                queryRet = tairTs.extsquery(randomPkey, filterList4);
                Assert.AreEqual(0, queryRet.Count);
            }
        }

        [Test]
        public void extsrangeTest()
        {
            string randomPkey = "randomPkey_" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string randomSkey = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string randomSkey2 = "randomSkey2" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            long startTs = (CurrentTimeMillis() - 100000) / 1000 * 1000;
            long endTs = (CurrentTimeMillis()) / 1000 * 1000;
            int num = 3;
            String startTsStr = startTs.ToString();
            String endTsStr = endTs.ToString();

            for (int i = 0; i < num; i++)
            {
                double val = i;
                long ts = startTs + i * 1000;
                String tsStr = ts.ToString();
                ExtsAttributesParams param = new ExtsAttributesParams();
                param.dataEt(1000000000);
                param.chunkSize(1024);
                param.uncompressed();
                List<String> labels = new List<String>();
                labels.Add("label1");
                labels.Add("1");
                labels.Add("label2");
                labels.Add("2");
                param.labels(labels);

                String addRet = tairTs.extsadd(randomPkey, randomSkey, tsStr, val, param);
                Assert.AreEqual("OK", addRet);
            }

            ExtsAggregationParams paramsAgg = new ExtsAggregationParams();
            paramsAgg.maxCountSize(10);
            paramsAgg.aggAvg(1000);

            ExtsRangeResult rangeByteRet = tairTs.extsrange(randomPkey, randomSkey, startTsStr, endTsStr, paramsAgg);
            List<ExtsDataPointResult> dataPointRet = rangeByteRet.getDataPoints();
            Assert.AreEqual(num, dataPointRet.Count);
            for (int i = 0; i < num; i++)
            {
                double val = i;
                long ts = startTs + i * 1000;
                Assert.AreEqual(ts, dataPointRet[i].getTs());
                Assert.AreEqual(val, dataPointRet[i].getDoubleValue(), 0.0);
            }
        }
    }
}