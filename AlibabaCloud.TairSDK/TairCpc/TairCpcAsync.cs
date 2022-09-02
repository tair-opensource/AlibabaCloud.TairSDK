using System.Text;
using System.Threading.Tasks;
using AlibabaCloud.TairSDK.TairCpc.Param;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairCpc
{
    public class TairCpcAsync
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairCpcAsync(ConnectionMultiplexer conn, int num = 0)
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
        /// <returns></returns>
        public Task<RedisResult> cpcEstimate(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCESTIMATE, key);
            return obj;
        }

        public Task<RedisResult> cpcEstimate(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCESTIMATE, key);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<RedisResult> cpcUpdate(string key, string item)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCUPDATE, key, item);
            return obj;
        }

        public Task<RedisResult> cpcUpdate(byte[] key, byte[] item)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCUPDATE, key, item);
            return obj;
        }

        public Task<RedisResult> cpcUpdate(string key, string item, CpcUpdateParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCUPDATE,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(item)));
            return obj;
        }

        public Task<RedisResult> cpcUpdate(byte[] key, byte[] item, CpcUpdateParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCUPDATE, param.getByteParams(key, item));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<RedisResult> cpcUpdate2Est(string key, string item)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCUPDATE2EST, key, item);
            return obj;
        }

        public Task<RedisResult> cpcUpdate2Est(byte[] key, byte[] item)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCUPDATE2EST, key, item);
            return obj;
        }

        public Task<RedisResult> cpcUpdate2Est(string key, string item, CpcUpdateParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCUPDATE2EST,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(item)));
            return obj;
        }

        public Task<RedisResult> cpcUpdate2Est(byte[] key, byte[] item, CpcUpdateParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCUPDATE2EST, param.getByteParams(key, item));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<RedisResult> cpcUpdate2Jud(string key, string item)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCUPDATE2JUD, key, item);
            return obj;
        }

        public Task<RedisResult> CpcUpdate2Jud(byte[] key, byte[] item)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCUPDATE2JUD, key, item);
            return obj;
        }

        public Task<RedisResult> cpcUpdate2Jud(string key, string item, CpcUpdateParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCUPDATE2JUD,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(item)));
            return obj;
        }

        public Task<RedisResult> CpcUpdate2Jud(byte[] key, byte[] item, CpcUpdateParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCUPDATE2JUD, param.getByteParams(key, item));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timestamp"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<RedisResult> cpcArrayUpdate(string key, long timestamp, string item)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYUPDATE, key, timestamp.ToString(), item);
            return obj;
        }

        public Task<RedisResult> cpcArrayUpdate(byte[] key, long timestamp, byte[] item)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYUPDATE, key,
                Encoding.UTF8.GetBytes(timestamp.ToString()), item);
            return obj;
        }

        public Task<RedisResult> cpcArrayUpdate(string key, long timestamp, string item, CpcUpdateParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYUPDATE,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(timestamp.ToString()),
                    Encoding.UTF8.GetBytes(item)));
            return obj;
        }

        public Task<RedisResult> cpcArrayUpdate(byte[] key, long timestamp, byte[] item, CpcUpdateParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYUPDATE,
                param.getByteParams(key, Encoding.UTF8.GetBytes(timestamp.ToString()), item));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public Task<RedisResult> cpcArrayEstimate(string key, long timestamp)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYESTIMATE, key, timestamp);
            return obj;
        }

        public Task<RedisResult> cpcArrayEstimate(byte[] key, long timestamp)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYESTIMATE, key,
                Encoding.UTF8.GetBytes(timestamp.ToString()));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public Task<RedisResult> cpcArrayEstimateRange(string key, long starttime, long endtime)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYESTIMATERANGE, key, starttime, endtime);
            return obj;
        }

        public Task<RedisResult> cpcArrayEstimateRange(byte[] key, long starttime, long endtime)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYESTIMATERANGE, key,
                Encoding.UTF8.GetBytes(starttime.ToString()), Encoding.UTF8.GetBytes(endtime.ToString()));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timestamp"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public Task<RedisResult> cpcArrayEstimateRangeMerge(string key, long timestamp, long range)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYESTIMATERANGEMERGE, key, timestamp.ToString(),
                range.ToString());
            return obj;
        }

        public Task<RedisResult> cpcArrayEstimateRangeMerge(byte[] key, long timestamp, long range)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYESTIMATERANGEMERGE, key,
                Encoding.UTF8.GetBytes(timestamp.ToString()), Encoding.UTF8.GetBytes(range.ToString()));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timestamp"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<RedisResult> cpcArrayUpdate2Est(string key, long timestamp, string item)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYUPDATE2EST, key, timestamp.ToString(), item);
            return obj;
        }

        public Task<RedisResult> cpcArrayUpdate2Est(byte[] key, long timestamp, byte[] item)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYUPDATE2EST, key,
                Encoding.UTF8.GetBytes(timestamp.ToString()), item);
            return obj;
        }

        public Task<RedisResult> cpcArrayUpdate2Est(string key, long timestamp, string item, CpcUpdateParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYUPDATE2EST,
                param.getByteParams(Encoding.UTF8.GetBytes(key),
                    Encoding.UTF8.GetBytes(timestamp.ToString()), Encoding.UTF8.GetBytes(item)));
            return obj;
        }

        public Task<RedisResult> cpcArrayUpdate2Est(byte[] key, long timestamp, byte[] item, CpcUpdateParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYUPDATE2EST,
                param.getByteParams(key, Encoding.UTF8.GetBytes(timestamp.ToString()), item));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timestamp"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<RedisResult> cpcArrayUpdate2Jud(string key, long timestamp, string item)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYUPDATE2JUD, key, timestamp.ToString(), item);
            return obj;
        }

        public Task<RedisResult> cpcArrayUpdate2Jud(byte[] key, long timestamp, byte[] item)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYUPDATE2JUD, key,
                Encoding.UTF8.GetBytes(timestamp.ToString()), item);
            return obj;
        }

        public Task<RedisResult> cpcArrayUpdate2Jud(string key, long timestamp, string item, CpcUpdateParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYUPDATE2JUD,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(timestamp.ToString()),
                    Encoding.UTF8.GetBytes(item)));
            return obj;
        }

        public Task<RedisResult> cpcArrayUpdate2Jud(byte[] key, long timestamp, byte[] item, CpcUpdateParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CPCARRAYUPDATE2JUD,
                param.getByteParams(key, Encoding.UTF8.GetBytes(timestamp.ToString()), item));
            return obj;
        }
    }
}