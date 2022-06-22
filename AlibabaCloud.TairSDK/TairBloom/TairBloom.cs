using System.Collections.Generic;
using System.Text;
using AlibabaCloud.TairSDK.TairBloom.Param;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairBloom
{
    public class TairBloom
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairBloom(ConnectionMultiplexer conn, int num = 0)
        {
            _conn = conn;
            _redis = _conn.GetDatabase(num);
        }

        private IDatabase getRedis()
        {
            return _redis;
        }

        /// <summary>
        /// Create a empty bloomfilter with the initCapacity & errorRate.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="errorRate"></param>
        /// <param name="initCapacity"></param>
        /// <returns>String simple-string-reply: {@literal OK} if success; {@literal error} if failure</returns>
        public string bfreserve(string key, double errorRate, long initCapacity)
        {
            return bfreserve(Encoding.UTF8.GetBytes(key), errorRate, initCapacity);
        }

        public string bfreserve(byte[] key, double errorRate, long initCapacity)
        {
            var obj = getRedis().Execute(ModuleCommand.BFRESERVE, key, errorRate, initCapacity);
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// Add a item to bloomfilter.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns>Success: true, fail: false</returns>
        public bool bfadd(string key, string item)
        {
            return bfadd(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(item));
        }

        public bool bfadd(byte[] key, byte[] item)
        {
            var obj = getRedis().Execute(ModuleCommand.BFADD, key, item);
            return ResultHelper.Bool(obj);
        }

        /// <summary>
        /// Add items to bloomfilter.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="items"></param>
        /// <returns>Boolean array; Success: true, fail: false</returns>
        public bool[] bfmadd(string key, params string[] items)
        {
            return bfmadd(Encoding.UTF8.GetBytes(key), BloomHelper.encodemany(items));
        }

        public bool[] bfmadd(byte[] key, params byte[][] items)
        {
            BfmaddParams param = new BfmaddParams();
            var obj = getRedis().Execute(ModuleCommand.BFMADD, param.getByteParams(key, items));
            return ResultHelper.BoolArray(obj);
        }

        /// <summary>
        /// find if the item in bloomfilter.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool bfexists(string key, string item)
        {
            var obj = getRedis().Execute(ModuleCommand.BFEXISTS, key, item);
            return ResultHelper.Bool(obj);
        }

        public bool bfexists(byte[] key, byte[] item)
        {
            var obj = getRedis().Execute(ModuleCommand.BFEXISTS, key, item);
            return ResultHelper.Bool(obj);
        }

        /// <summary>
        /// find if the items in bloomfilter.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public bool[] bfmexists(string key, params string[] items)
        {
            byte[][] byte_items = new byte[items.Length][];
            for (int i = 0; i < items.Length; i++)
            {
                byte_items[i] = Encoding.UTF8.GetBytes(items[i]);
            }

            return bfmexists(Encoding.UTF8.GetBytes(key), byte_items);
        }

        public bool[] bfmexists(byte[] key, params byte[][] items)
        {
            BfmexistsParams param = new BfmexistsParams();
            var obj = getRedis().Execute(ModuleCommand.BFMEXISTS, param.getByteParams(key, items));
            return ResultHelper.BoolArray(obj);
        }


        /// <summary>
        /// insert the multiple items in bloomfilter
        /// </summary>
        /// <param name="key"></param>
        /// <param name="param"></param>
        /// <param name="items"></param>
        /// <returns>bool array; Success: true, fail: false</returns>
        public bool[] bfinsert(string key, BfinsertParams param, params string[] items)
        {
            byte[][] byte_items = new byte[items.Length][];
            for (int i = 0; i < items.Length; i++)
            {
                byte_items[i] = Encoding.UTF8.GetBytes(items[i]);
            }

            return bfinsert(Encoding.UTF8.GetBytes(key), param, byte_items);
        }

        public bool[] bfinsert(byte[] key, BfinsertParams param, params byte[][] items)
        {
            var obj = getRedis().Execute(ModuleCommand.BFINSERT, param.getByteParams(key, items));

            return ResultHelper.BoolArray(obj);
        }

        /// <summary>
        /// insert the multiple items in bloomfilter.{key} [CAPACITY {cap}] [ERROR {error}] ITEMS {item...}.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="initCapacityTag">the initCapacityTag: "CAPACITY"</param>
        /// <param name="initCapacity">the initCapacity</param>
        /// <param name="errorRateTag">the errorRateTag: "ERROR"</param>
        /// <param name="errorRate">the errorRate</param>
        /// <param name="itemTag">the itemTag: "ITEMS"</param>
        /// <param name="items">the items: item [item...]</param>
        /// <returns></returns>
        public bool[] bfinsert(string key, string initCapacityTag, long initCapacity, string errorRateTag,
            double errorRate, string itemTag, params string[] items)
        {
            BfinsertParams param = new BfinsertParams();
            byte[][] metadata = param.getByteParamsMeta(key, initCapacityTag, initCapacity.ToString(), errorRateTag,
                errorRate.ToString(), itemTag);
            var obj = getRedis().Execute(ModuleCommand.BFINSERT, param.getByteParams(metadata, items));
            return ResultHelper.BoolArray(obj);
        }

        /// <summary>
        /// insert the multiple items in bloomfilter. It doesn't create bloomfilter, if bloomfilter isn't exist.{key} [NOCREATE] ITEMS {item...}.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="nocreateTag">the nocreateTag: "NOCREATE"</param>
        /// <param name="itemTag">the itemTag: "ITEMS"</param>
        /// <param name="items"></param>
        /// <returns></returns>
        public bool[] bfinsert(string key, string nocreateTag, string itemTag, params string[] items)
        {
            BfinsertParams param = new BfinsertParams();
            byte[][] metadata = param.getByteParamsMeta(key, nocreateTag, itemTag);
            var obj = getRedis().Execute(ModuleCommand.BFINSERT, param.getByteParams(metadata, items));
            return ResultHelper.BoolArray(obj);
        }

        public bool[] bfinsert(string key, string itemTag, params string[] items)
        {
            BfinsertParams param = new BfinsertParams();
            byte[][] metadata = param.getByteParamsMeta(key, itemTag);
            var obj = getRedis().Execute(ModuleCommand.BFINSERT, param.getByteParams(metadata, items));
            return ResultHelper.BoolArray(obj);
        }

        public bool[] bfinsert(byte[] key, byte[] initCapacityTag, long initCapacity, byte[] errorRateTag,
            double errorRate, byte[] itemTag, params byte[][] items)
        {
            BfinsertParams param = new BfinsertParams();
            byte[][] metadata = param.getByteParamsMeta(key, initCapacityTag,
                Encoding.UTF8.GetBytes(initCapacity.ToString()), errorRateTag,
                Encoding.UTF8.GetBytes(errorRate.ToString()), itemTag);
            var obj = getRedis().Execute(ModuleCommand.BFINSERT, param.getByteParams(metadata, items));
            return ResultHelper.BoolArray(obj);
        }

        public bool[] bfinsert(byte[] key, byte[] nocreateTag, byte[] itemTag, params byte[][] items)
        {
            BfinsertParams param = new BfinsertParams();
            byte[][] metadata = param.getByteParamsMeta(key, nocreateTag, itemTag);
            var obj = getRedis().Execute(ModuleCommand.BFINSERT, param.getByteParams(metadata, items));
            return ResultHelper.BoolArray(obj);
        }

        public bool[] bfinsert(byte[] key, byte[] itemTag, params byte[][] items)
        {
            BfinsertParams param = new BfinsertParams();
            byte[][] metadata = param.getByteParamsMeta(key, itemTag);
            var obj = getRedis().Execute(ModuleCommand.BFINSERT, param.getByteParams(metadata, items));
            return ResultHelper.BoolArray(obj);
        }


        /// <summary>
        /// debug the bloomfilter.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> bfdebug(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.BFDEBUG, key);
            return ResultHelper.ListString(obj);
        }

        public List<string> bfdebug(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.BFDEBUG, key);
            return ResultHelper.ListString(obj);
        }
    }
}