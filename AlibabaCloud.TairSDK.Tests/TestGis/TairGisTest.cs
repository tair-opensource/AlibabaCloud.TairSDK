using System;
using System.Collections.Generic;
using System.Text;
using AlibabaCloud.TairSDK.TairGis;
using NUnit.Framework;
using StackExchange.Redis;

namespace TestGisTest
{
    public class GisTest
    {
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost");
        private readonly TairGis tair = new(connDC, 0);


        [Test]
        public void gissearchTest()
        {
            string area = "areaasdgalkjer";
            byte[] barea = Encoding.UTF8.GetBytes(area);
            long updated = 0;
            string retWktText = "";
            byte[] bretWktText = Encoding.UTF8.GetBytes("");

            string polygonName = "alibaba-xixi-campus",
                polygonWktText = "POLYGON((30 10,40 40,20 40,10 20,30 10))",
                pointWktText = "POINT(30 11)";

            // String
            updated = tair.gisadd(area, polygonName, polygonWktText);
            Assert.AreEqual(1, updated);

            retWktText = tair.gisget(area, polygonName);
            Assert.AreEqual(polygonWktText, retWktText);


            Dictionary<string, string> searchResults = tair.giscontains(area, pointWktText);
            Assert.AreEqual(1, searchResults.Count);
            Assert.AreEqual(retWktText, searchResults[polygonName]);

            searchResults = tair.gissearch(area, pointWktText);
            Assert.AreEqual(1, searchResults.Count);
            Assert.AreEqual(retWktText, searchResults[polygonName]);

            searchResults = tair.gisintersects(area, polygonWktText);
            Assert.AreEqual(1, searchResults.Count);
            Assert.AreEqual(retWktText, searchResults[polygonName]);

            //binary
            updated = tair.gisadd(barea, Encoding.UTF8.GetBytes(polygonName), Encoding.UTF8.GetBytes(polygonWktText));
            Assert.AreEqual(1, updated);


            bretWktText = tair.gisget(barea, Encoding.UTF8.GetBytes(polygonName));
            Assert.AreEqual(Encoding.UTF8.GetBytes(polygonWktText), bretWktText);


            Dictionary<byte[], byte[]> bsearchResults = tair.giscontains(barea, Encoding.UTF8.GetBytes(pointWktText));
            Assert.AreEqual(1, bsearchResults.Count);


            bsearchResults = tair.gisintersects(barea, Encoding.UTF8.GetBytes(pointWktText));
            Assert.AreEqual(1, bsearchResults.Count);
        }

        [Test]
        public void gisdelTest()
        {
            String uuid = "UUID.randomUUID().toString()";
            String key = "hangzhou" + uuid;
            String polygonName = "alibaba-xixi-campus";
            String polygonWktText = "POLYGON((30 10,40 40,20 40,10 20,30 10))";
            String polygonName1 = "alibaba-aliyun";
            String polygonWktText1 = "POLYGON((30 10,40 40))";

            long l = tair.gisadd(key, polygonName, polygonWktText);
            Assert.AreEqual(l, 1);
            l = tair.gisadd(key, polygonName1, polygonWktText1);
            Assert.AreEqual(l, 1);

            String retWktText = tair.gisget(key, polygonName);
            Assert.AreEqual(polygonWktText, retWktText);

            string ret = tair.gisdel(key, polygonName);
            Assert.AreEqual("OK", ret);

            String retWktText1 = tair.gisget(key, polygonName1);
            Assert.AreEqual(polygonWktText1, retWktText1);

            Dictionary<String, String> retMap = tair.giscontains(key, polygonWktText1);
            Assert.AreEqual(1, retMap.Count);
            Assert.AreEqual(polygonWktText1, retMap[polygonName1]);
        }

        [Test]
        public void gisdelValueNotExistsTest()
        {
            String uuid = "UUID.randomUUID().toString()";
            String key = "hangzhou" + uuid;
            String polygonName = "alibaba-xixi-campus";
            String polygonWktText = "POLYGON((30 10,40 40,20 40,10 20,30 10))";

            long l = tair.gisadd(key, polygonName, polygonWktText);
            Assert.AreEqual(l, 1);

            String ret = tair.gisdel(key, "not-exists-polygon");
            Assert.IsEmpty(ret);
        }


        [Test]
        public void giscontainsTest()
        {
            String uuid = "UUID.randomUUID().toString()+contain";
            String key = "hangzhou" + uuid;
            String polygonName = "alibaba-xixi-campus";
            String polygonWktText = "POLYGON((30 10,40 40,20 40,10 20,30 10))";
            String pointWkt = "POINT (30 11)";

            long l = tair.gisadd(key, polygonName, polygonWktText);
            Assert.AreEqual(l, 1);

            // giscontains
            Dictionary<String, String> retMap = tair.giscontains(key, pointWkt);
            Assert.AreEqual(1, retMap.Count);

            Assert.AreEqual(polygonWktText, retMap[polygonName]);

            // giscontains withoutwkt
            List<String> retList = tair.giscontains(key, pointWkt, GisParams.gisParams().withoutWkt());
            Assert.AreEqual(1, retList.Count);
            Assert.AreEqual(polygonName, retList[0]);
        }

        [Test]
        public void giswithinTest()
        {
            String uuid = "UUID.randomUUID().toString()sdfa";
            String key = "hangzhou" + uuid;
            String polygonName = "alibaba-xixi-campus";
            String polygonWktText = "POLYGON((30 10,40 40,20 40,10 20,30 10))";
            String polygonWkt = "POLYGON ((30 5, 50 50, 20 50, 5 20, 30 5))";

            long l = tair.gisadd(key, polygonName, polygonWktText);
            Assert.AreEqual(l, 1);

            // giswithin
            Dictionary<String, String> retMap = tair.giswithin(key, polygonWkt);
            Assert.AreEqual(polygonWktText, retMap[polygonName]);

            // giswithin withoutwkt
            List<String> retList = tair.giswithin(key, polygonWkt, GisParams.gisParams().withoutWkt());
            Assert.AreEqual(polygonName, retList[0]);
        }

        [Test]
        public void gisgetallTest()
        {
            String uuid = "UUID.randomUUID().toString()_getall";
            String key = "hangzhou" + uuid;
            String polygonName = "alibaba-xixi-campus";
            String polygonWktText = "POLYGON((30 10,40 40,20 40,10 20,30 10))";

            long l = tair.gisadd(key, polygonName, polygonWktText);
            Assert.AreEqual(l, 1);

            // gisgetall
            Dictionary<String, String> retMap = tair.gisgetall(key);

            Assert.AreEqual(polygonWktText, retMap[polygonName]);

            // gisgetall withoutwkt
            List<String> retList = tair.gisgetall(key, GisParams.gisParams().withoutWkt());
            Assert.AreEqual(1, retList.Count);
            Assert.AreEqual(polygonName, retList[0]);
        }

        [Test]
        public void gissearchByMember()
        {
            String uuid = "UUID.randomUUID().toString()+bymember";
            String key = "hangzhou" + uuid;

            tair.gisadd(key, "Palermo", "POINT (13.361389 38.115556)");
            tair.gisadd(key, "Catania", "POINT (15.087269 37.502669)");
            tair.gisadd(key, "Agrigento", "POINT (13.583333 37.316667)");

            // withoutvalue
            List<GisSearchResponse> responses = tair.gissearchByMember(key, "Palermo", 200,
                GeoUnit.Kilometers, new GisParams().withoutValue());
            Assert.AreEqual(3, responses.Count);
            Assert.AreEqual("Palermo", responses[0].getFieldByString());
            Assert.AreEqual(0, responses[0].getDistance());

            // withvalue
            responses = tair.gissearchByMember(key, "Palermo", 200,
                GeoUnit.Kilometers, new GisParams());
            Assert.AreEqual(3, responses.Count);
            Assert.AreEqual("Palermo", responses[0].getFieldByString());
            Assert.AreEqual("POINT(13.361389 38.115556)", responses[0].getValueByString());
            Assert.AreEqual(0, responses[0].getDistance());

            // withdist
            responses = tair.gissearchByMember(key, "Palermo", 200,
                GeoUnit.Kilometers, new GisParams().withDist());
            Assert.AreEqual("Palermo", responses[0].getFieldByString());
            Assert.AreEqual("POINT(13.361389 38.115556)", responses[0].getValueByString());
            Assert.AreEqual(166.2743, responses[1].getDistance());

            // SORT ASC
            responses = tair.gissearchByMember(key, "Palermo", 200,
                GeoUnit.Kilometers, new GisParams().withDist().sortAscending());
            Assert.AreEqual("Palermo", responses[0].getFieldByString());
            Assert.AreEqual("POINT(13.361389 38.115556)", responses[0].getValueByString());
            Assert.AreEqual(0.0, responses[0].getDistance());

            // SORT DESC
            responses = tair.gissearchByMember(key, "Palermo", 200,
                GeoUnit.Kilometers, new GisParams().withDist().sortDescending());
            Assert.AreEqual("Catania", responses[0].getFieldByString());
            Assert.AreEqual("POINT(15.087269 37.502669)", responses[0].getValueByString());
            Assert.AreEqual(166.2743, responses[0].getDistance());

            // COUNT 2
            responses = tair.gissearchByMember(key, "Palermo", 200, GeoUnit.Kilometers,
                new GisParams().withDist().sortDescending().count(2));
            Assert.AreEqual("Catania", responses[0].getFieldByString());
            Assert.AreEqual("POINT(15.087269 37.502669)", responses[0].getValueByString());
            Assert.AreEqual(166.2743, responses[0].getDistance());
            Assert.AreEqual("Agrigento", responses[1].getFieldByString());
            Assert.AreEqual("POINT(13.583333 37.316667)", responses[1].getValueByString());
            Assert.AreEqual(90.9779, responses[1].getDistance());
        }

        [Test]
        public void gissearchWithParams()
        {
            String uuid = "UUID.randomUUID().toString()+withparams";
            String key = "hangzhou" + uuid;

            tair.gisadd(key, "Palermo", "POINT (13.361389 38.115556)");
            tair.gisadd(key, "Catania", "POINT (15.087269 37.502669)");
            tair.gisadd(key, "Agrigento", "POINT (13.583333 37.316667)");

            // withoutvalue
            List<GisSearchResponse> responses = tair.gissearch(key, 15, 37, 200,
                GeoUnit.Kilometers, new GisParams().withoutValue());
            Assert.AreEqual(3, responses.Count);
            Assert.AreEqual("Palermo", responses[0].getFieldByString());
            Assert.AreEqual(0, responses[0].getDistance());

            // withvalue
            responses = tair.gissearch(key, 15, 37, 200,
                GeoUnit.Kilometers, new GisParams());
            Assert.AreEqual(3, responses.Count);
            Assert.AreEqual("Palermo", responses[0].getFieldByString());
            Assert.AreEqual("POINT(13.361389 38.115556)", responses[0].getValueByString());
            Assert.AreEqual(0, responses[0].getDistance());

            // withdist
            responses = tair.gissearch(key, 15, 37, 200,
                GeoUnit.Kilometers, new GisParams().withDist());
            Assert.AreEqual("Palermo", responses[0].getFieldByString());
            Assert.AreEqual("POINT(13.361389 38.115556)", responses[0].getValueByString());
            Assert.AreEqual(190.4424, responses[0].getDistance());

            // SORT ASC
            responses = tair.gissearch(key, 15, 37, 200,
                GeoUnit.Kilometers, new GisParams().withDist().sortAscending());
            Assert.AreEqual("Catania", responses[0].getFieldByString());
            Assert.AreEqual("POINT(15.087269 37.502669)", responses[0].getValueByString());
            Assert.AreEqual(56.4413, responses[0].getDistance());

            // SORT DESC
            responses = tair.gissearch(key, 15, 37, 200,
                GeoUnit.Kilometers, new GisParams().withDist().sortDescending());
            Assert.AreEqual("Palermo", responses[0].getFieldByString());
            Assert.AreEqual("POINT(13.361389 38.115556)", responses[0].getValueByString());
            Assert.AreEqual(190.4424, responses[0].getDistance());

            // COUNT 2
            responses = tair.gissearch(key, 15, 37, 200,
                GeoUnit.Kilometers, new GisParams().withDist().sortDescending().count(2));
            Assert.AreEqual("Palermo", responses[0].getFieldByString());
            Assert.AreEqual("POINT(13.361389 38.115556)", responses[0].getValueByString());
            Assert.AreEqual(190.4424, responses[0].getDistance());
            Assert.AreEqual("Agrigento", responses[1].getFieldByString());
            Assert.AreEqual("POINT(13.583333 37.316667)", responses[1].getValueByString());
            Assert.AreEqual(130.4233, responses[1].getDistance());
        }
    }
}