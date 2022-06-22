using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairGis
{
    public class TairGisAsync
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairGisAsync(ConnectionMultiplexer conn, int num = 0)
        {
            _conn = conn;
            _redis = _conn.GetDatabase(num);
        }

        private IDatabase getRedis()
        {
            return _redis;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="polygonName"></param>
        /// <param name="polygonWktText"></param>
        /// <returns></returns>
        public Task<RedisResult> gisadd(string key, string polygonName, string polygonWktText)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISADD, key, polygonName, polygonWktText);
            return obj;
        }

        public Task<RedisResult> gisadd(byte[] key, byte[] polygonName, byte[] polygonWktText)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISADD, key, polygonName, polygonWktText);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="polygonName"></param>
        /// <returns></returns>
        public Task<RedisResult> gisget(string key, string polygonName)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISGET, key, polygonName);
            return obj;
        }

        public Task<RedisResult> gisget(byte[] key, byte[] polygonName)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISGET, key, polygonName);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pointWktText"></param>
        /// <returns></returns>
        public Task<RedisResult> gissearch(string key, string pointWktText)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISSEARCH, key, pointWktText);
            return obj;
        }

        public Task<RedisResult> gissearch(byte[] key, byte[] pointWktText)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISSEARCH, key, pointWktText);
            return obj;
        }

        public Task<RedisResult> gissearch(string key, double longitude, double latitude, double radius,
            GeoUnit unit, GisParams gisParams)
        {
            string unit_tmp = GisHelper.geoUnitToString(unit);

            var obj = getRedis().ExecuteAsync(ModuleCommand.GISSEARCH, gisParams.getByteParams(
                Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(GisParams.RAIDUS), Encoding.UTF8.GetBytes(longitude.ToString()),
                Encoding.UTF8.GetBytes(latitude.ToString()),
                Encoding.UTF8.GetBytes(radius.ToString()), Encoding.UTF8.GetBytes(unit_tmp)));
            return obj;
        }

        public Task<RedisResult> gissearch(byte[] key, double longitude, double latitude, double radius,
            GeoUnit unit, GisParams gisParams)
        {
            string unit_tmp = GisHelper.geoUnitToString(unit);

            var obj = getRedis().ExecuteAsync(ModuleCommand.GISSEARCH, gisParams.getByteParams(key,
                Encoding.UTF8.GetBytes(GisParams.RAIDUS), Encoding.UTF8.GetBytes(longitude.ToString()),
                Encoding.UTF8.GetBytes(latitude.ToString()), Encoding.UTF8.GetBytes(radius.ToString()),
                Encoding.UTF8.GetBytes(unit_tmp)));

            return obj;
        }

        public Task<RedisResult> gissearchByMember(string key, string member, double radius, GeoUnit unit,
            GisParams gisParams)
        {
            string unit_tmp = GisHelper.geoUnitToString(unit);

            var obj = getRedis().ExecuteAsync(ModuleCommand.GISSEARCH, gisParams.getByteParams(
                Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(GisParams.MEMBER), Encoding.UTF8.GetBytes(member),
                Encoding.UTF8.GetBytes(radius.ToString()), Encoding.UTF8.GetBytes(unit_tmp)));
            return obj;
        }

        public Task<RedisResult> gissearchByMember(byte[] key, byte[] member, double radius, GeoUnit unit,
            GisParams gisParams)
        {
            string unit_tmp = GisHelper.geoUnitToString(unit);

            var obj = getRedis().ExecuteAsync(ModuleCommand.GISSEARCH,
                gisParams.getByteParams(key, Encoding.UTF8.GetBytes(GisParams.MEMBER), member,
                    Encoding.UTF8.GetBytes(radius.ToString()), Encoding.UTF8.GetBytes(unit_tmp)));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pointWktText"></param>
        /// <returns></returns>
        public Task<RedisResult> giscontains(string key, string pointWktText)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISCONTAINS, key, pointWktText);
            return obj;
        }

        public Task<RedisResult> giscontains(byte[] key, byte[] pointWktText)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISCONTAINS, key, pointWktText);
            return obj;
        }

        public Task<RedisResult> giscontains(string key, string pointWktText, GisParams gisParams)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISCONTAINS,
                gisParams.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(pointWktText)));
            return obj;
        }

        public Task<RedisResult> giscontains(byte[] key, byte[] pointWktText, GisParams gisParams)
        {
            var obj = getRedis()
                .ExecuteAsync(ModuleCommand.GISCONTAINS, gisParams.getByteParams(key, pointWktText));
            return obj;
        }

        public Task<RedisResult> giswithin(string key, string pointWktText)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISWITHIN, key, pointWktText);
            return obj;
        }

        public Task<RedisResult> giswithin(byte[] key, byte[] pointWktText)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISWITHIN, key, pointWktText);
            return obj;
        }

        public Task<RedisResult> giswithin(string key, string pointWktText, GisParams gisParams)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISWITHIN,
                gisParams.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(pointWktText)));
            return obj;
        }

        public Task<RedisResult> giswithin(byte[] key, byte[] pointWktText, GisParams gisParams)
        {
            var obj = getRedis()
                .ExecuteAsync(ModuleCommand.GISWITHIN, gisParams.getByteParams(key, pointWktText));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pointWktText"></param>
        /// <returns></returns>
        public Task<RedisResult> gisintersects(string key, string pointWktText)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISINTERSECTS, key, pointWktText);
            return obj;
        }

        public Task<RedisResult> gisintersects(byte[] key, byte[] pointWktText)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISINTERSECTS, key, pointWktText);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="polygonName"></param>
        /// <returns></returns>
        public Task<RedisResult> gisdel(string key, string polygonName)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISDEL, key, polygonName);
            return obj;
        }

        public Task<RedisResult> gisdel(byte[] key, byte[] polygonName)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISDEL, key, polygonName);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<RedisResult> gisgetall(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISGETALL, key);
            return obj;
        }

        public Task<RedisResult> gisgetall(string key, GisParams gisParams)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISGETALL,
                gisParams.getByteParams(Encoding.UTF8.GetBytes(key)));
            return obj;
        }

        public Task<RedisResult> gisgetall(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISGETALL, key);
            return obj;
        }

        public Task<RedisResult> gisgetall(byte[] key, GisParams gisParams)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.GISGETALL, gisParams.getByteParams(key));
            return obj;
        }
    }
}