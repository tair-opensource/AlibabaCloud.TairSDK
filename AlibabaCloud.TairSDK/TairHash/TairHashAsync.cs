using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AlibabaCloud.TairSDK.TairHash.Param;
using AlibabaCloud.TairSDK.Util;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairHash
{
    public class TairHashAsync
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairHashAsync(ConnectionMultiplexer conn, int num = 0)
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
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<RedisResult> exhset(string key, string field, string value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHSET, key, field, value);
            return obj;
        }

        public Task<RedisResult> exhset(byte[] key, byte[] field, byte[] value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHSET, key, field, value);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<RedisResult> exhset(string key, string field, string value, ExhsetParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHSET,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field),
                    Encoding.UTF8.GetBytes(value)));
            return obj;
        }

        public Task<RedisResult> exhset(byte[] key, byte[] field, byte[] value, ExhsetParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHSET, param.getByteParams(key, field, value));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<RedisResult> exhsetnx(string key, string field, string value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHSETNX, key, field, value);
            return obj;
        }

        public Task<RedisResult> exhsetnx(byte[] key, byte[] field, byte[] value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHSETNX, key, field, value);
            return obj;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public Task<RedisResult> exhmset(string key, Dictionary<string, string> hash)
        {
            var bhash = new Dictionary<byte[], byte[]>(hash.Count);
            foreach (var entry in hash)
                bhash.Add(Encoding.UTF8.GetBytes(entry.Key), Encoding.UTF8.GetBytes(entry.Value));

            return exhmset(Encoding.UTF8.GetBytes(key), bhash);
        }

        public Task<RedisResult> exhmset(byte[] key, Dictionary<byte[], byte[]> hash)
        {
            var param = new List<byte[]>();
            param.Add(key);

            foreach (var entry in hash)
            {
                param.Add(entry.Key);
                param.Add(entry.Value);
            }

            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHMSET, param.ToArray());
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<RedisResult> exhmsetwithopts(string key, List<ExhmsetwithopsParams<string>> param)
        {
            var bexhash = new List<ExhmsetwithopsParams<byte[]>>();
            foreach (var entry in param)
                bexhash.Add(new ExhmsetwithopsParams<byte[]>(Encoding.UTF8.GetBytes(entry.getField()),
                    Encoding.UTF8.GetBytes(entry.getValue()), entry.getVer(), entry.getExp()));

            return exhmsetwithopts(Encoding.UTF8.GetBytes(key), bexhash);
        }

        public Task<RedisResult> exhmsetwithopts(byte[] key, List<ExhmsetwithopsParams<byte[]>> param)
        {
            var p = new List<byte[]>();
            p.Add(key);

            foreach (var entry in param)
            {
                p.Add(entry.getField());
                p.Add(entry.getValue());
                p.Add(Encoding.UTF8.GetBytes(entry.getVer().ToString()));
                p.Add(Encoding.UTF8.GetBytes(entry.getExp().ToString()));
            }

            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHMSETWITHOPTS, p.ToArray());
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public Task<RedisResult> exhpexpire(string key, string field, int milliseconds)
        {
            return exhpexpire(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), milliseconds);
        }

        public Task<RedisResult> exhpexpire(byte[] key, byte[] field, int milliseconds)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHPEXPIRE, key, field,
                Encoding.UTF8.GetBytes(milliseconds.ToString()));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public Task<RedisResult> exhpexpireAt(string key, string field, long unixTime)
        {
            return exhpexpireAt(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), unixTime);
        }

        public Task<RedisResult> exhpexpireAt(byte[] key, byte[] field, long unixTime)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHPEXPIREAT, key, field,
                Encoding.UTF8.GetBytes(unixTime.ToString()));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public Task<RedisResult> exhexpire(string key, string field, int seconds)
        {
            return exhexpire(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), seconds);
        }

        public Task<RedisResult> exhexpire(byte[] key, byte[] field, int seconds)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHEXPIRE, key, field,
                Encoding.UTF8.GetBytes(seconds.ToString()));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public Task<RedisResult> exhexpireAt(string key, string field, long unixTime)
        {
            return exhexpireAt(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), unixTime);
        }

        public Task<RedisResult> exhexpireAt(byte[] key, byte[] field, long unixTime)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHPEXPIREAT, key, field,
                Encoding.UTF8.GetBytes(unixTime.ToString()));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Task<RedisResult> exhpttl(string key, string field)
        {
            return exhpttl(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field));
        }

        public Task<RedisResult> exhpttl(byte[] key, byte[] field)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHPTTL, key, field);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Task<RedisResult> exhttl(string key, string field)
        {
            return exhttl(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field));
        }

        public Task<RedisResult> exhttl(byte[] key, byte[] field)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHTTL, key, field);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Task<RedisResult> exhver(string key, string field)
        {
            return exhver(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field));
        }

        public Task<RedisResult> exhver(byte[] key, byte[] field)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHVER, key, field);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public Task<RedisResult> exhsetver(string key, string field, long version)
        {
            return exhsetver(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), version);
        }

        public Task<RedisResult> exhsetver(byte[] key, byte[] field, long version)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHSETVER, key, field,
                Encoding.UTF8.GetBytes(version.ToString()));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<RedisResult> exhincrBy(string key, string field, long value)
        {
            return exhincrBy(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), value);
        }

        public Task<RedisResult> exhincrBy(byte[] key, byte[] field, long value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHINCRBY, key, field,
                Encoding.UTF8.GetBytes(value.ToString()));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<RedisResult> exhincrBy(string key, string field, long value, ExhincrByParams param)
        {
            return exhincrBy(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), value, param);
        }

        public Task<RedisResult> exhincrBy(byte[] key, byte[] field, long value, ExhincrByParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHINCRBY,
                param.getByteParams(key, field, Encoding.UTF8.GetBytes(value.ToString())));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<RedisResult> exhincrByFloat(string key, string field, double value)
        {
            return exhincrByFloat(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), value);
        }

        public Task<RedisResult> exhincrByFloat(byte[] key, byte[] field, double value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHINCRBYFLOAT, key, field,
                Encoding.UTF8.GetBytes(value.ToString()));
            return obj;
        }

        public Task<RedisResult> exhincrByFloat(string key, string field, double value, ExhincrByFloatParams param)
        {
            return exhincrByFloat(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), value, param);
        }

        public Task<RedisResult> exhincrByFloat(byte[] key, byte[] field, double value, ExhincrByFloatParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHINCRBYFLOAT,
                param.getByteParams(key, field, Encoding.UTF8.GetBytes(value.ToString())));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Task<RedisResult> exhget(string key, string field)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHGET, key, field);
            return obj;
        }

        public Task<RedisResult> exhget(byte[] key, byte[] field)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHGET, key, field);
            return obj;
        }

        public Task<RedisResult> exhgetwithver(string key, string field)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHGETWITHVER, key, field);
            return obj;
        }

        public Task<RedisResult> exhgetwithver(byte[] key, byte[] field)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHGETWITHVER, key, field);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public Task<RedisResult> exhmget(string key, params string[] fields)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHMGET,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(key), HashHepler.encodemany(fields)));
            return obj;
        }

        public Task<RedisResult> exhmget(byte[] key, params byte[][] fields)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHMGET, JoinParameters.joinParameters(key, fields));
            return obj;
        }

        public Task<RedisResult> exhmgetwithver(string key, string[] fields)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHMGETWITHVER,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(key), HashHepler.encodemany(fields)));
            return obj;
        }

        public Task<RedisResult> exhmgetwithver(byte[] key, params byte[][] fields)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHMGETWITHVER, JoinParameters.joinParameters(key, fields));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public Task<RedisResult> exhdel(string key, params string[] fields)
        {
            return exhdel(Encoding.UTF8.GetBytes(key), HashHepler.encodemany(fields));
        }

        public Task<RedisResult> exhdel(byte[] key, params byte[][] fields)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHDEL, JoinParameters.joinParameters(key, fields));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<RedisResult> exhlen(string key)
        {
            return exhlen(Encoding.UTF8.GetBytes(key));
        }

        public Task<RedisResult> exhlen(string key, bool noexp)
        {
            return exhlen(Encoding.UTF8.GetBytes(key), noexp);
        }

        public Task<RedisResult> exhlen(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHLEN, key);
            return obj;
        }

        public Task<RedisResult> exhlen(byte[] key, bool noexp)
        {
            Task<RedisResult> obj;
            if (noexp)
                obj = getRedis().ExecuteAsync(ModuleCommand.EXHLEN, key, Encoding.UTF8.GetBytes("noexp"));
            else
                obj = getRedis().ExecuteAsync(ModuleCommand.EXHLEN, key);

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Task<RedisResult> exhexists(string key, string field)
        {
            return exhexists(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field));
        }

        public Task<RedisResult> exhexists(byte[] key, byte[] field)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHEXISTS, key, field);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Task<RedisResult> exhstrlen(string key, string field)
        {
            return exhstrlen(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field));
        }

        public Task<RedisResult> exhstrlen(byte[] key, byte[] field)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHSTRLEN, key, field);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<RedisResult> exhkeys(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHKEYS, key);
            return obj;
        }

        public Task<RedisResult> exhkeys(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHKEYS, key);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<RedisResult> exhvals(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHVALS, key);
            return obj;
        }

        public Task<RedisResult> exhvals(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHVALS, key);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<RedisResult> exhgetAll(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHGETALL, key);
            return obj;
        }

        public Task<RedisResult> exhgetAll(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHGETALL, key);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="op"></param>
        /// <param name="subkey"></param>
        /// <returns></returns>
        public Task<RedisResult> exhscan(string key, string op, string subkey)
        {
            return exhscan(key, op, subkey, new ExhscanParams());
        }

        public Task<RedisResult> exhscan(byte[] key, byte[] op, byte[] subkey)
        {
            return exhscan(key, op, subkey, new ExhscanParams());
        }

        public Task<RedisResult> exhscan(string key, string op, string subkey, ExhscanParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHSCAN,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(op),
                    Encoding.UTF8.GetBytes(subkey)));

            return obj;
        }

        public Task<RedisResult> exhscan(byte[] key, byte[] op, byte[] subkey, ExhscanParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXHSCAN, param.getByteParams(key, op, subkey));
            return obj;
        }
    }
}