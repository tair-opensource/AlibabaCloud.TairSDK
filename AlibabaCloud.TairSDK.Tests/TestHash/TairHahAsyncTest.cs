using System;
using System.Threading;
using AlibabaCloud.TairSDK;
using AlibabaCloud.TairSDK.TairHash;
using NUnit.Framework;
using StackExchange.Redis;

namespace TestHashTest
{
    public class HahAsyncTest
    {
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost");
        private readonly TairHashAsync tairHashAsync = new(connDC, 0);

        [Test]
        public void exhsetTest()
        {
            var foo = "foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");

            var ret1 = tairHashAsync.exhset(foo, "bar", "car");
            var ret2 = tairHashAsync.exhset(foo, "car", "bar");
            var ret3 = tairHashAsync.exhlen(foo);

            Assert.AreEqual(1, ResultHelper.Long(ret1.Result));
            Assert.AreEqual(1, ResultHelper.Long(ret2.Result));
            Assert.AreEqual(2, ResultHelper.Long(ret3.Result));
        }
    }
}