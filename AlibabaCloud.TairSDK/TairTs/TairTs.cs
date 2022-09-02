using System.Collections.Generic;
using System.Text;
using AlibabaCloud.TairSDK.TairTs.Param;
using AlibabaCloud.TairSDK.TairTs.Result;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairTs
{
    public class TairTs
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairTs(ConnectionMultiplexer conn, int num = 0)
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
        public string extpcreate(string pkey)
        {
            var obj = getRedis().Execute(ModuleCommand.TSPCREATE, pkey);
            return ResultHelper.String(obj);
        }

        public string extpcreate(byte[] pkey)
        {
            var obj = getRedis().Execute(ModuleCommand.TSPCREATE, pkey);
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skey"></param>
        /// <returns></returns>
        public string extscreate(string pkey, string skey)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSCREATE, pkey, skey);
            return ResultHelper.String(obj);
        }

        public string extscreate(byte[] pkey, byte[] skey)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSCREATE, pkey, skey);
            return ResultHelper.String(obj);
        }

        public string extscreate(string pkey, string skey, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSCREATE, param.getByteParams(pkey, skey));
            return ResultHelper.String(obj);
        }

        public string extscreate(byte[] pkey, byte[] skey, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSCREATE, param.getByteParams(pkey, skey));
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skey"></param>
        /// <param name="ts"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string extsadd(string pkey, string skey, string ts, double value)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSADD, pkey, skey, ts, value);
            return ResultHelper.String(obj);
        }

        public string extsadd(byte[] pkey, byte[] skey, byte[] ts, double value)
        {
            var obj = getRedis()
                .Execute(ModuleCommand.TSSADD, pkey, skey, ts, Encoding.UTF8.GetBytes(value.ToString()));
            return ResultHelper.String(obj);
        }

        public string extsadd(string pkey, string skey, string ts, double value, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSADD, param.getByteParams(pkey, skey, ts, value.ToString()));
            return ResultHelper.String(obj);
        }

        public string extsadd(byte[] pkey, byte[] skey, byte[] ts, double value, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSADD,
                param.getByteParams(pkey, skey, ts, Encoding.UTF8.GetBytes(value.ToString())));
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skeys"></param>
        /// <returns></returns>
        public List<string> extsmadd(string pkey, List<ExtsDataPoint<string>> skeys)
        {
            ExtsMaddParams addList = new ExtsMaddParams();
            var obj = getRedis().Execute(ModuleCommand.TSSMADD, addList.getByteParams(pkey, skeys));
            return ResultHelper.ListString(obj);
        }

        public List<string> extsmadd(byte[] pkey, List<ExtsDataPoint<byte[]>> skeys)
        {
            ExtsMaddParams addList = new ExtsMaddParams();
            var obj = getRedis().Execute(ModuleCommand.TSSMADD, addList.getByteParams(pkey, skeys));
            return ResultHelper.ListString(obj);
        }

        public List<string> extsmadd(string pkey, List<ExtsDataPoint<string>> skeys, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSMADD, param.getByteParams(pkey, skeys));
            return ResultHelper.ListString(obj);
        }

        public List<string> extsmadd(byte[] pkey, List<ExtsDataPoint<byte[]>> skeys, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSMADD, param.getByteParams(pkey, skeys));
            return ResultHelper.ListString(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skey"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public string extssalter(string pkey, string skey, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSALTER, param.getByteParams(pkey, skey));
            return ResultHelper.String(obj);
        }

        public string extssalter(byte[] pkey, byte[] skey, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSALTER, param.getByteParams(pkey, skey));
            return ResultHelper.String(obj);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skey"></param>
        /// <param name="ts"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string extsincr(string pkey, string skey, string ts, double value)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSINCRBY, pkey, skey, ts, value.ToString());
            return ResultHelper.String(obj);
        }

        public string extsincr(byte[] pkey, byte[] skey, byte[] ts, double value)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSINCRBY, pkey, skey, ts,
                Encoding.UTF8.GetBytes(value.ToString()));
            return ResultHelper.String(obj);
        }

        public string extsincr(string pkey, string skey, string ts, double value, ExtsAttributesParams param)
        {
            var obj = getRedis()
                .Execute(ModuleCommand.TSSINCRBY, param.getByteParams(pkey, skey, ts, value.ToString()));
            return ResultHelper.String(obj);
        }

        public string extsincr(byte[] pkey, byte[] skey, byte[] ts, double value, ExtsAttributesParams param)
        {
            var obj = getRedis()
                .Execute(ModuleCommand.TSSINCRBY,
                    param.getByteParams(pkey, skey, ts, Encoding.UTF8.GetBytes(value.ToString())));
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skeys"></param>
        /// <returns></returns>
        public List<string> extsmincr(string pkey, List<ExtsDataPoint<string>> skeys)
        {
            ExtsMaddParams addList = new ExtsMaddParams();
            var obj = getRedis().Execute(ModuleCommand.TSSMINCRBY, addList.getByteParams(pkey, skeys));
            return ResultHelper.ListString(obj);
        }

        public List<string> extsmincr(byte[] pkey, List<ExtsDataPoint<byte[]>> skeys)
        {
            ExtsMaddParams addList = new ExtsMaddParams();
            var obj = getRedis().Execute(ModuleCommand.TSSMINCRBY, addList.getByteParams(pkey, skeys));
            return ResultHelper.ListString(obj);
        }

        public List<string> extsmincr(string pkey, List<ExtsDataPoint<string>> skeys, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSMINCRBY, param.getByteParams(pkey, skeys));
            return ResultHelper.ListString(obj);
        }

        public List<string> extsmincr(byte[] pkey, List<ExtsDataPoint<byte[]>> skeys, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSMINCRBY, param.getByteParams(pkey, skeys));
            return ResultHelper.ListString(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skey"></param>
        /// <returns></returns>
        public string extsdel(string pkey, string skey)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSDEL, pkey, skey);
            return ResultHelper.String(obj);
        }

        public string extsdel(byte[] pkey, byte[] skey)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSDEL, pkey, skey);
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skey"></param>
        /// <returns></returns>
        public ExtsDataPointResult extsget(string pkey, string skey)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSGET, pkey, skey);
            return TsHelper.GetResult(obj);
        }

        public ExtsDataPointResult extsget(byte[] pkey, byte[] skey)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSGET, pkey, skey);
            return TsHelper.GetResult(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public List<string> extsquery(string pkey, List<ExtsFilter<string>> filters)
        {
            ExtsQueryParams addList = new ExtsQueryParams();
            var obj = getRedis().Execute(ModuleCommand.TSSQUERYINDEX, addList.getByteParams(pkey, filters));
            return ResultHelper.ListString(obj);
        }

        public List<byte[]> extsquery(byte[] pkey, List<ExtsFilter<byte[]>> filters)
        {
            ExtsQueryParams addList = new ExtsQueryParams();
            var obj = getRedis().Execute(ModuleCommand.TSSQUERYINDEX, addList.getByteParams(pkey, filters));
            return ResultHelper.ListByte(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skey"></param>
        /// <param name="startTs"></param>
        /// <param name="endTs"></param>
        /// <returns></returns>
        public ExtsRangeResult extsrange(string pkey, string skey, string startTs, string endTs)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSRANGE, pkey, skey, startTs, endTs);
            return TsHelper.RangeResult(obj);
        }

        public ExtsRangeResult extsrange(byte[] pkey, byte[] skey, byte[] startTs, byte[] endTs)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSRANGE, pkey, skey, startTs, endTs);
            return TsHelper.RangeResult(obj);
        }

        public ExtsRangeResult extsrange(string pkey, string skey, string startTs, string endTs,
            ExtsAggregationParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSRANGE, param.getByteRangeParams(pkey, skey, startTs, endTs));
            return TsHelper.RangeResult(obj);
        }

        public ExtsRangeResult extsrange(byte[] pkey, byte[] skey, byte[] startTs, byte[] endTs,
            ExtsAggregationParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSRANGE, param.getByteRangeParams(pkey, skey, startTs, endTs));
            return TsHelper.RangeResult(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skeys"></param>
        /// <param name="startTs"></param>
        /// <param name="endTs"></param>
        /// <returns></returns>
        public List<ExtsMrangeResult> extsmarange(string pkey, string startTs, string endTs,
            List<ExtsFilter<string>> filters)
        {
            ExtsAggregationParams param = new ExtsAggregationParams();
            var obj = getRedis().Execute(ModuleCommand.TSSMRANGE,
                param.getByteMrangeParams(pkey, startTs, endTs, filters));
            return TsHelper.MrangeResult(obj);
        }

        public List<ExtsMrangeResult> extsmarange(byte[] pkey, byte[] startTs, byte[] endTs,
            List<ExtsFilter<byte[]>> filters)
        {
            ExtsAggregationParams param = new ExtsAggregationParams();
            var obj = getRedis().Execute(ModuleCommand.TSSMRANGE,
                param.getByteMrangeParams(pkey, startTs, endTs, filters));
            return TsHelper.MrangeResult(obj);
        }

        public List<ExtsMrangeResult> extsmarange(string pkey, string startTs, string endTs,
            ExtsAggregationParams param, List<ExtsFilter<string>> filters)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSMRANGE,
                param.getByteMrangeParams(pkey, startTs, endTs, filters));
            return TsHelper.MrangeResult(obj);
        }

        public List<ExtsMrangeResult> extsmarange(byte[] pkey, byte[] startTs, byte[] endTs,
            ExtsAggregationParams param, List<ExtsFilter<byte[]>> filters)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSMRANGE,
                param.getByteMrangeParams(pkey, startTs, endTs, filters));
            return TsHelper.MrangeResult(obj);
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
        public ExtsRangeResult extsprange(string pkey, string startTs, string endTs, string pkeyAggregationType,
            long pkeyTimeBucket, List<ExtsFilter<string>> filters)
        {
            ExtsAggregationParams param = new ExtsAggregationParams();
            var obj = getRedis().Execute(ModuleCommand.TSPRANGE,
                param.getBytePrangeParams(pkey, startTs, endTs, pkeyAggregationType, pkeyTimeBucket, filters));
            return TsHelper.RangeResult(obj);
        }

        public ExtsRangeResult extsprange(byte[] pkey, byte[] startTs, byte[] endTs, byte[] pkeyAggregationType,
            long pkeyTimeBucket, List<ExtsFilter<byte[]>> filters)
        {
            ExtsAggregationParams param = new ExtsAggregationParams();
            var obj = getRedis().Execute(ModuleCommand.TSPRANGE,
                param.getBytePrangeParams(pkey, startTs, endTs, pkeyAggregationType, pkeyTimeBucket, filters));
            return TsHelper.RangeResult(obj);
        }

        public ExtsRangeResult extsprange(string pkey, string startTs, string endTs, string pkeyAggregationType,
            long pkeyTimeBucket, ExtsAggregationParams param, List<ExtsFilter<string>> filters)
        {
            var obj = getRedis().Execute(ModuleCommand.TSPRANGE,
                param.getBytePrangeParams(pkey, startTs, endTs, pkeyAggregationType, pkeyTimeBucket, filters));
            return TsHelper.RangeResult(obj);
        }

        public ExtsRangeResult extsprange(byte[] pkey, byte[] startTs, byte[] endTs, byte[] pkeyAggregationType,
            long pkeyTimeBucket, ExtsAggregationParams param, List<ExtsFilter<byte[]>> filters)
        {
            var obj = getRedis().Execute(ModuleCommand.TSPRANGE,
                param.getBytePrangeParams(pkey, startTs, endTs, pkeyAggregationType, pkeyTimeBucket, filters));
            return TsHelper.RangeResult(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skey"></param>
        /// <param name="ts"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string extsrawmodify(string pkey, string skey, string ts, double value)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSRAWMODIFY, pkey, skey, ts, value.ToString());
            return ResultHelper.String(obj);
        }

        public string extsrawmodify(byte[] pkey, byte[] skey, byte[] ts, double value)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSRAWMODIFY, pkey, skey, ts,
                Encoding.UTF8.GetBytes(value.ToString()));
            return ResultHelper.String(obj);
        }

        public string extsrawmodify(string pkey, string skey, string ts, double value, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSRAWMODIFY,
                param.getByteParams(pkey, skey, ts, value.ToString()));
            return ResultHelper.String(obj);
        }

        public string extsrawmodify(byte[] pkey, byte[] skey, byte[] ts, double value, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSRAWMODIFY,
                param.getByteParams(pkey, skey, ts, Encoding.UTF8.GetBytes(value.ToString())));
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skeys"></param>
        /// <returns></returns>
        public List<string> extsmrawmodify(string pkey, List<ExtsDataPoint<string>> skeys)
        {
            ExtsMaddParams addList = new ExtsMaddParams();
            var obj = getRedis().Execute(ModuleCommand.TSSRAWMULTIMODIFY, addList.getByteParams(pkey, skeys));
            return ResultHelper.ListString(obj);
        }

        public List<string> extsmrawmodify(byte[] pkey, List<ExtsDataPoint<byte[]>> skeys)
        {
            ExtsMaddParams addList = new ExtsMaddParams();
            var obj = getRedis().Execute(ModuleCommand.TSSRAWMULTIMODIFY, addList.getByteParams(pkey, skeys));
            return ResultHelper.ListString(obj);
        }

        public List<string> extsmrawmodify(string pkey, List<ExtsDataPoint<string>> skeys, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSRAWMULTIMODIFY, param.getByteParams(pkey, skeys));
            return ResultHelper.ListString(obj);
        }

        public List<string> extsmrawmodify(byte[] pkey, List<ExtsDataPoint<byte[]>> skeys, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSRAWMULTIMODIFY, param.getByteParams(pkey, skeys));
            return ResultHelper.ListString(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skey"></param>
        /// <param name="ts"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string extsrawincr(string pkey, string skey, string ts, double value)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSRAWINCTBY, pkey, skey, ts, value.ToString());
            return ResultHelper.String(obj);
        }

        public string extsrawincr(byte[] pkey, byte[] skey, byte[] ts, double value)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSRAWINCTBY, pkey, skey, ts,
                Encoding.UTF8.GetBytes(value.ToString()));
            return ResultHelper.String(obj);
        }

        public string extsrawincr(string pkey, string skey, string ts, double value, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSRAWINCTBY,
                param.getByteParams(pkey, skey, ts, value.ToString()));
            return ResultHelper.String(obj);
        }

        public string extsrawincr(byte[] pkey, byte[] skey, byte[] ts, double value, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSRAWINCTBY,
                param.getByteParams(pkey, skey, ts, Encoding.UTF8.GetBytes(value.ToString())));
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pkey"></param>
        /// <param name="skeys"></param>
        /// <returns></returns>
        public List<string> extsmrawincr(string pkey, List<ExtsDataPoint<string>> skeys)
        {
            ExtsMaddParams addList = new ExtsMaddParams();
            var obj = getRedis().Execute(ModuleCommand.TSSRAWMULTIINCRBY, addList.getByteParams(pkey, skeys));
            return ResultHelper.ListString(obj);
        }

        public List<string> extsmrawincr(byte[] pkey, List<ExtsDataPoint<byte[]>> skeys)
        {
            ExtsMaddParams addList = new ExtsMaddParams();
            var obj = getRedis().Execute(ModuleCommand.TSSRAWMULTIINCRBY, addList.getByteParams(pkey, skeys));
            return ResultHelper.ListString(obj);
        }

        public List<string> extsmrawincr(string pkey, List<ExtsDataPoint<string>> skeys, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSRAWMULTIINCRBY, param.getByteParams(pkey, skeys));
            return ResultHelper.ListString(obj);
        }

        public List<string> extsmrawincr(byte[] pkey, List<ExtsDataPoint<byte[]>> skeys, ExtsAttributesParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TSSRAWMULTIINCRBY, param.getByteParams(pkey, skeys));
            return ResultHelper.ListString(obj);
        }
    }
}