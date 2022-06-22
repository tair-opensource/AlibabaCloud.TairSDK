using System;
using System.Threading;
using AlibabaCloud.TairSDK;
using AlibabaCloud.TairSDK.TairBloom;
using NUnit.Framework;
using StackExchange.Redis;

namespace TestBloomTest
{
    public class BloomAsyncTest
    {
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost");
        private readonly TairBloomAsync tairBloomAsync = new(connDC, 0);

        [Test]
        public void bfaddTest()
        {
            string bbf = "bbf" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var ret1 = tairBloomAsync.bfreserve(bbf, 0.01, 100);
            var ret2 = tairBloomAsync.bfadd(bbf, "val1");
            var ret3 = tairBloomAsync.bfexists(bbf, "val1");
            var ret4 = tairBloomAsync.bfexists(bbf, "val2");

            Assert.AreEqual("OK", ResultHelper.String(ret1.Result));
            Assert.AreEqual(true, ResultHelper.Bool(ret2.Result));
            Assert.AreEqual(true, ResultHelper.Bool(ret3.Result));
            Assert.AreEqual(false, ResultHelper.Bool(ret4.Result));
        }


        [Test]
        public void bfmaddTest()
        {
            string bbf = "bbf" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var ret1 = tairBloomAsync.bfreserve(bbf, 0.01, 100);
            var ret2 = tairBloomAsync.bfmadd(bbf, "val1", "val2");
            var ret3 = tairBloomAsync.bfmexists(bbf, "val1", "val2");

            Assert.AreEqual("OK", ResultHelper.String(ret1.Result));
            Assert.AreEqual(true, ResultHelper.BoolArray(ret2.Result)[0]);
            Assert.AreEqual(true, ResultHelper.BoolArray(ret2.Result)[1]);
            Assert.AreEqual(true, ResultHelper.BoolArray(ret3.Result)[0]);
            Assert.AreEqual(true, ResultHelper.BoolArray(ret3.Result)[1]);
        }

        [Test]
        public void bfinsertTest()
        {
            string bbf = "bbf" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var ret1 = tairBloomAsync.bfinsert(bbf, "CAPACITY", 100, "ERROR", 0.001, "ITEMS", "val1", "val2");
            var ret2 = tairBloomAsync.bfmadd(bbf, "val1", "val2");
            var ret3 = tairBloomAsync.bfmexists(bbf, "val1", "val2");

            Assert.AreEqual(true, ResultHelper.BoolArray(ret1.Result)[0]);
            Assert.AreEqual(true, ResultHelper.BoolArray(ret1.Result)[1]);
            Assert.AreEqual(false, ResultHelper.BoolArray(ret2.Result)[0]);
            Assert.AreEqual(false, ResultHelper.BoolArray(ret2.Result)[1]);
            Assert.AreEqual(true, ResultHelper.BoolArray(ret3.Result)[0]);
            Assert.AreEqual(true, ResultHelper.BoolArray(ret3.Result)[1]);
        }
    }
}