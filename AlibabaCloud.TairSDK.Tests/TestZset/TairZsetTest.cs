using System;
using System.Collections.Generic;
using System.Text;
using AlibabaCloud.TairSDK.TairZset;
using AlibabaCloud.TairSDK.TairZset.Param;
using NUnit.Framework;
using StackExchange.Redis;

namespace TestZset
{
    public class tairzsetTest
    {
        byte[] bfoo = {0x01, 0x02, 0x03, 0x04};
        byte[] bbar = {0x05, 0x06, 0x07, 0x08};
        byte[] bcar = {0x09, 0x0A, 0x0B, 0x0C};
        byte[] ba = {0x0A};
        byte[] bb = {0x0B};
        byte[] bc = {0x0C};
        byte[] bInclusiveB = {0x5B, 0x0B};
        byte[] bExclusiveC = {0x28, 0x0C};
        byte[] bLexMinusInf = {0x2D};
        byte[] bLexPlusInf = {0x2B};
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
        private IDatabase db = connDC.GetDatabase(0);
        private readonly TairZset tairzset = new(connDC, 0);

        [Test]
        public void zadd()
        {
            db.KeyDelete("foo");
            long status = tairzset.exzadd("foo", "a", 1d);
            Assert.AreEqual(1, status);
            status = tairzset.exzadd("foo", "b", 10d);
            Assert.AreEqual(1, status);
            status = tairzset.exzadd("foo", "a", 2d);
            Assert.AreEqual(0, status);

            //binary
            db.KeyDelete(bfoo);
            long bstatus = tairzset.exzadd(bfoo, ba, 1d);
            Assert.AreEqual(1, bstatus);
            bstatus = tairzset.exzadd(bfoo, bb, 10d);
            Assert.AreEqual(1, bstatus);
            bstatus = tairzset.exzadd(bfoo, ba, 2d);
            Assert.AreEqual(0, bstatus);
        }

        [Test]
        public void zaddWithParams()
        {
            db.KeyDelete("foo");
            long status = tairzset.exzadd("foo", "1", "a", new ExzaddParams().xx());
            Assert.AreEqual(0L, status);
            tairzset.exzadd("foo", "1", "a");
            status = tairzset.exzadd("foo", "2", "a", new ExzaddParams().nx());
            Assert.AreEqual(0l, status);
            Assert.AreEqual("1", tairzset.exzscore("foo", "a"));
        }

        [Test]
        public void zrange()
        {
            db.KeyDelete("foo");
            tairzset.exzadd("foo", "1", "a");
            tairzset.exzadd("foo", "10", "b");
            tairzset.exzadd("foo", "0.1", "c");
            tairzset.exzadd("foo", "2", "a");

            List<string> expected = new List<string>();
            expected.Add("c");
            expected.Add("a");

            List<String> range = tairzset.exzrange("foo", 0, 1);
            Assert.AreEqual(expected, range);

            expected.Add("b");
            range = tairzset.exzrange("foo", 0, 100);
            Assert.AreEqual(expected, range);
        }

        [Test]
        public void zrangByLex()
        {
            db.KeyDelete("foo");
            tairzset.exzadd("foo", "1", "aa");
            tairzset.exzadd("foo", "1", "c");
            tairzset.exzadd("foo", "1", "bb");
            tairzset.exzadd("foo", "1", "d");

            List<string> expected = new List<string>();
            expected.Add("bb");
            expected.Add("c");

            // exclusive aa ~ inclusive c
            Assert.AreEqual(expected, tairzset.exzrangeByLex("foo", "(aa", "[c"));

            expected.Clear();
            expected.Add("bb");
            expected.Add("c");

            // with LIMIT
            Assert.AreEqual(expected, tairzset.exzrangeByLex("foo", "-", "+", new ExzrangeParams().limit(1, 2)));
        }


        [Test]
        public void zrevrange()
        {
            db.KeyDelete("foo");
            tairzset.exzadd("foo", "1", "a");
            tairzset.exzadd("foo", "10", "b");
            tairzset.exzadd("foo", "0.1", "c");
            tairzset.exzadd("foo", "2", "a");

            List<string> expected = new List<string>();
            expected.Add("b");
            expected.Add("a");

            List<String> range = tairzset.exzrevrange("foo", 0, 1);
            Assert.AreEqual(expected, range);

            expected.Add("c");
            range = tairzset.exzrevrange("foo", 0, 100);
            Assert.AreEqual(expected, range);


            // Binary
            db.KeyDelete(bfoo);
            tairzset.exzadd(bfoo, ba, 1d);
            tairzset.exzadd(bfoo, bb, 10d);
            tairzset.exzadd(bfoo, bc, 0.1d);
            tairzset.exzadd(bfoo, ba, 2d);

            List<byte[]> bexpected = new List<byte[]>();
            bexpected.Add(bb);
            bexpected.Add(ba);

            List<byte[]> brange = tairzset.exzrevrange(bfoo, 0, 1);
            Assert.AreEqual(bexpected, brange);

            bexpected.Add(bc);
            brange = tairzset.exzrevrange(bfoo, 0, 100);
            Assert.AreEqual(bexpected, brange);
        }

        [Test]
        public void zrem()
        {
            db.KeyDelete("foo");
            tairzset.exzadd("foo", "1", "a");
            tairzset.exzadd("foo", "2", "b");

            long status = tairzset.exzrem("foo", "a");

            List<String> expected = new List<string>();
            expected.Add("b");

            Assert.AreEqual(1, status);
            Assert.AreEqual(expected, tairzset.exzrange("foo", 0, 100));

            status = tairzset.exzrem("foo", "bar");

            Assert.AreEqual(0, status);

            // Binary
            db.KeyDelete(bfoo);
            tairzset.exzadd(bfoo, ba, 1d);
            tairzset.exzadd(bfoo, bb, 2d);

            long bstatus = tairzset.exzrem(bfoo, ba);

            List<byte[]> bexpected = new List<byte[]>();
            bexpected.Add(bb);

            Assert.AreEqual(1, bstatus);
            Assert.AreEqual(bexpected, tairzset.exzrange(bfoo, 0, 100));

            bstatus = tairzset.exzrem(bfoo, bbar);
            Assert.AreEqual(0, bstatus);
        }

        [Test]
        public void zincrby()
        {
            db.KeyDelete("foo");
            tairzset.exzadd("foo", "1", "a");
            tairzset.exzadd("foo", "2", "b");

            string score = tairzset.exzincrBy("foo", "2", "a");
            List<string> expected = new List<string>();
            expected.Add("b");
            expected.Add("a");
            Assert.AreEqual("3", score);
            Assert.AreEqual(expected, tairzset.exzrange("foo", 0, 100));
        }

        [Test]
        public void zrank()
        {
            db.KeyDelete("foo");
            tairzset.exzadd("foo", "1", "a");
            tairzset.exzadd("foo", "2", "b");
            long rank = tairzset.exzrank("foo", "a");
            Assert.AreEqual(0, rank);
            rank = tairzset.exzrank("foo", "b");
            Assert.AreEqual(1, rank);


            //binary
            db.KeyDelete(bfoo);
            tairzset.exzadd(bfoo, ba, 1d);
            tairzset.exzadd(bfoo, bb, 2d);
            long brank = tairzset.exzrank(bfoo, ba);
            Assert.AreEqual(0, brank);
        }

        [Test]
        public void zrevrank()
        {
            db.KeyDelete("foo");
            tairzset.exzadd("foo", "1", "a");
            tairzset.exzadd("foo", "2", "b");
            long rank = tairzset.exzrevrank("foo", "a");
            Assert.AreEqual(1, rank);
            rank = tairzset.exzrevrank("foo", "b");
            Assert.AreEqual(0, rank);

            //binary
            db.KeyDelete(bfoo);
            tairzset.exzadd(bfoo, ba, 1d);
            tairzset.exzadd(bfoo, bb, 2d);
            long brank = tairzset.exzrevrank(bfoo, ba);
            Assert.AreEqual(1, brank);
        }

        [Test]
        public void zrangeWithScores()
        {
            db.KeyDelete("foo");
            tairzset.exzadd("foo", "a", 1);
            tairzset.exzadd("foo", "b", 10);
            tairzset.exzadd("foo", "c", 0.1);
            tairzset.exzadd("foo", "a", 2);

            List<string> expected = new List<string>();
            expected.Add("c");
            expected.Add("0.10000000000000001");
            expected.Add("a");
            expected.Add("2");

            List<String> range = tairzset.exzrangeWithScores("foo", 0, 1);
            Assert.AreEqual(expected, range);

            expected.Add("b");
            expected.Add("10");
            range = tairzset.exzrangeWithScores("foo", 0, 100);
            Assert.AreEqual(expected, range);
        }


        [Test]
        public void zcard()
        {
            db.KeyDelete("foo");
            tairzset.exzadd("foo", "a", 1d);
            tairzset.exzadd("foo", "b", 10d);
            tairzset.exzadd("foo", "c", 0.1d);
            tairzset.exzadd("foo", "a", 2d);

            long size = tairzset.exzcard("foo");
            Assert.AreEqual(3, size);

            // Binary
            db.KeyDelete(bfoo);
            tairzset.exzadd(bfoo, ba, 1d);
            tairzset.exzadd(bfoo, bb, 10d);
            tairzset.exzadd(bfoo, bc, 0.1d);
            tairzset.exzadd(bfoo, ba, 2d);

            long bsize = tairzset.exzcard(bfoo);
            Assert.AreEqual(3, bsize);
        }

        [Test]
        public void zscore()
        {
            db.KeyDelete("foo");
            tairzset.exzadd("foo", "a", 1d);
            tairzset.exzadd("foo", "b", 10d);
            tairzset.exzadd("foo", "c", 0.1d);
            tairzset.exzadd("foo", "a", 2d);

            string score = tairzset.exzscore("foo", "b");
            Assert.AreEqual("10", score);
            score = tairzset.exzscore("foo", "c");
            Assert.AreEqual("0.10000000000000001", score);

            // Binary
            db.KeyDelete(bfoo);
            tairzset.exzadd(bfoo, ba, 1d);
            tairzset.exzadd(bfoo, bb, 10d);
            tairzset.exzadd(bfoo, bc, 0.1d);
            tairzset.exzadd(bfoo, ba, 2d);

            byte[] bscore = tairzset.exzscore(bfoo, bb);
            Assert.AreEqual(Encoding.UTF8.GetBytes("10"), bscore);
        }

        [Test]
        public void zcount()
        {
            db.KeyDelete("foo");
            tairzset.exzadd("foo", "a", 1d);
            tairzset.exzadd("foo", "b", 10d);
            tairzset.exzadd("foo", "c", 0.1d);
            tairzset.exzadd("foo", "a", 2d);

            long result = tairzset.exzcount("foo", "0.01", "2.1");
            Assert.AreEqual(2, result);

            // Binary
            db.KeyDelete(bfoo);
            tairzset.exzadd(bfoo, ba, 1d);
            tairzset.exzadd(bfoo, bb, 10d);
            tairzset.exzadd(bfoo, bc, 0.1d);
            tairzset.exzadd(bfoo, ba, 2d);
            long bresult = tairzset.exzcount(bfoo, Encoding.UTF8.GetBytes("0.01"), Encoding.UTF8.GetBytes("2.1"));
            Assert.AreEqual(2, bresult);
        }

        [Test]
        public void zlexcount()
        {
            db.KeyDelete("foo");
            tairzset.exzadd("foo", "a", 1);
            tairzset.exzadd("foo", "b", 1);
            tairzset.exzadd("foo", "c", 1);
            tairzset.exzadd("foo", "aa", 1);

            long result = tairzset.exzlexcount("foo", "[aa", "(c");
            Assert.AreEqual(2, result);
            result = tairzset.exzlexcount("foo", "-", "+");
            Assert.AreEqual(4, result);
        }

        [Test]
        public void zrangebyscore()
        {
            db.KeyDelete("foo");
            tairzset.exzadd("foo", "a", 1d);
            tairzset.exzadd("foo", "b", 10d);
            tairzset.exzadd("foo", "c", 0.1d);
            tairzset.exzadd("foo", "a", 2d);

            List<string> range = tairzset.exzrangeByScore("foo", "0", "2");
            List<string> expected = new List<string>();
            expected.Add("c");
            expected.Add("a");

            Assert.AreEqual(expected, range);

            range = tairzset.exzrangeByScore("foo", "0", "2", new ExzrangeParams().limit(0, 1));
            expected.Clear();
            expected.Add("c");
            Assert.AreEqual(expected, range);
        }

        [Test]
        public void zremrangeByRank()
        {
            db.KeyDelete("foo");
            tairzset.exzadd("foo", "a", 1d);
            tairzset.exzadd("foo", "b", 10d);
            tairzset.exzadd("foo", "c", 0.1d);
            tairzset.exzadd("foo", "a", 2d);

            long result = tairzset.exzremrangeByRank("foo", 0, 0);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void zremrangeByScore()
        {
            db.KeyDelete("foo");
            tairzset.exzadd("foo", "a", 1d);
            tairzset.exzadd("foo", "b", 10d);
            tairzset.exzadd("foo", "c", 0.1d);
            tairzset.exzadd("foo", "a", 2d);

            long result = tairzset.exzremrangeByScore("foo", "0", "2");
            Assert.AreEqual(2, result);
        }

        [Test]
        public void zremrangeByLex()
        {
            db.KeyDelete("foo");
            tairzset.exzadd("foo", "a", 1);
            tairzset.exzadd("foo", "b", 1);
            tairzset.exzadd("foo", "c", 1);
            tairzset.exzadd("foo", "aa", 1);

            long result = tairzset.exzremrangeByLex("foo", "[aa", "(c");
            Assert.AreEqual(2, result);
        }
    }
}