using System.Collections.Generic;
using AlibabaCloud.TairSDK;
using AlibabaCloud.TairSDK.TairSearch;
using AlibabaCloud.TairSDK.TairString;
using NUnit.Framework;
using StackExchange.Redis;

namespace TestSearchTest
{
    public class SearchAsyncTest
    {
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost");
        private IDatabase db = connDC.GetDatabase(0);
        private readonly TairSearchAsync tairSearchAsync = new(connDC, 0);

        [Test]
        public void tftTest()
        {
            db.KeyDelete("tftkey");
            var ret1 = tairSearchAsync.tftmappingindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"text\"},\"f1\":{\"type\":\"text\"}}}}");
            var ret2 = tairSearchAsync.tftadddoc("tftkey", "{\"f0\":\"v0\",\"f1\":\"3\"}", "1");
            var ret3 = tairSearchAsync.tftadddoc("tftkey", "{\"f0\":\"v1\",\"f1\":\"3\"}", "2");
            var ret4 = tairSearchAsync.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"3\"}", "3");
            var ret5 = tairSearchAsync.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"4\"}", "4");
            var ret6 = tairSearchAsync.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"5\"}", "5");
            var ret7 = tairSearchAsync.tftsearch("tftkey", "{\"query\":{\"match\":{\"f1\":\"3\"}}}");
            var ret8 = tairSearchAsync.tftsearch("tftkey", "{\"query\":{\"match\":{\"f1\":\"3\"}}}", true);
            var ret9 = tairSearchAsync.tftgetdoc("tftkey", "3");
            var ret10 = tairSearchAsync.tftdeldoc("tftkey", "3");
            var ret11 = tairSearchAsync.tftgetdoc("tftkey", "3");
            var ret12 = tairSearchAsync.tftgetindexmappings("tftkey");

            Assert.AreEqual(
                "{\"hits\":{\"hits\":[{\"_id\":\"1\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v0\",\"f1\":\"3\"}},{\"_id\":\"2\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v1\",\"f1\":\"3\"}},{\"_id\":\"3\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v3\",\"f1\":\"3\"}}],\"max_score\":1.223144,\"total\":{\"relation\":\"eq\",\"value\":3}}}",
                ResultHelper.String(ret7.Result));
            Assert.AreEqual(
                "{\"hits\":{\"hits\":[{\"_id\":\"1\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v0\",\"f1\":\"3\"}},{\"_id\":\"2\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v1\",\"f1\":\"3\"}},{\"_id\":\"3\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v3\",\"f1\":\"3\"}}],\"max_score\":1.223144,\"total\":{\"relation\":\"eq\",\"value\":3}}}",
                ResultHelper.String(ret8.Result));
            Assert.AreEqual("{\"_id\":\"3\",\"_source\":{\"f0\":\"v3\",\"f1\":\"3\"}}",
                ResultHelper.String(ret9.Result));
            Assert.AreEqual("1", ResultHelper.String(ret10.Result));
            Assert.AreEqual(null, ResultHelper.String(ret11.Result));
            Assert.AreEqual(
                "{\"tftkey\":{\"mappings\":{\"_source\":{\"enabled\":true,\"excludes\":[],\"includes\":[]},\"dynamic\":\"false\",\"properties\":{\"f0\":{\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\"},\"f1\":{\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\"}}}}}",
                ResultHelper.String(ret12.Result));
        }

        [Test]
        public void ftfmaddTest()
        {
            db.KeyDelete("tftkey");
            var ret1 = tairSearchAsync.tftmappingindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"text\"},\"f1\":{\"type\":\"text\"}}}}");
            Dictionary<string, string> docs = new Dictionary<string, string>();

            docs.Add("{\"f0\":\"v0\",\"f1\":\"3\"}", "1");
            docs.Add("{\"f0\":\"v1\",\"f1\":\"3\"}", "2");
            docs.Add("{\"f0\":\"v3\",\"f1\":\"3\"}", "3");
            docs.Add("{\"f0\":\"v3\",\"f1\":\"4\"}", "4");
            docs.Add("{\"f0\":\"v3\",\"f1\":\"5\"}", "5");

            var ret2 = tairSearchAsync.tftmadddoc("tftkey", docs);
            var ret3 = tairSearchAsync.tftsearch("tftkey", "{\"query\":{\"match\":{\"f1\":\"3\"}}}");
            var ret4 = tairSearchAsync.tftsearch("tftkey", "{\"query\":{\"match\":{\"f1\":\"3\"}}}", true);
            var ret5 = tairSearchAsync.tftgetdoc("tftkey", "3");
            var ret6 = tairSearchAsync.tftdeldoc("tftkey", "3");
            var ret7 = tairSearchAsync.tftgetdoc("tftkey", "3");
            var ret8 = tairSearchAsync.tftgetindexmappings("tftkey");

            Assert.AreEqual(
                "{\"hits\":{\"hits\":[{\"_id\":\"1\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v0\",\"f1\":\"3\"}},{\"_id\":\"2\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v1\",\"f1\":\"3\"}},{\"_id\":\"3\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v3\",\"f1\":\"3\"}}],\"max_score\":1.223144,\"total\":{\"relation\":\"eq\",\"value\":3}}}",
                ResultHelper.String(ret3.Result));

            Assert.AreEqual(
                "{\"hits\":{\"hits\":[{\"_id\":\"1\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v0\",\"f1\":\"3\"}},{\"_id\":\"2\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v1\",\"f1\":\"3\"}},{\"_id\":\"3\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v3\",\"f1\":\"3\"}}],\"max_score\":1.223144,\"total\":{\"relation\":\"eq\",\"value\":3}}}",
                ResultHelper.String(ret4.Result));

            Assert.AreEqual("{\"_id\":\"3\",\"_source\":{\"f0\":\"v3\",\"f1\":\"3\"}}",
                ResultHelper.String(ret5.Result));
            Assert.AreEqual("1", ResultHelper.String(ret6.Result));
            Assert.AreEqual(null, ResultHelper.String(ret7.Result));

            Assert.AreEqual(
                "{\"tftkey\":{\"mappings\":{\"_source\":{\"enabled\":true,\"excludes\":[],\"includes\":[]},\"dynamic\":\"false\",\"properties\":{\"f0\":{\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\"},\"f1\":{\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\"}}}}}",
                ResultHelper.String(ret8.Result));
        }
    }
}