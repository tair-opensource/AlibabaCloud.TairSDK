using System;
using System.Collections.Generic;
using System.Threading;
using AlibabaCloud.TairSDK.TairCpc;
using AlibabaCloud.TairSDK.TairCpc.Param;
using AlibabaCloud.TairSDK.TairCpc.Result;
using NUnit.Framework;
using StackExchange.Redis;

namespace TestCpcTest
{
    public class CpcTest
    {
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost");
        private readonly TairCpc taircpc = new(connDC, 0);

        [Test]
        public void cpcupdateTest()
        {
            string key = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string item = "item" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string addRet = taircpc.cpcUpdate(key, item);
            Assert.AreEqual("OK", addRet);
        }

        [Test]
        public void cpcupdateParamTest()
        {
            string key = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string item = "item" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string item2 = "item2" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            CpcUpdateParams cpcUpdateParams = new CpcUpdateParams();
            cpcUpdateParams.ex(2);
            String addRet = taircpc.cpcUpdate(key, item, cpcUpdateParams);
            Assert.AreEqual("OK", addRet);
            addRet = taircpc.cpcUpdate(key, item2);
            Assert.AreEqual("OK", addRet);

            Double estimateRet = taircpc.cpcEstimate(key);
            Assert.AreEqual(2.00, estimateRet, 0.001);

            Thread.Sleep(2000);

            estimateRet = taircpc.cpcEstimate(key);
            Assert.NotNull(estimateRet);

            addRet = taircpc.cpcUpdate(key, item);
            Assert.AreEqual("OK", addRet);
            cpcUpdateParams.ex(0);
            addRet = taircpc.cpcUpdate(key, item2, cpcUpdateParams);
            Assert.AreEqual("OK", addRet);

            Thread.Sleep(1000);
            estimateRet = taircpc.cpcEstimate(key);
            Assert.AreEqual(2.00, estimateRet, 0.001);

            cpcUpdateParams.ex(2);
            addRet = taircpc.cpcUpdate(key, item2, cpcUpdateParams);

            cpcUpdateParams.ex(-1);
            addRet = taircpc.cpcUpdate(key, item2, cpcUpdateParams);

            Thread.Sleep(2000);
            estimateRet = taircpc.cpcEstimate(key);
            Assert.AreEqual(2.00, estimateRet, 0.001);
        }

        [Test]
        public void cpcupdate2judTest()
        {
            string key = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string item = "item" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string item2 = "item2" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string item3 = "item3" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string item4 = "item4" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            String addRet = taircpc.cpcUpdate(key, item);
            Assert.AreEqual("OK", addRet);

            addRet = taircpc.cpcUpdate(key, item2);
            Assert.AreEqual("OK", addRet);

            addRet = taircpc.cpcUpdate(key, item3);
            Assert.AreEqual("OK", addRet);

            Double estimateRet = taircpc.cpcEstimate(key);
            Assert.AreEqual(3.00, estimateRet, 0.001);

            Update2JudResult judRet = taircpc.cpcUpdate2Jud(key, item);
            Assert.AreEqual(3.00, judRet.getValue(), 0.001);
            Assert.AreEqual(0.00, judRet.getDiffValue(), 0.001);

            judRet = taircpc.cpcUpdate2Jud(key, item4);
            Assert.AreEqual(4.00, judRet.getValue(), 0.001);
            Assert.AreEqual(1.00, judRet.getDiffValue(), 0.001);
        }

        [Test]
        public void cpcArrayUpdateTest()
        {
            string key = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string item = "item" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string item2 = "item2" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            CpcUpdateParams param = new CpcUpdateParams();
            param.win(5);
            String addRet = taircpc.cpcArrayUpdate(key, 1, item, param);
            Assert.AreEqual("OK", addRet);
            addRet = taircpc.cpcArrayUpdate(key, 1, item2, param);
            Assert.AreEqual("OK", addRet);
            addRet = taircpc.cpcArrayUpdate(key, 3, item, param);
            Assert.AreEqual("OK", addRet);
            addRet = taircpc.cpcArrayUpdate(key, 5, item, param);
            Assert.AreEqual("OK", addRet);

            Double estimateRet = taircpc.cpcArrayEstimate(key, 1);
            Assert.AreEqual(2.00, estimateRet, 0.001);
            estimateRet = taircpc.cpcArrayEstimate(key, 5);
            Assert.AreEqual(1.00, estimateRet, 0.001);
        }

        [Test]
        public void cpcArrayEstimateRangeTest()
        {
            string key = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string item = "item" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            long timestamp = 1000000;
            string addRet = taircpc.cpcArrayUpdate(key, timestamp, item);
            Assert.AreEqual("OK", addRet);

            List<double> estimateRet = taircpc.cpcArrayEstimateRange(key, timestamp - 1000, timestamp + 1000);
            Assert.AreEqual(1.00, estimateRet[0]);
        }
    }
}