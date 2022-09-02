using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AlibabaCloud.TairSDK.TairTs.Param;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairTs
{
    public class TairTsAsync
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairTsAsync(ConnectionMultiplexer conn, int num = 0)
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
        /// <param name="pkey"></param>
        /// <returns></returns>
        public Task<RedisResult> extpcreate(string pkey)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSPCREATE, pkey);
            return obj;
        }

        public Task<RedisResult> extpcreate(byte[] pkey)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSPCREATE, pkey);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skey"></param>
        /// <returns></returns>
        public Task<RedisResult> extscreate(string pkey, string skey)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSCREATE, pkey, skey);
            return obj;
        }

        public Task<RedisResult> extscreate(byte[] pkey, byte[] skey)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSCREATE, pkey, skey);
            return obj;
        }

        public Task<RedisResult> extscreate(string pkey, string skey, ExtsAttributesParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSCREATE, param.getByteParams(pkey, skey));
            return obj;
        }

        public Task<RedisResult> extscreate(byte[] pkey, byte[] skey, ExtsAttributesParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSCREATE, param.getByteParams(pkey, skey));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skey"></param>
        /// <param name="ts"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<RedisResult> extsadd(string pkey, string skey, string ts, double value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSADD, pkey, skey, ts, value);
            return obj;
        }

        public Task<RedisResult> extsadd(byte[] pkey, byte[] skey, byte[] ts, double value)
        {
            var obj = getRedis()
                .ExecuteAsync(ModuleCommand.TSSADD, pkey, skey, ts, Encoding.UTF8.GetBytes(value.ToString()));
            return obj;
        }

        public Task<RedisResult> extsadd(string pkey, string skey, string ts, double value, ExtsAttributesParams param)
        {
            var obj = getRedis()
                .ExecuteAsync(ModuleCommand.TSSADD, param.getByteParams(pkey, skey, ts, value.ToString()));
            return obj;
        }

        public Task<RedisResult> extsadd(byte[] pkey, byte[] skey, byte[] ts, double value, ExtsAttributesParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSADD,
                param.getByteParams(pkey, skey, ts, Encoding.UTF8.GetBytes(value.ToString())));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skeys"></param>
        /// <returns></returns>
        public Task<RedisResult> extsmadd(string pkey, List<ExtsDataPoint<string>> skeys)
        {
            ExtsMaddParams addList = new ExtsMaddParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSMADD, addList.getByteParams(pkey, skeys));
            return obj;
        }

        public Task<RedisResult> extsmadd(byte[] pkey, List<ExtsDataPoint<byte[]>> skeys)
        {
            ExtsMaddParams addList = new ExtsMaddParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSMADD, addList.getByteParams(pkey, skeys));
            return obj;
        }

        public Task<RedisResult> extsmadd(string pkey, List<ExtsDataPoint<string>> skeys, ExtsAttributesParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSMADD, param.getByteParams(pkey, skeys));
            return obj;
        }

        public Task<RedisResult> extsmadd(byte[] pkey, List<ExtsDataPoint<byte[]>> skeys, ExtsAttributesParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSMADD, param.getByteParams(pkey, skeys));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skey"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Task<RedisResult> extssalter(string pkey, string skey, ExtsAttributesParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSALTER, param.getByteParams(pkey, skey));
            return obj;
        }

        public Task<RedisResult> extssalter(byte[] pkey, byte[] skey, ExtsAttributesParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSALTER, param.getByteParams(pkey, skey));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skey"></param>
        /// <param name="ts"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<RedisResult> extsincr(string pkey, string skey, string ts, double value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSINCRBY, pkey, skey, ts, value.ToString());
            return obj;
        }

        public Task<RedisResult> extsincr(byte[] pkey, byte[] skey, byte[] ts, double value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSINCRBY, pkey, skey, ts,
                Encoding.UTF8.GetBytes(value.ToString()));
            return obj;
        }

        public Task<RedisResult> extsincr(string pkey, string skey, string ts, double value, ExtsAttributesParams param)
        {
            var obj = getRedis()
                .ExecuteAsync(ModuleCommand.TSSINCRBY, param.getByteParams(pkey, skey, ts, value.ToString()));
            return obj;
        }

        public Task<RedisResult> extsincr(byte[] pkey, byte[] skey, byte[] ts, double value, ExtsAttributesParams param)
        {
            var obj = getRedis()
                .ExecuteAsync(ModuleCommand.TSSINCRBY,
                    param.getByteParams(pkey, skey, ts, Encoding.UTF8.GetBytes(value.ToString())));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skeys"></param>
        /// <returns></returns>
        public Task<RedisResult> extsmincr(string pkey, List<ExtsDataPoint<string>> skeys)
        {
            ExtsMaddParams addList = new ExtsMaddParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSMINCRBY, addList.getByteParams(pkey, skeys));
            return obj;
        }

        public Task<RedisResult> extsmincr(byte[] pkey, List<ExtsDataPoint<byte[]>> skeys)
        {
            ExtsMaddParams addList = new ExtsMaddParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSMINCRBY, addList.getByteParams(pkey, skeys));
            return obj;
        }

        public Task<RedisResult> extsmincr(string pkey, List<ExtsDataPoint<string>> skeys, ExtsAttributesParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSMINCRBY, param.getByteParams(pkey, skeys));
            return obj;
        }

        public Task<RedisResult> extsmincr(byte[] pkey, List<ExtsDataPoint<byte[]>> skeys, ExtsAttributesParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSMINCRBY, param.getByteParams(pkey, skeys));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skey"></param>
        /// <returns></returns>
        public Task<RedisResult> extsdel(string pkey, string skey)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSDEL, pkey, skey);
            return obj;
        }

        public Task<RedisResult> extsdel(byte[] pkey, byte[] skey)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSDEL, pkey, skey);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skey"></param>
        /// <returns></returns>
        public Task<RedisResult> extsget(string pkey, string skey)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSGET, pkey, skey);
            return obj;
        }

        public Task<RedisResult> extsget(byte[] pkey, byte[] skey)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSGET, pkey, skey);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public Task<RedisResult> extsquery(string pkey, List<ExtsFilter<string>> filters)
        {
            ExtsQueryParams addList = new ExtsQueryParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSQUERYINDEX, addList.getByteParams(pkey, filters));
            return obj;
        }

        public Task<RedisResult> extsquery(byte[] pkey, List<ExtsFilter<byte[]>> filters)
        {
            ExtsQueryParams addList = new ExtsQueryParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSQUERYINDEX, addList.getByteParams(pkey, filters));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skey"></param>
        /// <param name="startTs"></param>
        /// <param name="endTs"></param>
        /// <returns></returns>
        public Task<RedisResult> extsrange(string pkey, string skey, string startTs, string endTs)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRANGE, pkey, skey, startTs, endTs);
            return obj;
        }

        public Task<RedisResult> extsrange(byte[] pkey, byte[] skey, byte[] startTs, byte[] endTs)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRANGE, pkey, skey, startTs, endTs);
            return obj;
        }

        public Task<RedisResult> extsrange(string pkey, string skey, string startTs, string endTs,
            ExtsAggregationParams param)
        {
            var obj = getRedis()
                .ExecuteAsync(ModuleCommand.TSSRANGE, param.getByteRangeParams(pkey, skey, startTs, endTs));
            return obj;
        }

        public Task<RedisResult> extsrange(byte[] pkey, byte[] skey, byte[] startTs, byte[] endTs,
            ExtsAggregationParams param)
        {
            var obj = getRedis()
                .ExecuteAsync(ModuleCommand.TSSRANGE, param.getByteRangeParams(pkey, skey, startTs, endTs));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="startTs"></param>
        /// <param name="endTs"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public Task<RedisResult> extsmarange(string pkey, string startTs, string endTs,
            List<ExtsFilter<string>> filters)
        {
            ExtsAggregationParams param = new ExtsAggregationParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSMRANGE,
                param.getByteMrangeParams(pkey, startTs, endTs, filters));
            return obj;
        }

        public Task<RedisResult> extsmarange(byte[] pkey, byte[] startTs, byte[] endTs,
            List<ExtsFilter<byte[]>> filters)
        {
            ExtsAggregationParams param = new ExtsAggregationParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSMRANGE,
                param.getByteMrangeParams(pkey, startTs, endTs, filters));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="startTs"></param>
        /// <param name="endTs"></param>
        /// <param name="pkeyAggregationType"></param>
        /// <param name="pkeyTimeBucket"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public Task<RedisResult> extsprange(string pkey, string startTs, string endTs, string pkeyAggregationType,
            long pkeyTimeBucket, List<ExtsFilter<string>> filters)
        {
            ExtsAggregationParams param = new ExtsAggregationParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSPRANGE,
                param.getBytePrangeParams(pkey, startTs, endTs, pkeyAggregationType, pkeyTimeBucket, filters));
            return obj;
        }

        public Task<RedisResult> extsprange(byte[] pkey, byte[] startTs, byte[] endTs, byte[] pkeyAggregationType,
            long pkeyTimeBucket, List<ExtsFilter<byte[]>> filters)
        {
            ExtsAggregationParams param = new ExtsAggregationParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSPRANGE,
                param.getBytePrangeParams(pkey, startTs, endTs, pkeyAggregationType, pkeyTimeBucket, filters));
            return obj;
        }

        public Task<RedisResult> extsrawmodify(string pkey, string skey, string ts, double value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRAWMODIFY, pkey, skey, ts, value.ToString());
            return obj;
        }

        public Task<RedisResult> extsrawmodify(byte[] pkey, byte[] skey, byte[] ts, double value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRAWMODIFY, pkey, skey, ts,
                Encoding.UTF8.GetBytes(value.ToString()));
            return obj;
        }

        public Task<RedisResult> extsrawmodify(string pkey, string skey, string ts, double value,
            ExtsAttributesParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRAWMODIFY,
                param.getByteParams(pkey, skey, ts, value.ToString()));
            return obj;
        }

        public Task<RedisResult> extsrawmodify(byte[] pkey, byte[] skey, byte[] ts, double value,
            ExtsAttributesParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRAWMODIFY,
                param.getByteParams(pkey, skey, ts, Encoding.UTF8.GetBytes(value.ToString())));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skeys"></param>
        /// <returns></returns>
        public Task<RedisResult> extsmrawmodify(string pkey, List<ExtsDataPoint<string>> skeys)
        {
            ExtsMaddParams addList = new ExtsMaddParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRAWMULTIMODIFY, addList.getByteParams(pkey, skeys));
            return obj;
        }

        public Task<RedisResult> extsmrawmodify(byte[] pkey, List<ExtsDataPoint<byte[]>> skeys)
        {
            ExtsMaddParams addList = new ExtsMaddParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRAWMULTIMODIFY, addList.getByteParams(pkey, skeys));
            return obj;
        }

        public Task<RedisResult> extsmrawmodify(string pkey, List<ExtsDataPoint<string>> skeys,
            ExtsAttributesParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRAWMULTIMODIFY, param.getByteParams(pkey, skeys));
            return obj;
        }

        public Task<RedisResult> extsmrawmodify(byte[] pkey, List<ExtsDataPoint<byte[]>> skeys,
            ExtsAttributesParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRAWMULTIMODIFY, param.getByteParams(pkey, skeys));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skey"></param>
        /// <param name="ts"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<RedisResult> extsrawincr(string pkey, string skey, string ts, double value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRAWINCTBY, pkey, skey, ts, value.ToString());
            return obj;
        }

        public Task<RedisResult> extsrawincr(byte[] pkey, byte[] skey, byte[] ts, double value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRAWINCTBY, pkey, skey, ts,
                Encoding.UTF8.GetBytes(value.ToString()));
            return obj;
        }

        public Task<RedisResult> extsrawincr(string pkey, string skey, string ts, double value,
            ExtsAttributesParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRAWINCTBY,
                param.getByteParams(pkey, skey, ts, value.ToString()));
            return obj;
        }

        public Task<RedisResult> extsrawincr(byte[] pkey, byte[] skey, byte[] ts, double value,
            ExtsAttributesParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRAWINCTBY,
                param.getByteParams(pkey, skey, ts, Encoding.UTF8.GetBytes(value.ToString())));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skeys"></param>
        /// <returns></returns>
        public Task<RedisResult> extsmrawincr(string pkey, List<ExtsDataPoint<string>> skeys)
        {
            ExtsMaddParams addList = new ExtsMaddParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRAWMULTIINCRBY, addList.getByteParams(pkey, skeys));
            return obj;
        }

        public Task<RedisResult> extsmrawincr(byte[] pkey, List<ExtsDataPoint<byte[]>> skeys)
        {
            ExtsMaddParams addList = new ExtsMaddParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRAWMULTIINCRBY, addList.getByteParams(pkey, skeys));
            return obj;
        }

        public Task<RedisResult> extsmrawincr(string pkey, List<ExtsDataPoint<string>> skeys,
            ExtsAttributesParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRAWMULTIINCRBY, param.getByteParams(pkey, skeys));
            return obj;
        }

        public Task<RedisResult> extsmrawincr(byte[] pkey, List<ExtsDataPoint<byte[]>> skeys,
            ExtsAttributesParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TSSRAWMULTIINCRBY, param.getByteParams(pkey, skeys));
            return obj;
        }
    }
}