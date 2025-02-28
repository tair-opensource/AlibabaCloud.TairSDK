using System.Collections.Generic;
using AlibabaCloud.TairSDK.TairRoaring;
using NUnit.Framework;
using StackExchange.Redis;

namespace TestRoaring
{
    public class RoaringTest
    {
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
        private IDatabase db = connDC.GetDatabase(0);
        private readonly TairRoaring tairRoaring = new(connDC, 0);

        [Test]
        public void trbit_mixed_test()
        {
            db.KeyDelete("foo");
            Assert.AreEqual(0, tairRoaring.trsetbit("foo", 10, 1));
            Assert.AreEqual(0, tairRoaring.trsetbit("foo", 20, "1"));
            Assert.AreEqual(0, tairRoaring.trsetbit("foo", 30, 1));
            Assert.AreEqual(1, tairRoaring.trsetbit("foo", 30, 0));
            Assert.AreEqual(2, tairRoaring.trbitcount("foo"));

            Assert.AreEqual(1, tairRoaring.trgetbit("foo", 10));
            Assert.AreEqual(0, tairRoaring.trgetbit("foo", 11));

            Assert.AreEqual(10, tairRoaring.trmin("foo"));
            Assert.AreEqual(20, tairRoaring.trmax("foo"));
            db.KeyDelete("foo");
        }

        [Test]
        public void trbits_mixed_test()
        {
            db.KeyDelete("foo");
            Assert.AreEqual(5, tairRoaring.trsetbits("foo", 1, 3, 5, 7, 9));
            Assert.AreEqual(5, tairRoaring.trbitcount("foo"));

            Assert.AreEqual(7, tairRoaring.trsetbits("foo", 5, 7, 9, 11, 13));
            Assert.AreEqual(7, tairRoaring.trbitcount("foo"));

            Assert.AreEqual(3, tairRoaring.trclearbits("foo", 5, 6, 7, 8, 9));
            Assert.AreEqual(4, tairRoaring.trbitcount("foo"));

            List<long> result = tairRoaring.trgetbits("foo", 1, 2, 3, 4, 5);
            List<long> expect = new List<long>();
            expect.Add((long)1);
            expect.Add((long)0);
            expect.Add((long)1);
            expect.Add((long)0);
            expect.Add((long)0);
            Assert.AreEqual(expect, result);

            Assert.AreEqual("OK", tairRoaring.trappendintarray("foo", 1, 2, 3));
            result = tairRoaring.trgetbits("foo", 1, 2, 3, 4, 5);
            expect.Clear();
            expect.Add((long)1);
            expect.Add((long)1);
            expect.Add((long)1);
            expect.Add((long)0);
            expect.Add((long)0);
            Assert.AreEqual(expect, result);

            Assert.AreEqual("OK", tairRoaring.trsetintarray("foo", 2, 3));
            result = tairRoaring.trgetbits("foo", 1, 2, 3, 4, 5);
            expect.Clear();
            expect.Add((long)0);
            expect.Add((long)1);
            expect.Add((long)1);
            expect.Add((long)0);
            expect.Add((long)0);
            Assert.AreEqual(expect, result);

            result = tairRoaring.trgetbits("foo", 1, 1, 2, 3, 3);
            expect.Clear();
            expect.Add((long)0);
            expect.Add((long)0);
            expect.Add((long)1);
            expect.Add((long)1);
            expect.Add((long)1);
            Assert.AreEqual(expect, result);

            Assert.AreEqual(2, tairRoaring.trmin("foo"));
            Assert.AreEqual(3, tairRoaring.trmax("foo"));

            db.KeyDelete("foo");
        }

        [Test]
        public void trbitrangeing_mixed_test()
        {
            db.KeyDelete("foo");
            Assert.AreEqual(5, tairRoaring.trsetbits("foo", 1, 3, 5, 7, 9));
            List<long> result = tairRoaring.trrange("foo", 1, 5);
            List<long> expected = new List<long>();
            expected.Add(1);
            expected.Add(3);
            expected.Add(5);
            Assert.AreEqual(expected, result);

            result = tairRoaring.trrange("foo", 0, 4);
            expected.Clear();
            expected.Add(1);
            expected.Add(3);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void trscantest()
        {
            db.KeyDelete("foo");
            ScanResult<long> rawresult = tairRoaring.trscan("no-key", 0);
            Assert.AreEqual(0, rawresult.getCursor());
            List<long> result = rawresult.getResult();
            List<long> expect = new List<long>();
            Assert.AreEqual(expect, result);

            db.KeyDelete("foo");
            Assert.AreEqual(5, tairRoaring.trsetbits("foo", 1, 3, 5, 7, 9));

            rawresult = tairRoaring.trscan("foo", 0);
            Assert.AreEqual(0, rawresult.getCursor());
            result = rawresult.getResult();
            expect.Clear();
            expect.Add((long)1);
            expect.Add((long)3);
            expect.Add((long)5);
            expect.Add((long)7);
            expect.Add((long)9);
            Assert.AreEqual(expect, result);

            rawresult = tairRoaring.trscan("foo", 4, 2);
            Assert.AreEqual(9, rawresult.getCursor());
            result = rawresult.getResult();
            expect.Clear();
            expect.Add((long)5);
            expect.Add((long)7);
            Assert.AreEqual(expect, result);
        }

        [Test]
        public void trappendbitarraytest()
        {
            db.KeyDelete("foo");
            Assert.AreEqual(5, tairRoaring.trappendbitarray("foo", 0, "101010101"));
            List<long> result = tairRoaring.trrange("foo", 0, 10);
            List<long> expected = new List<long>();
            expected.Add(1);
            expected.Add(3);
            expected.Add(5);
            expected.Add(7);
            expected.Add(9);
            Assert.AreEqual(expected, result);
            db.KeyDelete("foo");

            Assert.AreEqual(5, tairRoaring.trappendbitarray("foo", -1, "101010101"));
            result = tairRoaring.trrange("foo", 0, 10);
            expected.Clear();
            expected.Add(0);
            expected.Add(2);
            expected.Add(4);
            expected.Add(6);
            expected.Add(8);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void trstatus_mixed_test()
        {
            db.KeyDelete("foo");
            Assert.AreEqual(9, tairRoaring.trsetbits("foo", 1, 2, 3, 4, 5, 6, 7, 8, 9));
            Assert.AreEqual("OK", tairRoaring.troptimize("foo"));

            Assert.AreEqual(9, tairRoaring.trbitcount("foo"));
            Assert.AreEqual(5, tairRoaring.trbicount("foo", 0, 5));
            Assert.AreEqual(1, tairRoaring.trbicount("foo", 9, 20));
            Assert.AreEqual(1, tairRoaring.trbitpos("foo", 1));

            Assert.AreEqual(0, tairRoaring.trbitpos("foo", 0));

            Assert.AreEqual(2, tairRoaring.trbitpos("foo", 1, 2));
            Assert.AreEqual(6, tairRoaring.trbitpos("foo", 1, -4));
            Assert.AreEqual(0, tairRoaring.trbitpos("foo", 0, 1));

            Assert.AreEqual("cardinality: 9\r\n" +
                            "number of containers: 1\r\n" +
                            "max value: 9\r\n" +
                            "min value: 1\r\n" +
                            "sum value: 45\r\n" +
                            "number of array containers: 0\r\n" +
                            "\tarray container values: 0\r\n" +
                            "\tarray container bytes: 0\r\n" +
                            "number of bitset containers: 0\r\n" +
                            "\tbitset container values: 0\r\n" +
                            "\tbitset container bytes: 0\r\n" +
                            "number of run containers: 1\r\n" +
                            "\trun container values: 9\r\n" +
                            "\trun container bytes: 6\r\n", tairRoaring.trstat("foo", false));
        }

        [Test]
        public void tremptytest()
        {
            db.KeyDelete("foo");
            List<long> expect = new List<long>();
            List<long> result = tairRoaring.trrange("foo", 0, 4);
            Assert.AreEqual(expect, result);

            result = tairRoaring.trgetbits("foo", 0, 4);
            Assert.AreEqual(expect, result);

            Assert.AreEqual(-1, tairRoaring.trmin("foo"));
            Assert.AreEqual(-1, tairRoaring.trmax("foo"));
            Assert.AreEqual(-1, tairRoaring.trbitpos("foo", "1", 1));
            Assert.AreEqual(-1, tairRoaring.trrank("foo", 1));
            Assert.IsEmpty(tairRoaring.trstat("foo", false));
            Assert.IsEmpty(tairRoaring.troptimize("foo"));
            Assert.AreEqual(0, tairRoaring.trbitcount("foo"));
            Assert.AreEqual(0, tairRoaring.trclearbits("foo", 1, 3, 5));
        }

        [Test]
        public void trbitoptest()
        {
            db.KeyDelete("foo");
            Assert.AreEqual("OK", tairRoaring.trappendintarray("foo", 1, 3, 5, 7, 9));
            Assert.AreEqual("OK", tairRoaring.trappendintarray("bar", 2, 4, 6, 8, 10));

            Assert.AreEqual(10, tairRoaring.trbitop("dest", "OR", "foo", "bar"));
            Assert.AreEqual(0, tairRoaring.trbitopcard("AND", "foo", "bar"));

            db.KeyDelete("foo");
            db.KeyDelete("bar");
            db.KeyDelete("dest");
        }

        [Test]
        public void trgetmultitest()
        {
            db.KeyDelete("foo");
            Assert.AreEqual("OK", tairRoaring.trappendintarray("foo", 1, 3, 5, 7, 9, 11, 13, 15, 17, 19));

            List<long> result = tairRoaring.trrange("foo", 0, 4);
            List<long> expect = new List<long>();
            expect.Add(1);
            expect.Add(3);
            Assert.AreEqual(expect, result);
        }

        [Test]
        public void trmultikeytest()
        {
            db.KeyDelete("foo");
            db.KeyDelete("bar");
            db.KeyDelete("baz");
            Assert.AreEqual(5, tairRoaring.trsetbits("foo", 1, 3, 5, 7, 9));
            Assert.AreEqual(5, tairRoaring.trsetbits("bar", 2, 4, 6, 8, 10));
            Assert.AreEqual(10, tairRoaring.trsetrange("baz", 1, 10));

            Assert.AreEqual(false, tairRoaring.trcontains("foo", "bar"));
            Assert.AreEqual(true, tairRoaring.trcontains("foo", "baz"));
            Assert.AreEqual(0.5, tairRoaring.trjaccard("foo", "baz"));

            db.KeyDelete("foo");
            db.KeyDelete("bar");
            db.KeyDelete("baz");
        }
    }
}