using System.Text;
using AlibabaCloud.TairSDK.TairString.Param;
using AlibabaCloud.TairSDK.TairString.Result;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairString
{
    public class TairString
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairString(ConnectionMultiplexer conn, int num = 0)
        {
            _conn = conn;
            _redis = _conn.GetDatabase(num);
        }

        private IDatabase getRedis()
        {
            return _redis;
        }

        /// <summary>
        /// Compare And Set
        /// </summary>
        /// <param name="key"></param>
        /// <param name="oldvalue"></param>
        /// <param name="newvalue"></param>
        /// <returns>Success:1; Not exist:-1; Fail:0</returns>
        public long cas(string key, string oldvalue, string newvalue)
        {
            return cas(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(oldvalue), Encoding.UTF8.GetBytes(newvalue));
        }

        public long cas(byte[] key, byte[] oldvalue, byte[] newvalue)
        {
            var obj = getRedis().Execute(ModuleCommand.CAS, key, oldvalue, newvalue);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Compare And Set.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="oldvalue"></param>
        /// <param name="newvalue"></param>
        /// <param name="param">the params: [EX time] [EXAT time] [PX time] [PXAT time]
        /// `EX` - Set expire time (seconds)
        /// `EXAT` - Set expire time as a UNIX timestamp (seconds)
        /// `PX` - Set expire time (milliseconds)
        /// `PXAT` - Set expire time as a UNIX timestamp (milliseconds)
        /// <returns>Success: 1; Not exist: -1; Fail: 0.</returns>
        public long cas(string key, string oldvalue, string newvalue, CasParams param)
        {
            return cas(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(oldvalue), Encoding.UTF8.GetBytes(newvalue),
                param);
        }

        public long cas(byte[] key, byte[] oldvalue, byte[] newvalue, CasParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.CAS, param.getByteParams(key, oldvalue, newvalue));
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Compare And Delete
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>Success: 1; Not exist: -1; Fail: 0.</returns>
        public long cad(string key, string value)
        {
            return cad(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(value));
        }

        public long cad(byte[] key, byte[] value)
        {
            var obj = getRedis().Execute(ModuleCommand.CAD, key, value);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Get the value of the key.
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>List, Success: [value, version]; Fail: error.</returns>
        public ExgetResult<string> exget(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.EXGET, key);
            return StringHelper.GetResultString(obj);
        }

        public ExgetResult<byte[]> exget(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.EXGET, key);
            return StringHelper.GetResultByte(obj);
        }

        /// <summary>
        /// Get the value and flags of the key.
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>List, Success: [value, version, flags]; Fail: error.</returns>
        public ExgetResult<string> exgetFlags(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.EXGET, key, "WITHFLAGS");

            return StringHelper.GetFlagString(obj);
        }

        public ExgetResult<byte[]> exgetFlags(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.EXGET, key, Encoding.UTF8.GetBytes("WITHFLAGS"));
            return StringHelper.GetResultByte(obj);
        }


        /// <summary>
        /// Set the string value of the key.
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="value">the value</param>
        /// <returns>Success: OK; Fail: error.</returns>
        public string exset(string key, string value)
        {
            var obj = getRedis().Execute(ModuleCommand.EXSET, key, value);
            return ResultHelper.String(obj);
        }

        public string exset(byte[] key, byte[] value)
        {
            var obj = getRedis().Execute(ModuleCommand.EXSET, key, value);
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// Set the string value of the key.
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="value">the value</param>
        /// <param name="param">
        /// the params: [EX time] [EXAT time] [PX time] [PXAT time] [NX|XX] [VER version | ABS version]
        /// `EX` - Set expire time (seconds)
        /// `EXAT` - Set expire time as a UNIX timestamp (seconds)
        /// `PX` - Set expire time (milliseconds)
        /// `PXAT` - Set expire time as a UNIX timestamp (milliseconds)
        /// `NX` - only set the key if it does not already exists
        /// `XX` - only set the key if it already exists
        /// `VER` - Set if version matched or not exist
        /// `ABS` - Set with abs version
        /// `FLAGS` - MEMCACHED flags
        /// </param>
        /// <returns>Success: OK; Fail: error.</returns>
        public string exset(string key, string value, ExsetParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXSET, param.getByteParams(key, value));
            return ResultHelper.String(obj);
        }

        public string exset(byte[] key, byte[] value, ExsetParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXSET, param.getByteParams(key, value));
            return ResultHelper.String(obj);
        }

        public long exsetVersion(string key, string value, ExsetParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXSET,
                param.getByteParams(key, value, "WITHVERSION"));
            return ResultHelper.Long(obj);
        }

        public long exsetVersion(byte[] key, byte[] value, ExsetParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXSET,
                param.getByteParams(key, value, Encoding.UTF8.GetBytes("WITHVERSION")));
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Set the version for the key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="version"></param>
        /// <returns>Success: 1; Not exist: 0; Fail: error.</returns>
        public long exsetver(string key, long version)
        {
            return exsetver(Encoding.UTF8.GetBytes(key), version);
        }

        public long exsetver(byte[] key, long version)
        {
            var obj = getRedis().Execute(ModuleCommand.EXSETVER, key, version);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Increment the integer value of the key by the given number.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="incr"></param>
        /// <returns>Success: value of key; Fail: error.</returns>
        public long exincrBy(string key, long incr)
        {
            return exincrBy(Encoding.UTF8.GetBytes(key), incr);
        }

        public long exincrBy(byte[] key, long incr)
        {
            var obj = getRedis().Execute(ModuleCommand.EXINCRBY, key, incr);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Increment the integer value of the key by the given number.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="incr"></param>
        /// <param name="param">
        /// the params: [EX time] [EXAT time] [PX time] [PXAT time] [VER version | ABS version][MIN minval] [MAX maxval]
        /// `EX` - Set expire time (seconds)
        /// `EXAT` - Set expire time as a UNIX timestamp (seconds)
        /// `PX` - Set expire time (milliseconds)
        /// `PXAT` - Set expire time as a UNIX timestamp (milliseconds)
        /// `VER` - Set if version matched or not exist
        /// `ABS` - Set with abs version
        /// `MIN` - Set the min value for the value.
        /// `MAX` - Set the max value for the value.
        /// </param>
        /// <returns>Success: value of key; Fail: error.</returns>
        public long exincrBy(string key, long incr, ExincrbyParams param)
        {
            return exincrBy(Encoding.UTF8.GetBytes(key), incr, param);
        }

        public long exincrBy(byte[] key, long incr, ExincrbyParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXINCRBY,
                param.getByteParams(key, Encoding.UTF8.GetBytes(incr.ToString())));
            return ResultHelper.Long(obj);
        }

        public ExincrbyVersionResult exincrByVersion(string key, long incr, ExincrbyParams param)
        {
            return exincrByVersion(Encoding.UTF8.GetBytes(key), incr, param);
        }

        public ExincrbyVersionResult exincrByVersion(byte[] key, long incr, ExincrbyParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXINCRBY,
                param.getByteParams(key, Encoding.UTF8.GetBytes(incr.ToString())), "WITHVERSION");
            return StringHelper.IncrbyVersionResult(obj);
        }


        /// <summary>
        /// Increment the float value of the key by the given number.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="incr"></param>
        /// <returns>Success: value of key; Fail: error.</returns>
        public double exincrByFloat(string key, double incr)
        {
            return exincrByFloat(Encoding.UTF8.GetBytes(key), incr);
        }

        public double exincrByFloat(byte[] key, double incr)
        {
            var obj = getRedis().Execute(ModuleCommand.EXINCRBYFLOAT, key, Encoding.UTF8.GetBytes(incr.ToString()));
            return ResultHelper.Double(obj);
        }


        /// <summary>
        /// Increment the float value of the key by the given number.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="incr"></param>
        /// <param name="param">
        /// the params: [EX time] [EXAT time] [PX time] [PXAT time] [VER version | ABS version][MIN minval] [MAX maxval]
        /// `EX` - Set expire time (seconds)
        /// `EXAT` - Set expire time as a UNIX timestamp (seconds)
        /// `PX` - Set expire time (milliseconds)
        /// `PXAT` - Set expire time as a UNIX timestamp (milliseconds)
        /// `VER` - Set if version matched or not exist
        /// `ABS` - Set with abs version
        /// `MIN` - Set the min value for the value.
        /// `MAX` - Set the max value for the value.
        /// </param>
        /// <returns>Success: value of key; Fail: error.</returns>
        public double exincrByFloat(string key, double incr, ExincrbyFloatParams param)
        {
            return exincrByFloat(Encoding.UTF8.GetBytes(key), incr, param);
        }

        public double exincrByFloat(byte[] key, double incr, ExincrbyFloatParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXINCRBYFLOAT,
                param.getByteParams(key, Encoding.UTF8.GetBytes(incr.ToString())));
            return ResultHelper.Double(obj);
        }


        /// <summary>
        /// Compare And Set.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="newvalue"></param>
        /// <param name="version"></param>
        /// <returns>List, Success: ["OK", "", version]; Fail: ["Err", value, version].</returns>
        public ExcasResult<string> excas(string key, string newvalue, long version)
        {
            var obj = getRedis().Execute(ModuleCommand.EXCAS, key, newvalue, version.ToString());
            return StringHelper.CasResultString(obj);
        }

        public ExcasResult<byte[]> excas(byte[] key, byte[] newvalue, long version)
        {
            var obj = getRedis().Execute(ModuleCommand.EXCAS, key, newvalue, version);
            return StringHelper.CasResultByte(obj);
        }

        /// <summary>
        /// Compare And Delete.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="version"></param>
        /// <returns>Success: 1; Not exist: -1; Fail: 0.</returns>
        public long excad(string key, long version)
        {
            return excad(Encoding.UTF8.GetBytes(key), version);
        }

        public long excad(byte[] key, long version)
        {
            var obj = getRedis().Execute(ModuleCommand.EXCAD, key, Encoding.UTF8.GetBytes(version.ToString()));
            return ResultHelper.Long(obj);
        }
    }
}