using System.Collections.Generic;
using System.Text;
using AlibabaCloud.TairSDK.TairCpc.Param;
using AlibabaCloud.TairSDK.TairCpc.Result;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairCpc
{
    public class TairCpc
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairCpc(ConnectionMultiplexer conn, int num = 0)
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
        public double cpcEstimate(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCESTIMATE, key);
            return ResultHelper.Double(obj);
        }

        public double cpcEstimate(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCESTIMATE, key);
            return ResultHelper.Double(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string cpcUpdate(string key, string item)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCUPDATE, key, item);
            return ResultHelper.String(obj);
        }

        public string cpcUpdate(byte[] key, byte[] item)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCUPDATE, key, item);
            return ResultHelper.String(obj);
        }

        public string cpcUpdate(string key, string item, CpcUpdateParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCUPDATE,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(item)));
            return ResultHelper.String(obj);
        }

        public string cpcUpdate(byte[] key, byte[] item, CpcUpdateParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCUPDATE, param.getByteParams(key, item));
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public double cpcUpdate2Est(string key, string item)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCUPDATE2EST, key, item);
            return ResultHelper.Double(obj);
        }

        public double cpcUpdate2Est(byte[] key, byte[] item)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCUPDATE2EST, key, item);
            return ResultHelper.Double(obj);
        }

        public double cpcUpdate2Est(string key, string item, CpcUpdateParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCUPDATE2EST,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(item)));
            return ResultHelper.Double(obj);
        }

        public double cpcUpdate2Est(byte[] key, byte[] item, CpcUpdateParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCUPDATE2EST, param.getByteParams(key, item));
            return ResultHelper.Double(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Update2JudResult cpcUpdate2Jud(string key, string item)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCUPDATE2JUD, key, item);
            return CpcHelper.UpdateResult(obj);
        }

        public Update2JudResult CpcUpdate2Jud(byte[] key, byte[] item)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCUPDATE2JUD, key, item);
            return CpcHelper.UpdateResult(obj);
        }

        public Update2JudResult cpcUpdate2Jud(string key, string item, CpcUpdateParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCUPDATE2JUD,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(item)));
            return CpcHelper.UpdateResult(obj);
        }

        public Update2JudResult CpcUpdate2Jud(byte[] key, byte[] item, CpcUpdateParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCUPDATE2JUD, param.getByteParams(key, item));
            return CpcHelper.UpdateResult(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timestamp"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string cpcArrayUpdate(string key, long timestamp, string item)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYUPDATE, key, timestamp.ToString(), item);
            return ResultHelper.String(obj);
        }

        public string cpcArrayUpdate(byte[] key, long timestamp, byte[] item)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYUPDATE, key,
                Encoding.UTF8.GetBytes(timestamp.ToString()), item);
            return ResultHelper.String(obj);
        }

        public string cpcArrayUpdate(string key, long timestamp, string item, CpcUpdateParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYUPDATE,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(timestamp.ToString()),
                    Encoding.UTF8.GetBytes(item)));
            return ResultHelper.String(obj);
        }

        public string cpcArrayUpdate(byte[] key, long timestamp, byte[] item, CpcUpdateParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYUPDATE,
                param.getByteParams(key, Encoding.UTF8.GetBytes(timestamp.ToString()), item));
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public double cpcArrayEstimate(string key, long timestamp)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYESTIMATE, key, timestamp);
            return ResultHelper.Double(obj);
        }

        public double cpcArrayEstimate(byte[] key, long timestamp)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYESTIMATE, key,
                Encoding.UTF8.GetBytes(timestamp.ToString()));
            return ResultHelper.Double(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public List<double> cpcArrayEstimateRange(string key, long starttime, long endtime)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYESTIMATERANGE, key, starttime, endtime);
            return CpcHelper.ListDouble(obj);
        }

        public List<double> cpcArrayEstimateRange(byte[] key, long starttime, long endtime)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYESTIMATERANGE, key,
                Encoding.UTF8.GetBytes(starttime.ToString()), Encoding.UTF8.GetBytes(endtime.ToString()));
            return CpcHelper.ListDouble(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timestamp"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public double cpcArrayEstimateRangeMerge(string key, long timestamp, long range)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYESTIMATERANGEMERGE, key, timestamp.ToString(),
                range.ToString());
            return ResultHelper.Double(obj);
        }

        public double cpcArrayEstimateRangeMerge(byte[] key, long timestamp, long range)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYESTIMATERANGEMERGE, key,
                Encoding.UTF8.GetBytes(timestamp.ToString()), Encoding.UTF8.GetBytes(range.ToString()));
            return ResultHelper.Double(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timestamp"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public double cpcArrayUpdate2Est(string key, long timestamp, string item)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYUPDATE2EST, key, timestamp.ToString(), item);
            return ResultHelper.Double(obj);
        }

        public double cpcArrayUpdate2Est(byte[] key, long timestamp, byte[] item)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYUPDATE2EST, key,
                Encoding.UTF8.GetBytes(timestamp.ToString()), item);
            return ResultHelper.Double(obj);
        }

        public double cpcArrayUpdate2Est(string key, long timestamp, string item, CpcUpdateParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYUPDATE2EST,
                param.getByteParams(Encoding.UTF8.GetBytes(key),
                    Encoding.UTF8.GetBytes(timestamp.ToString()), Encoding.UTF8.GetBytes(item)));
            return ResultHelper.Double(obj);
        }

        public double cpcArrayUpdate2Est(byte[] key, long timestamp, byte[] item, CpcUpdateParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYUPDATE2EST,
                param.getByteParams(key, Encoding.UTF8.GetBytes(timestamp.ToString()), item));
            return ResultHelper.Double(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timestamp"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Update2JudResult cpcArrayUpdate2Jud(string key, long timestamp, string item)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYUPDATE2JUD, key, timestamp.ToString(), item);
            return CpcHelper.UpdateResult(obj);
        }

        public Update2JudResult cpcArrayUpdate2Jud(byte[] key, long timestamp, byte[] item)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYUPDATE2JUD, key,
                Encoding.UTF8.GetBytes(timestamp.ToString()), item);
            return CpcHelper.UpdateResult(obj);
        }

        public Update2JudResult cpcArrayUpdate2Jud(string key, long timestamp, string item, CpcUpdateParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYUPDATE2JUD,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(timestamp.ToString()),
                    Encoding.UTF8.GetBytes(item)));
            return CpcHelper.UpdateResult(obj);
        }

        public Update2JudResult cpcArrayUpdate2Jud(byte[] key, long timestamp, byte[] item, CpcUpdateParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.CPCARRAYUPDATE2JUD,
                param.getByteParams(key, Encoding.UTF8.GetBytes(timestamp.ToString()), item));
            return CpcHelper.UpdateResult(obj);
        }
    }
}