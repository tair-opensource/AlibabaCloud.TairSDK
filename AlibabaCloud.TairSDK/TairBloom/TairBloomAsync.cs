using System.Text;
using System.Threading.Tasks;
using AlibabaCloud.TairSDK.TairBloom.Param;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairBloom
{
    public class TairBloomAsync
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairBloomAsync(ConnectionMultiplexer conn, int num = 0)
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
        /// <param name="errorRate"></param>
        /// <param name="initCapacity"></param>
        /// <returns></returns>
        public Task<RedisResult> bfreserve(string key, double errorRate, long initCapacity)
        {
            return bfreserve(Encoding.UTF8.GetBytes(key), errorRate, initCapacity);
        }

        public Task<RedisResult> bfreserve(byte[] key, double errorRate, long initCapacity)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.BFRESERVE, key, errorRate, initCapacity);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<RedisResult> bfadd(string key, string item)
        {
            return bfadd(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(item));
        }

        public Task<RedisResult> bfadd(byte[] key, byte[] item)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.BFADD, key, item);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public Task<RedisResult> bfmadd(string key, params string[] items)
        {
            return bfmadd(Encoding.UTF8.GetBytes(key), BloomHelper.encodemany(items));
        }

        public Task<RedisResult> bfmadd(byte[] key, params byte[][] items)
        {
            BfmaddParams param = new BfmaddParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.BFMADD, param.getByteParams(key, items));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<RedisResult> bfexists(string key, string item)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.BFEXISTS, key, item);
            return obj;
        }

        public Task<RedisResult> bfexists(byte[] key, byte[] item)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.BFEXISTS, key, item);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public Task<RedisResult> bfmexists(string key, params string[] items)
        {
            return bfmexists(Encoding.UTF8.GetBytes(key), BloomHelper.encodemany(items));
        }

        public Task<RedisResult> bfmexists(byte[] key, params byte[][] items)
        {
            BfmexistsParams param = new BfmexistsParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.BFMEXISTS, param.getByteParams(key, items));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="param"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public Task<RedisResult> bfinsert(string key, BfinsertParams param, params string[] items)
        {
            return bfinsert(Encoding.UTF8.GetBytes(key), param, BloomHelper.encodemany(items));
        }

        public Task<RedisResult> bfinsert(byte[] key, BfinsertParams param, params byte[][] items)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.BFINSERT, param.getByteParams(key, items));

            return obj;
        }

        public Task<RedisResult> bfinsert(string key, string initCapacityTag, long initCapacity, string errorRateTag,
            double errorRate, string itemTag, params string[] items)
        {
            BfinsertParams param = new BfinsertParams();
            byte[][] metadata = param.getByteParamsMeta(key, initCapacityTag, initCapacity.ToString(), errorRateTag,
                errorRate.ToString(), itemTag);
            var obj = getRedis().ExecuteAsync(ModuleCommand.BFINSERT, param.getByteParams(metadata, items));
            return obj;
        }

        public Task<RedisResult> bfinsert(string key, string nocreateTag, string itemTag, params string[] items)
        {
            BfinsertParams param = new BfinsertParams();
            byte[][] metadata = param.getByteParamsMeta(key, nocreateTag, itemTag);
            var obj = getRedis().ExecuteAsync(ModuleCommand.BFINSERT, param.getByteParams(metadata, items));
            return obj;
        }

        public Task<RedisResult> bfinsert(string key, string itemTag, params string[] items)
        {
            BfinsertParams param = new BfinsertParams();
            byte[][] metadata = param.getByteParamsMeta(key, itemTag);
            var obj = getRedis().ExecuteAsync(ModuleCommand.BFINSERT, param.getByteParams(metadata, items));
            return obj;
        }

        public Task<RedisResult> bfinsert(byte[] key, byte[] initCapacityTag, long initCapacity, byte[] errorRateTag,
            double errorRate, byte[] itemTag, params byte[][] items)
        {
            BfinsertParams param = new BfinsertParams();
            byte[][] metadata = param.getByteParamsMeta(key, initCapacityTag,
                Encoding.UTF8.GetBytes(initCapacity.ToString()), errorRateTag,
                Encoding.UTF8.GetBytes(errorRate.ToString()), itemTag);
            var obj = getRedis().ExecuteAsync(ModuleCommand.BFINSERT, param.getByteParams(metadata, items));
            return obj;
        }

        public Task<RedisResult> bfinsert(byte[] key, byte[] nocreateTag, byte[] itemTag, params byte[][] items)
        {
            BfinsertParams param = new BfinsertParams();
            byte[][] metadata = param.getByteParamsMeta(key, nocreateTag, itemTag);
            var obj = getRedis().ExecuteAsync(ModuleCommand.BFINSERT, param.getByteParams(metadata, items));
            return obj;
        }

        public Task<RedisResult> bfinsert(byte[] key, byte[] itemTag, params byte[][] items)
        {
            BfinsertParams param = new BfinsertParams();
            byte[][] metadata = param.getByteParamsMeta(key, itemTag);
            var obj = getRedis().ExecuteAsync(ModuleCommand.BFINSERT, param.getByteParams(metadata, items));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<RedisResult> bfdebug(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.BFDEBUG, key);
            return obj;
        }

        public Task<RedisResult> bfdebug(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.BFDEBUG, key);
            return obj;
        }
    }
}