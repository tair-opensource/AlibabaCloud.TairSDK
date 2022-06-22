using System.Collections.Generic;
using System.Text;
using AlibabaCloud.TairSDK.TairDoc.Param;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairDoc
{
    public class TairDoc
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairDoc(ConnectionMultiplexer conn, int num = 0)
        {
            _conn = conn;
            _redis = _conn.GetDatabase(num);
        }

        private IDatabase getRedis()
        {
            return _redis;
        }

        /// <summary>
        /// JSON.SET <key> <path> <json> [NX|XX]
        /// Sets the JSON value at `path` in `key`
        /// For new Redis keys the `path` must be the root. For existing keys, when the entire `path` exists,
        /// the value that it contains is replaced with the `json` value.
        /// 
        /// `NX` - only set the key if it does not already exists
        /// `XX` - only set the key if it already exists
        /// </summary>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <param name="json"></param>
        /// <returns> Simple String `OK` if executed correctly, or Null if the specified `NX` or `XX` conditions were not met.</returns>
        public string jsonset(string key, string path, string json)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONSET, key, path, json);
            return ResultHelper.String(obj);
        }

        public string jsonset(string key, string path, string json, JsonsetParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONSET,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(path),
                    Encoding.UTF8.GetBytes(json)));
            return ResultHelper.String(obj);
        }

        public string jsonset(byte[] key, byte[] path, byte[] json)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONSET, key, path, json);
            return ResultHelper.String(obj);
        }

        public string jsonset(byte[] key, byte[] path, byte[] json, JsonsetParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONSET, param.getByteParams(key, path, json));
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// JSON.GET <key> [PATH] [FORMAT <XML/YAML>] [ROOTNAME <root>] [ARRNAME <arr>]
        /// Return the value at `path` in JSON serialized form.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Bulk String, specifically the JSON serialization or XML/YAML.</returns>
        public string jsonget(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONGET, key);
            return ResultHelper.String(obj);
        }

        public string jsonget(string key, string path)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONGET, key, path);
            return ResultHelper.String(obj);
        }

        public string jsonget(string key, string path, JsongetParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONGET,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(path)));
            return ResultHelper.String(obj);
        }

        public byte[] jsonget(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONGET, key);
            return ResultHelper.ByteArray(obj);
        }

        public byte[] jsonget(byte[] key, byte[] path)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONGET, key, path);
            return ResultHelper.ByteArray(obj);
        }

        public byte[] jsonget(byte[] key, byte[] path, JsongetParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONGET, param.getByteParams(key, path));
            return ResultHelper.ByteArray(obj);
        }


        /// <summary>
        /// JSON.MGET <key> [<key> ...] <path>
        /// Returns the values at `path` from multiple `key`s. Non-existing keys and non-existing paths are reported as null.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Array of Bulk Strings, specifically the JSON serialization of the value at each key's path.</returns>
        public List<string> jsonmget(params string[] args)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONMGET, args);
            return ResultHelper.ListString(obj);
        }

        public List<byte[]> jsonmget(params byte[][] args)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONMGET, args);
            return ResultHelper.ListByte(obj);
        }

        /// <summary>
        /// JSON.DEL <key> [path]
        /// Delete a value.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Reply: Integer, specifically the number of paths deleted (0 or 1).</returns>
        public long jsondel(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONDEL, key);
            return ResultHelper.Long(obj);
        }

        public long jsondel(string key, string path)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONDEL, key, path);
            return ResultHelper.Long(obj);
        }

        public long jsondel(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONDEL, key);
            return ResultHelper.Long(obj);
        }

        public long jsondel(byte[] key, byte[] path)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONDEL, key, path);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// JSON.TYPE <key> [path]
        /// Reports the type of JSON value at `path`.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Simple string, specifically the type.</returns>
        public string jsontype(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONTYPE, key);
            return ResultHelper.String(obj);
        }

        public string jsontype(string key, string path)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONTYPE, key, path);
            return ResultHelper.String(obj);
        }

        public byte[] jsontype(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONTYPE, key);
            return ResultHelper.ByteArray(obj);
        }

        public byte[] jsontype(byte[] key, byte[] path)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONTYPE, key, path);
            return ResultHelper.ByteArray(obj);
        }

        /// <summary>
        /// JSON.NUMICRBY <key> [path] <value>
        /// long value range: [-2^53, 2^53] [-9007199254740992, 9007199254740992]
        /// double value range: [Double.MIN_VALUE, Double.MAX_VALUE]
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>int number, specifically the resulting.</returns>
        public double jsonnumincrBy(string key, double value)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONNUMINCRBY, Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(value.ToString()));
            return ResultHelper.Double(obj);
        }

        public double jsonnumincrBy(string key, string path, double value)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONNUMINCRBY, Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(path), Encoding.UTF8.GetBytes(value.ToString()));
            return ResultHelper.Double(obj);
        }

        public double jsonnumincrBy(byte[] key, double value)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONNUMINCRBY, key,
                Encoding.UTF8.GetBytes(value.ToString()));
            return ResultHelper.Double(obj);
        }

        public double jsonnumincrBy(byte[] key, byte[] path, double value)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONNUMINCRBY, key,
                path, Encoding.UTF8.GetBytes(value.ToString()));
            return ResultHelper.Double(obj);
        }

        /// <summary>
        /// JSON.STRAPPEND <key> [path] <json-string>
        /// Append the `json-string` value(s) the string at `path`.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="json"></param>
        /// <returns>Integer, -1 : key not exists, other: specifically the string's new length.</returns>
        public long jsonstrAppend(string key, string json)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONSTRAPPEND, key, json);
            return ResultHelper.Long(obj);
        }

        public long jsonstrAppend(string key, string path, string json)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONSTRAPPEND, key, path, json);
            return ResultHelper.Long(obj);
        }

        public long jsonstrAppend(byte[] key, byte[] json)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONSTRAPPEND, key, json);
            return ResultHelper.Long(obj);
        }

        public long jsonstrAppend(byte[] key, byte[] path, byte[] json)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONSTRAPPEND, key, path, json);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// JSON.STRLEN <key> [path]
        /// Report the length of the JSON value at `path` in `key`.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Integer, specifically the length of the value.</returns>
        public long jsonstrlen(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONSTRLEN, key);
            return ResultHelper.Long(obj);
        }

        public long jsonstrlen(string key, string path)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONSTRLEN, key, path);
            return ResultHelper.Long(obj);
        }

        public long jsonstrlen(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONSTRLEN, key);
            return ResultHelper.Long(obj);
        }

        public long jsonstrlen(byte[] key, byte[] path)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONSTRLEN, key, path);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// JSON.ARRAPPEND <key> <path> <json> [<json> ...]
        /// Append the `json` value(s) into the array at `path` after the last element in it.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Integer, specifically the array's new size</returns>
        public long jsonarrAppend(params string[] args)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONARRAPPEND, args);
            return ResultHelper.Long(obj);
        }

        public long jsonarrAppend(params byte[][] args)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONARRAPPEND, args);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// JSON.ARRPOP <key> <path> [index]
        /// Remove and return element from the index in the array.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <returns>Bulk String, specifically the popped JSON value.</returns>
        public string jsonarrPop(string key, string path)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONARRPOP, key, path);
            return ResultHelper.String(obj);
        }

        public string jsonarrPop(string key, string path, int index)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONARRPOP, key, path, index.ToString());
            return ResultHelper.String(obj);
        }

        public byte[] jsonarrPop(byte[] key, byte[] path)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONARRPOP, key, path);
            return ResultHelper.ByteArray(obj);
        }

        public byte[] jsonarrPop(byte[] key, byte[] path, int index)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONARRPOP, key, path, Encoding.UTF8.GetBytes(index.ToString()));
            return ResultHelper.ByteArray(obj);
        }

        /// <summary>
        /// JSON.ARRINSERT <key> <path> <index> <json> [<json> ...]
        /// Insert the `json` value(s) into the array at `path` before the `index` (shifts to the right).
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Reply: Integer, specifically the array's new size</returns>
        public long jsonarrInsert(params string[] args)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONARRINSERT, args);
            return ResultHelper.Long(obj);
        }

        public long jsonarrInsert(params byte[][] args)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONARRINSERT, args);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// JSON.ARRLEN <key> [path]
        /// Report the length of the array at `path` in `key`.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Integer, specifically the length of the array.</returns>
        public long jsonArrLen(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONARRLEN, key);
            return ResultHelper.Long(obj);
        }

        public long jsonArrLen(string key, string path)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONARRLEN, key, path);
            return ResultHelper.Long(obj);
        }

        public long jsonArrLen(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONARRLEN, key);
            return ResultHelper.Long(obj);
        }

        public long jsonArrLen(byte[] key, byte[] path)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONARRLEN, key, path);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// JSON.ARRTRIM <key> <path> <start> <stop>
        /// Trim an array so that it contains only the specified inclusive range of elements.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns>Integer, specifically the array's new size.</returns>
        public long jsonarrTrim(string key, string path, int start, int stop)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONARRTRIM, key, path, start.ToString(), stop.ToString());
            return ResultHelper.Long(obj);
        }

        public long jsonarrTrim(byte[] key, byte[] path, int start, int stop)
        {
            var obj = getRedis().Execute(ModuleCommand.JSONARRTRIM, key, path, Encoding.UTF8.GetBytes(start.ToString()),
                Encoding.UTF8.GetBytes(stop.ToString()));
            return ResultHelper.Long(obj);
        }
    }
}