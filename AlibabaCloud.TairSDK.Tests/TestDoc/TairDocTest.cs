using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using AlibabaCloud.TairSDK.TairDoc;
using AlibabaCloud.TairSDK.TairDoc.Param;
using StackExchange.Redis;

namespace TestDocTest
{
    public class DocTest
    {
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost");
        private readonly TairDoc tair = new(connDC, 0);

        [Test]
        public void jsonSetTest()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string JSON_STRING_EXAMPLE = "{\"foo\":\"bar\",\"baz\":42}";
            string ret = tair.jsonset(jsonKey, ".", JSON_STRING_EXAMPLE);
            Assert.AreEqual("OK", ret);

            ret = tair.jsonget(jsonKey, ".");
            Assert.AreEqual(JSON_STRING_EXAMPLE, ret);

            ret = tair.jsonget(jsonKey, ".foo");
            Assert.AreEqual("\"bar\"", ret);

            ret = tair.jsonget(jsonKey, ".baz");
            Assert.AreEqual("42", ret);
        }

        [Test]
        public void jsonSetTestBinary()
        {
        }

        [Test]
        public void jsonSetWithNXXX()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string JSON_STRING_EXAMPLE = "{\"foo\":\"bar\",\"baz\":42}";
            string ret = tair.jsonset(jsonKey, ".", JSON_STRING_EXAMPLE, JsonsetParams.jsonsetParams().xx());
            Assert.IsEmpty(ret);

            ret = tair.jsonset(jsonKey, ".", JSON_STRING_EXAMPLE, JsonsetParams.jsonsetParams().nx());
            Assert.AreEqual("OK", ret);

            ret = tair.jsonset(jsonKey, ".", JSON_STRING_EXAMPLE, JsonsetParams.jsonsetParams().xx());
            Assert.AreEqual("OK", ret);
        }

        [Test]
        public void jsonMgetTest()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string JSON_STRING_EXAMPLE = "{\"foo\":\"bar\",\"baz\":42}";
            string jsonkey1 = jsonKey + "1";
            string jsonkey2 = jsonKey + "2";
            string jsonkey3 = jsonKey + "3";

            Assert.AreEqual("OK", tair.jsonset(jsonkey1, ".", JSON_STRING_EXAMPLE));
            Assert.AreEqual("OK", tair.jsonset(jsonkey2, ".", JSON_STRING_EXAMPLE));
            Assert.AreEqual("OK", tair.jsonset(jsonkey3, ".", JSON_STRING_EXAMPLE));
            List<string> mgetret = tair.jsonmget(jsonkey1, jsonkey2, jsonkey3, ".baz");
            for (int i = 0; i < mgetret.Count; i++)
            {
                Assert.AreEqual("42", mgetret[i]);
            }
        }

        [Test]
        public void jsonSetWithException()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string JSON_STRING_EXAMPLE = "{\"foo\":\"bar\",\"baz\":42}";
            try
            {
                tair.jsonset(jsonKey, "/abc", JSON_STRING_EXAMPLE);
            }
            catch (Exception e)
            {
                Assert.AreEqual("ERR new objects must be created at the root", e.Message);
            }
        }

        [Test]
        public void jsonGetTest()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string JSON_STRING_EXAMPLE = "{\"foo\":\"bar\",\"baz\":42}";
            string ret = tair.jsonset(jsonKey, ".", JSON_STRING_EXAMPLE);
            Assert.AreEqual("OK", ret);

            ret = tair.jsonget(jsonKey, ".");
            Assert.AreEqual(JSON_STRING_EXAMPLE, ret);

            ret = tair.jsonget(jsonKey, ".foo");
            Assert.AreEqual("\"bar\"", ret);

            ret = tair.jsonget(jsonKey, ".baz");
            Assert.AreEqual("42", ret);

            try
            {
                tair.jsonget(jsonKey, ".not-exists");
            }
            catch (Exception e)
            {
                Assert.True(e.Message.Contains("ERR pointer illegal"));
            }
        }

        [Test]
        public void jsonGetWithXmlAndYaml()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string JSON_STRING_EXAMPLE = "{\"foo\":\"bar\",\"baz\":42}";
            string ret = tair.jsonset(jsonKey, ".", JSON_STRING_EXAMPLE);
            Assert.AreEqual("OK", ret);

            ret = tair.jsonget(jsonKey, ".", JsongetParams.jsongetParams().format("xml"));
            Assert.AreEqual("<?xml version=\"1.0\" encoding=\"UTF-8\"?><root><foo>bar</foo><baz>42</baz></root>", ret);

            ret = tair.jsonget(jsonKey, ".", JsongetParams.jsongetParams().format("yaml"));
            Assert.AreEqual("\nfoo: bar\nbaz: 42\n", ret);

            byte[] bret = tair.jsonget(Encoding.UTF8.GetBytes(jsonKey), Encoding.UTF8.GetBytes("."),
                JsongetParams.jsongetParams().format("yaml"));
            Assert.AreEqual("\nfoo: bar\nbaz: 42\n", Encoding.UTF8.GetString(bret));
        }

        [Test]
        public void jsonDelTest()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string JSON_STRING_EXAMPLE = "{\"foo\":\"bar\",\"baz\":42}";
            string ret = tair.jsonset(jsonKey, ".", JSON_STRING_EXAMPLE);
            Assert.AreEqual("OK", ret);

            long lret = tair.jsondel(jsonKey, ".foo");
            Assert.AreEqual(1, lret);

            try
            {
                tair.jsondel(jsonKey, ".not-exists");
            }
            catch (Exception e)
            {
                Assert.True(e.Message.Contains("ERR old item is null"));
            }
        }

        [Test]
        public void jsonTyepTest()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string JSON_STRING_EXAMPLE = "{\"foo\":\"bar\",\"baz\":42}";
            string ret = tair.jsonset(jsonKey, ".", JSON_STRING_EXAMPLE);
            Assert.AreEqual("OK", ret);

            ret = tair.jsontype(jsonKey);
            Assert.AreEqual("object", ret);

            ret = tair.jsontype(jsonKey, ".foo");
            Assert.AreEqual("string", ret);

            ret = tair.jsontype(jsonKey, ".baz");
            Assert.AreEqual("number", ret);
        }

        [Test]
        public void jsonNumincrbyTest()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string JSON_STRING_EXAMPLE = "{\"foo\":\"bar\",\"baz\":42}";
            string ret = tair.jsonset(jsonKey, ".", JSON_STRING_EXAMPLE);
            Assert.AreEqual("OK", ret);

            double dret = tair.jsonnumincrBy(jsonKey, ".baz", 1d);
            Assert.AreEqual(43, dret, 0.1);

            dret = tair.jsonnumincrBy(Encoding.UTF8.GetBytes(jsonKey), Encoding.UTF8.GetBytes(".baz"), 1.5d);
            Assert.AreEqual(44.5, dret, 0.1);

            try
            {
                tair.jsonnumincrBy(jsonKey, ".foo", 1d);
            }
            catch (Exception e)
            {
                Assert.True(e.Message.Contains("ERR node not exists"));
            }
        }

        [Test]
        public void jsonStrappendTest()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string JSON_STRING_EXAMPLE = "{\"foo\":\"bar\",\"baz\":42}";
            string ret = tair.jsonset(jsonKey, ".", JSON_STRING_EXAMPLE);
            Assert.AreEqual("OK", ret);

            long lret = tair.jsonstrAppend(jsonKey, ".foo", "rrrr");
            Assert.AreEqual(7, lret);

            lret = tair.jsonstrAppend(Encoding.UTF8.GetBytes(jsonKey), Encoding.UTF8.GetBytes(".foo"),
                Encoding.UTF8.GetBytes("r"));
            Assert.AreEqual(8, lret);

            try
            {
                tair.jsonstrAppend(jsonKey, ".not-exists");
            }
            catch (Exception e)
            {
                Assert.True(e.Message.Contains("ERR node not exists"));
            }
        }

        [Test]
        public void jsonStrlenTest()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string JSON_STRING_EXAMPLE = "{\"foo\":\"bar\",\"baz\":42}";
            string ret = tair.jsonset(jsonKey, ".", JSON_STRING_EXAMPLE);
            Assert.AreEqual("OK", ret);

            long lret = tair.jsonstrAppend(jsonKey, ".foo", "rrrrr");
            Assert.AreEqual(lret, 8);

            lret = tair.jsonstrlen(jsonKey, ".foo");
            Assert.AreEqual(8, lret);
        }

        [Test]
        public void jsonArrappendTest()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string JSON_ARRAY_EXAMPLE = "{\"id\":[1,2,3]}";
            string ret = tair.jsonset(jsonKey, ".", JSON_ARRAY_EXAMPLE);
            Assert.AreEqual("OK", ret);

            long lret = tair.jsonarrAppend(jsonKey, ".id", "null", "false", "true");
            Assert.AreEqual(6, lret);
            ret = tair.jsonget(jsonKey, ".id.2");
            Assert.AreEqual("3", ret);
        }

        [Test]
        public void jsonArrpopTest()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string JSON_ARRAY_EXAMPLE = "{\"id\":[1,2,3]}";
            string ret = tair.jsonset(jsonKey, ".", JSON_ARRAY_EXAMPLE);
            Assert.AreEqual("OK", ret);

            ret = tair.jsonarrPop(jsonKey, ".id", 1);
            Assert.AreEqual("2", ret);

            byte[] bret = tair.jsonarrPop(Encoding.UTF8.GetBytes(jsonKey), Encoding.UTF8.GetBytes(".id"), -1);
            Assert.AreEqual("3", Encoding.UTF8.GetString(bret));

            try
            {
                tair.jsonarrPop(jsonKey, ".id", 10);
            }
            catch (Exception e)
            {
                Assert.True(e.Message.Contains("ERR array index outflow"));
            }
        }

        [Test]
        public void jsonArrinsertTest()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string JSON_ARRAY_EXAMPLE = "{\"id\":[1,2,3]}";
            string ret = tair.jsonset(jsonKey, ".", JSON_ARRAY_EXAMPLE);
            Assert.AreEqual("OK", ret);

            long lret = tair.jsonarrInsert(jsonKey, ".id", "3", "5", "6");
            Assert.AreEqual(5, lret);

            ret = tair.jsonget(jsonKey, ".id");
            Assert.AreEqual("[1,2,3,5,6]", ret);

            lret = tair.jsonarrInsert(Encoding.UTF8.GetBytes(jsonKey), Encoding.UTF8.GetBytes(".id"),
                Encoding.UTF8.GetBytes("3"), Encoding.UTF8.GetBytes("4"));
            Assert.AreEqual(6, lret);
            ret = tair.jsonget(jsonKey, ".id");
            Assert.AreEqual("[1,2,3,4,5,6]", ret);
        }

        [Test]
        public void jsonArrlenTest()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string JSON_ARRAY_EXAMPLE = "{\"id\":[1,2,3]}";
            string ret = tair.jsonset(jsonKey, ".", JSON_ARRAY_EXAMPLE);
            Assert.AreEqual("OK", ret);

            long lret = tair.jsonArrLen(jsonKey, ".id");
            Assert.AreEqual(3, lret);

            lret = tair.jsonArrLen(Encoding.UTF8.GetBytes(jsonKey), Encoding.UTF8.GetBytes(".id"));
            Assert.AreEqual(3, lret);
        }

        [Test]
        public void jsonArrtrimTest()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string ret = tair.jsonset(jsonKey, ".", "{\"id\":[1,2,3,4,5,6]}");
            Assert.AreEqual("OK", ret);

            long lret = tair.jsonarrTrim(jsonKey, ".id", 3, 4);
            Assert.AreEqual(2, lret);

            byte[] bret = tair.jsonget(Encoding.UTF8.GetBytes(jsonKey), Encoding.UTF8.GetBytes(".id"));
            Assert.AreEqual("[4,5]", Encoding.UTF8.GetString(bret));

            lret = tair.jsonarrTrim(Encoding.UTF8.GetBytes(jsonKey), Encoding.UTF8.GetBytes(".id"), 0, 0);
            Assert.AreEqual(1, lret);

            try
            {
                tair.jsonarrTrim(jsonKey, ".id", 3, 4);
            }
            catch (Exception e)
            {
                Assert.True(e.Message.Contains("ERR array index outflow"));
            }
        }
    }
}