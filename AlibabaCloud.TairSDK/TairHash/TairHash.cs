using System.Collections.Generic;
using System.Text;
using AlibabaCloud.TairSDK.TairHash.Param;
using AlibabaCloud.TairSDK.Util;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairHash
{
    public class TairHash
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairHash(ConnectionMultiplexer conn, int num = 0)
        {
            _conn = conn;
            _redis = _conn.GetDatabase(num);
        }

        private IDatabase getRedis()
        {
            return _redis;
        }

        /// <summary>
        /// Set the string value of a exhash field.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns>
        /// integer-reply specifically:
        /// {@literal 1} if {@code field} is a new field in the hash and {@code value} was set.
        /// {@literal 0} if{@code field} already exists in the hash and the value was updated.
        /// </returns>
        public long exhset(string key, string field, string value)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHSET, key, field, value);
            return ResultHelper.Long(obj);
        }

        public long exhset(byte[] key, byte[] field, byte[] value)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHSET, key, field, value);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Set the string value of a exhash field.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="param">
        /// the params:[EX time] [EXAT time] [PX time] [PXAT time] [NX|XX] [VER version | ABS version]
        /// `EX` - Set expire time (seconds)
        /// `EXAT` - Set expire time as a UNIX timestamp (seconds)
        /// `PX` - Set expire time (milliseconds)
        /// `PXAT` - Set expire time as a UNIX timestamp (milliseconds)
        /// `NX` - only set the key if it does not already exists
        /// `XX` - only set the key if it already exists
        /// `VER` - Set if version matched or not exist
        /// `ABS` - Set with abs version
        /// </param>
        /// <returns>
        /// integer-reply specifically:
        /// {@literal 1} if {@code field} is a new field in the hash and {@code value} was set.
        /// {@literal 0} if{@code field} already exists in the hash and the value was updated.
        /// </returns>
        public long exhset(string key, string field, string value, ExhsetParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHSET,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field),
                    Encoding.UTF8.GetBytes(value)));
            return ResultHelper.Long(obj);
        }

        public long exhset(byte[] key, byte[] field, byte[] value, ExhsetParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHSET, param.getByteParams(key, field, value));
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Set the value of a exhash field,only if the field does not exist.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns>
        /// integer-reply specifically:
        /// {@literal 1} if {@code field} is a new field in the hash and {@code value} was set.
        /// {@literal 0} if{@code field} already exists in the hash and the value was updated.
        /// </returns>
        public long exhsetnx(string key, string field, string value)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHSETNX, key, field, value);
            return ResultHelper.Long(obj);
        }

        public long exhsetnx(byte[] key, byte[] field, byte[] value)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHSETNX, key, field, value);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Set multiple hash fields to multiple values.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hash"></param>
        /// <returns>String simple-string-reply</returns>
        public string exhmset(string key, Dictionary<string, string> hash)
        {
            var bhash = new Dictionary<byte[], byte[]>(hash.Count);
            foreach (var entry in hash)
                bhash.Add(Encoding.UTF8.GetBytes(entry.Key), Encoding.UTF8.GetBytes(entry.Value));

            return exhmset(Encoding.UTF8.GetBytes(key), bhash);
        }

        public string exhmset(byte[] key, Dictionary<byte[], byte[]> hash)
        {
            var param = new List<byte[]>();
            param.Add(key);

            foreach (var entry in hash)
            {
                param.Add(entry.Key);
                param.Add(entry.Value);
            }

            var obj = getRedis().Execute(ModuleCommand.EXHMSET, param.ToArray());
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// Set multiple hash field with version
        /// </summary>
        /// <param name="key"></param>
        /// <param name="param"></param>
        /// <returns>success ok</returns>
        public string exhmsetwithopts(string key, List<ExhmsetwithopsParams<string>> param)
        {
            var bexhash = new List<ExhmsetwithopsParams<byte[]>>();
            foreach (var entry in param)
                bexhash.Add(new ExhmsetwithopsParams<byte[]>(Encoding.UTF8.GetBytes(entry.getField()),
                    Encoding.UTF8.GetBytes(entry.getValue()), entry.getVer(), entry.getExp()));

            return exhmsetwithopts(Encoding.UTF8.GetBytes(key), bexhash);
        }

        public string exhmsetwithopts(byte[] key, List<ExhmsetwithopsParams<byte[]>> param)
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

            var obj = getRedis().Execute(ModuleCommand.EXHMSETWITHOPTS, p.ToArray());
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// Set expire time(milliseconds)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="milliseconds"></param>
        /// <returns>Success:true,fail:false.</returns>
        public bool exhpexpire(string key, string field, int milliseconds)
        {
            return exhpexpire(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), milliseconds);
        }

        public bool exhpexpire(byte[] key, byte[] field, int milliseconds)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHPEXPIRE, key, field,
                Encoding.UTF8.GetBytes(milliseconds.ToString()));
            return ResultHelper.Bool(obj);
        }

        /// <summary>
        /// Set the expiration for a key as a UNIX timestamp(milliseconds).
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="unixTime"></param>
        /// <returns>Success:true,fail:false.</returns>
        public bool exhpexpireAt(string key, string field, long unixTime)
        {
            return exhpexpireAt(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), unixTime);
        }

        public bool exhpexpireAt(byte[] key, byte[] field, long unixTime)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHPEXPIREAT, key, field,
                Encoding.UTF8.GetBytes(unixTime.ToString()));
            return ResultHelper.Bool(obj);
        }

        /// <summary>
        /// Set expire time(seconds).
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="seconds"></param>
        /// <returns>Success:true,fail:false.</returns>
        public bool exhexpire(string key, string field, int seconds)
        {
            return exhexpire(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), seconds);
        }

        public bool exhexpire(byte[] key, byte[] field, int seconds)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHEXPIRE, key, field,
                Encoding.UTF8.GetBytes(seconds.ToString()));
            return ResultHelper.Bool(obj);
        }

        /// <summary>
        /// Set the expiration for a key as a UNIX timestamp(seconds).
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="unixTime"></param>
        /// <returns>Success:true,fail:false.</returns>
        public bool exhexpireAt(string key, string field, long unixTime)
        {
            return exhexpireAt(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), unixTime);
        }

        public bool exhexpireAt(byte[] key, byte[] field, long unixTime)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHPEXPIREAT, key, field,
                Encoding.UTF8.GetBytes(unixTime.ToString()));
            return ResultHelper.Bool(obj);
        }

        /// <summary>
        /// Get ttl(milliseconds).
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns>ttl</returns>
        public long exhpttl(string key, string field)
        {
            return exhpttl(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field));
        }

        public long exhpttl(byte[] key, byte[] field)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHPTTL, key, field);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Get ttl(seconds).
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns>ttl</returns>
        public long exhttl(string key, string field)
        {
            return exhttl(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field));
        }

        public long exhttl(byte[] key, byte[] field)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHTTL, key, field);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Get version
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns>version</returns>
        public long exhver(string key, string field)
        {
            return exhver(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field));
        }

        public long exhver(byte[] key, byte[] field)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHVER, key, field);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Set the field version
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public bool exhsetver(string key, string field, long version)
        {
            return exhsetver(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), version);
        }

        public bool exhsetver(byte[] key, byte[] field, long version)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHSETVER, key, field,
                Encoding.UTF8.GetBytes(version.ToString()));
            return ResultHelper.Bool(obj);
        }

        /// <summary>
        /// Increment the integer value of a hash filed by the given number.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns>Long integer-reply the value at {@code field} after the increment operation.</returns>
        public long exhincrBy(string key, string field, long value)
        {
            return exhincrBy(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), value);
        }

        public long exhincrBy(byte[] key, byte[] field, long value)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHINCRBY, key, field, Encoding.UTF8.GetBytes(value.ToString()));
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Increment the integer value of a hash field by the given number.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="param">
        /// the params:[EX time] [EXAT time] [PX time] [PXAT time]
        /// `EX` - Set expire time (seconds)
        /// `EXAT` - Set expire time as a UNIX timestamp (seconds)
        /// `PX` - Set expire time (milliseconds)
        /// `PXAT` - Set expire time as a UNIX timestamp (milliseconds)
        /// </param>
        /// <returns>Long integer-reply the value at {@code field} after the increment operation.</returns>
        public long exhincrBy(string key, string field, long value, ExhincrByParams param)
        {
            return exhincrBy(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), value, param);
        }

        public long exhincrBy(byte[] key, byte[] field, long value, ExhincrByParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHINCRBY,
                param.getByteParams(key, field, Encoding.UTF8.GetBytes(value.ToString())));
            return ResultHelper.Long(obj);
        }


        /// <summary>
        /// Increment the float value of a hash field by the given amount.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns>Double bulk-string-reply the value of {@code field} after the increment.</returns>
        public double exhincrByFloat(string key, string field, double value)
        {
            return exhincrByFloat(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), value);
        }

        public double exhincrByFloat(byte[] key, byte[] field, double value)
        {
            var obj = getRedis()
                .Execute(ModuleCommand.EXHINCRBYFLOAT, key, field, Encoding.UTF8.GetBytes(value.ToString()));
            return ResultHelper.Double(obj);
        }

        /// <summary>
        /// Increment the float value of a hash field by the given amount.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="param">
        /// the params:[EX time] [EXAT time] [PX time] [PXAT time]
        /// `EX` - Set expire time (seconds)
        /// `EXAT` - Set expire time as a UNIX timestamp (seconds)
        /// `PX` - Set expire time (milliseconds)
        /// `PXAT` - Set expire time as a UNIX timestamp (milliseconds)
        /// </param>
        /// <returns>Double bulk-string-reply the value of {@code field} after the increment.</returns>
        public double exhincrByFloat(string key, string field, double value, ExhincrByFloatParams param)
        {
            return exhincrByFloat(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field), value, param);
        }

        public double exhincrByFloat(byte[] key, byte[] field, double value, ExhincrByFloatParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHINCRBYFLOAT,
                param.getByteParams(key, field, Encoding.UTF8.GetBytes(value.ToString())));
            return ResultHelper.Double(obj);
        }

        /// <summary>
        /// Get he value of a exhsh field.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns>
        /// K bulk-string-reply the value associated with {@code field}
        /// or{@literal null} when {@code field} is not presentin the hash or {@code key} does not exist.
        /// </returns>
        public string exhget(string key, string field)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHGET, key, field);
            if (obj.IsNull) return null;

            return ResultHelper.String(obj);
        }

        public byte[] exhget(byte[] key, byte[] field)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHGET, key, field);
            if (obj.IsNull) return null;

            return ResultHelper.ByteArray(obj);
        }

        /// <summary>
        /// Get the value and the version of a exhash field.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns>
        /// ExhgetwithverResult the value and the version associated with {@code field}
        /// or {@literal null} when {@code field} is not presentd in the hash or {@code key} does not exist.
        /// </returns>
        public ExhgetwithverResult<string> exhgetwithver(string key, string field)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHGETWITHVER, key, field);
            return HashHepler.GetVerResultString(obj);
        }

        public ExhgetwithverResult<byte[]> exhgetwithver(byte[] key, byte[] field)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHGETWITHVER, key, field);
            return HashHepler.GetVerResultByte(obj);
        }

        /// <summary>
        /// Get the values of all the given hash fields.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns>List&lt;K&gt; array-reply list of values associated with the given fields</returns>
        public List<string> exhmget(string key, params string[] fields)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHMGET,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(key), HashHepler.encodemany(fields)));
            return ResultHelper.ListString(obj);
        }

        public List<byte[]> exhmget(byte[] key, params byte[][] fields)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHMGET, JoinParameters.joinParameters(key, fields));
            return ResultHelper.ListByte(obj);
        }

        /// <summary>
        /// Get the values and version of all the given hash fields.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns>List&lt;K&gt; array-reply list of values associated with the given fields</returns>
        public List<ExhgetwithverResult<string>> exhmgetwithver(string key, string[] fields)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHMGETWITHVER,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(key), HashHepler.encodemany(fields)));
            return HashHepler.HmgetVerResultString(obj);
        }

        public List<ExhgetwithverResult<byte[]>> exhmgetwithver(byte[] key, params byte[][] fields)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHMGETWITHVER, JoinParameters.joinParameters(key, fields));
            return HashHepler.HmgetVerResultByte(obj);
        }

        /// <summary>
        /// Delete one or more hash fields.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns>
        /// Long integer-reply the number of fields that were removed from the hash not including specified but non
        /// existing fields.
        /// </returns>
        public long exhdel(string key, params string[] fields)
        {
            return exhdel(Encoding.UTF8.GetBytes(key), HashHepler.encodemany(fields));
        }

        public long exhdel(byte[] key, params byte[][] fields)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHDEL, JoinParameters.joinParameters(key, fields));
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Get the number of fields in a hash.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Long integer-reply number of fields in the hash, or {@code 0} when {@code key} does not exist.</returns>
        public long exhlen(string key)
        {
            return exhlen(Encoding.UTF8.GetBytes(key));
        }

        public long exhlen(string key, bool noexp)
        {
            return exhlen(Encoding.UTF8.GetBytes(key), noexp);
        }

        public long exhlen(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHLEN, key);
            return ResultHelper.Long(obj);
        }

        public long exhlen(byte[] key, bool noexp)
        {
            RedisResult obj;
            if (noexp)
                obj = getRedis().Execute(ModuleCommand.EXHLEN, key, Encoding.UTF8.GetBytes("noexp"));
            else
                obj = getRedis().Execute(ModuleCommand.EXHLEN, key);

            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Determine if a hash field exists.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns>
        /// boolean integer-reply specifically:
        /// {@literal true} if the hash contains {@code field}
        /// {@literal false} if the hash does not contain {@code field}, or {@code key} does not exist.
        /// </returns>
        public bool exhexists(string key, string field)
        {
            return exhexists(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field));
        }

        public bool exhexists(byte[] key, byte[] field)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHEXISTS, key, field);
            return ResultHelper.Bool(obj);
        }

        /// <summary>
        /// Get the length of a hash field.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns>the length</returns>
        public long exhstrlen(string key, string field)
        {
            return exhstrlen(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(field));
        }

        public long exhstrlen(byte[] key, byte[] field)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHSTRLEN, key, field);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Get all the fields in a hash.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Set&lt;K&gt; array-reply list of fields in the hash, or an empty list when {@code key} does not exist.</returns>
        public HashSet<string> exhkeys(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHKEYS, key);
            return HashHepler.HkeysResultString(obj);
        }

        public HashSet<byte[]> exhkeys(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHKEYS, key);
            return HashHepler.HkeysResultByte(obj);
        }

        /// <summary>
        /// Get all the values in a hash.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Set&lt;K&gt; array-reply list of fields in the hash, or an empty list when {@code key} does not exist.</returns>
        public List<string> exhvals(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHVALS, key);
            return ResultHelper.ListString(obj);
        }

        public List<byte[]> exhvals(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHVALS, key);
            return ResultHelper.ListByte(obj);
        }

        /// <summary>
        /// Get all the fields and values in a hash.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>
        /// Map&lt;K,K&gt; array-reply list of fields and their values stored in the hash or an empty list when {@code
        /// key} does not exist.
        /// </returns>
        public Dictionary<string, string> exhgetAll(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHGETALL, key);
            return HashHepler.HgetallResultString(obj);
        }

        public Dictionary<byte[], byte[]> exhgetAll(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHGETALL, key);
            return HashHepler.HgetallResultByte(obj);
        }


        /// <summary>
        /// Exhscan a exhash.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="op"></param>
        /// <param name="subkey"></param>
        /// <returns>A ExhscanResult</returns>
        public ExhscanResult<string> exhscan(string key, string op, string subkey)
        {
            return exhscan(key, op, subkey, new ExhscanParams());
        }

        public ExhscanResult<byte[]> exhscan(byte[] key, byte[] op, byte[] subkey)
        {
            return exhscan(key, op, subkey, new ExhscanParams());
        }

        /// <summary>
        /// Exhscan a exhash.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="op"></param>
        /// <param name="subkey"></param>
        /// <param name="param">
        /// the params:[MATCH pattern] [COUNT count]
        /// `MATCH` - Set the pattern which is used to filter the results.
        /// `COUNT` - Set the number of fields in a single scan(default is 10)
        /// </param>
        /// <returns>A ExhscanResult</returns>
        public ExhscanResult<string> exhscan(string key, string op, string subkey, ExhscanParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHSCAN,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(op),
                    Encoding.UTF8.GetBytes(subkey)));

            return HashHepler.ScanResultString(obj);
        }

        public ExhscanResult<byte[]> exhscan(byte[] key, byte[] op, byte[] subkey, ExhscanParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXHSCAN, param.getByteParams(key, op, subkey));
            return HashHepler.ScanResultByte(obj);
        }
    }
}