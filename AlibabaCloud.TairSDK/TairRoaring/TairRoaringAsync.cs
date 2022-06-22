using System.Text;
using System.Threading.Tasks;
using AlibabaCloud.TairSDK.Util;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairRoaring
{
    public class TairRoaringAsync
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairRoaringAsync(ConnectionMultiplexer conn, int num = 0)
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
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<RedisResult> trsetbit(string key, long offset, string value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRSETBIT, Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(offset.ToString()), Encoding.UTF8.GetBytes(value));
            return obj;
        }

        public Task<RedisResult> trsetbit(string key, long offset, long value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRSETBIT, Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(offset.ToString()), Encoding.UTF8.GetBytes(value.ToString()));
            return obj;
        }

        public Task<RedisResult> trsetbit(byte[] key, long offset, byte[] value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRSETBIT, key,
                Encoding.UTF8.GetBytes(offset.ToString()), value);
            return obj;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public Task<RedisResult> trsetbits(string key, params long[] fields)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRSETBITS,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(key), RoaringHelper.longTobyte(fields)));
            return obj;
        }

        public Task<RedisResult> trsetbits(byte[] key, params long[] fields)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRSETBITS,
                JoinParameters.joinParameters(key, RoaringHelper.longTobyte(fields)));
            return obj;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public Task<RedisResult> trclearbits(string key, params long[] fields)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRCLEARBITS,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(key), RoaringHelper.longTobyte(fields)));
            return obj;
        }

        public Task<RedisResult> trclearbits(byte[] key, params long[] fields)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRCLEARBITS,
                JoinParameters.joinParameters(key, RoaringHelper.longTobyte(fields)));
            return obj;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<RedisResult> trsetrange(string key, long start, long end)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRSETRANGE, key, start.ToString(), end.ToString());
            return obj;
        }

        public Task<RedisResult> trsetrange(byte[] key, long start, long end)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRSETRANGE, key, Encoding.UTF8.GetBytes(start.ToString()),
                Encoding.UTF8.GetBytes(end.ToString()));
            return obj;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<RedisResult> trappendbitarray(string key, long offset, string value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRAPPENDBITARRAY, key, offset.ToString(), value);
            return obj;
        }

        public Task<RedisResult> trappendbitarray(string key, long offset, byte[] value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRAPPENDBITARRAY, Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(offset.ToString(), value));
            return obj;
        }

        public Task<RedisResult> trappendbitarray(byte[] key, long offset, byte[] value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRAPPENDBITARRAY, key,
                Encoding.UTF8.GetBytes(offset.ToString(), value));
            return obj;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<RedisResult> trfliprange(string key, long start, long end)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRFLIPRANGE, key, start.ToString(), end.ToString());
            return obj;
        }

        public Task<RedisResult> trfliprange(byte[] key, long start, long end)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRFLIPRANGE, key, Encoding.UTF8.GetBytes(start.ToString()),
                Encoding.UTF8.GetBytes(end.ToString()));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public Task<RedisResult> trappendintarray(string key, params long[] fields)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRAPPENDINTARRAY,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(key), RoaringHelper.longTobyte(fields)));
            return obj;
        }

        public Task<RedisResult> trappendintarray(byte[] key, params long[] fields)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRAPPENDINTARRAY,
                JoinParameters.joinParameters(key, RoaringHelper.longTobyte(fields)));
            return obj;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public Task<RedisResult> trsetintarray(string key, params long[] fields)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRSETINTARRAY,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(key), RoaringHelper.longTobyte(fields)));
            return obj;
        }

        public Task<RedisResult> trsetintarray(byte[] key, params long[] fields)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRSETINTARRAY,
                JoinParameters.joinParameters(key, RoaringHelper.longTobyte(fields)));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<RedisResult> trsetbitarray(string key, string value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRSETBITARRAY, key, value);
            return obj;
        }

        public Task<RedisResult> trsetbitarray(byte[] key, byte[] value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRSETBITARRAY, key, value);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destkey"></param>
        /// <param name="operation"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public Task<RedisResult> trbitop(string destkey, string operation, params string[] keys)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRBITOP,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(destkey), Encoding.UTF8.GetBytes(operation),
                    RoaringHelper.encodemany(keys)));
            return obj;
        }

        public Task<RedisResult> trbitop(byte[] destkey, byte[] operation, params byte[][] keys)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRBITOP,
                JoinParameters.joinParameters(destkey, operation, keys));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public Task<RedisResult> trbitopcard(string operation, params string[] keys)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRBITOPCARD,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(operation), RoaringHelper.encodemany(keys)));
            return obj;
        }

        public Task<RedisResult> trbitopcard(byte[] operation, params byte[][] keys)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRBITOPCARD,
                JoinParameters.joinParameters(operation, keys));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<RedisResult> troptimize(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TROPTIMIZE, key);
            return obj;
        }

        public Task<RedisResult> troptimize(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TROPTIMIZE, key);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public Task<RedisResult> trgetbit(string key, long offset)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRGETBIT, key, offset.ToString());
            return obj;
        }

        public Task<RedisResult> trgetbit(byte[] key, long offset)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRGETBIT, key, Encoding.UTF8.GetBytes(offset.ToString()));
            return obj;
        }

        public Task<RedisResult> trgetbit(string key, params long[] fields)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRGETBITS,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(key), RoaringHelper.longTobyte(fields)));
            return obj;
        }

        public Task<RedisResult> trgetbit(byte[] key, params long[] fields)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRGETBITS,
                JoinParameters.joinParameters(key, RoaringHelper.longTobyte(fields)));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<RedisResult> trbitcount(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRBITCOUNT, key);
            return obj;
        }

        public Task<RedisResult> trbitcount(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRBITCOUNT, key);
            return obj;
        }

        public Task<RedisResult> trbicount(string key, long start, long end)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRBITCOUNT, key, start.ToString(), end.ToString());
            return obj;
        }

        public Task<RedisResult> trbitcount(byte[] key, long start, long end)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRBITCOUNT, key, Encoding.UTF8.GetBytes(start.ToString()),
                Encoding.UTF8.GetBytes(end.ToString()));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<RedisResult> trbitpos(string key, string value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRBITPOS, key, value);
            return obj;
        }

        public Task<RedisResult> trbitpos(string key, long value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRBITPOS, key, value.ToString());
            return obj;
        }

        public Task<RedisResult> trbitpos(byte[] key, byte[] value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRBITPOS, key, value);
            return obj;
        }

        public Task<RedisResult> trbitpos(string key, string value, long count)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRBITPOS, key, value, count.ToString());
            return obj;
        }

        public Task<RedisResult> trbitpos(string key, long value, long count)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRBITPOS, key, value.ToString(), count.ToString());
            return obj;
        }

        public Task<RedisResult> trbitpos(byte[] key, byte[] value, byte[] count)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRBITPOS, key, value, count);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cursor"></param>
        /// <returns></returns>
        public Task<RedisResult> trscan(string key, long cursor)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRSCAN, key, cursor);
            return obj;
        }

        public Task<RedisResult> trscan(string key, long cursor, long count)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRSCAN, key, cursor.ToString(), "COUNT", count.ToString());
            return obj;
        }

        public Task<RedisResult> trscan(byte[] key, byte[] cursor)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRSCAN, key, cursor);
            return obj;
        }

        public Task<RedisResult> trscan(byte[] key, byte[] cursor, byte[] count)
        {
            var obj = getRedis()
                .ExecuteAsync(ModuleCommand.TRSCAN, key, cursor, Encoding.UTF8.GetBytes("COUNT"), count);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<RedisResult> trrange(string key, long start, long end)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRRANGE, key, start.ToString(), end.ToString());
            return obj;
        }

        public Task<RedisResult> trrange(byte[] key, long start, long end)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRRANGE, key,
                Encoding.UTF8.GetBytes(start.ToString(), Encoding.UTF8.GetBytes(end.ToString())));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<RedisResult> trrangebitarray(string key, long start, long end)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRRANGEBITARRAY, key, start.ToString(), end.ToString());
            return obj;
        }

        public Task<RedisResult> trrangebitarray(byte[] key, long start, long end)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRRANGEBITARRAY, key,
                Encoding.UTF8.GetBytes(start.ToString()),
                Encoding.UTF8.GetBytes(end.ToString()));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<RedisResult> trmin(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRMIN, key);
            return obj;
        }

        public Task<RedisResult> trmin(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRMIN, key);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<RedisResult> trmax(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRMAX, key);
            return obj;
        }

        public Task<RedisResult> trmax(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRMAX, key);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public Task<RedisResult> trstat(string key, bool json)
        {
            if (json)
            {
                var obj_json = getRedis().ExecuteAsync(ModuleCommand.TRSTAT, key, "JSON");
                return obj_json;
            }

            var obj = getRedis().ExecuteAsync(ModuleCommand.TRSTAT, key);
            return obj;
        }

        public Task<RedisResult> trstat(byte[] key, bool json)
        {
            if (json)
            {
                var obj_json = getRedis().ExecuteAsync(ModuleCommand.TRSTAT, key, Encoding.UTF8.GetBytes("JSON"));
                return obj_json;
            }

            var obj = getRedis().ExecuteAsync(ModuleCommand.TRSTAT, key);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        public Task<RedisResult> trjaccard(string key1, string key2)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRJACCARD, key1, key2);
            return obj;
        }

        public Task<RedisResult> trjaccard(byte[] key1, byte[] key2)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRJACCARD, key1, key2);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        public Task<RedisResult> trcontains(string key1, string key2)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRCONTAINS, key1, key2);
            return obj;
        }

        public Task<RedisResult> trcontains(byte[] key1, byte[] key2)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRCONTAINS, key1, key2);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public Task<RedisResult> trrank(string key, long offset)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRRANK, key, offset.ToString());
            return obj;
        }

        public Task<RedisResult> trrank(byte[] key, byte[] offset)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TRRANK, key, offset);
            return obj;
        }
    }
}