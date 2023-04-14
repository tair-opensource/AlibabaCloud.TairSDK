using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AlibabaCloud.TairSDK.TairSearch;
using AlibabaCloud.TairSDK.TairSearch.Param;
using NUnit.Framework;
using StackExchange.Redis;

namespace TestSearchTest
{
    public class SearchTests
    {
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost");
        private IDatabase db = connDC.GetDatabase(0);
        private readonly TairSearch tair = new(connDC, 0);

        [Test]
        public void tftcreateindex()
        {
            db.KeyDelete("tftkey");
            string ret = tair.tftcreateindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"text\"},\"f1\":{\"type\":\"text\"}}}}");
            Assert.AreEqual("OK", ret);

            string mapping = tair.tftgetindexmappings("tftkey");
            Assert.AreEqual(
                "{\"tftkey\":{\"mappings\":{\"_source\":{\"enabled\":true,\"excludes\":[],\"includes\":[]},\"dynamic\":\"false\",\"properties\":{\"f0\":{\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\"},\"f1\":{\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\"}}}}}",
                mapping);
        }

        [Test]
        public void tftupdateindex()
        {
            db.KeyDelete("tftkey");
            string ret = tair.tftcreateindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"text\"}}}}");
            Assert.AreEqual("OK", ret);

            string mapping = tair.tftgetindexmappings("tftkey");
            Assert.AreEqual(
                "{\"tftkey\":{\"mappings\":{\"_source\":{\"enabled\":true,\"excludes\":[],\"includes\":[]},\"dynamic\":\"false\",\"properties\":{\"f0\":{\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\"}}}}}",
                mapping);

            ret = tair.tftupdateindex("tftkey", "{\"mappings\":{\"properties\":{\"f1\":{\"type\":\"text\"}}}}");
            Assert.AreEqual("OK", ret);

            mapping = tair.tftgetindexmappings("tftkey");
            Assert.AreEqual(
                "{\"tftkey\":{\"mappings\":{\"_source\":{\"enabled\":true,\"excludes\":[],\"includes\":[]},\"dynamic\":\"false\",\"properties\":{\"f0\":{\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\"},\"f1\":{\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\"}}}}}",
                mapping);
        }

        [Test]
        public void tftadddoc()
        {
            db.KeyDelete("tftkey");
            tair.tftcreateindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"text\"},\"f1\":{\"type\":\"text\"}}}}");
            tair.tftadddoc("tftkey", "{\"f0\":\"v0\",\"f1\":\"3\"}", "1");
            tair.tftadddoc("tftkey", "{\"f0\":\"v1\",\"f1\":\"3\"}", "2");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"3\"}", "3");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"4\"}", "4");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"5\"}", "5");

            Assert.AreEqual(
                "{\"hits\":{\"hits\":[{\"_id\":\"1\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v0\",\"f1\":\"3\"}},{\"_id\":\"2\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v1\",\"f1\":\"3\"}},{\"_id\":\"3\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v3\",\"f1\":\"3\"}}],\"max_score\":1.223144,\"total\":{\"relation\":\"eq\",\"value\":3}}}",
                tair.tftsearch("tftkey", "{\"query\":{\"match\":{\"f1\":\"3\"}}}"));

            Assert.AreEqual("{\"_id\":\"3\",\"_source\":{\"f0\":\"v3\",\"f1\":\"3\"}}", tair.tftgetdoc("tftkey", "3"));
            Assert.AreEqual("1", tair.tftdeldoc("tftkey", "3"));
            Assert.AreEqual(null, tair.tftgetdoc("tftkey", "3"));

            Assert.AreEqual(
                "{\"tftkey\":{\"mappings\":{\"_source\":{\"enabled\":true,\"excludes\":[],\"includes\":[]},\"dynamic\":\"false\",\"properties\":{\"f0\":{\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\"},\"f1\":{\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\"}}}}}",
                tair.tftgetindexmappings("tftkey"));
        }

        [Test]
        public void tftupdatedocfield()
        {
            db.KeyDelete("tftkey");
            string ret = tair.tftcreateindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"text\"}}}}");
            Assert.AreEqual(ret, "OK");

            tair.tftadddoc("tftkey", "{\"f0\":\"redis is a nosql database\"}", "1");
            Assert.AreEqual(
                "{\"hits\":{\"hits\":[{\"_id\":\"1\",\"_index\":\"tftkey\",\"_score\":0.153426,\"_source\":{\"f0\":\"redis is a nosql database\"}}],\"max_score\":0.153426,\"total\":{\"relation\":\"eq\",\"value\":1}}}",
                tair.tftsearch("tftkey", "{\"query\":{\"term\":{\"f0\":\"redis\"}}}"));

            ret = tair.tftupdateindex("tftkey", "{\"mappings\":{\"properties\":{\"f1\":{\"type\":\"text\"}}}}");
            Assert.AreEqual(ret, "OK");

            tair.tftupdatedocfield("tftkey", "1", "{\"f1\":\"mysql is a dbms\"}");
            Assert.AreEqual(
                "{\"hits\":{\"hits\":[{\"_id\":\"1\",\"_index\":\"tftkey\",\"_score\":0.191783,\"_source\":{\"f0\":\"redis is a nosql database\",\"f1\":\"mysql is a dbms\"}}],\"max_score\":0.191783,\"total\":{\"relation\":\"eq\",\"value\":1}}}",
                tair.tftsearch("tftkey", "{\"query\":{\"term\":{\"f1\":\"mysql\"}}}"));
        }

        [Test]
        public void tftincrlongdocfield()
        {
            db.KeyDelete("tftkey");
            try
            {
                tair.tftincrlongdocfield("tftkey", "1", "f0", 1);
            }
            catch (Exception e)
            {
                Assert.AreEqual("ERR index [tftkey] not exists", e.Message);
            }

            string ret = tair.tftcreateindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"text\"}}}}");
            Assert.AreEqual(ret, "OK");
            try
            {
                tair.tftincrlongdocfield("tftkey", "1", "f0", 1);
            }
            catch (Exception e)
            {
                Assert.AreEqual("ERR failed to parse field [f0]", e.Message);
            }

            db.KeyDelete("tftkey");
            ret = tair.tftcreateindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"long\"}}}}");
            Assert.AreEqual(ret, "OK");

            Assert.AreEqual(1, tair.tftincrlongdocfield("tftkey", "1", "f0", 1));
            Assert.AreEqual(0, tair.tftincrlongdocfield("tftkey", "1", "f0", -1));
            Assert.AreEqual(1, tair.tftexists("tftkey", "1"));
        }

        [Test]
        public void tftincrfloatdocfield()
        {
            db.KeyDelete("tftkey");
            try
            {
                tair.tftincrfloatdocfield("tftkey", "1", "f0", 1.1);
            }
            catch (Exception e)
            {
                Assert.AreEqual("ERR index [tftkey] not exists", e.Message);
            }

            String ret = tair.tftcreateindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"text\"}}}}");
            Assert.AreEqual(ret, "OK");
            try
            {
                tair.tftincrfloatdocfield("tftkey", "1", "f0", 1.1);
            }
            catch (Exception e)
            {
                Assert.AreEqual("ERR failed to parse field [f0]", e.Message);
            }

            db.KeyDelete("tftkey");
            ret = tair.tftcreateindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"double\"}}}}");
            Assert.AreEqual(ret, "OK");
            double value = tair.tftincrfloatdocfield("tftkey", "1", "f0", 1.1);
            Assert.AreEqual(1.1d, value);
            value = tair.tftincrfloatdocfield("tftkey", "1", "f0", -1.1);
            Assert.AreEqual(0d, value);
            Assert.AreEqual(1, tair.tftexists("tftkey", "1"));
        }

        [Test]
        public void tftdeldocfield()
        {
            db.KeyDelete("tftkey");
            Assert.AreEqual(0, tair.tftdeldocfield("tffkey", "1", "f0"));

            string ret = tair.tftcreateindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"long\"}}}}");
            Assert.AreEqual("OK", ret);
            tair.tftincrlongdocfield("tftkey", "1", "f0", 1);
            tair.tftincrfloatdocfield("tftkey", "1", "f1", 1.1);
            Assert.AreEqual(2, tair.tftdeldocfield("tftkey", "1", "f0", "f1", "f2"));
        }

        [Test]
        public void tftdeldoc()
        {
            db.KeyDelete("tftkey");
            tair.tftcreateindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"text\"},\"f1\":{\"type\":\"text\"}}}}");
            tair.tftadddoc("tftkey", "{\"f0\":\"v0\",\"f1\":\"3\"}", "1");
            tair.tftadddoc("tftkey", "{\"f0\":\"v1\",\"f1\":\"3\"}", "2");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"3\"}", "3");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"4\"}", "4");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"5\"}", "5");

            Assert.AreEqual(1, tair.tftexists("tftkey", "3"));
            Assert.AreEqual(5, tair.tftdocnum("tftkey"));
            Assert.AreEqual("3", tair.tftdeldoc("tftkey", "3", "4", "5"));
            Assert.AreEqual(0, tair.tftexists("tftkey", "3"));
            Assert.AreEqual(2, tair.tftdocnum("tftkey"));
        }

        [Test]
        public void tftdelall()
        {
            db.KeyDelete("tftkey");
            tair.tftcreateindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"text\"},\"f1\":{\"type\":\"text\"}}}}");
            tair.tftadddoc("tftkey", "{\"f0\":\"v0\",\"f1\":\"3\"}", "1");
            tair.tftadddoc("tftkey", "{\"f0\":\"v1\",\"f1\":\"3\"}", "2");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"3\"}", "3");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"4\"}", "4");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"5\"}", "5");

            Assert.AreEqual("OK", tair.tftdelall("tftkey"));
            Assert.AreEqual(0, tair.tftdocnum("tftkey"));
        }

        [Test]
        public void tftscandocid()
        {
            db.KeyDelete("tftkey");
            tair.tftcreateindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"text\"},\"f1\":{\"type\":\"text\"}}}}");
            tair.tftadddoc("tftkey", "{\"f0\":\"v0\",\"f1\":\"3\"}", "1");
            tair.tftadddoc("tftkey", "{\"f0\":\"v1\",\"f1\":\"3\"}", "2");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"3\"}", "3");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"4\"}", "4");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"5\"}", "5");
            TFTScanResult<String> res = tair.tftscandocid("tftkey", "0");
            Assert.AreEqual(0.ToString(), res.getCursor());
            Assert.True(res.getResult().Count == 5);

            Assert.AreEqual("1", res.getResult()[0]);
            Assert.AreEqual("2", res.getResult()[1]);
        }

        [Test]
        public void tftscandocidwithcount()
        {
            db.KeyDelete("tftkey");
            tair.tftcreateindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"text\"},\"f1\":{\"type\":\"text\"}}}}");
            tair.tftadddoc("tftkey", "{\"f0\":\"v0\",\"f1\":\"3\"}", "1");
            tair.tftadddoc("tftkey", "{\"f0\":\"v1\",\"f1\":\"3\"}", "2");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"3\"}", "3");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"4\"}", "4");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"5\"}", "5");

            TFTScanParams param = new TFTScanParams();
            param.count(3);
            TFTScanResult<String> res = tair.tftscandocid("tftkey", "0", param);
            Assert.AreEqual("3", res.getCursor());
            Assert.True(res.getResult().Count == 3);

            Assert.AreEqual("1", res.getResult()[0]);
            Assert.AreEqual("2", res.getResult()[1]);
            Assert.AreEqual("3", res.getResult()[2]);

            TFTScanResult<String> res2 = tair.tftscandocid("tftkey", "3", param);
            Assert.AreEqual("0", res2.getCursor());
            Assert.True(res2.getResult().Count == 2);

            Assert.AreEqual("4", res2.getResult()[0]);
            Assert.AreEqual("5", res2.getResult()[1]);
        }

        [Test]
        public void tftscandocidwithmatch()
        {
            db.KeyDelete("tftkey");

            tair.tftcreateindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"text\"},\"f1\":{\"type\":\"text\"}}}}");
            tair.tftadddoc("tftkey", "{\"f0\":\"v0\",\"f1\":\"3\"}", "1_redis_doc");
            tair.tftadddoc("tftkey", "{\"f0\":\"v1\",\"f1\":\"3\"}", "2_redis_doc");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"3\"}", "3_mysql_doc");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"4\"}", "4_mysql_doc");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"5\"}", "5_tidb_doc");

            TFTScanParams param = new TFTScanParams();
            param.match("*redis*");
            TFTScanResult<String> res = tair.tftscandocid("tftkey", "0", param);
            Assert.AreEqual("0", res.getCursor());
            Assert.True(res.getResult().Count == 2);

            Assert.AreEqual("1_redis_doc", res.getResult()[0]);
            Assert.AreEqual("2_redis_doc", res.getResult()[1]);

            TFTScanParams params2 = new TFTScanParams();
            params2.match("*tidb*");
            TFTScanResult<String> res2 = tair.tftscandocid("tftkey", "0", params2);
            Assert.AreEqual("0", res2.getCursor());
            Assert.True(res2.getResult().Count == 1);

            Assert.AreEqual("5_tidb_doc", res2.getResult()[0]);
        }

        [Test]
        public void unicodetest()
        {
            db.KeyDelete("tftkey");
            tair.tftmappingindex("tftkey",
                "{\"mappings\":{\"properties\":{\"f0\":{\"type\":\"text\",\"analyzer\":\"chinese\"}}}}");
            Assert.AreEqual(
                "{\"tftkey\":{\"mappings\":{\"_source\":{\"enabled\":true,\"excludes\":[],\"includes\":[]},\"dynamic\":\"false\",\"properties\":{\"f0\":{\"analyzer\":\"chinese\",\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\"}}}}}",
                tair.tftgetindexmappings("tftkey"));

            db.KeyDelete("tftkey");
            tair.tftmappingindex("tftkey",
                "{\"mappings\":{\"properties\":{\"f0\":{\"type\":\"text\",\"search_analyzer\":\"chinese\"}}}}");
            Assert.AreEqual(
                "{\"tftkey\":{\"mappings\":{\"_source\":{\"enabled\":true,\"excludes\":[],\"includes\":[]},\"dynamic\":\"false\",\"properties\":{\"f0\":{\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\",\"search_analyzer\":\"chinese\"}}}}}",
                tair.tftgetindexmappings("tftkey"));
            db.KeyDelete("tftkey");

            tair.tftmappingindex("tftkey",
                "{\"mappings\":{\"properties\":{\"f0\":{\"type\":\"text\",\"analyzer\":\"chinese\", \"search_analyzer\":\"chinese\"}}}}");
            Assert.AreEqual(
                "{\"tftkey\":{\"mappings\":{\"_source\":{\"enabled\":true,\"excludes\":[],\"includes\":[]},\"dynamic\":\"false\",\"properties\":{\"f0\":{\"analyzer\":\"chinese\",\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\",\"search_analyzer\":\"chinese\"}}}}}",
                tair.tftgetindexmappings("tftkey"));
            tair.tftadddoc("tftkey", "{\"f0\":\"夏天是一个很热的季节\"}", "1");
            Assert.AreEqual(
                "{\"hits\":{\"hits\":[{\"_id\":\"1\",\"_index\":\"tftkey\",\"_score\":0.077948,\"_source\":{\"f0\":\"夏天是一个很热的季节\"}}],\"max_score\":0.077948,\"total\":{\"relation\":\"eq\",\"value\":1}}}",
                tair.tftsearch("tftkey", "{\"query\":{\"match\":{\"f0\":\"夏天冬天\"}}}"));
        }

        [Test]
        public void searchchcachetest()
        {
            db.KeyDelete("tftkey");
            tair.tftmappingindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"text\"},\"f1\":{\"type\":\"text\"}}}}");
            tair.tftadddoc("tftkey", "{\"f0\":\"v0\",\"f1\":\"3\"}", "1");
            tair.tftadddoc("tftkey", "{\"f0\":\"v1\",\"f1\":\"3\"}", "2");

            Assert.AreEqual(
                "{\"hits\":{\"hits\":[{\"_id\":\"1\",\"_index\":\"tftkey\",\"_score\":0.594535,\"_source\":{\"f0\":\"v0\",\"f1\":\"3\"}},{\"_id\":\"2\",\"_index\":\"tftkey\",\"_score\":0.594535,\"_source\":{\"f0\":\"v1\",\"f1\":\"3\"}}],\"max_score\":0.594535,\"total\":{\"relation\":\"eq\",\"value\":2}}}",
                tair.tftsearch("tftkey", "{\"query\":{\"match\":{\"f1\":\"3\"}}}", true));

            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"3\"}", "3");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"4\"}", "4");
            tair.tftadddoc("tftkey", "{\"f0\":\"v3\",\"f1\":\"5\"}", "5");

            Assert.AreEqual(
                "{\"hits\":{\"hits\":[{\"_id\":\"1\",\"_index\":\"tftkey\",\"_score\":0.594535,\"_source\":{\"f0\":\"v0\",\"f1\":\"3\"}},{\"_id\":\"2\",\"_index\":\"tftkey\",\"_score\":0.594535,\"_source\":{\"f0\":\"v1\",\"f1\":\"3\"}}],\"max_score\":0.594535,\"total\":{\"relation\":\"eq\",\"value\":2}}}",
                tair.tftsearch("tftkey", "{\"query\":{\"match\":{\"f1\":\"3\"}}}", true));

            Assert.AreEqual(
                "{\"hits\":{\"hits\":[{\"_id\":\"1\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v0\",\"f1\":\"3\"}},{\"_id\":\"2\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v1\",\"f1\":\"3\"}},{\"_id\":\"3\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v3\",\"f1\":\"3\"}}],\"max_score\":1.223144,\"total\":{\"relation\":\"eq\",\"value\":3}}}",
                tair.tftsearch("tftkey", "{\"query\":{\"match\":{\"f1\":\"3\"}}}"));

            // wait for LRU cache expired
            Thread.Sleep(10000);
            Assert.AreEqual(
                "{\"hits\":{\"hits\":[{\"_id\":\"1\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v0\",\"f1\":\"3\"}},{\"_id\":\"2\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v1\",\"f1\":\"3\"}},{\"_id\":\"3\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v3\",\"f1\":\"3\"}}],\"max_score\":1.223144,\"total\":{\"relation\":\"eq\",\"value\":3}}}",
                tair.tftsearch("tftkey", "{\"query\":{\"match\":{\"f1\":\"3\"}}}", true));
        }

        [Test]
        public void tftmsearchtest()
        {
            db.KeyDelete("{index1}");
            db.KeyDelete("{index2}");
            tair.tftmappingindex("{index1}",
                "{\"mappings\": {\"_source\": {\"enabled\": true },\"properties\": {\"product_id\": {\"type\": \"keyword\", \"ignore_above\": 128 },\"product_name\": { \"type\": \"text\" },\"product_title\": { \"type\": \"text\", \"analyzer\": \"jieba\" },\"price\": { \"type\": \"double\" }}}}");
            tair.tftmappingindex("{index2}",
                "{\"mappings\": {\"_source\": {\"enabled\": true },\"properties\": {\"product_id\": {\"type\": \"keyword\", \"ignore_above\": 128 },\"product_name\": { \"type\": \"text\" },\"product_title\": { \"type\": \"text\", \"analyzer\": \"jieba\" },\"price\": { \"type\": \"double\" }}}}");
            tair.tftadddoc("{index1}", "{\"product_id\":\"test1\"}", "00001");
            tair.tftadddoc("{index1}", "{\"product_id\":\"test2\"}", "00002");
            tair.tftadddoc("{index2}", "{\"product_id\":\"test3\"}", "00003");
            tair.tftadddoc("{index2}", "{\"product_id\":\"test4\"}", "00004");

            string want =
                @"{""hits"":{""hits"":[{""_id"":""00001"",""_index"":""{index1}"",""_score"":1.0,""_source"":{""product_id"":""test1""}},{""_id"":""00002"",""_index"":""{index1}"",""_score"":1.0,""_source"":{""product_id"":""test2""}},{""_id"":""00003"",""_index"":""{index2}"",""_score"":1.0,""_source"":{""product_id"":""test3""}},{""_id"":""00004"",""_index"":""{index2}"",""_score"":1.0,""_source"":{""product_id"":""test4""}}],""max_score"":1.0,""total"":{""relation"":""eq"",""value"":4}},""aux_info"":{""index_crc64"":5843875291690071373}}";
            string[] index = {"{index1}", "{index2}"};
            string result = tair.tftmsearch(2, index, "{\"sort\":[{\"price\":{\"order\":\"desc\"}}]}");
            Assert.AreEqual(want, result);
        }

        [Test]
        public void tftmaddteststring()
        {
            db.KeyDelete("tftkey");
            tair.tftmappingindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"text\"},\"f1\":{\"type\":\"text\"}}}}");
            Dictionary<String, String> docs = new Dictionary<string, string>();
            docs.Add("{\"f0\":\"v0\",\"f1\":\"3\"}", "1");
            docs.Add("{\"f0\":\"v1\",\"f1\":\"3\"}", "2");
            docs.Add("{\"f0\":\"v3\",\"f1\":\"3\"}", "3");
            docs.Add("{\"f0\":\"v3\",\"f1\":\"4\"}", "4");
            docs.Add("{\"f0\":\"v3\",\"f1\":\"5\"}", "5");

            tair.tftmadddoc("tftkey", docs);

            Assert.AreEqual(
                "{\"hits\":{\"hits\":[{\"_id\":\"1\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v0\",\"f1\":\"3\"}},{\"_id\":\"2\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v1\",\"f1\":\"3\"}},{\"_id\":\"3\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v3\",\"f1\":\"3\"}}],\"max_score\":1.223144,\"total\":{\"relation\":\"eq\",\"value\":3}}}",
                tair.tftsearch("tftkey", "{\"query\":{\"match\":{\"f1\":\"3\"}}}"));

            Assert.AreEqual("{\"_id\":\"3\",\"_source\":{\"f0\":\"v3\",\"f1\":\"3\"}}", tair.tftgetdoc("tftkey", "3"));
            Assert.AreEqual("1", tair.tftdeldoc("tftkey", "3"));
            Assert.AreEqual(null, tair.tftgetdoc("tftkey", "3"));

            Assert.AreEqual(
                "{\"tftkey\":{\"mappings\":{\"_source\":{\"enabled\":true,\"excludes\":[],\"includes\":[]},\"dynamic\":\"false\",\"properties\":{\"f0\":{\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\"},\"f1\":{\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\"}}}}}",
                tair.tftgetindexmappings("tftkey"));
        }

        [Test]
        public void tftmaddtestbyte()
        {
            db.KeyDelete("tftkey");
            tair.tftmappingindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"text\"},\"f1\":{\"type\":\"text\"}}}}");
            Dictionary<byte[], byte[]> docs = new Dictionary<byte[], byte[]>();
            docs.Add(Encoding.UTF8.GetBytes("{\"f0\":\"v0\",\"f1\":\"3\"}"), Encoding.UTF8.GetBytes("1"));
            docs.Add(Encoding.UTF8.GetBytes("{\"f0\":\"v1\",\"f1\":\"3\"}"), Encoding.UTF8.GetBytes("2"));
            docs.Add(Encoding.UTF8.GetBytes("{\"f0\":\"v3\",\"f1\":\"3\"}"), Encoding.UTF8.GetBytes("3"));
            docs.Add(Encoding.UTF8.GetBytes("{\"f0\":\"v3\",\"f1\":\"4\"}"), Encoding.UTF8.GetBytes("4"));
            docs.Add(Encoding.UTF8.GetBytes("{\"f0\":\"v3\",\"f1\":\"5\"}"), Encoding.UTF8.GetBytes("5"));

            tair.tftmadddoc(Encoding.UTF8.GetBytes("tftkey"), docs);

            Assert.AreEqual(
                "{\"hits\":{\"hits\":[{\"_id\":\"1\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v0\",\"f1\":\"3\"}},{\"_id\":\"2\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v1\",\"f1\":\"3\"}},{\"_id\":\"3\",\"_index\":\"tftkey\",\"_score\":1.223144,\"_source\":{\"f0\":\"v3\",\"f1\":\"3\"}}],\"max_score\":1.223144,\"total\":{\"relation\":\"eq\",\"value\":3}}}",
                tair.tftsearch("tftkey", "{\"query\":{\"match\":{\"f1\":\"3\"}}}"));

            Assert.AreEqual("{\"_id\":\"3\",\"_source\":{\"f0\":\"v3\",\"f1\":\"3\"}}", tair.tftgetdoc("tftkey", "3"));
            Assert.AreEqual("1", tair.tftdeldoc("tftkey", "3"));
            Assert.AreEqual(null, tair.tftgetdoc("tftkey", "3"));

            Assert.AreEqual(
                "{\"tftkey\":{\"mappings\":{\"_source\":{\"enabled\":true,\"excludes\":[],\"includes\":[]},\"dynamic\":\"false\",\"properties\":{\"f0\":{\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\"},\"f1\":{\"boost\":1.0,\"enabled\":true,\"ignore_above\":-1,\"index\":true,\"similarity\":\"classic\",\"type\":\"text\"}}}}}",
                tair.tftgetindexmappings("tftkey"));
        }

        [Test]
        public void tftanalyzer()
        {
            db.KeyDelete("tftkey");
            string res = tair.tftanalyzer("standard", "tair is a nosql database");
            Assert.AreEqual(
                "{\"tokens\":[{\"token\":\"tair\",\"start_offset\":0,\"end_offset\":4,\"position\":0},{\"token\":\"nosql\",\"start_offset\":10,\"end_offset\":15,\"position\":3},{\"token\":\"database\",\"start_offset\":16,\"end_offset\":24,\"position\":4}]}",
                res);
            res = tair.tftanalyzer(Encoding.UTF8.GetBytes("standard"),
                Encoding.UTF8.GetBytes("tair is a nosql database"));
            Assert.AreEqual(
                "{\"tokens\":[{\"token\":\"tair\",\"start_offset\":0,\"end_offset\":4,\"position\":0},{\"token\":\"nosql\",\"start_offset\":10,\"end_offset\":15,\"position\":3},{\"token\":\"database\",\"start_offset\":16,\"end_offset\":24,\"position\":4}]}",
                res);
            res = tair.tftanalyzer("standard", "tair is a nosql database", new TFTAnalyzerParams().showTime());
            Assert.True(res.Contains("consuming time (ms)"));
            tair.tftcreateindex("tftkey",
                "{\"mappings\":{\"properties\":{\"f0\":{\"type\":\"text\",\"analyzer\":\"my_jieba_analyzer\"}}},\"settings\":{\"analysis\":{\"analyzer\":{\"my_jieba_analyzer\":{\"type\":\"jieba\",\"userwords\":[\"key-value数据结构存储 \"],\"use_hmm\":true}}}}}");
            res = tair.tftanalyzer("jieba",
                "Redis是完全开源免费的，遵守BSD协议，是一个灵活的高性能key-value数据结构存储，可以用来作为数据库、缓存和消息队列。Redis比其他key-value缓存产品有以下三个特点：Redis支持数据的持久化，可以将内存中的数据保存在磁盘中，重启的时候可以再次加载到内存使用。");
            Assert.AreEqual(
                "{\"tokens\":[{\"token\":\"redis\",\"start_offset\":0,\"end_offset\":5,\"position\":0},{\"token\":\"完全\",\"start_offset\":6,\"end_offset\":8,\"position\":2},{\"token\":\"开源\",\"start_offset\":8,\"end_offset\":10,\"position\":3},{\"token\":\"免费\",\"start_offset\":10,\"end_offset\":12,\"position\":4},{\"token\":\"遵守\",\"start_offset\":14,\"end_offset\":16,\"position\":7},{\"token\":\"bsd\",\"start_offset\":16,\"end_offset\":19,\"position\":8},{\"token\":\"协议\",\"start_offset\":19,\"end_offset\":21,\"position\":9},{\"token\":\"一个\",\"start_offset\":23,\"end_offset\":25,\"position\":12},{\"token\":\"灵活\",\"start_offset\":25,\"end_offset\":27,\"position\":13},{\"token\":\"性能\",\"start_offset\":29,\"end_offset\":31,\"position\":15},{\"token\":\"高性能\",\"start_offset\":28,\"end_offset\":31,\"position\":16},{\"token\":\"key\",\"start_offset\":31,\"end_offset\":34,\"position\":17},{\"token\":\"value\",\"start_offset\":35,\"end_offset\":40,\"position\":19},{\"token\":\"数据\",\"start_offset\":40,\"end_offset\":42,\"position\":20},{\"token\":\"结构\",\"start_offset\":42,\"end_offset\":44,\"position\":21},{\"token\":\"数据结构\",\"start_offset\":40,\"end_offset\":44,\"position\":22},{\"token\":\"存储\",\"start_offset\":44,\"end_offset\":46,\"position\":23},{\"token\":\"数据\",\"start_offset\":53,\"end_offset\":55,\"position\":28},{\"token\":\"数据库\",\"start_offset\":53,\"end_offset\":56,\"position\":29},{\"token\":\"缓存\",\"start_offset\":57,\"end_offset\":59,\"position\":31},{\"token\":\"消息\",\"start_offset\":60,\"end_offset\":62,\"position\":33},{\"token\":\"队列\",\"start_offset\":62,\"end_offset\":64,\"position\":34},{\"token\":\"redis\",\"start_offset\":65,\"end_offset\":70,\"position\":36},{\"token\":\"key\",\"start_offset\":73,\"end_offset\":76,\"position\":39},{\"token\":\"value\",\"start_offset\":77,\"end_offset\":82,\"position\":41},{\"token\":\"缓存\",\"start_offset\":82,\"end_offset\":84,\"position\":42},{\"token\":\"产品\",\"start_offset\":84,\"end_offset\":86,\"position\":43},{\"token\":\"以下\",\"start_offset\":87,\"end_offset\":89,\"position\":45},{\"token\":\"三个\",\"start_offset\":89,\"end_offset\":91,\"position\":46},{\"token\":\"特点\",\"start_offset\":91,\"end_offset\":93,\"position\":47},{\"token\":\"redis\",\"start_offset\":94,\"end_offset\":99,\"position\":49},{\"token\":\"支持\",\"start_offset\":99,\"end_offset\":101,\"position\":50},{\"token\":\"数据\",\"start_offset\":101,\"end_offset\":103,\"position\":51},{\"token\":\"持久\",\"start_offset\":104,\"end_offset\":106,\"position\":53},{\"token\":\"化\",\"start_offset\":106,\"end_offset\":107,\"position\":54},{\"token\":\"内存\",\"start_offset\":111,\"end_offset\":113,\"position\":58},{\"token\":\"中\",\"start_offset\":113,\"end_offset\":114,\"position\":59},{\"token\":\"数据\",\"start_offset\":115,\"end_offset\":117,\"position\":61},{\"token\":\"保存\",\"start_offset\":117,\"end_offset\":119,\"position\":62},{\"token\":\"磁盘\",\"start_offset\":120,\"end_offset\":122,\"position\":64},{\"token\":\"中\",\"start_offset\":122,\"end_offset\":123,\"position\":65},{\"token\":\"重启\",\"start_offset\":124,\"end_offset\":126,\"position\":67},{\"token\":\"再次\",\"start_offset\":131,\"end_offset\":133,\"position\":71},{\"token\":\"加载\",\"start_offset\":133,\"end_offset\":135,\"position\":72},{\"token\":\"内存\",\"start_offset\":136,\"end_offset\":138,\"position\":74},{\"token\":\"使用\",\"start_offset\":138,\"end_offset\":140,\"position\":75}]}",
                res);
            Assert.False(res.Contains("key-value 数据结构存储"));
            res = tair.tftanalyzer("my_jieba_analyzer",
                "Redis是完全开源免费的，遵守BSD协议，是一个灵活的高性能key-value数据结构存储，可以用来作为数据库、缓存和消息队列。Redis比其他key-value缓存产品有以下三个特点：Redis支持数据的持久化，可以将内存中的数据保存在磁盘中，重启的时候可以再次加载到内存使用。",
                new TFTAnalyzerParams().index("tftkey"));

            Assert.AreEqual(
                "{\"tokens\":[{\"token\":\"redis\",\"start_offset\":0,\"end_offset\":5,\"position\":0},{\"token\":\"完全\",\"start_offset\":6,\"end_offset\":8,\"position\":2},{\"token\":\"开源\",\"start_offset\":8,\"end_offset\":10,\"position\":3},{\"token\":\"免费\",\"start_offset\":10,\"end_offset\":12,\"position\":4},{\"token\":\"遵守\",\"start_offset\":14,\"end_offset\":16,\"position\":7},{\"token\":\"bsd\",\"start_offset\":16,\"end_offset\":19,\"position\":8},{\"token\":\"协议\",\"start_offset\":19,\"end_offset\":21,\"position\":9},{\"token\":\"一个\",\"start_offset\":23,\"end_offset\":25,\"position\":12},{\"token\":\"灵活\",\"start_offset\":25,\"end_offset\":27,\"position\":13},{\"token\":\"性能\",\"start_offset\":29,\"end_offset\":31,\"position\":15},{\"token\":\"高性能\",\"start_offset\":28,\"end_offset\":31,\"position\":16},{\"token\":\"数据\",\"start_offset\":40,\"end_offset\":42,\"position\":17},{\"token\":\"结构\",\"start_offset\":42,\"end_offset\":44,\"position\":18},{\"token\":\"存储\",\"start_offset\":44,\"end_offset\":46,\"position\":19},{\"token\":\"key-value数据结构存储\",\"start_offset\":31,\"end_offset\":46,\"position\":20},{\"token\":\"数据\",\"start_offset\":53,\"end_offset\":55,\"position\":25},{\"token\":\"数据库\",\"start_offset\":53,\"end_offset\":56,\"position\":26},{\"token\":\"缓存\",\"start_offset\":57,\"end_offset\":59,\"position\":28},{\"token\":\"消息\",\"start_offset\":60,\"end_offset\":62,\"position\":30},{\"token\":\"队列\",\"start_offset\":62,\"end_offset\":64,\"position\":31},{\"token\":\"redis\",\"start_offset\":65,\"end_offset\":70,\"position\":33},{\"token\":\"key\",\"start_offset\":73,\"end_offset\":76,\"position\":36},{\"token\":\"value\",\"start_offset\":77,\"end_offset\":82,\"position\":38},{\"token\":\"缓存\",\"start_offset\":82,\"end_offset\":84,\"position\":39},{\"token\":\"产品\",\"start_offset\":84,\"end_offset\":86,\"position\":40},{\"token\":\"以下\",\"start_offset\":87,\"end_offset\":89,\"position\":42},{\"token\":\"三个\",\"start_offset\":89,\"end_offset\":91,\"position\":43},{\"token\":\"特点\",\"start_offset\":91,\"end_offset\":93,\"position\":44},{\"token\":\"redis\",\"start_offset\":94,\"end_offset\":99,\"position\":46},{\"token\":\"支持\",\"start_offset\":99,\"end_offset\":101,\"position\":47},{\"token\":\"数据\",\"start_offset\":101,\"end_offset\":103,\"position\":48},{\"token\":\"持久\",\"start_offset\":104,\"end_offset\":106,\"position\":50},{\"token\":\"化\",\"start_offset\":106,\"end_offset\":107,\"position\":51},{\"token\":\"内存\",\"start_offset\":111,\"end_offset\":113,\"position\":55},{\"token\":\"中\",\"start_offset\":113,\"end_offset\":114,\"position\":56},{\"token\":\"数据\",\"start_offset\":115,\"end_offset\":117,\"position\":58},{\"token\":\"保存\",\"start_offset\":117,\"end_offset\":119,\"position\":59},{\"token\":\"磁盘\",\"start_offset\":120,\"end_offset\":122,\"position\":61},{\"token\":\"中\",\"start_offset\":122,\"end_offset\":123,\"position\":62},{\"token\":\"重启\",\"start_offset\":124,\"end_offset\":126,\"position\":64},{\"token\":\"再次\",\"start_offset\":131,\"end_offset\":133,\"position\":68},{\"token\":\"加载\",\"start_offset\":133,\"end_offset\":135,\"position\":69},{\"token\":\"内存\",\"start_offset\":136,\"end_offset\":138,\"position\":71},{\"token\":\"使用\",\"start_offset\":138,\"end_offset\":140,\"position\":72}]}",
                res);
            Assert.True(res.Contains("key-value数据结构存储"));
        }

        [Test]
        public void tftanalyzerwithunicodekey()
        {
            db.KeyDelete("这是一个unicode_key");
            tair.tftcreateindex("这是一个unicode_key",
                "{\"mappings\":{\"properties\":{\"f0\":{\"type\":\"text\",\"analyzer\":\"my_jieba_analyzer\"}}},\"settings\":{\"analysis\":{\"analyzer\":{\"my_jieba_analyzer\":{\"type\":\"jieba\",\"userwords\":[\"key-value数据结构存储\"],\"use_hmm\":true}}}}}");

            String res = tair.tftanalyzer("jieba",
                "Redis是完全开源免费的，遵守BSD协议，是一个灵活的高性能key-value数据结构存储，可以用来作为数据库、缓存和消息队列。Redis比其他key-value缓存产品有以下三个特点：Redis支持数据的持久化，可以将内存中的数据保存在磁盘中，重启的时候可以再次加载到内存使用。");
            Assert.AreEqual(
                "{\"tokens\":[{\"token\":\"redis\",\"start_offset\":0,\"end_offset\":5,\"position\":0},{\"token\":\"完全\",\"start_offset\":6,\"end_offset\":8,\"position\":2},{\"token\":\"开源\",\"start_offset\":8,\"end_offset\":10,\"position\":3},{\"token\":\"免费\",\"start_offset\":10,\"end_offset\":12,\"position\":4},{\"token\":\"遵守\",\"start_offset\":14,\"end_offset\":16,\"position\":7},{\"token\":\"bsd\",\"start_offset\":16,\"end_offset\":19,\"position\":8},{\"token\":\"协议\",\"start_offset\":19,\"end_offset\":21,\"position\":9},{\"token\":\"一个\",\"start_offset\":23,\"end_offset\":25,\"position\":12},{\"token\":\"灵活\",\"start_offset\":25,\"end_offset\":27,\"position\":13},{\"token\":\"性能\",\"start_offset\":29,\"end_offset\":31,\"position\":15},{\"token\":\"高性能\",\"start_offset\":28,\"end_offset\":31,\"position\":16},{\"token\":\"key\",\"start_offset\":31,\"end_offset\":34,\"position\":17},{\"token\":\"value\",\"start_offset\":35,\"end_offset\":40,\"position\":19},{\"token\":\"数据\",\"start_offset\":40,\"end_offset\":42,\"position\":20},{\"token\":\"结构\",\"start_offset\":42,\"end_offset\":44,\"position\":21},{\"token\":\"数据结构\",\"start_offset\":40,\"end_offset\":44,\"position\":22},{\"token\":\"存储\",\"start_offset\":44,\"end_offset\":46,\"position\":23},{\"token\":\"数据\",\"start_offset\":53,\"end_offset\":55,\"position\":28},{\"token\":\"数据库\",\"start_offset\":53,\"end_offset\":56,\"position\":29},{\"token\":\"缓存\",\"start_offset\":57,\"end_offset\":59,\"position\":31},{\"token\":\"消息\",\"start_offset\":60,\"end_offset\":62,\"position\":33},{\"token\":\"队列\",\"start_offset\":62,\"end_offset\":64,\"position\":34},{\"token\":\"redis\",\"start_offset\":65,\"end_offset\":70,\"position\":36},{\"token\":\"key\",\"start_offset\":73,\"end_offset\":76,\"position\":39},{\"token\":\"value\",\"start_offset\":77,\"end_offset\":82,\"position\":41},{\"token\":\"缓存\",\"start_offset\":82,\"end_offset\":84,\"position\":42},{\"token\":\"产品\",\"start_offset\":84,\"end_offset\":86,\"position\":43},{\"token\":\"以下\",\"start_offset\":87,\"end_offset\":89,\"position\":45},{\"token\":\"三个\",\"start_offset\":89,\"end_offset\":91,\"position\":46},{\"token\":\"特点\",\"start_offset\":91,\"end_offset\":93,\"position\":47},{\"token\":\"redis\",\"start_offset\":94,\"end_offset\":99,\"position\":49},{\"token\":\"支持\",\"start_offset\":99,\"end_offset\":101,\"position\":50},{\"token\":\"数据\",\"start_offset\":101,\"end_offset\":103,\"position\":51},{\"token\":\"持久\",\"start_offset\":104,\"end_offset\":106,\"position\":53},{\"token\":\"化\",\"start_offset\":106,\"end_offset\":107,\"position\":54},{\"token\":\"内存\",\"start_offset\":111,\"end_offset\":113,\"position\":58},{\"token\":\"中\",\"start_offset\":113,\"end_offset\":114,\"position\":59},{\"token\":\"数据\",\"start_offset\":115,\"end_offset\":117,\"position\":61},{\"token\":\"保存\",\"start_offset\":117,\"end_offset\":119,\"position\":62},{\"token\":\"磁盘\",\"start_offset\":120,\"end_offset\":122,\"position\":64},{\"token\":\"中\",\"start_offset\":122,\"end_offset\":123,\"position\":65},{\"token\":\"重启\",\"start_offset\":124,\"end_offset\":126,\"position\":67},{\"token\":\"再次\",\"start_offset\":131,\"end_offset\":133,\"position\":71},{\"token\":\"加载\",\"start_offset\":133,\"end_offset\":135,\"position\":72},{\"token\":\"内存\",\"start_offset\":136,\"end_offset\":138,\"position\":74},{\"token\":\"使用\",\"start_offset\":138,\"end_offset\":140,\"position\":75}]}",
                res);
            Assert.False(res.Contains("key-value数据结构存储"));

            res = tair.tftanalyzer("my_jieba_analyzer",
                "Redis是完全开源免费的，遵守BSD协议，是一个灵活的高性能key-value数据结构存储，可以用来作为数据库、缓存和消息队列。Redis比其他key-value缓存产品有以下三个特点：Redis支持数据的持久化，可以将内存中的数据保存在磁盘中，重启的时候可以再次加载到内存使用。",
                new TFTAnalyzerParams().index("这是一个unicode_key"));
            Assert.AreEqual(
                "{\"tokens\":[{\"token\":\"redis\",\"start_offset\":0,\"end_offset\":5,\"position\":0},{\"token\":\"完全\",\"start_offset\":6,\"end_offset\":8,\"position\":2},{\"token\":\"开源\",\"start_offset\":8,\"end_offset\":10,\"position\":3},{\"token\":\"免费\",\"start_offset\":10,\"end_offset\":12,\"position\":4},{\"token\":\"遵守\",\"start_offset\":14,\"end_offset\":16,\"position\":7},{\"token\":\"bsd\",\"start_offset\":16,\"end_offset\":19,\"position\":8},{\"token\":\"协议\",\"start_offset\":19,\"end_offset\":21,\"position\":9},{\"token\":\"一个\",\"start_offset\":23,\"end_offset\":25,\"position\":12},{\"token\":\"灵活\",\"start_offset\":25,\"end_offset\":27,\"position\":13},{\"token\":\"性能\",\"start_offset\":29,\"end_offset\":31,\"position\":15},{\"token\":\"高性能\",\"start_offset\":28,\"end_offset\":31,\"position\":16},{\"token\":\"数据\",\"start_offset\":40,\"end_offset\":42,\"position\":17},{\"token\":\"结构\",\"start_offset\":42,\"end_offset\":44,\"position\":18},{\"token\":\"存储\",\"start_offset\":44,\"end_offset\":46,\"position\":19},{\"token\":\"key-value数据结构存储\",\"start_offset\":31,\"end_offset\":46,\"position\":20},{\"token\":\"数据\",\"start_offset\":53,\"end_offset\":55,\"position\":25},{\"token\":\"数据库\",\"start_offset\":53,\"end_offset\":56,\"position\":26},{\"token\":\"缓存\",\"start_offset\":57,\"end_offset\":59,\"position\":28},{\"token\":\"消息\",\"start_offset\":60,\"end_offset\":62,\"position\":30},{\"token\":\"队列\",\"start_offset\":62,\"end_offset\":64,\"position\":31},{\"token\":\"redis\",\"start_offset\":65,\"end_offset\":70,\"position\":33},{\"token\":\"key\",\"start_offset\":73,\"end_offset\":76,\"position\":36},{\"token\":\"value\",\"start_offset\":77,\"end_offset\":82,\"position\":38},{\"token\":\"缓存\",\"start_offset\":82,\"end_offset\":84,\"position\":39},{\"token\":\"产品\",\"start_offset\":84,\"end_offset\":86,\"position\":40},{\"token\":\"以下\",\"start_offset\":87,\"end_offset\":89,\"position\":42},{\"token\":\"三个\",\"start_offset\":89,\"end_offset\":91,\"position\":43},{\"token\":\"特点\",\"start_offset\":91,\"end_offset\":93,\"position\":44},{\"token\":\"redis\",\"start_offset\":94,\"end_offset\":99,\"position\":46},{\"token\":\"支持\",\"start_offset\":99,\"end_offset\":101,\"position\":47},{\"token\":\"数据\",\"start_offset\":101,\"end_offset\":103,\"position\":48},{\"token\":\"持久\",\"start_offset\":104,\"end_offset\":106,\"position\":50},{\"token\":\"化\",\"start_offset\":106,\"end_offset\":107,\"position\":51},{\"token\":\"内存\",\"start_offset\":111,\"end_offset\":113,\"position\":55},{\"token\":\"中\",\"start_offset\":113,\"end_offset\":114,\"position\":56},{\"token\":\"数据\",\"start_offset\":115,\"end_offset\":117,\"position\":58},{\"token\":\"保存\",\"start_offset\":117,\"end_offset\":119,\"position\":59},{\"token\":\"磁盘\",\"start_offset\":120,\"end_offset\":122,\"position\":61},{\"token\":\"中\",\"start_offset\":122,\"end_offset\":123,\"position\":62},{\"token\":\"重启\",\"start_offset\":124,\"end_offset\":126,\"position\":64},{\"token\":\"再次\",\"start_offset\":131,\"end_offset\":133,\"position\":68},{\"token\":\"加载\",\"start_offset\":133,\"end_offset\":135,\"position\":69},{\"token\":\"内存\",\"start_offset\":136,\"end_offset\":138,\"position\":71},{\"token\":\"使用\",\"start_offset\":138,\"end_offset\":140,\"position\":72}]}",
                res);
            Assert.True(res.Contains("key-value数据结构存储"));

            // unicode key
            db.KeyDelete("这是一个unicode_key");
            tair.tftcreateindex(Encoding.UTF8.GetBytes("这是一个unicode_key"),
                Encoding.UTF8.GetBytes(
                    "{\"mappings\":{\"properties\":{\"f0\":{\"type\":\"text\",\"analyzer\":\"my_jieba_analyzer\"}}},\"settings\":{\"analysis\":{\"analyzer\":{\"my_jieba_analyzer\":{\"type\":\"jieba\",\"userwords\":[\"key-value数据结构存储\"],\"use_hmm\":true}}}}}"));

            res = tair.tftanalyzer(Encoding.UTF8.GetBytes("my_jieba_analyzer"),
                Encoding.UTF8.GetBytes(
                    "Redis是完全开源免费的，遵守BSD协议，是一个灵活的高性能key-value数据结构存储，可以用来作为数据库、缓存和消息队列。Redis比其他key-value缓存产品有以下三个特点：Redis支持数据的持久化，可以将内存中的数据保存在磁盘中，重启的时候可以再次加载到内存使用。"),
                new TFTAnalyzerParams().index("这是一个unicode_key"));
            Assert.AreEqual(
                "{\"tokens\":[{\"token\":\"redis\",\"start_offset\":0,\"end_offset\":5,\"position\":0},{\"token\":\"完全\",\"start_offset\":6,\"end_offset\":8,\"position\":2},{\"token\":\"开源\",\"start_offset\":8,\"end_offset\":10,\"position\":3},{\"token\":\"免费\",\"start_offset\":10,\"end_offset\":12,\"position\":4},{\"token\":\"遵守\",\"start_offset\":14,\"end_offset\":16,\"position\":7},{\"token\":\"bsd\",\"start_offset\":16,\"end_offset\":19,\"position\":8},{\"token\":\"协议\",\"start_offset\":19,\"end_offset\":21,\"position\":9},{\"token\":\"一个\",\"start_offset\":23,\"end_offset\":25,\"position\":12},{\"token\":\"灵活\",\"start_offset\":25,\"end_offset\":27,\"position\":13},{\"token\":\"性能\",\"start_offset\":29,\"end_offset\":31,\"position\":15},{\"token\":\"高性能\",\"start_offset\":28,\"end_offset\":31,\"position\":16},{\"token\":\"数据\",\"start_offset\":40,\"end_offset\":42,\"position\":17},{\"token\":\"结构\",\"start_offset\":42,\"end_offset\":44,\"position\":18},{\"token\":\"存储\",\"start_offset\":44,\"end_offset\":46,\"position\":19},{\"token\":\"key-value数据结构存储\",\"start_offset\":31,\"end_offset\":46,\"position\":20},{\"token\":\"数据\",\"start_offset\":53,\"end_offset\":55,\"position\":25},{\"token\":\"数据库\",\"start_offset\":53,\"end_offset\":56,\"position\":26},{\"token\":\"缓存\",\"start_offset\":57,\"end_offset\":59,\"position\":28},{\"token\":\"消息\",\"start_offset\":60,\"end_offset\":62,\"position\":30},{\"token\":\"队列\",\"start_offset\":62,\"end_offset\":64,\"position\":31},{\"token\":\"redis\",\"start_offset\":65,\"end_offset\":70,\"position\":33},{\"token\":\"key\",\"start_offset\":73,\"end_offset\":76,\"position\":36},{\"token\":\"value\",\"start_offset\":77,\"end_offset\":82,\"position\":38},{\"token\":\"缓存\",\"start_offset\":82,\"end_offset\":84,\"position\":39},{\"token\":\"产品\",\"start_offset\":84,\"end_offset\":86,\"position\":40},{\"token\":\"以下\",\"start_offset\":87,\"end_offset\":89,\"position\":42},{\"token\":\"三个\",\"start_offset\":89,\"end_offset\":91,\"position\":43},{\"token\":\"特点\",\"start_offset\":91,\"end_offset\":93,\"position\":44},{\"token\":\"redis\",\"start_offset\":94,\"end_offset\":99,\"position\":46},{\"token\":\"支持\",\"start_offset\":99,\"end_offset\":101,\"position\":47},{\"token\":\"数据\",\"start_offset\":101,\"end_offset\":103,\"position\":48},{\"token\":\"持久\",\"start_offset\":104,\"end_offset\":106,\"position\":50},{\"token\":\"化\",\"start_offset\":106,\"end_offset\":107,\"position\":51},{\"token\":\"内存\",\"start_offset\":111,\"end_offset\":113,\"position\":55},{\"token\":\"中\",\"start_offset\":113,\"end_offset\":114,\"position\":56},{\"token\":\"数据\",\"start_offset\":115,\"end_offset\":117,\"position\":58},{\"token\":\"保存\",\"start_offset\":117,\"end_offset\":119,\"position\":59},{\"token\":\"磁盘\",\"start_offset\":120,\"end_offset\":122,\"position\":61},{\"token\":\"中\",\"start_offset\":122,\"end_offset\":123,\"position\":62},{\"token\":\"重启\",\"start_offset\":124,\"end_offset\":126,\"position\":64},{\"token\":\"再次\",\"start_offset\":131,\"end_offset\":133,\"position\":68},{\"token\":\"加载\",\"start_offset\":133,\"end_offset\":135,\"position\":69},{\"token\":\"内存\",\"start_offset\":136,\"end_offset\":138,\"position\":71},{\"token\":\"使用\",\"start_offset\":138,\"end_offset\":140,\"position\":72}]}",
                res);
            Assert.True(res.Contains("key-value数据结构存储"));

            // bytes key
            byte[] bytes_key = new byte[] {0x00};
            db.KeyDelete(bytes_key);
            tair.tftcreateindex(bytes_key,
                Encoding.UTF8.GetBytes(
                    "{\"mappings\":{\"properties\":{\"f0\":{\"type\":\"text\",\"analyzer\":\"my_jieba_analyzer\"}}},\"settings\":{\"analysis\":{\"analyzer\":{\"my_jieba_analyzer\":{\"type\":\"jieba\",\"userwords\":[\"key-value数据结构存储\"],\"use_hmm\":true}}}}}"));
            res = tair.tftanalyzer(Encoding.UTF8.GetBytes("my_jieba_analyzer"),
                Encoding.UTF8.GetBytes(
                    "Redis是完全开源免费的，遵守BSD协议，是一个灵活的高性能key-value数据结构存储，可以用来作为数据库、缓存和消息队列。Redis比其他key-value缓存产品有以下三个特点：Redis支持数据的持久化，可以将内存中的数据保存在磁盘中，重启的时候可以再次加载到内存使用。"),
                new TFTAnalyzerParams().index(bytes_key));
            Assert.AreEqual(
                "{\"tokens\":[{\"token\":\"redis\",\"start_offset\":0,\"end_offset\":5,\"position\":0},{\"token\":\"完全\",\"start_offset\":6,\"end_offset\":8,\"position\":2},{\"token\":\"开源\",\"start_offset\":8,\"end_offset\":10,\"position\":3},{\"token\":\"免费\",\"start_offset\":10,\"end_offset\":12,\"position\":4},{\"token\":\"遵守\",\"start_offset\":14,\"end_offset\":16,\"position\":7},{\"token\":\"bsd\",\"start_offset\":16,\"end_offset\":19,\"position\":8},{\"token\":\"协议\",\"start_offset\":19,\"end_offset\":21,\"position\":9},{\"token\":\"一个\",\"start_offset\":23,\"end_offset\":25,\"position\":12},{\"token\":\"灵活\",\"start_offset\":25,\"end_offset\":27,\"position\":13},{\"token\":\"性能\",\"start_offset\":29,\"end_offset\":31,\"position\":15},{\"token\":\"高性能\",\"start_offset\":28,\"end_offset\":31,\"position\":16},{\"token\":\"数据\",\"start_offset\":40,\"end_offset\":42,\"position\":17},{\"token\":\"结构\",\"start_offset\":42,\"end_offset\":44,\"position\":18},{\"token\":\"存储\",\"start_offset\":44,\"end_offset\":46,\"position\":19},{\"token\":\"key-value数据结构存储\",\"start_offset\":31,\"end_offset\":46,\"position\":20},{\"token\":\"数据\",\"start_offset\":53,\"end_offset\":55,\"position\":25},{\"token\":\"数据库\",\"start_offset\":53,\"end_offset\":56,\"position\":26},{\"token\":\"缓存\",\"start_offset\":57,\"end_offset\":59,\"position\":28},{\"token\":\"消息\",\"start_offset\":60,\"end_offset\":62,\"position\":30},{\"token\":\"队列\",\"start_offset\":62,\"end_offset\":64,\"position\":31},{\"token\":\"redis\",\"start_offset\":65,\"end_offset\":70,\"position\":33},{\"token\":\"key\",\"start_offset\":73,\"end_offset\":76,\"position\":36},{\"token\":\"value\",\"start_offset\":77,\"end_offset\":82,\"position\":38},{\"token\":\"缓存\",\"start_offset\":82,\"end_offset\":84,\"position\":39},{\"token\":\"产品\",\"start_offset\":84,\"end_offset\":86,\"position\":40},{\"token\":\"以下\",\"start_offset\":87,\"end_offset\":89,\"position\":42},{\"token\":\"三个\",\"start_offset\":89,\"end_offset\":91,\"position\":43},{\"token\":\"特点\",\"start_offset\":91,\"end_offset\":93,\"position\":44},{\"token\":\"redis\",\"start_offset\":94,\"end_offset\":99,\"position\":46},{\"token\":\"支持\",\"start_offset\":99,\"end_offset\":101,\"position\":47},{\"token\":\"数据\",\"start_offset\":101,\"end_offset\":103,\"position\":48},{\"token\":\"持久\",\"start_offset\":104,\"end_offset\":106,\"position\":50},{\"token\":\"化\",\"start_offset\":106,\"end_offset\":107,\"position\":51},{\"token\":\"内存\",\"start_offset\":111,\"end_offset\":113,\"position\":55},{\"token\":\"中\",\"start_offset\":113,\"end_offset\":114,\"position\":56},{\"token\":\"数据\",\"start_offset\":115,\"end_offset\":117,\"position\":58},{\"token\":\"保存\",\"start_offset\":117,\"end_offset\":119,\"position\":59},{\"token\":\"磁盘\",\"start_offset\":120,\"end_offset\":122,\"position\":61},{\"token\":\"中\",\"start_offset\":122,\"end_offset\":123,\"position\":62},{\"token\":\"重启\",\"start_offset\":124,\"end_offset\":126,\"position\":64},{\"token\":\"再次\",\"start_offset\":131,\"end_offset\":133,\"position\":68},{\"token\":\"加载\",\"start_offset\":133,\"end_offset\":135,\"position\":69},{\"token\":\"内存\",\"start_offset\":136,\"end_offset\":138,\"position\":71},{\"token\":\"使用\",\"start_offset\":138,\"end_offset\":140,\"position\":72}]}",
                res);
            Assert.True(res.Contains("key-value数据结构存储"));
        }

        [Test]
        public void explaincost()
        {
            db.KeyDelete("tftkey");
            String ret = tair.tftcreateindex("tftkey",
                "{\"mappings\":{\"dynamic\":\"false\",\"properties\":{\"f0\":{\"type\":\"text\"}}}}");
            Assert.AreEqual(ret, "OK");

            tair.tftadddoc("tftkey", "{\"f0\":\"redis is a nosql database\"}", "1");
            Assert.AreEqual(
                "{\"hits\":{\"hits\":[{\"_id\":\"1\",\"_index\":\"tftkey\",\"_score\":0.153426,\"_source\":{\"f0\":\"redis is a nosql database\"}}],\"max_score\":0.153426,\"total\":{\"relation\":\"eq\",\"value\":1}}}",
                tair.tftsearch("tftkey", "{\"query\":{\"term\":{\"f0\":\"redis\"}}}"));
            tair.tftadddoc("tftkey", "{\"f0\":\"redis is an in-memory database that persists on disk\"}", "2");
            tair.tftadddoc("tftkey", "{\"f0\":\"redis supports many different kind of values\"}", "3");

            String response = tair.tftexplaincost("tftkey", "{\"query\":{\"match\":{\"f0\":\"3\"}}}");

            Assert.True(response.Contains("QUERY_COST"));
        }
    }
}