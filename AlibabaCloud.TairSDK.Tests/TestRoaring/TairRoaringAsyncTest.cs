using System.Collections.Generic;
using AlibabaCloud.TairSDK;
using AlibabaCloud.TairSDK.TairRoaring;
using NUnit.Framework;
using StackExchange.Redis;

namespace TestRoaring
{
    public class TairRoaringAsyncTest
    {
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
        private IDatabase db = connDC.GetDatabase(0);
        private readonly TairRoaringAsync tairRoaringAsync = new(connDC, 0);

        [Test]
        public void tairroaringtest()
        {
            db.KeyDelete("foo");
            var bitret = tairRoaringAsync.trsetbit("foo", 10, 1);
            var bitret2 = tairRoaringAsync.trsetbit("foo", 20, "1");
            var bitret3 = tairRoaringAsync.trsetbit("foo", 30, 1);
            var bitret4 = tairRoaringAsync.trsetbit("foo", 30, 0);
            var count_ret = tairRoaringAsync.trbitcount("foo");
            var get_ret = tairRoaringAsync.trgetbit("foo", 10);
            Assert.AreEqual(0, ResultHelper.Long(bitret.Result));
            Assert.AreEqual(0, ResultHelper.Long(bitret2.Result));
            Assert.AreEqual(0, ResultHelper.Long(bitret3.Result));
            Assert.AreEqual(1, ResultHelper.Long(bitret4.Result));
            Assert.AreEqual(2, ResultHelper.Long(count_ret.Result));
            Assert.AreEqual(1, ResultHelper.Long(get_ret.Result));
        }

        [Test]
        public void trbitsrange_mixedtest()
        {
            db.KeyDelete("foo");
            tairRoaringAsync.trsetbits("foo", 1, 3, 5, 7, 9);
            var scanret = tairRoaringAsync.trscan("foo", 5, 10);
            List<long> expect = new List<long>();
            expect.Add(5);
            expect.Add(7);
            expect.Add(9);
            Assert.AreEqual(expect, RoaringHelper.ScanResultLong(scanret.Result).getResult());

            var app_ret = tairRoaringAsync.trappendbitarray("foo", 3, "1010101"); // 1, 4, 6, 8, 10
            var range_ret = tairRoaringAsync.trrangebitarray("foo", 0, 10);
            Assert.AreEqual(6, ResultHelper.Long(app_ret.Result));
            Assert.AreEqual("01011010101", ResultHelper.String(range_ret.Result));
        }
    }
}