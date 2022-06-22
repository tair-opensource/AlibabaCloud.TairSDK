using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AlibabaCloud.TairSDK.TairHash;
using AlibabaCloud.TairSDK.TairHash.Param;
using NUnit.Framework;
using StackExchange.Redis;

namespace TestHashTest
{
    public class HashTests
    {
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost");
        private readonly TairHash tair = new(connDC, 0);

        [Test]
        public void exhmsetwithopts()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var foo = "foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var randomkey_ = "randomkey_" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var randomkeyBinary_ =
                Encoding.UTF8.GetBytes("randomkey_" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var param = new List<ExhmsetwithopsParams<string>>();
            param.Add(new ExhmsetwithopsParams<string>("foo", "bar", 0, 0));
            param.Add(new ExhmsetwithopsParams<string>("bar", "foo", 0, 0));
            var status = tair.exhmsetwithopts(foo, param);
            Assert.AreEqual("OK", status);
            Assert.AreEqual("bar", tair.exhget(foo, "foo"));
            Assert.AreEqual("foo", tair.exhget(foo, "bar"));

            //binary
            var bparam = new List<ExhmsetwithopsParams<byte[]>>();
            var bparam1 = new ExhmsetwithopsParams<byte[]>(bbar, bcar, 4, 0);
            var bparam2 = new ExhmsetwithopsParams<byte[]>(bcar, bbar, 4, 0);
            bparam.Add(bparam1);
            bparam.Add(bparam2);

            var bstatus = tair.exhmsetwithopts(bfoo, bparam);
            Assert.AreEqual("OK", bstatus);
            Assert.AreEqual(bcar, tair.exhget(bfoo, bbar));
        }

        [Test]
        public void exhgetwithver()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var foo = "foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var randomkey_ = "randomkey_" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var randomkeyBinary_ =
                Encoding.UTF8.GetBytes("randomkey_" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));


            tair.exhset(foo, "bar", "car");
            var result = tair.exhgetwithver(foo, "bar");
            Assert.AreEqual("car", result.getValue());
            Assert.AreEqual(1, result.getVer());

            //binary
            tair.exhset(bfoo, bbar, bcar);
            var bresult = tair.exhgetwithver(bfoo, bbar);
            Assert.AreEqual(bcar, bresult.getValue());
            Assert.AreEqual((long) 1, bresult.getVer());
        }

        [Test]
        public void exhver()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var foo = "foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var randomkey_ = "randomkey_" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var randomkeyBinary_ =
                Encoding.UTF8.GetBytes("randomkey_" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bar = "bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var car = "car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");

            tair.exhset(foo, bar, car);
            Assert.AreEqual(1, tair.exhver(foo, bar));

            tair.exhset(bfoo, bbar, bcar);
            Assert.AreEqual((long) 1, tair.exhver(bfoo, bbar));
        }

        [Test]
        public void exhttl()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            tair.exhset(bfoo, bbar, bcar);
            tair.exhexpire(bfoo, bbar, 20);
            Assert.True(tair.exhttl(bfoo, bbar) <= 20);
            Assert.True(tair.exhttl(bfoo, bbar) > 0);
        }

        [Test]
        public void exhexpireAt()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var unixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + 20;
            tair.exhset(bfoo, bbar, bcar);
            var status = tair.exhexpireAt(bfoo, bbar, unixTime);
            Assert.True(status);
        }

        [Test]
        public void exhmgetwithver()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var bparam = new List<ExhmsetwithopsParams<byte[]>>();
            var bparam1 = new ExhmsetwithopsParams<byte[]>(bbar, bcar, 4, 0);
            var bparam2 = new ExhmsetwithopsParams<byte[]>(bcar, bbar, 4, 0);
            bparam.Add(bparam1);
            bparam.Add(bparam2);
            var bstatus = tair.exhmsetwithopts(bfoo, bparam);
            Assert.AreEqual("OK", bstatus);
            Assert.AreEqual(bcar, tair.exhget(bfoo, bbar));

            var bresults = tair.exhmgetwithver(bfoo, bbar, bcar);
            Assert.AreEqual(2, bresults.Count);
            Assert.AreEqual(1, bresults[0].getVer());
            Assert.AreEqual(bcar, bresults[0].getValue());
            Assert.AreEqual(1, bresults[1].getVer());
        }

        [Test]
        public void exhset()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var bstatus = tair.exhset(bfoo, bbar, bcar);
            Assert.AreEqual(1, bstatus);
            bstatus = tair.exhset(bfoo, bbar, bfoo);
            Assert.AreEqual(0, bstatus);
        }

        [Test]
        public void exhsetparams()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var bstatus = tair.exhset(bfoo, bbar, bcar, ExhsetParams.exhsetParams().ver(1));
            Assert.AreEqual(1, bstatus);
            Assert.AreEqual((long) 1, tair.exhver(bfoo, bbar));
            bstatus = tair.exhset(bfoo, bbar, bfoo);
            Assert.AreEqual((long) 2, tair.exhver(bfoo, bbar));
            Assert.AreEqual(0, bstatus);
        }

        [Test]
        public void exhget()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            tair.exhset(bfoo, bbar, bcar);
            Assert.Null(tair.exhget(bbar, bfoo));
            Assert.AreEqual(bcar, tair.exhget(bfoo, bbar));
        }

        [Test]
        public void exhsetnx()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var foo = "foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");

            var status = tair.exhsetnx(foo, "bar", "car");
            Assert.AreEqual(1, status);
            Assert.AreEqual("car", tair.exhget(foo, "bar"));
            status = tair.exhsetnx(foo, "bar", "foo");
            Assert.AreEqual(0, status);

            var bstatus = tair.exhsetnx(bfoo, bcar, bbar);
            Assert.AreEqual(1, bstatus);
            Assert.AreEqual(bbar, tair.exhget(bfoo, bcar));
        }

        [Test]
        public void exhmset()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var bhash = new Dictionary<byte[], byte[]>();
            bhash.Add(bbar, bcar);
            bhash.Add(bcar, bbar);
            var bstatus = tair.exhmset(bfoo, bhash);
            Assert.AreEqual("OK", bstatus);
            Assert.AreEqual(bbar, tair.exhget(bfoo, bcar));
        }

        [Test]
        public void exhmget()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var bhash = new Dictionary<byte[], byte[]>();
            bhash.Add(bbar, bcar);
            bhash.Add(bcar, bbar);
            tair.exhmset(bfoo, bhash);
            var bvalues = tair.exhmget(bfoo, bbar, bcar, bfoo);
            var bexpected = new List<byte[]>();
            bexpected.Add(bcar);
            bexpected.Add(bbar);
            bexpected.Add(null);
            Assert.AreEqual(bexpected, bvalues);
        }

        [Test]
        public void exhincrBy()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var bvalue = tair.exhincrBy(bfoo, bbar, 1);
            Assert.AreEqual(1, bvalue);
            bvalue = tair.exhincrBy(bfoo, bbar, -1);
            Assert.AreEqual((long) 0, bvalue);
            bvalue = tair.exhincrBy(bfoo, bbar, -10);
            Assert.AreEqual(-10, bvalue);
        }

        [Test]
        public void exhincrByWithBoundary()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var exhincrByParams = new ExhincrByParams();
            exhincrByParams.min(0);
            exhincrByParams.max(10);
            try
            {
                tair.exhincrBy(bfoo, bbar, 11, exhincrByParams);
            }
            catch (Exception e)
            {
                Assert.AreEqual("ERR increment or decrement would overflow", e.Message);
            }

            try
            {
                tair.exhincrBy(bfoo, bbar, -1, exhincrByParams);
            }
            catch (Exception e)
            {
                Assert.AreEqual("ERR increment or decrement would overflow", e.Message);
            }

            exhincrByParams.min(22);
            exhincrByParams.max(1);

            try
            {
                tair.exhincrBy(bfoo, bbar, 5, exhincrByParams);
            }
            catch (Exception e)
            {
                Assert.AreEqual("ERR min value is bigger than max value", e.Message);
            }
        }

        [Test]
        public void exhincrByWithExpire()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var exhincrByParams = new ExhincrByParams();
            exhincrByParams.ex(1);
            Assert.AreEqual(5, tair.exhincrBy(bfoo, bbar, 5, exhincrByParams));
            Thread.Sleep(2000);
            Assert.AreEqual(0, tair.exhlen(bfoo));
        }

        [Test]
        public void exhincrByWithVersion()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var exhincrByParams = new ExhincrByParams();
            exhincrByParams.ver(1);
            Assert.AreEqual(5, tair.exhincrBy(bfoo, bbar, 5, exhincrByParams));
            Assert.AreEqual(10, tair.exhincrBy(bfoo, bbar, 5, exhincrByParams));
            try
            {
                tair.exhincrBy(bfoo, bbar, 5, exhincrByParams);
            }
            catch (Exception e)
            {
                Assert.AreEqual("ERR update version is stale", e.Message);
            }

            Assert.AreEqual(15, tair.exhincrBy(bfoo, bbar, 5, new ExhincrByParams().abs(5)));
            Assert.AreEqual(5, tair.exhver(bfoo, bbar));
            Assert.AreEqual(20, tair.exhincrBy(bfoo, bbar, 5, new ExhincrByParams().ver(0)));
            Assert.AreEqual(6, tair.exhver(bfoo, bbar));
        }

        [Test]
        public void exhincrByFloat()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var bvalue = tair.exhincrByFloat(bfoo, bbar, 1.5d);
            Assert.AreEqual(1.5d, bvalue);
            bvalue = tair.exhincrByFloat(bfoo, bbar, -1.5d);
            Assert.AreEqual(0d, bvalue);
        }

        [Test]
        public void exhexists()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var bhash = new Dictionary<byte[], byte[]>();
            bhash.Add(bbar, bcar);
            bhash.Add(bcar, bbar);
            tair.exhmset(bfoo, bhash);

            Assert.False(tair.exhexists(bbar, bfoo));
            Assert.True(tair.exhexists(bfoo, bbar));
        }

        [Test]
        public void exhdel()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var bhash = new Dictionary<byte[], byte[]>();
            bhash.Add(bbar, bcar);
            bhash.Add(bcar, bbar);
            tair.exhmset(bfoo, bhash);

            Assert.AreEqual((long) 0, tair.exhdel(bbar, bfoo));
            Assert.AreEqual((long) 1, tair.exhdel(bfoo, bbar));
            Assert.Null(tair.exhget(bfoo, bbar));
        }

        [Test]
        public void exhlen()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var bhash = new Dictionary<byte[], byte[]>();
            bhash.Add(bbar, bcar);
            bhash.Add(bcar, bbar);
            tair.exhmset(bfoo, bhash);

            Assert.AreEqual((long) 0, tair.exhlen(bbar));
            Assert.AreEqual((long) 2, tair.exhlen(bfoo));
        }

        [Test]
        public void exhkeys()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var bhash = new Dictionary<byte[], byte[]>();
            bhash.Add(bbar, bcar);
            bhash.Add(bcar, bbar);
            tair.exhmset(bfoo, bhash);

            var bkeys = tair.exhkeys(bfoo);
            var bexpected = new HashSet<byte[]>();
            bexpected.Add(bbar);
            bexpected.Add(bcar);
            Assert.AreEqual(bexpected.ToList(), bkeys.ToList());
        }

        [Test]
        public void exhvals()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));

            var bhash = new Dictionary<byte[], byte[]>();
            bhash.Add(bbar, bcar);
            bhash.Add(bcar, bbar);
            tair.exhmset(bfoo, bhash);

            var bvals = tair.exhvals(bfoo);
            Assert.AreEqual(2, bvals.Count);
            var bexpected = new List<byte[]>();
            bexpected.Add(bcar);
            bexpected.Add(bbar);
            Assert.AreEqual(bexpected, bvals);
        }

        [Test]
        public void exhgetAll()
        {
            var bbar =
                Encoding.UTF8.GetBytes("bar" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bcar =
                Encoding.UTF8.GetBytes("car" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bfoo =
                Encoding.UTF8.GetBytes("foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var foo = "foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");

            var h = new Dictionary<string, string>();
            h.Add("sdf", "rtf");
            h.Add("dsf", "erw");
            tair.exhmset(foo, h);
            var hash = tair.exhgetAll(foo);
            Assert.AreEqual(2, hash.Count);
            Assert.AreEqual("rtf", hash["sdf"]);

            var bh = new Dictionary<byte[], byte[]>();
            bh.Add(bbar, bcar);
            bh.Add(bcar, bbar);
            tair.exhmset(bfoo, bh);
            var bhash = tair.exhgetAll(bfoo);
            Assert.AreEqual(2, bhash.Count);
        }

        [Test]
        public void exhscanTest()
        {
            var foo = "foo" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");

            var dict = new Dictionary<string, string>();
            for (var i = 1; i < 10; i++) dict.Add("field" + i, "val" + i);

            tair.exhmset(foo, dict);

            var scanparam = new ExhscanParams();
            scanparam.count(3);
            var scanresult = tair.exhscan(foo, "^", "", scanparam);
            var j = 1;
            foreach (var entry in scanresult.getResult())
            {
                Assert.AreEqual("field" + j, entry.Key);
                Assert.AreEqual("val" + j, entry.Value);
                j++;
            }

            scanresult = tair.exhscan(foo, ">=", scanresult.getCursor());
            foreach (var entry in scanresult.getResult())
            {
                Assert.AreEqual("field" + j, entry.Key);
                Assert.AreEqual("val" + j, entry.Value);
                j++;
            }
        }
    }
}