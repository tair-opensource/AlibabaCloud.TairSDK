using System.Text;
using System.Threading.Tasks;
using AlibabaCloud.TairSDK.TairDoc.Param;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairDoc
{
    public class TairDocAsync
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairDocAsync(ConnectionMultiplexer conn, int num = 0)
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
        /// <param name="path"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public Task<RedisResult> jsonset(string key, string path, string json)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONSET, key, path, json);
            return obj;
        }

        public Task<RedisResult> jsonset(string key, string path, string json, JsonsetParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONSET,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(path),
                    Encoding.UTF8.GetBytes(json)));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public Task<RedisResult> jsonset(byte[] key, byte[] path, byte[] json)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONSET, key, path, json);
            return obj;
        }

        public Task<RedisResult> jsonset(byte[] key, byte[] path, byte[] json, JsonsetParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONSET, param.getByteParams(key, path, json));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<RedisResult> jsonget(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONGET, key);
            return obj;
        }

        public Task<RedisResult> jsonget(string key, string path)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONGET, key, path);
            return obj;
        }

        public Task<RedisResult> jsonget(string key, string path, JsongetParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONGET,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(path)));
            return obj;
        }

        public Task<RedisResult> jsonget(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONGET, key);
            return obj;
        }

        public Task<RedisResult> jsonget(byte[] key, byte[] path)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONGET, key, path);
            return obj;
        }

        public Task<RedisResult> jsonget(byte[] key, byte[] path, JsongetParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONGET, param.getByteParams(key, path));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Task<RedisResult> jsonmget(params string[] args)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONMGET, args);
            return obj;
        }

        public Task<RedisResult> jsonmget(params byte[][] args)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONMGET, args);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<RedisResult> jsondel(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONDEL, key);
            return obj;
        }

        public Task<RedisResult> jsondel(string key, string path)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONDEL, key, path);
            return obj;
        }

        public Task<RedisResult> jsondel(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONDEL, key);
            return obj;
        }

        public Task<RedisResult> jsondel(byte[] key, byte[] path)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONDEL, key, path);
            return obj;
        }

        public Task<RedisResult> jsontype(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONTYPE, key);
            return obj;
        }

        public Task<RedisResult> jsontype(string key, string path)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONTYPE, key, path);
            return obj;
        }

        public Task<RedisResult> jsontype(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONTYPE, key);
            return obj;
        }

        public Task<RedisResult> jsontype(byte[] key, byte[] path)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONTYPE, key, path);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<RedisResult> jsonnumincrBy(string key, double value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONNUMINCRBY, Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(value.ToString()));
            return obj;
        }

        public Task<RedisResult> jsonnumincrBy(string key, string path, double value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONNUMINCRBY, Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(path), Encoding.UTF8.GetBytes(value.ToString()));
            return obj;
        }

        public Task<RedisResult> jsonnumincrBy(byte[] key, double value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONNUMINCRBY, key,
                Encoding.UTF8.GetBytes(value.ToString()));
            return obj;
        }

        public Task<RedisResult> jsonnumincrBy(byte[] key, byte[] path, double value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONNUMINCRBY, key,
                path, Encoding.UTF8.GetBytes(value.ToString()));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public Task<RedisResult> jsonstrAppend(string key, string json)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONSTRAPPEND, key, json);
            return obj;
        }

        public Task<RedisResult> jsonstrAppend(string key, string path, string json)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONSTRAPPEND, key, path, json);
            return obj;
        }

        public Task<RedisResult> jsonstrAppend(byte[] key, byte[] json)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONSTRAPPEND, key, json);
            return obj;
        }

        public Task<RedisResult> jsonstrAppend(byte[] key, byte[] path, byte[] json)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONSTRAPPEND, key, path, json);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<RedisResult> jsonstrlen(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONSTRLEN, key);
            return obj;
        }

        public Task<RedisResult> jsonstrlen(string key, string path)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONSTRLEN, key, path);
            return obj;
        }

        public Task<RedisResult> jsonstrlen(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONSTRLEN, key);
            return obj;
        }

        public Task<RedisResult> jsonstrlen(byte[] key, byte[] path)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONSTRLEN, key, path);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Task<RedisResult> jsonarrAppend(params string[] args)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONARRAPPEND, args);
            return obj;
        }

        public Task<RedisResult> jsonarrAppend(params byte[][] args)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONARRAPPEND, args);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public Task<RedisResult> jsonarrPop(string key, string path)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONARRPOP, key, path);
            return obj;
        }

        public Task<RedisResult> jsonarrPop(string key, string path, int index)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONARRPOP, key, path, index.ToString());
            return obj;
        }

        public Task<RedisResult> jsonarrPop(byte[] key, byte[] path)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONARRPOP, key, path);
            return obj;
        }

        public Task<RedisResult> jsonarrPop(byte[] key, byte[] path, int index)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONARRPOP, key, path,
                Encoding.UTF8.GetBytes(index.ToString()));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Task<RedisResult> jsonarrInsert(params string[] args)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONARRINSERT, args);
            return obj;
        }

        public Task<RedisResult> jsonarrInsert(params byte[][] args)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONARRINSERT, args);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<RedisResult> jsonArrLen(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONARRLEN, key);
            return obj;
        }

        public Task<RedisResult> jsonArrLen(string key, string path)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONARRLEN, key, path);
            return obj;
        }

        public Task<RedisResult> jsonArrLen(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONARRLEN, key);
            return obj;
        }

        public Task<RedisResult> jsonArrLen(byte[] key, byte[] path)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONARRLEN, key, path);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public Task<RedisResult> jsonarrTrim(string key, string path, int start, int stop)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONARRTRIM, key, path, start.ToString(), stop.ToString());
            return obj;
        }

        public Task<RedisResult> jsonarrTrim(byte[] key, byte[] path, int start, int stop)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.JSONARRTRIM, key, path,
                Encoding.UTF8.GetBytes(start.ToString()),
                Encoding.UTF8.GetBytes(stop.ToString()));
            return obj;
        }
    }
}