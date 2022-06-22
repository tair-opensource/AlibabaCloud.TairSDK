using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairGis
{
    public class TairGis
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairGis(ConnectionMultiplexer conn, int num = 0)
        {
            _conn = conn;
            _redis = _conn.GetDatabase(num);
        }

        private IDatabase getRedis()
        {
            return _redis;
        }

        /// <summary>
        /// Add a polygon named polygongName in key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="polygonName"></param>
        /// <param name="polygonWktText">the polygonWktText
        /// example for polygonWktText: 'POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))'</param>
        /// <returns>Success:1; Cover old value:0.</returns>
        public long gisadd(string key, string polygonName, string polygonWktText)
        {
            var obj = getRedis().Execute(ModuleCommand.GISADD, key, polygonName, polygonWktText);
            return ResultHelper.Long(obj);
        }

        public long gisadd(byte[] key, byte[] polygonName, byte[] polygonWktText)
        {
            var obj = getRedis().Execute(ModuleCommand.GISADD, key, polygonName, polygonWktText);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Get a polygon named polygonName in key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="polygonName"></param>
        /// <returns>Success:polygonWktText;Noe exist:null;Fail:errror.</returns>
        public string gisget(string key, string polygonName)
        {
            var obj = getRedis().Execute(ModuleCommand.GISGET, key, polygonName);
            return ResultHelper.String(obj);
        }

        public byte[] gisget(byte[] key, byte[] polygonName)
        {
            var obj = getRedis().Execute(ModuleCommand.GISGET, key, polygonName);
            return ResultHelper.ByteArray(obj);
        }

        /// <summary>
        /// Find a polygon named polygonName in key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pointWktText"></param>
        /// <returns>Success:polygonWktText;Not find: null;Fail:error</returns>
        public Dictionary<string, string> gissearch(string key, string pointWktText)
        {
            var obj = getRedis().Execute(ModuleCommand.GISSEARCH, key, pointWktText);
            return GisHelper.DictResultString(obj);
        }

        public Dictionary<byte[], byte[]> gissearch(byte[] key, byte[] pointWktText)
        {
            var obj = getRedis().Execute(ModuleCommand.GISSEARCH, key, pointWktText);
            return GisHelper.DictResultByte(obj);
        }

        public List<GisSearchResponse> gissearch(string key, double longitude, double latitude, double radius,
            GeoUnit unit, GisParams gisParams)
        {
            string unit_tmp = GisHelper.geoUnitToString(unit);

            var obj = getRedis().Execute(ModuleCommand.GISSEARCH, gisParams.getByteParams(Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(GisParams.RAIDUS), Encoding.UTF8.GetBytes(longitude.ToString()),
                Encoding.UTF8.GetBytes(latitude.ToString()),
                Encoding.UTF8.GetBytes(radius.ToString()), Encoding.UTF8.GetBytes(unit_tmp)));
            return GisHelper.SearchListResponse(obj);
        }

        public List<GisSearchResponse> gissearch(byte[] key, double longitude, double latitude, double radius,
            GeoUnit unit, GisParams gisParams)
        {
            string unit_tmp = GisHelper.geoUnitToString(unit);

            var obj = getRedis().Execute(ModuleCommand.GISSEARCH, gisParams.getByteParams(key,
                Encoding.UTF8.GetBytes(GisParams.RAIDUS), Encoding.UTF8.GetBytes(longitude.ToString()),
                Encoding.UTF8.GetBytes(latitude.ToString()), Encoding.UTF8.GetBytes(radius.ToString()),
                Encoding.UTF8.GetBytes(unit_tmp)));

            return GisHelper.SearchListResponse(obj);
        }

        public List<GisSearchResponse> gissearchByMember(string key, string member, double radius, GeoUnit unit,
            GisParams gisParams)
        {
            string unit_tmp = GisHelper.geoUnitToString(unit);

            var obj = getRedis().Execute(ModuleCommand.GISSEARCH, gisParams.getByteParams(Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(GisParams.MEMBER), Encoding.UTF8.GetBytes(member),
                Encoding.UTF8.GetBytes(radius.ToString()), Encoding.UTF8.GetBytes(unit_tmp)));
            return GisHelper.SearchListResponse(obj);
        }

        public List<GisSearchResponse> gissearchByMember(byte[] key, byte[] member, double radius, GeoUnit unit,
            GisParams gisParams)
        {
            string unit_tmp = GisHelper.geoUnitToString(unit);

            var obj = getRedis().Execute(ModuleCommand.GISSEARCH,
                gisParams.getByteParams(key, Encoding.UTF8.GetBytes(GisParams.MEMBER), member,
                    Encoding.UTF8.GetBytes(radius.ToString()), Encoding.UTF8.GetBytes(unit_tmp)));
            return GisHelper.SearchListResponse(obj);
        }


        /// <summary>
        /// Judge the contain relationship fo rth pointWktText(poitn or linestring or polygonname) and the key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pointWktText">the pointWktText:<POINT/LINESTRING/POLYGONNAME></param>
        /// <returns>Success:polygonWktText;Not find:null;Fail:error.</returns>
        public Dictionary<string, string> giscontains(string key, string pointWktText)
        {
            var obj = getRedis().Execute(ModuleCommand.GISCONTAINS, key, pointWktText);
            return GisHelper.DictResultString(obj);
        }

        public Dictionary<byte[], byte[]> giscontains(byte[] key, byte[] pointWktText)
        {
            var obj = getRedis().Execute(ModuleCommand.GISCONTAINS, key, pointWktText);
            return GisHelper.DictResultByte(obj);
        }

        public List<string> giscontains(string key, string pointWktText, GisParams gisParams)
        {
            var obj = getRedis().Execute(ModuleCommand.GISCONTAINS,
                gisParams.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(pointWktText)));
            return GisHelper.ResultListString(obj);
        }

        public List<byte[]> giscontains(byte[] key, byte[] pointWktText, GisParams gisParams)
        {
            var obj = getRedis()
                .Execute(ModuleCommand.GISCONTAINS, gisParams.getByteParams(key, pointWktText));
            return GisHelper.ResultListByte(obj);
        }

        public Dictionary<string, string> giswithin(string key, string pointWktText)
        {
            var obj = getRedis().Execute(ModuleCommand.GISWITHIN, key, pointWktText);
            return GisHelper.DictResultString(obj);
        }

        public Dictionary<byte[], byte[]> giswithin(byte[] key, byte[] pointWktText)
        {
            var obj = getRedis().Execute(ModuleCommand.GISWITHIN, key, pointWktText);
            return GisHelper.DictResultByte(obj);
        }

        public List<string> giswithin(string key, string pointWktText, GisParams gisParams)
        {
            var obj = getRedis().Execute(ModuleCommand.GISWITHIN,
                gisParams.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(pointWktText)));
            return GisHelper.ResultListString(obj);
        }

        public List<byte[]> giswithin(byte[] key, byte[] pointWktText, GisParams gisParams)
        {
            var obj = getRedis()
                .Execute(ModuleCommand.GISWITHIN, gisParams.getByteParams(key, pointWktText));
            return GisHelper.ResultListByte(obj);
        }

        /// <summary>
        /// Judege the intersect relationship for the pointWktText (point or linestring or polygonname) and the key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pointWktText"></param>
        /// <returns>Success: polygonWktText; Not find: null; Fail: error.</returns>
        public Dictionary<string, string> gisintersects(string key, string pointWktText)
        {
            var obj = getRedis().Execute(ModuleCommand.GISINTERSECTS, key, pointWktText);
            return GisHelper.DictResultString(obj);
        }

        public Dictionary<byte[], byte[]> gisintersects(byte[] key, byte[] pointWktText)
        {
            var obj = getRedis().Execute(ModuleCommand.GISINTERSECTS, key, pointWktText);
            return GisHelper.DictResultByte(obj);
        }

        /// <summary>
        /// Delete a polygon named polygonName in key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="polygonName"></param>
        /// <returns>Success:OK; Not exist:null;Dail:error.</returns>
        public string gisdel(string key, string polygonName)
        {
            var obj = getRedis().Execute(ModuleCommand.GISDEL, key, polygonName);
            return ResultHelper.String(obj);
        }

        public byte[] gisdel(byte[] key, byte[] polygonName)
        {
            var obj = getRedis().Execute(ModuleCommand.GISDEL, key, polygonName);
            return ResultHelper.ByteArray(obj);
        }

        /// <summary>
        /// Get all polygon in key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, string> gisgetall(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.GISGETALL, key);
            return ResultHelper.DictString(obj);
        }

        public List<string> gisgetall(string key, GisParams gisParams)
        {
            var obj = getRedis().Execute(ModuleCommand.GISGETALL, gisParams.getByteParams(Encoding.UTF8.GetBytes(key)));
            return ResultHelper.ListString(obj);
        }

        public Dictionary<byte[], byte[]> gisgetall(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.GISGETALL, key);
            return ResultHelper.DictByte(obj);
        }

        public List<byte[]> gisgetall(byte[] key, GisParams gisParams)
        {
            var obj = getRedis().Execute(ModuleCommand.GISGETALL, gisParams.getByteParams(key));
            return ResultHelper.ListByte(obj);
        }
    }
}