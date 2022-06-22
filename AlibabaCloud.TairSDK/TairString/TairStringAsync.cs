using System.Text;
using System.Threading.Tasks;
using AlibabaCloud.TairSDK.TairString.Param;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairString
{
    public class TairStringAsync
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairStringAsync(ConnectionMultiplexer conn, int num = 0)
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
        /// <param name="oldvalue"></param>
        /// <param name="newvalue"></param>
        /// <returns></returns>
        public Task<RedisResult> cas(string key, string oldvalue, string newvalue)
        {
            return cas(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(oldvalue), Encoding.UTF8.GetBytes(newvalue));
        }

        public Task<RedisResult> cas(byte[] key, byte[] oldvalue, byte[] newvalue)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CAS, key, oldvalue, newvalue);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="oldvalue"></param>
        /// <param name="newvalue"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<RedisResult> cas(string key, string oldvalue, string newvalue, CasParams param)
        {
            return cas(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(oldvalue), Encoding.UTF8.GetBytes(newvalue),
                param);
        }

        public Task<RedisResult> cas(byte[] key, byte[] oldvalue, byte[] newvalue, CasParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CAS, param.getByteParams(key, oldvalue, newvalue));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<RedisResult> cad(string key, string value)
        {
            return cad(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(value));
        }

        public Task<RedisResult> cad(byte[] key, byte[] value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.CAD, key, value);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<RedisResult> exget(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXGET, key);
            return obj;
        }

        public Task<RedisResult> exget(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXGET, key);
            return obj;
        }

        public Task<RedisResult> exgetFlags(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXGET, key, "WITHFLAGS");

            return obj;
        }

        public Task<RedisResult> exgetFlags(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXGET, key, Encoding.UTF8.GetBytes("WITHFLAGS"));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<RedisResult> exset(string key, string value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXSET, key, value);
            return obj;
        }

        public Task<RedisResult> exset(byte[] key, byte[] value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXSET, key, value);
            return obj;
        }


        public Task<RedisResult> exset(string key, string value, ExsetParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXSET, param.getByteParams(key, value));
            return obj;
        }

        public Task<RedisResult> exset(byte[] key, byte[] value, ExsetParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXSET, param.getByteParams(key, value));
            return obj;
        }

        public Task<RedisResult> exsetVersion(string key, string value, ExsetParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXSET,
                param.getByteParams(key, value, "WITHVERSION"));
            return obj;
        }

        public Task<RedisResult> exsetVersion(byte[] key, byte[] value, ExsetParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXSET,
                param.getByteParams(key, value, Encoding.UTF8.GetBytes("WITHVERSION")));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public Task<RedisResult> exsetver(string key, long version)
        {
            return exsetver(Encoding.UTF8.GetBytes(key), version);
        }

        public Task<RedisResult> exsetver(byte[] key, long version)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXSETVER, key, version);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="incr"></param>
        /// <returns></returns>
        public Task<RedisResult> exincrBy(string key, long incr)
        {
            return exincrBy(Encoding.UTF8.GetBytes(key), incr);
        }

        public Task<RedisResult> exincrBy(byte[] key, long incr)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXINCRBY, key, incr);
            return obj;
        }


        public Task<RedisResult> exincrBy(string key, long incr, ExincrbyParams param)
        {
            return exincrBy(Encoding.UTF8.GetBytes(key), incr, param);
        }

        public Task<RedisResult> exincrBy(byte[] key, long incr, ExincrbyParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXINCRBY,
                param.getByteParams(key, Encoding.UTF8.GetBytes(incr.ToString())));
            return obj;
        }

        public Task<RedisResult> exincrByVersion(string key, long incr, ExincrbyParams param)
        {
            return exincrByVersion(Encoding.UTF8.GetBytes(key), incr, param);
        }

        public Task<RedisResult> exincrByVersion(byte[] key, long incr, ExincrbyParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXINCRBY,
                param.getByteParams(key, Encoding.UTF8.GetBytes(incr.ToString())), "WITHVERSION");
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="incr"></param>
        /// <returns></returns>
        public Task<RedisResult> exincrByFloat(string key, double incr)
        {
            return exincrByFloat(Encoding.UTF8.GetBytes(key), incr);
        }

        public Task<RedisResult> exincrByFloat(byte[] key, double incr)
        {
            var obj = getRedis()
                .ExecuteAsync(ModuleCommand.EXINCRBYFLOAT, key, Encoding.UTF8.GetBytes(incr.ToString()));
            return obj;
        }

        public Task<RedisResult> exincrByFloat(string key, double incr, ExincrbyFloatParams param)
        {
            return exincrByFloat(Encoding.UTF8.GetBytes(key), incr, param);
        }

        public Task<RedisResult> exincrByFloat(byte[] key, double incr, ExincrbyFloatParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXINCRBYFLOAT,
                param.getByteParams(key, Encoding.UTF8.GetBytes(incr.ToString())));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="newvalue"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public Task<RedisResult> excas(string key, string newvalue, long version)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXCAS, key, newvalue, version.ToString());
            return obj;
        }

        public Task<RedisResult> excas(byte[] key, byte[] newvalue, long version)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXCAS, key, newvalue, version);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public Task<RedisResult> excad(string key, long version)
        {
            return excad(Encoding.UTF8.GetBytes(key), version);
        }

        public Task<RedisResult> excad(byte[] key, long version)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXCAD, key, Encoding.UTF8.GetBytes(version.ToString()));
            return obj;
        }
    }
}