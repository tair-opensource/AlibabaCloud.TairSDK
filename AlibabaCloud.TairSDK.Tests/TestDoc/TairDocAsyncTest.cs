using System;
using System.Threading;
using AlibabaCloud.TairSDK;
using AlibabaCloud.TairSDK.TairDoc;
using AlibabaCloud.TairSDK.TairDoc.Param;
using NUnit.Framework;
using StackExchange.Redis;

namespace TestDocTest
{
    public class DocAsyncTest
    {
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost");
        private readonly TairDocAsync tairDocAsync = new(connDC, 0);

        [Test]
        public void jsonTest1()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string JSON_STRING_EXAMPLE = "{\"foo\":\"bar\",\"baz\":42, \"id\":[1,2,3]}";
            var ret1 = tairDocAsync.jsonset(jsonKey, ".", JSON_STRING_EXAMPLE);
            var ret2 = tairDocAsync.jsonget(jsonKey, ".foo");
            var ret3 = tairDocAsync.jsontype(jsonKey, ".foo");
            var ret4 = tairDocAsync.jsonstrAppend(jsonKey, ".foo", "r");
            var ret5 = tairDocAsync.jsonstrlen(jsonKey, ".foo");
            var ret6 = tairDocAsync.jsonnumincrBy(jsonKey, ".baz", 10.0);
            var ret7 = tairDocAsync.jsonarrAppend(jsonKey, ".id", "4", "5");
            var ret8 = tairDocAsync.jsonarrPop(jsonKey, ".id");
            var ret9 = tairDocAsync.jsonarrInsert(jsonKey, ".id", "3", "5", "6");
            var ret10 = tairDocAsync.jsonarrTrim(jsonKey, ".id", 4, 5);
            var ret11 = tairDocAsync.jsonArrLen(jsonKey, ".id");
            var ret12 = tairDocAsync.jsonarrPop(jsonKey, ".id", 0);

            Assert.AreEqual("OK", ResultHelper.String(ret1.Result));
            Assert.AreEqual("\"bar\"", ResultHelper.String(ret2.Result));
            Assert.AreEqual("string", ResultHelper.String(ret3.Result));
            Assert.AreEqual(4, ResultHelper.Long(ret4.Result));
            Assert.AreEqual(4, ResultHelper.Long(ret5.Result));
            Assert.AreEqual(52.0, ResultHelper.Double(ret6.Result));
            Assert.AreEqual(5, ResultHelper.Long(ret7.Result));
            Assert.AreEqual("5", ResultHelper.String(ret8.Result));
            Assert.AreEqual(6, ResultHelper.Long(ret9.Result));
            Assert.AreEqual(2, ResultHelper.Long(ret10.Result));
            Assert.AreEqual(2, (ResultHelper.Long(ret11.Result)));
            Assert.AreEqual("6", ResultHelper.String(ret12.Result));
        }

        [Test]
        public void jsonTest2()
        {
            string jsonKey = "jsonkey" + "-" + Thread.CurrentThread.Name + "-" + Guid.NewGuid().ToString("N");
            string JSON_STRING_EXAMPLE = "{\"foo\":\"bar\",\"baz\":42, \"id\":[1,2,3]}";
            var ret1 = tairDocAsync.jsonset(jsonKey, ".", JSON_STRING_EXAMPLE, new JsonsetParams().nx());
            var ret2 = tairDocAsync.jsonget(jsonKey);
            var ret3 = tairDocAsync.jsonget(jsonKey, ".id", new JsongetParams().format("yaml"));
            var ret4 = tairDocAsync.jsondel(jsonKey, ".id");
            var ret5 = tairDocAsync.jsondel(jsonKey);
            var ret6 = tairDocAsync.jsonset(jsonKey, ".", "42", new JsonsetParams().nx());
            var ret7 = tairDocAsync.jsontype(jsonKey);
            var ret8 = tairDocAsync.jsonnumincrBy(jsonKey, 10.0);
            var ret9 = tairDocAsync.jsonset(jsonKey, ".", "\"bar\"");
            var ret10 = tairDocAsync.jsonstrAppend(jsonKey, "r");
            var ret11 = tairDocAsync.jsonstrlen(jsonKey);
            var ret12 = tairDocAsync.jsonset(jsonKey, ".", "[1,2,3]");
            var ret13 = tairDocAsync.jsonArrLen(jsonKey);
            var ret14 = tairDocAsync.jsonmget(jsonKey, ".");

            Assert.AreEqual("OK", ResultHelper.String(ret1.Result));
            Assert.AreEqual("{\"foo\":\"bar\",\"baz\":42,\"id\":[1,2,3]}", ResultHelper.String(ret2.Result));
            Assert.AreEqual("\n- 1\n- 2\n- 3", ResultHelper.String(ret3.Result));
            Assert.AreEqual(1, ResultHelper.Long(ret4.Result));
            Assert.AreEqual(1, ResultHelper.Long(ret5.Result));
            Assert.AreEqual("OK", ResultHelper.String(ret6.Result));
            Assert.AreEqual("number", ResultHelper.String(ret7.Result));
            Assert.AreEqual(52.0, ResultHelper.Double(ret8.Result));
            Assert.AreEqual("OK", ResultHelper.String(ret9.Result));
            Assert.AreEqual(4, ResultHelper.Long(ret10.Result));
            Assert.AreEqual(4, (ResultHelper.Long(ret11.Result)));
            Assert.AreEqual("OK", ResultHelper.String(ret12.Result));
            Assert.AreEqual(3, ResultHelper.Double(ret13.Result));
            Assert.AreEqual(1, ResultHelper.ListString(ret14.Result).Count);
        }
    }
}