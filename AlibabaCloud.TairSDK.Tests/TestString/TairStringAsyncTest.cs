using System;
using System.Threading;
using AlibabaCloud.TairSDK;
using AlibabaCloud.TairSDK.TairString;
using AlibabaCloud.TairSDK.TairString.Param;
using NUnit.Framework;
using StackExchange.Redis;

namespace TestStringTest
{
    public class StringAsyncTest
    {
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
        private readonly TairStringAsync tairStringAsync = new(connDC, 0);

        [Test]
        public void exsetTest()
        {
            var key1 = "key1" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var value1 = "value1" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var key2 = "key2" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var value2 = "value2" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var ret1 = tairStringAsync.exset(key1, value1);
            var ret2 = tairStringAsync.exset(key2, value2);
            var ret3 = tairStringAsync.exget(key1);
            var ret4 = tairStringAsync.exget(key2);

            Assert.AreEqual("OK", ResultHelper.String(ret1.Result));
            Assert.AreEqual("OK", ResultHelper.String(ret2.Result));
            Assert.AreEqual(value1, StringHelper.GetResultString(ret3.Result).getValue());
            Assert.AreEqual(value2, StringHelper.GetResultString(ret4.Result).getValue());
        }

        [Test]
        public void exsetParamsTest()
        {
            ExsetParams params_nx = new ExsetParams();
            params_nx.nx();
            ExsetParams params_xx = new ExsetParams();
            params_xx.xx();
            String ret_xx = "";
            String ret_nx = "";

            var key1 = "key1" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var value1 = "value1" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var key2 = "key2" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var value2 = "value2" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var ret1 = tairStringAsync.exset(key1, value1, params_xx);
            var ret2 = tairStringAsync.exset(key2, value2, params_nx);
            var ret4 = tairStringAsync.exget(key2);

            Assert.AreEqual(null, ResultHelper.String(ret1.Result));
            Assert.AreEqual("OK", ResultHelper.String(ret2.Result));
            Assert.AreEqual(value2, StringHelper.GetResultString(ret4.Result).getValue());
        }

        [Test]
        public void exincrbyTest()
        {
            String num_string_value = "100";
            long incr_value = 100;
            String new_string_value = "200";
            long new_long_value = 200;
            var key1 = "key1" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");

            var ret1 = tairStringAsync.exset(key1, num_string_value);
            var ret2 = tairStringAsync.exincrBy(key1, incr_value);
            var ret3 = tairStringAsync.exget(key1);

            Assert.AreEqual("OK", ResultHelper.String(ret1.Result));
            Assert.AreEqual(new_long_value, ResultHelper.Long(ret2.Result));
            Assert.AreEqual(new_string_value, StringHelper.GetResultString(ret3.Result).getValue());
        }

        [Test]
        public void excasTest()
        {
            var key1 = "key1" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var value1 = "value1" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");

            var ret1 = tairStringAsync.exset(key1, value1);
            var ret2 = tairStringAsync.excas(key1, "new" + value1, 2);
            var ret3 = tairStringAsync.excas(key1, "new" + value1, 1);

            Assert.AreEqual("OK", ResultHelper.String(ret1.Result));
            Assert.AreEqual(value1, StringHelper.CasResultString(ret2.Result).getValue());
            Assert.AreEqual(1, StringHelper.CasResultString(ret2.Result).getVersion());
            Assert.AreEqual("OK", StringHelper.CasResultString(ret3.Result).getMsg());
            Assert.AreEqual("", StringHelper.CasResultString(ret3.Result).getValue());
            Assert.AreEqual(2, StringHelper.CasResultString(ret3.Result).getVersion());
        }

        [Test]
        public void excadTest()
        {
            var key1 = "key1" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var value1 = "value1" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");

            var ret1 = tairStringAsync.exset(key1, value1);
            var ret2 = tairStringAsync.excad(key1, 2);
            var ret3 = tairStringAsync.excad(key1, 1);

            Assert.AreEqual("OK", ResultHelper.String(ret1.Result));
            Assert.AreEqual(0, ResultHelper.Long(ret2.Result));
            Assert.AreEqual(1, ResultHelper.Long(ret3.Result));
        }
    }
}