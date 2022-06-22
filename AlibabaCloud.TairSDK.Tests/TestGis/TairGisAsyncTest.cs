using System.Collections.Generic;
using AlibabaCloud.TairSDK;
using AlibabaCloud.TairSDK.TairGis;
using NUnit.Framework;
using StackExchange.Redis;

namespace TestGisTest
{
    public class TairGisAsyncTest
    {
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost");
        private readonly TairGis tairGis = new TairGis(connDC, 0);
        private readonly TairGisAsync tairGisAsync = new(connDC, 0);

        [Test]
        public void gissearchTest()
        {
            string area = "areaasdgalkjer";

            string retWktText = "";

            string polygonName = "alibaba-xixi-campus",
                polygonWktText = "POLYGON((30 10,40 40,20 40,10 20,30 10))",
                pointWktText = "POINT(30 11)";

            // String
            var ret1 = tairGisAsync.gisadd(area, polygonName, polygonWktText);
            var ret2 = tairGisAsync.gisget(area, polygonName);
            var ret3 = tairGisAsync.gissearch(area, pointWktText);

            Assert.AreEqual(1, ResultHelper.Long(ret1.Result));
            Assert.AreEqual(polygonWktText, ResultHelper.String(ret2.Result));
            Assert.AreEqual(1, ResultHelper.DictString(ret3.Result).Count);
            Assert.AreEqual(polygonWktText, GisHelper.DictResultString(ret3.Result)[polygonName]);
        }

        [Test]
        public void gisdelTest()
        {
            string key = "hangzhou";
            string polygonName = "alibaba-xixi-campus";
            string polygonWktText = "POLYGON((30 10,40 40,20 40,10 20,30 10))";
            string polygonName1 = "alibaba-aliyun";
            string polygonWktText1 = "POLYGON((30 10,40 40))";

            var ret1 = tairGisAsync.gisadd(key, polygonName, polygonWktText);
            var ret2 = tairGisAsync.gisadd(key, polygonName1, polygonWktText1);
            var ret3 = tairGisAsync.gisget(key, polygonName);
            var ret4 = tairGisAsync.gisdel(key, polygonName);
            var ret5 = tairGisAsync.gisget(key, polygonName1);
            var ret6 = tairGisAsync.gissearch(key, polygonWktText1);
            var ret7 = tairGisAsync.giscontains(key, polygonWktText1, GisParams.gisParams().withoutWkt());
            var ret8 = tairGisAsync.gisgetall(key);

            Assert.AreEqual(1, ResultHelper.Long(ret1.Result));
            Assert.AreEqual(1, ResultHelper.Long(ret2.Result));
            Assert.AreEqual(polygonWktText, ResultHelper.String(ret3.Result));
            Assert.AreEqual("OK", ResultHelper.String(ret4.Result));
            Assert.AreEqual(polygonWktText1, ResultHelper.String(ret5.Result));

            Assert.AreEqual(1, GisHelper.DictResultString(ret6.Result).Count);
            Assert.True(GisHelper.DictResultString(ret6.Result).ContainsKey(polygonName1));

            Assert.AreEqual(1, GisHelper.ResultListString(ret7.Result).Count);
            Assert.AreEqual(polygonName1, GisHelper.ResultListByte(ret7.Result)[0]);


            Assert.AreEqual(1, ResultHelper.DictString(ret8.Result).Count);
            Assert.AreEqual(polygonWktText1, ResultHelper.DictString(ret8.Result)[polygonName1]);
        }

        [Test]
        public void gissearchTest2()
        {
            string key = "hangzhou-yuhang";
            tairGis.gisadd(key, "Palermo", "POINT (13.361389 38.115556)");
            tairGis.gisadd(key, "Catania", "POINT (15.087269 37.502669)");
            tairGis.gisadd(key, "Agrigento", "POINT (13.583333 37.316667)");

            var ret1 = tairGisAsync.gissearch(key, 15, 37, 200, GeoUnit.Kilometers, new GisParams().withoutValue());
            var ret2 = tairGisAsync.gissearchByMember(key, "Palermo", 200, GeoUnit.Kilometers,
                new GisParams().withoutValue());

            List<GisSearchResponse> response = GisHelper.SearchListResponse(ret1.Result);
            Assert.AreEqual(3, response.Count);
            Assert.AreEqual("Palermo", response[0].getFieldByString());

            response = GisHelper.SearchListResponse(ret2.Result);
            Assert.AreEqual(3, response.Count);
            Assert.AreEqual("Palermo", response[0].getFieldByString());
        }

        [Test]
        public void gisgetallTest()
        {
            string key = "hangzhou-xixi";
            string polygonName = "alibaba-xixi-campus";
            string polygonWktText = "POLYGON((30 10,40 40,20 40,10 20,30 10))";

            tairGis.gisadd(key, polygonName, polygonWktText);

            var ret1 = tairGisAsync.gisgetall(key);
            var ret2 = tairGisAsync.gisgetall(key, GisParams.gisParams().withoutWkt());

            Dictionary<string, string> retdict = ResultHelper.DictString(ret1.Result);
            Assert.AreEqual(1, retdict.Count);
            Assert.AreEqual(polygonWktText, retdict[polygonName]);

            List<string> retlist = ResultHelper.ListString(ret2.Result);
            Assert.AreEqual(1, retlist.Count);
            Assert.AreEqual(polygonName, retlist[0]);
        }

        [Test]
        public void giscontainTest()
        {
            string area = "hanhzhou-yungu";
            string polygonName = "alibaba-xixi-campus",
                polygonWktText = "POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))",
                pointWktText = "POINT (30 11)";
            tairGis.gisadd(area, polygonName, polygonWktText);

            var ret1 = tairGisAsync.gissearch(area, pointWktText);
            var ret2 = tairGisAsync.giscontains(area, pointWktText);
            var ret3 = tairGisAsync.gisintersects(area, pointWktText);

            Dictionary<string, string> searchres = GisHelper.DictResultString(ret1.Result);
            Assert.AreEqual(1, searchres.Count);


            searchres = GisHelper.DictResultString(ret2.Result);
            Assert.True(searchres.ContainsKey(polygonName));

            searchres = GisHelper.DictResultString(ret3.Result);
            Assert.AreEqual(1, searchres.Count);
        }
    }
}