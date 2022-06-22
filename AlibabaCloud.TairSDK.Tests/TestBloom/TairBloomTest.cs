using System;
using System.Text;
using System.Threading;
using StackExchange.Redis;
using AlibabaCloud.TairSDK.TairBloom;
using NUnit.Framework;

namespace TestBloomTest
{
    public class BloomTest
    {
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost");
        private readonly TairBloom tair = new(connDC, 0);

        [Test]
        public void bfaddTest()
        {
            string bbf = "bbf" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            byte[] bcf = Encoding.UTF8.GetBytes("bcf" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            string ret = "";
            bool ret_bool = false;

            ret = tair.bfreserve(bbf, 0.01, 100);
            Assert.AreEqual("OK", ret);
            ret_bool = tair.bfadd(bbf, "val1");
            Assert.AreEqual(true, ret_bool);
            ret_bool = tair.bfexists(bbf, "val1");
            Assert.AreEqual(true, ret_bool);
            ret_bool = tair.bfexists(bbf, "val2");
            Assert.AreEqual(false, ret_bool);

            //binary
            ret = tair.bfreserve(bcf, 0.01, 100);
            Assert.AreEqual("OK", ret);
            ret_bool = tair.bfadd(bcf, Encoding.UTF8.GetBytes("val1"));
            Assert.AreEqual(true, ret_bool);
            ret_bool = tair.bfexists(bcf, Encoding.UTF8.GetBytes("val1"));
            Assert.AreEqual(true, ret_bool);
            ret_bool = tair.bfexists(bcf, Encoding.UTF8.GetBytes("val2"));
            Assert.AreEqual(false, ret_bool);
        }

        [Test]
        public void bfmaddTest()
        {
            string bbf = "bbf" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            byte[] bcf = Encoding.UTF8.GetBytes("bcf" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            string ret = "";
            bool[] ret_bool_list;

            ret = tair.bfreserve(bbf, 0.01, 100);
            Assert.AreEqual("OK", ret);
            ret_bool_list = tair.bfmadd(bbf, "val1", "val2");
            Assert.AreEqual(true, ret_bool_list[0]);
            Assert.AreEqual(true, ret_bool_list[1]);
            ret_bool_list = tair.bfmexists(bbf, "val1", "val2");
            Assert.AreEqual(true, ret_bool_list[0]);
            Assert.AreEqual(true, ret_bool_list[1]);


            ret = tair.bfreserve(bcf, 0.01, 100);
            Assert.AreEqual("OK", ret);
            ret_bool_list = tair.bfmadd(bcf, Encoding.UTF8.GetBytes("val1"), Encoding.UTF8.GetBytes("val2"));
            Assert.AreEqual(true, ret_bool_list[0]);
            Assert.AreEqual(true, ret_bool_list[1]);
            ret_bool_list = tair.bfmexists(bcf, Encoding.UTF8.GetBytes("val1"), Encoding.UTF8.GetBytes("val2"));
            Assert.AreEqual(true, ret_bool_list[0]);
            Assert.AreEqual(true, ret_bool_list[1]);
        }

        [Test]
        public void bfinsertTest()
        {
            string bbf = "bbf" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            byte[] bcf = Encoding.UTF8.GetBytes("bcf" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            string ret = "";
            bool[] ret_bool_list;

            ret_bool_list = tair.bfinsert(bbf, "CAPACITY", 100, "ERROR", 0.001, "ITEMS", "val1", "val2");
            Assert.AreEqual(true, ret_bool_list[0]);
            Assert.AreEqual(true, ret_bool_list[1]);

            ret_bool_list = tair.bfinsert(bcf, Encoding.UTF8.GetBytes("CAPACITY"), 100, Encoding.UTF8.GetBytes("ERROR"),
                0.001, Encoding.UTF8.GetBytes("ITEMS"), Encoding.UTF8.GetBytes("val1"), Encoding.UTF8.GetBytes("val2"));
            Assert.AreEqual(true, ret_bool_list[0]);
            Assert.AreEqual(true, ret_bool_list[1]);
        }
    }
}