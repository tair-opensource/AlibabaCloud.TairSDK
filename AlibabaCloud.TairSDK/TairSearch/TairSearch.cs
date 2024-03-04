using System.Collections.Generic;
using System.Text;
using AlibabaCloud.TairSDK.TairSearch.Param;
using AlibabaCloud.TairSDK.Util;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairSearch
{
    public class TairSearch
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairSearch(ConnectionMultiplexer conn, int num = 0)
        {
            _conn = conn;
            _redis = _conn.GetDatabase(num);
        }

        private IDatabase getRedis()
        {
            return _redis;
        }


        /// <summary>
        /// Create an Index and specify its schema. Note that this command will only succeed if the Index does not exist.
        /// </summary>
        /// <param name="index">index name</param>
        /// <param name="request">the index schema</param>
        /// <returns>Success: OK; Fail: error</returns>
        public string tftmappingindex(string index, string request)
        {
            return tftmappingindex(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(request));
        }

        public string tftmappingindex(byte[] index, byte[] request)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTMAPPINGINDEX, index, request);
            return ResultHelper.String(obj);
        }


        /// <summary>
        /// Create an Index and specify its schema. Note that this command will only succeed if the Index does not exist.
        /// </summary>
        /// <param name="index">the index name</param>
        /// <param name="request">the index schema</param>
        /// <returns>Success: OK; Fail: error</returns>
        public string tftcreateindex(string index, string request)
        {
            return tftcreateindex(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(request));
        }

        public string tftcreateindex(byte[] index, byte[] request)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTCREATEINDEX, index, request);
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// Update an existing index mapping.
        /// Note that you cannot update (append) mapping properties.
        /// </summary>
        /// <param name="index">the index name</param>
        /// <param name="request">the index schema</param>
        /// <returns>Success: OK; Fail: error</returns>
        public string tftupdateindex(string index, string request)
        {
            return tftupdateindex(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(request));
        }

        public string tftupdateindex(byte[] index, byte[] request)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTUPDATEINDEX, index, request);
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// Get index schema information
        /// </summary>
        /// <param name="index">the index name</param>
        /// <returns>Success: Schema information represented by json; Fail: error</returns>
        public string tftgetindexmappings(string index)
        {
            return tftgetindexmappings(Encoding.UTF8.GetBytes(index));
        }

        public string tftgetindexmappings(byte[] index)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTGETINDEX, index, Encoding.UTF8.GetBytes("mappings"));
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// Add a document to Index
        /// </summary>
        /// <param name="index">the index name</param>
        /// <param name="request">the json representation of a document</param>
        /// <returns>Success: Schema information represented by json; Fail: error</returns>
        public string tftadddoc(string index, string request)
        {
            return tftadddoc(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(request));
        }

        public string tftadddoc(byte[] index, byte[] request)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTADDDOC, index, request);
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// Similar to the above but you can manually specify the document id.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="request"></param>
        /// <param name="docId"></param>
        /// <returns></returns>
        public string tftadddoc(string index, string request, string docId)
        {
            return tftadddoc(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(request),
                Encoding.UTF8.GetBytes(docId));
        }

        public string tftadddoc(byte[] index, byte[] request, byte[] docId)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTADDDOC, index, request, Encoding.UTF8.GetBytes("WITH_ID"),
                docId);
            return ResultHelper.String(obj);
        }


        /// <summary>
        /// Add docs in batch. This command can guarantee atomicity, that is, either all documents are added successfully, or none are added.
        /// </summary>
        /// <param name="index">the index name</param>
        /// <param name="docs">the json representation of a document</param>
        /// <returns>Success: OK; Fail: error</returns>
        public string tftmadddoc(string index, Dictionary<string, string> docs)
        {
            TFTAddDocParams param = new TFTAddDocParams();
            var obj = getRedis().Execute(ModuleCommand.TFTMADDDOC, param.getbyteParams(index, docs));
            return ResultHelper.String(obj);
        }

        public string tftmadddoc(byte[] index, Dictionary<byte[], byte[]> docs)
        {
            TFTAddDocParams param = new TFTAddDocParams();
            var obj = getRedis().Execute(ModuleCommand.TFTMADDDOC, param.getbyteParams(index, docs));
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// Update an existing doc. You can add new fields to the document, or update an existing field.
        /// </summary>
        /// <param name="index">the index name</param>
        /// <param name="docId">the id of the document</param>
        /// <param name="docContent">the content of the document</param>
        /// <returns>Success: OK; Fail: error</returns>
        public string tftupdatedoc(string index, string docId, string docContent)
        {
            return tftupdatedoc(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(docId),
                Encoding.UTF8.GetBytes(docContent));
        }

        public string tftupdatedoc(byte[] index, byte[] docId, byte[] docContent)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTUPDATEDOC, index, docId, docContent);
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// Update doc fields. You can add new fields to the document, or update an existing field.
        /// The document is automatically created if it does not exist.
        /// </summary>
        /// <param name="index">the index name</param>
        /// <param name="docId">the id of the document</param>
        /// <param name="docContent">the content of the document</param>
        /// <returns>Success: OK; Fail: error</returns>
        public string tftupdatedocfield(string index, string docId, string docContent)
        {
            return tftupdatedocfield(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(docId),
                Encoding.UTF8.GetBytes(docContent));
        }

        public string tftupdatedocfield(byte[] index, byte[] docId, byte[] docContent)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTUPDATEDOCFIELD, index, docId, docContent);
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// Increment the integer value of a document field by the given number.
        /// </summary>
        /// <param name="index">the index name</param>
        /// <param name="docId">the document id</param>
        /// <param name="field">the field of the document that will be incremented</param>
        /// <param name="value">the value to bu incremented</param>
        /// <returns>Long integer-reply the value after the increment operation.</returns>
        public long tftincrlongdocfield(string index, string docId, string field, long value)
        {
            return tftincrlongdocfield(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(docId),
                Encoding.UTF8.GetBytes(field), value);
        }

        public long tftincrlongdocfield(byte[] index, byte[] docId, byte[] field, long value)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTINCRLONGDOCFIELD, index, docId, field,
                Encoding.UTF8.GetBytes(value.ToString()));
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Increment the double value of a document field by the given number.
        /// </summary>
        /// <param name="index">the index name</param>
        /// <param name="docId">the document id</param>
        /// <param name="field">The fields of the document that will be incremented</param>
        /// <param name="value">the value to be incremented</param>
        /// <returns>Long double-reply the value after the increment operation.</returns>
        public double tftincrfloatdocfield(string index, string docId, string field, double value)
        {
            return tftincrfloatdocfield(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(docId),
                Encoding.UTF8.GetBytes(field), value);
        }

        public double tftincrfloatdocfield(byte[] index, byte[] docId, byte[] field, double value)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTINCRFLOATDOCFIELD, index, docId, field,
                Encoding.UTF8.GetBytes(value.ToString()));
            return ResultHelper.Double(obj);
        }


        /// <summary>
        /// Delete fields in the document
        /// </summary>
        /// <param name="index">the index name</param>
        /// <param name="docId">the document id</param>
        /// <param name="field">The fields of the document that will be deleted</param>
        /// <returns>Long integer-reply the number of fields that were removed from the document.</returns>
        public long tftdeldocfield(string index, string docId, params string[] field)
        {
            return tftdeldocfield(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(docId),
                SearchHelper.encodemany(field));
        }

        public long tftdeldocfield(byte[] index, byte[] docId, params byte[][] field)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTDELDOCFIELD,
                JoinParameters.joinParameters(index, docId, field));
            return ResultHelper.Long(obj);
        }


        /// <summary>
        /// Get a document from Index 
        /// </summary>
        /// <param name="index">the index name</param>
        /// <param name="docId">the document id</param>
        /// <returns>Success: The content of the document; Not exists: null; Fail: error</returns>
        public string tftgetdoc(string index, string docId)
        {
            return tftgetdoc(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(docId));
        }

        public string tftgetdoc(byte[] index, byte[] docId)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTGETDOC, index, docId);
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// Same as above but you can specify some filtering rules through the request parameter.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="docId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public string tftgetdoc(string index, string docId, string request)
        {
            return tftgetdoc(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(docId),
                Encoding.UTF8.GetBytes(request));
        }

        public string tftgetdoc(byte[] index, byte[] docId, byte[] request)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTGETDOC, index, docId, request);
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// Delete the specified document(s) from the index.
        /// </summary>
        /// <param name="index">the index name</param>
        /// <param name="docId">the document id</param>
        /// <returns>Success: Number of successfully deleted documents; Fail: error</returns>
        public string tftdeldoc(string index, params string[] docId)
        {
            TFTDelDocParams param = new TFTDelDocParams();
            var obj = getRedis().Execute(ModuleCommand.TFTDELDOC, param.getByteParams(index, docId));
            return ResultHelper.String(obj);
        }

        public string tftdeldoc(byte[] index, params byte[][] docId)
        {
            TFTDelDocParams param = new TFTDelDocParams();
            var obj = getRedis().Execute(ModuleCommand.TFTDELDOC, param.getByteParams(index, docId));
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// Delete all document(s) from the index.
        /// </summary>
        /// <param name="index">the index name</param>
        /// <returns>Success: OK; Fail: error</returns>
        public string tftdelall(string index)
        {
            return tftdelall(Encoding.UTF8.GetBytes(index));
        }

        public string tftdelall(byte[] index)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTDELALL, index);
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// Full text search in an Index.
        /// </summary>
        /// <param name="index">the index name</param>
        /// <param name="request">Search expression, for detailed grammar, please refer to the official document</param>
        /// <returns>Success: Query result in json format; Fail: error</returns>
        public string tftsearch(string index, string request)
        {
            return tftsearch(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(request));
        }

        public string tftsearch(byte[] index, byte[] request)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTSEARCH, index, request);
            return ResultHelper.String(obj);
        }

        public string tftsearch(string index, string request, bool use_cache)
        {
            return tftsearch(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(request), use_cache);
        }

        public string tftsearch(byte[] index, byte[] request, bool use_cache)
        {
            RedisResult obj;
            if (use_cache)
            {
                obj = getRedis().Execute(ModuleCommand.TFTSEARCH, index, request, Encoding.UTF8.GetBytes("use_cache"));
            }
            else
            {
                obj = getRedis().Execute(ModuleCommand.TFTSEARCH, index, request);
            }

            return ResultHelper.String(obj);
        }
        
        public string tftmsearch(int index_count, string[] index, string query)
        {
            TFTMSearchParams param = new TFTMSearchParams();
            RedisResult obj;
            obj = getRedis().Execute(ModuleCommand.TFTMSEARCH, param.getByteParams(index_count, index, query));
            return ResultHelper.String(obj);
        }

        public string tftmsearch(int index_count, byte[][] index, byte[] query)
        {
            TFTMSearchParams param = new TFTMSearchParams();
            RedisResult obj;
            obj = getRedis().Execute(ModuleCommand.TFTMSEARCH, param.getByteParams(index_count, index, query));
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// Checks if the specified document exists in the index.
        /// </summary>
        /// <param name="index">the index name</param>
        /// <param name="docId">the id of the document</param>
        /// <returns>exists return 1 or return 0</returns>
        public long tftexists(string index, string docId)
        {
            return tftexists(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(docId));
        }

        public long tftexists(byte[] index, byte[] docId)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTEXISTS, index, docId);
            return ResultHelper.Long(obj);
        }


        /// <summary>
        /// Get the number of documents contained in Index.
        /// </summary>
        /// <param name="index">the index name</param>
        /// <returns>the number of documents contained in Index.</returns>
        public long tftdocnum(string index)
        {
            return tftdocnum(Encoding.UTF8.GetBytes(index));
        }

        public long tftdocnum(byte[] index)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTDOCNUM, index);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Scan all document ids in index.
        /// </summary>
        /// <param name="index">the index name</param>
        /// <param name="cursor">the cursor used for this scan</param>
        /// <returns>the scan result with the results of this iteration and the new position of the cursor.</returns>
        public TFTScanResult<string> tftscandocid(string index, string cursor)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTSCANDOCID, index, cursor);
            return SearchHelper.ScanResultString(obj);
        }

        public TFTScanResult<byte[]> tftscandocid(byte[] index, byte[] cursor)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTSCANDOCID, index, cursor);
            return SearchHelper.ScanResultByte(obj);
        }

        /// <summary>
        /// Scan all document ids in index.
        /// Time complexity: O(1) for every call. O(N) for a complete iteration, including enough command
        /// calls for the cursor to return back to 0. N is the number of documents inside the index.
        /// </summary>
        /// <param name="index">the index name</param>
        /// <param name="cursor">the cursor</param>
        /// <param name="param">the scan parameters. For example a glob-style match pattern</param>
        /// <returns>the scan result with the results of this iteration and the new position of the cursor.</returns>
        public TFTScanResult<string> tftscandocid(string index, string cursor, TFTScanParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTSCANDOCID,
                param.getByteParams(Encoding.UTF8.GetBytes(index), Encoding.UTF8.GetBytes(cursor)));
            return SearchHelper.ScanResultString(obj);
        }

        public TFTScanResult<byte[]> tftscamdocid(byte[] index, byte[] cursor, TFTScanParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTSCANDOCID,
                param.getByteParams(index, cursor));
            return SearchHelper.ScanResultByte(obj);
        }

        /// <summary>
        /// Debug analyzer.
        /// </summary>
        /// <param name="analyzerName">the analyzer name</param>
        /// <param name="text">the text to be tokenized</param>
        /// <returns>Success: Token information</returns>
        public string tftanalyzer(string analyzerName, string text)
        {
            return tftanalyzer(Encoding.UTF8.GetBytes(analyzerName), Encoding.UTF8.GetBytes(text));
        }

        public string tftanalyzer(byte[] analyzerName, byte[] text)
        {
            // byte[] key = getRedis().KeyRandom();
            var obj = getRedis().Execute(ModuleCommand.TFTANALYZER, analyzerName, text);
            return ResultHelper.String(obj);
        }

        public string tftanalyzer(string analyzerName, string text, TFTAnalyzerParams param)
        {
            return tftanalyzer(Encoding.UTF8.GetBytes(analyzerName), Encoding.UTF8.GetBytes(text), param);
        }

        public string tftanalyzer(byte[] analyzerName, byte[] text, TFTAnalyzerParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTANALYZER, param.getByteParams(analyzerName, text));
            return ResultHelper.String(obj);
        }
        
        /// <summary>
        /// explain the cost of every query phase.
        /// </summary>
        /// <param name="index">index name</param>
        /// <param name="request">query clause</param>
        /// <returns>Token information</returns>
        public string tftexplaincost(string index, string request)
        {
            return tftexplaincost(Encoding.UTF8.GetBytes(index),Encoding.UTF8.GetBytes(request));
        }

        public string tftexplaincost(byte[] index, byte[] request)
        {
            var obj = getRedis().Execute(ModuleCommand.TFTEXPLAINCOST, index, request);
            return ResultHelper.String(obj);
        }
        
        /// <summary>
        /// explain the score of query.
        /// </summary>
        /// <param name="index"> the index name</param>
        /// <param name="request"> the query clause</param>
        /// <param name="docIds"> the document ids</param>
        /// <returns>Success: Search result with score explanation</returns>
        public string tftexplainscore(string index, string request, params string[] docIds)
        {
            TFTExplainScoreParams param = new TFTExplainScoreParams();
            var obj = getRedis().Execute(ModuleCommand.TFTEXPLAINSCORE, param.getbyteParams(index, request, docIds));
            return ResultHelper.String(obj);
        }

        public string tftexplainscore(byte[] index, byte[] request, params byte[][] docIds)
        {
            TFTExplainScoreParams param = new TFTExplainScoreParams();
            var obj = getRedis().Execute(ModuleCommand.TFTEXPLAINSCORE, param.getbyteParams(index, request, docIds));
            return ResultHelper.String(obj);
        }
    }
}