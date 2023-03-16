using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AlibabaCloud.TairSDK.TairSearch.Param;
using AlibabaCloud.TairSDK.Util;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairSearch
{
    public class TairSearchAsync
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairSearchAsync(ConnectionMultiplexer conn, int num = 0)
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
        /// <param name="index"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<RedisResult> tftmappingindex(string index, string request)
        {
            return tftmappingindex(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(request));
        }

        public Task<RedisResult> tftmappingindex(byte[] index, byte[] request)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTMAPPINGINDEX, index, request);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<RedisResult> tftcreateindex(string index, string request)
        {
            return tftcreateindex(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(request));
        }

        public Task<RedisResult> tftcreateindex(byte[] index, byte[] request)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTCREATEINDEX, index, request);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<RedisResult> tftupdateindex(string index, string request)
        {
            return tftupdateindex(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(request));
        }

        public Task<RedisResult> tftupdateindex(byte[] index, byte[] request)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTUPDATEINDEX, index, request);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Task<RedisResult> tftgetindexmappings(string index)
        {
            return tftgetindexmappings(Encoding.UTF8.GetBytes(index));
        }

        public Task<RedisResult> tftgetindexmappings(byte[] index)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTGETINDEX, index, Encoding.UTF8.GetBytes("mappings"));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<RedisResult> tftadddoc(string index, string request)
        {
            return tftadddoc(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(request));
        }

        public Task<RedisResult> tftadddoc(byte[] index, byte[] request)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTADDDOC, index, request);
            return obj;
        }

        public Task<RedisResult> tftadddoc(string index, string request, string docId)
        {
            return tftadddoc(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(request),
                Encoding.UTF8.GetBytes(docId));
        }

        public Task<RedisResult> tftadddoc(byte[] index, byte[] request, byte[] docId)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTADDDOC, index, request,
                Encoding.UTF8.GetBytes("WITH_ID"),
                docId);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="docs"></param>
        /// <returns></returns>
        public Task<RedisResult> tftmadddoc(string index, Dictionary<string, string> docs)
        {
            TFTAddDocParams param = new TFTAddDocParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTMADDDOC, param.getbyteParams(index, docs));
            return obj;
        }

        public Task<RedisResult> tftmadddoc(byte[] index, Dictionary<byte[], byte[]> docs)
        {
            TFTAddDocParams param = new TFTAddDocParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTMADDDOC, param.getbyteParams(index, docs));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="docId"></param>
        /// <param name="docContent"></param>
        /// <returns></returns>
        public Task<RedisResult> tftupdatedoc(string index, string docId, string docContent)
        {
            return tftupdatedoc(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(docId),
                Encoding.UTF8.GetBytes(docContent));
        }

        public Task<RedisResult> tftupdatedoc(byte[] index, byte[] docId, byte[] docContent)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTUPDATEDOC, index, docId, docContent);
            return obj;
        }

        public Task<RedisResult> tftupdatedocfield(string index, string docId, string docContent)
        {
            return tftupdatedocfield(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(docId),
                Encoding.UTF8.GetBytes(docContent));
        }

        public Task<RedisResult> tftupdatedocfield(byte[] index, byte[] docId, byte[] docContent)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTUPDATEDOCFIELD, index, docId, docContent);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="docId"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<RedisResult> tftincrlongdocfield(string index, string docId, string field, long value)
        {
            return tftincrlongdocfield(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(docId),
                Encoding.UTF8.GetBytes(field), value);
        }

        public Task<RedisResult> tftincrlongdocfield(byte[] index, byte[] docId, byte[] field, long value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTINCRLONGDOCFIELD, index, docId, field,
                Encoding.UTF8.GetBytes(value.ToString()));
            return obj;
        }

        public Task<RedisResult> tftincrfloatdocfield(string index, string docId, string field, double value)
        {
            return tftincrfloatdocfield(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(docId),
                Encoding.UTF8.GetBytes(field), value);
        }

        public Task<RedisResult> tftincrfloatdocfield(byte[] index, byte[] docId, byte[] field, double value)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTINCRFLOATDOCFIELD, index, docId, field,
                Encoding.UTF8.GetBytes(value.ToString()));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="docId"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public Task<RedisResult> tftdeldocfield(string index, string docId, params string[] field)
        {
            return tftdeldocfield(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(docId),
                SearchHelper.encodemany(field));
        }

        public Task<RedisResult> tftdeldocfield(byte[] index, byte[] docId, params byte[][] field)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTDELDOCFIELD,
                JoinParameters.joinParameters(index, docId, field));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="docId"></param>
        /// <returns></returns>
        public Task<RedisResult> tftgetdoc(string index, string docId)
        {
            return tftgetdoc(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(docId));
        }

        public Task<RedisResult> tftgetdoc(byte[] index, byte[] docId)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTGETDOC, index, docId);
            return obj;
        }

        public Task<RedisResult> tftgetdoc(string index, string docId, string request)
        {
            return tftgetdoc(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(docId),
                Encoding.UTF8.GetBytes(request));
        }

        public Task<RedisResult> tftgetdoc(byte[] index, byte[] docId, byte[] request)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTGETDOC, index, docId, request);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="docId"></param>
        /// <returns></returns>
        public Task<RedisResult> tftdeldoc(string index, params string[] docId)
        {
            TFTDelDocParams param = new TFTDelDocParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTDELDOC, param.getByteParams(index, docId));
            return obj;
        }

        public Task<RedisResult> tftdeldoc(byte[] index, params byte[][] docId)
        {
            TFTDelDocParams param = new TFTDelDocParams();
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTDELDOC, param.getByteParams(index, docId));
            return obj;
        }

        public Task<RedisResult> tftdelall(string index)
        {
            return tftdelall(Encoding.UTF8.GetBytes(index));
        }

        public Task<RedisResult> tftdelall(byte[] index)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTDELALL, index);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<RedisResult> tftsearch(string index, string request)
        {
            return tftsearch(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(request));
        }

        public Task<RedisResult> tftsearch(byte[] index, byte[] request)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTSEARCH, index, request);
            return obj;
        }

        public Task<RedisResult> tftsearch(string index, string request, bool use_cache)
        {
            return tftsearch(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(request), use_cache);
        }

        public Task<RedisResult> tftsearch(byte[] index, byte[] request, bool use_cache)
        {
            Task<RedisResult> obj;
            if (use_cache)
            {
                obj = getRedis().ExecuteAsync(ModuleCommand.TFTSEARCH, index, request,
                    Encoding.UTF8.GetBytes("use_cache"));
            }
            else
            {
                obj = getRedis().ExecuteAsync(ModuleCommand.TFTSEARCH, index, request);
            }

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="docId"></param>
        /// <returns></returns>
        public Task<RedisResult> tftexists(string index, string docId)
        {
            return tftexists(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(docId));
        }

        public Task<RedisResult> tftexists(byte[] index, byte[] docId)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTEXISTS, index, docId);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Task<RedisResult> tftdocnum(string index)
        {
            return tftdocnum(Encoding.UTF8.GetBytes(index));
        }

        public Task<RedisResult> tftdocnum(byte[] index)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTDOCNUM, index);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="cursor"></param>
        /// <returns></returns>
        public Task<RedisResult> tftscandocid(string index, string cursor)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTSCANDOCID, index, cursor);
            return obj;
        }

        public Task<RedisResult> tftscandocid(byte[] index, byte[] cursor)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTSCANDOCID, index, cursor);
            return obj;
        }

        public Task<RedisResult> tftscandocid(string index, string cursor, TFTScanParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTSCANDOCID,
                param.getByteParams(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(cursor)));
            return obj;
        }

        public Task<RedisResult> tftscamdocid(byte[] index, byte[] cursor, TFTScanParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTSCANDOCID,
                param.getByteParams(index, cursor));
            return obj;
        }

        public Task<RedisResult> tftanalyzer(string index_name, string text)
        {
            return tftanalyzer(Encoding.UTF8.GetBytes(index_name), Encoding.UTF8.GetBytes(text));
        }

        public Task<RedisResult> tftanalyzer(byte[] index_name, byte[] text)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTANALYZER, index_name, text);
            return obj;
        }

        public Task<RedisResult> tftanalyzer(string index_name, string text, TFTAnalyzerParams param)
        {
            return tftanalyzer(Encoding.UTF8.GetBytes(index_name), Encoding.UTF8.GetBytes(text), param);
        }

        public Task<RedisResult> tftanalyzer(byte[] index_name, byte[] text, TFTAnalyzerParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.TFTANALYZER, param.getByteParams(index_name, text));
            return obj;
        }
    }
}