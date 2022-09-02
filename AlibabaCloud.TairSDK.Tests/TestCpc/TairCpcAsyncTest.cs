using System;
using System.Threading;
using AlibabaCloud.TairSDK;
using AlibabaCloud.TairSDK.TairCpc;
using NUnit.Framework;
using StackExchange.Redis;

namespace TestCpcTest
{
    public class TairCpcAsyncTest
    {
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost");
        private readonly TairCpcAsync taircpc = new(connDC, 0);

        [Test]
        public void cpcupdateTest()
        {
            string key = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            string item = "item" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var ret1 = taircpc.cpcUpdate(key, item);
            Assert.AreEqual("OK", ResultHelper.String(ret1.Result));
        }
    }
}