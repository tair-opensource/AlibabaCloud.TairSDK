using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;
using AlibabaCloud.TairSDK.Util;


namespace AlibabaCloud.TairSDK.TairRoaring
{
    public class TairRoaring
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairRoaring(ConnectionMultiplexer conn, int num = 0)
        {
            _conn = conn;
            _redis = _conn.GetDatabase(num);
        }

        private IDatabase getRedis()
        {
            return _redis;
        }

        /// <summary>
        /// TR.SETBIT    TR.SETBIT <key> <offset> <value>
        /// setting the value at the offset in roaringbitmap
        /// </summary>
        /// <param name="key">roaring key</param>
        /// <param name="offset">the bit offset</param>
        /// <param name="value">the bit value</param>
        /// <returns>Success: long; Fail: error</returns>
        public long trsetbit(string key, long offset, string value)
        {
            var obj = getRedis().Execute(ModuleCommand.TRSETBIT, Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(offset.ToString()), Encoding.UTF8.GetBytes(value));
            return ResultHelper.Long(obj);
        }

        public long trsetbit(string key, long offset, long value)
        {
            var obj = getRedis().Execute(ModuleCommand.TRSETBIT, Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(offset.ToString()), Encoding.UTF8.GetBytes(value.ToString()));
            return ResultHelper.Long(obj);
        }

        public long trsetbit(byte[] key, long offset, byte[] value)
        {
            var obj = getRedis().Execute(ModuleCommand.TRSETBIT, key,
                Encoding.UTF8.GetBytes(offset.ToString()), value);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        ///  TR.SETBITS    TR.SETBITS <key> <offset> [<offset2> <offset3> ... <offsetn>]
        /// setting the value at the offset in roaringbitmap
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <returns>Success: long; Fail: error</returns>
        public long trsetbits(string key, params long[] fields)
        {
            var obj = getRedis().Execute(ModuleCommand.TRSETBITS,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(key), RoaringHelper.longTobyte(fields)));
            return ResultHelper.Long(obj);
        }

        public long trsetbits(byte[] key, params long[] fields)
        {
            var obj = getRedis().Execute(ModuleCommand.TRSETBITS,
                JoinParameters.joinParameters(key, RoaringHelper.longTobyte(fields)));
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// TR.CLEARBITS    TR.CLEARBITS <key> <offset> [<offset2> <offset3> ... <offsetn>]
        /// remove the value at the offset in roaringbitmap
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns>Success: long; Fail: error</returns>
        public long trclearbits(string key, params long[] fields)
        {
            var obj = getRedis().Execute(ModuleCommand.TRCLEARBITS,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(key), RoaringHelper.longTobyte(fields)));
            return ResultHelper.Long(obj);
        }

        public long trclearbits(byte[] key, params long[] fields)
        {
            var obj = getRedis().Execute(ModuleCommand.TRCLEARBITS,
                JoinParameters.joinParameters(key, RoaringHelper.longTobyte(fields)));
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// R.SETRANGE TR.SETRANGE <key> <start> <end>
        /// set all the elements between min (included) and max (included).
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Success: long; Fail: error</returns>
        public long trsetrange(string key, long start, long end)
        {
            var obj = getRedis().Execute(ModuleCommand.TRSETRANGE, key, start.ToString(), end.ToString());
            return ResultHelper.Long(obj);
        }

        public long trsetrange(byte[] key, long start, long end)
        {
            var obj = getRedis().Execute(ModuleCommand.TRSETRANGE, key, Encoding.UTF8.GetBytes(start.ToString()),
                Encoding.UTF8.GetBytes(end.ToString()));
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// TR.APPENDBITARRAY TR.APPENDBITARRAY <key> <offset> <value>
        /// append the bit array after the offset in roaringbitmap
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <returns>Success: long; Fail: error</returns>
        public long trappendbitarray(string key, long offset, string value)
        {
            var obj = getRedis().Execute(ModuleCommand.TRAPPENDBITARRAY, key, offset.ToString(), value);
            return ResultHelper.Long(obj);
        }

        public long trappendbitarray(string key, long offset, byte[] value)
        {
            var obj = getRedis().Execute(ModuleCommand.TRAPPENDBITARRAY, Encoding.UTF8.GetBytes(key),
                Encoding.UTF8.GetBytes(offset.ToString(), value));
            return ResultHelper.Long(obj);
        }

        public long trappendbitarray(byte[] key, long offset, byte[] value)
        {
            var obj = getRedis().Execute(ModuleCommand.TRAPPENDBITARRAY, key,
                Encoding.UTF8.GetBytes(offset.ToString(), value));
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// TR.FLIPRANGE TR.FLIPRANGE <key> <start> <end>
        /// flip all elements in the roaring bitmap within a specified interval: [range_start, range_end].
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Success: long; Fail: error</returns>
        public long trfliprange(string key, long start, long end)
        {
            var obj = getRedis().Execute(ModuleCommand.TRFLIPRANGE, key, start.ToString(), end.ToString());
            return ResultHelper.Long(obj);
        }

        public long trfliprange(byte[] key, long start, long end)
        {
            var obj = getRedis().Execute(ModuleCommand.TRFLIPRANGE, key, Encoding.UTF8.GetBytes(start.ToString()),
                Encoding.UTF8.GetBytes(end.ToString()));
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// TR.APPENDINTARRAY	TR.APPENDINTARRAY <key> <value1> [<value2> <value3> ... <valueN>]
        /// add elements to the roaring bitmap.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields">bit offset value</param>
        /// <returns>Success: +OK; Fail: error</returns>
        public string trappendintarray(string key, params long[] fields)
        {
            var obj = getRedis().Execute(ModuleCommand.TRAPPENDINTARRAY,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(key), RoaringHelper.longTobyte(fields)));
            return ResultHelper.String(obj);
        }

        public string trappendintarray(byte[] key, params long[] fields)
        {
            var obj = getRedis().Execute(ModuleCommand.TRAPPENDINTARRAY,
                JoinParameters.joinParameters(key, RoaringHelper.longTobyte(fields)));
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// TR.SETINTARRAY	TR.SETINTARRAY <key> <value1> [<value2> <value3> ... <valueN>]
        /// reset the bitmap by given integer array.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns>Success: +OK; Fail: error</returns>
        public string trsetintarray(string key, params long[] fields)
        {
            var obj = getRedis().Execute(ModuleCommand.TRSETINTARRAY,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(key), RoaringHelper.longTobyte(fields)));
            return ResultHelper.String(obj);
        }

        public string trsetintarray(byte[] key, params long[] fields)
        {
            var obj = getRedis().Execute(ModuleCommand.TRSETINTARRAY,
                JoinParameters.joinParameters(key, RoaringHelper.longTobyte(fields)));
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// TR.SETBITARRAY	TR.SETBITARRAY <key> <value>
        /// reset the roaring bitmap by given 01-bit string bitset.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>Success: +OK; Fail: error</returns>
        public string trsetbitarray(string key, string value)
        {
            var obj = getRedis().Execute(ModuleCommand.TRSETBITARRAY, key, value);
            return ResultHelper.String(obj);
        }

        public string trsetbitarray(byte[] key, byte[] value)
        {
            var obj = getRedis().Execute(ModuleCommand.TRSETBITARRAY, key, value);
            return ResultHelper.String(obj);
        }


        /// <summary>
        /// TR.BITOP	TR.BITOP <destkey> <operation> <key> [<key2> <key3>...]
        /// call bitset computation on given roaring bitmaps, store the result into destkey
        /// return the cardinality of result.
        /// operation can be passed to AND OR XOR NOT DIFF.
        /// </summary>
        /// <param name="destkey"></param>
        /// <param name="operation"></param>
        /// <param name="keys"></param>
        /// <returns>Success:long;Fail:error</returns>
        public long trbitop(string destkey, string operation, params string[] keys)
        {
            var obj = getRedis().Execute(ModuleCommand.TRBITOP,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(destkey), Encoding.UTF8.GetBytes(operation),
                    RoaringHelper.encodemany(keys)));
            return ResultHelper.Long(obj);
        }

        public long trbitop(byte[] destkey, byte[] operation, params byte[][] keys)
        {
            var obj = getRedis().Execute(ModuleCommand.TRBITOP,
                JoinParameters.joinParameters(destkey, operation, keys));
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// R.BITOPCARD	TR.BITOPCARD <operation> <key> [<key2> <key3>...]
        /// call bitset computation on given roaring bitmaps, return the cardinality of result.
        /// operation can be passed to AND OR XOR NOT DIFF.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="keys"></param>
        /// <returns>Success: long; Fail: error</returns>
        public long trbitopcard(string operation, params string[] keys)
        {
            var obj = getRedis().Execute(ModuleCommand.TRBITOPCARD,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(operation), RoaringHelper.encodemany(keys)));
            return ResultHelper.Long(obj);
        }

        public long trbitopcard(byte[] operation, params byte[][] keys)
        {
            var obj = getRedis().Execute(ModuleCommand.TRBITOPCARD,
                JoinParameters.joinParameters(operation, keys));
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// TR.OPTIMIZE	TR.OPTIMIZE <key>
        /// optimize memory usage by trying to use RLE container instead of int array or bitset.
        /// it will also run shrink_to_fit on bitmap, this may cause memory reallocation.
        /// optimize will try this function but did not make a guarantee that any change would happen
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Success: +OK; Fail: error</returns>
        public string troptimize(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.TROPTIMIZE, key);
            return ResultHelper.String(obj);
        }

        public string troptimize(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.TROPTIMIZE, key);
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// TR.GETBIT    TR.GETBIT <key> <offset>
        /// getting the bit on the offset of roaringbitmap
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <returns>Success: long; Fail: error</returns>
        public long trgetbit(string key, long offset)
        {
            var obj = getRedis().Execute(ModuleCommand.TRGETBIT, key, offset.ToString());
            return ResultHelper.Long(obj);
        }

        public long trgetbit(byte[] key, long offset)
        {
            var obj = getRedis().Execute(ModuleCommand.TRGETBIT, key, Encoding.UTF8.GetBytes(offset.ToString()));
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// TR.GETBITS    TR.GETBITS <key> <offset> [<offset2> <offset3> ... <offsetn>]
        /// get the value at the offset in roaringbitmap
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns>Success: array long; Fail: error</returns>
        public List<long> trgetbits(string key, params long[] fields)
        {
            var obj = getRedis().Execute(ModuleCommand.TRGETBITS,
                JoinParameters.joinParameters(Encoding.UTF8.GetBytes(key), RoaringHelper.longTobyte(fields)));
            return ResultHelper.ListLong(obj);
        }

        public List<long> trgetbits(byte[] key, params long[] fields)
        {
            var obj = getRedis().Execute(ModuleCommand.TRGETBITS,
                JoinParameters.joinParameters(key, RoaringHelper.longTobyte(fields)));
            return ResultHelper.ListLong(obj);
        }

        /// <summary>
        /// TR.BITCOUNT	TR.BITCOUNT <key> [<start> <end>]
        /// counting bit set as 1 in the roaringbitmap
        /// start and end are optional, you can count 1-bit in range by passing start and end
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Success: long; Fail: error</returns>
        public long trbitcount(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.TRBITCOUNT, key);
            return ResultHelper.Long(obj);
        }

        public long trbitcount(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.TRBITCOUNT, key);
            return ResultHelper.Long(obj);
        }

        public long trbicount(string key, long start, long end)
        {
            var obj = getRedis().Execute(ModuleCommand.TRBITCOUNT, key, start.ToString(), end.ToString());
            return ResultHelper.Long(obj);
        }

        public long trbitcount(byte[] key, long start, long end)
        {
            var obj = getRedis().Execute(ModuleCommand.TRBITCOUNT, key, Encoding.UTF8.GetBytes(start.ToString()),
                Encoding.UTF8.GetBytes(end.ToString()));
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// TR.BITPOS	TR.BITPOS <key> <value> [counting]
        /// return the first element set as value at index, where the smallest element is at index 0.
        /// counting is an optional argument, you can pass positive Counting to indicate the command count for the n-th element from the top
        /// or pass an negative Counting to count from the n-th element form the bottom.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>Success: long; Fail: error</returns>
        public long trbitpos(string key, string value)
        {
            var obj = getRedis().Execute(ModuleCommand.TRBITPOS, key, value);
            return ResultHelper.Long(obj);
        }

        public long trbitpos(string key, long value)
        {
            var obj = getRedis().Execute(ModuleCommand.TRBITPOS, key, value.ToString());
            return ResultHelper.Long(obj);
        }

        public long trbitpos(byte[] key, byte[] value)
        {
            var obj = getRedis().Execute(ModuleCommand.TRBITPOS, key, value);
            return ResultHelper.Long(obj);
        }

        public long trbitpos(string key, string value, long count)
        {
            var obj = getRedis().Execute(ModuleCommand.TRBITPOS, key, value, count.ToString());
            return ResultHelper.Long(obj);
        }

        public long trbitpos(string key, long value, long count)
        {
            var obj = getRedis().Execute(ModuleCommand.TRBITPOS, key, value.ToString(), count.ToString());
            return ResultHelper.Long(obj);
        }

        public long trbitpos(byte[] key, byte[] value, byte[] count)
        {
            var obj = getRedis().Execute(ModuleCommand.TRBITPOS, key, value, count);
            return ResultHelper.Long(obj);
        }


        /// <summary>
        /// TR.SCAN TR.SCAN <key> <cursor> [COUNT <count>]
        /// iterating element from cursor, COUNT indecate the max elements count per request
        /// return cursor as 0 indicates the iteration reached the end.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cursor"></param>
        /// <returns>Success: cursor and array long; Fail: error</returns>
        public ScanResult<long> trscan(string key, long cursor)
        {
            var obj = getRedis().Execute(ModuleCommand.TRSCAN, key, cursor);
            return RoaringHelper.ScanResultLong(obj);
        }

        public ScanResult<long> trscan(string key, long cursor, long count)
        {
            var obj = getRedis().Execute(ModuleCommand.TRSCAN, key, cursor.ToString(), "COUNT", count.ToString());
            return RoaringHelper.ScanResultLong(obj);
        }

        public ScanResult<byte[]> trscan(byte[] key, byte[] cursor)
        {
            var obj = getRedis().Execute(ModuleCommand.TRSCAN, key, cursor);
            return RoaringHelper.ScanResultByte(obj);
        }

        public ScanResult<byte[]> trscan(byte[] key, byte[] cursor, byte[] count)
        {
            var obj = getRedis().Execute(ModuleCommand.TRSCAN, key, cursor, Encoding.UTF8.GetBytes("COUNT"), count);
            return RoaringHelper.ScanResultByte(obj);
        }


        /// <summary>
        /// TR.SETRANGE TR.SETRANGE <key> <start> <end>
        /// set all the elements between min (included) and max (included).
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Success: long; Fail: error</returns>
        public List<long> trrange(string key, long start, long end)
        {
            var obj = getRedis().Execute(ModuleCommand.TRRANGE, key, start.ToString(), end.ToString());
            return ResultHelper.ListLong(obj);
        }

        public List<long> trrange(byte[] key, long start, long end)
        {
            var obj = getRedis().Execute(ModuleCommand.TRRANGE, key,
                Encoding.UTF8.GetBytes(start.ToString(), Encoding.UTF8.GetBytes(end.ToString())));
            return ResultHelper.ListLong(obj);
        }


        /// <summary>
        /// TR.RANGEBITARRAY TR.RANGEBITARRAY <key> <start> <end>
        /// retrive the setted bit between the closed range, return them by the string bit-array
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Success: string; Fail: error</returns>
        public string trrangebitarray(string key, long start, long end)
        {
            var obj = getRedis().Execute(ModuleCommand.TRRANGEBITARRAY, key, start.ToString(), end.ToString());
            return ResultHelper.String(obj);
        }

        public string trrangebitarray(byte[] key, long start, long end)
        {
            var obj = getRedis().Execute(ModuleCommand.TRRANGEBITARRAY, key, Encoding.UTF8.GetBytes(start.ToString()),
                Encoding.UTF8.GetBytes(end.ToString()));
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// TR.MIN	TR.MIN <key>
        /// return the minimum element's offset set in the roaring bitmap
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Success: long; Fail: error</returns>
        public long trmin(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.TRMIN, key);
            return ResultHelper.Long(obj);
        }

        public long trmin(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.TRMIN, key);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// TR.MAX	TR.MAX <key>
        /// return the maximum element's offset set in the roaring bitmap
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Success: long; Fail: error</returns>
        public long trmax(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.TRMAX, key);
            return ResultHelper.Long(obj);
        }

        public long trmax(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.TRMAX, key);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// TR.STAT	TR.STAT <key>
        /// return roaring bitmap statistic information, you can get JSON formatted result by passing json = true.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="json"></param>
        /// <returns>Success: string; Fail: error</returns>
        public string trstat(string key, bool json)
        {
            if (json)
            {
                var obj_json = getRedis().Execute(ModuleCommand.TRSTAT, key, "JSON");
                return ResultHelper.String(obj_json);
            }

            var obj = getRedis().Execute(ModuleCommand.TRSTAT, key);
            return ResultHelper.String(obj);
        }

        public string trstat(byte[] key, bool json)
        {
            if (json)
            {
                var obj_json = getRedis().Execute(ModuleCommand.TRSTAT, key, Encoding.UTF8.GetBytes("JSON"));
                return ResultHelper.String(obj_json);
            }

            var obj = getRedis().Execute(ModuleCommand.TRSTAT, key);
            return ResultHelper.String(obj);
        }

        /// <summary>
        /// TR.JACCARD TR.JACCARD <key1> <key2>
        /// caculate roaringbitmap Jaccard index on key1 and key2.
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns>Success: double; Fail: error</returns>
        public double trjaccard(string key1, string key2)
        {
            var obj = getRedis().Execute(ModuleCommand.TRJACCARD, key1, key2);
            return ResultHelper.Double(obj);
        }

        public double trjaccard(byte[] key1, byte[] key2)
        {
            var obj = getRedis().Execute(ModuleCommand.TRJACCARD, key1, key2);
            return ResultHelper.Double(obj);
        }

        /// <summary>
        /// TR.CONTAINS TR.CONTAINS <key1> <key2>
        /// return wether roaring bitmap key1 is a sub-set of key2
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns>Success: bool; Fail: error</returns>
        public bool trcontains(string key1, string key2)
        {
            var obj = getRedis().Execute(ModuleCommand.TRCONTAINS, key1, key2);
            return ResultHelper.Bool(obj);
        }

        public bool trcontains(byte[] key1, byte[] key2)
        {
            var obj = getRedis().Execute(ModuleCommand.TRCONTAINS, key1, key2);
            return ResultHelper.Bool(obj);
        }

        /// <summary>
        /// TR.RANK TR.RANK <key> <offset>
        /// rank returns the number of elements that are smaller or equal to offset.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <returns>Success: long; Fail: error</returns>
        public long trrank(string key, long offset)
        {
            var obj = getRedis().Execute(ModuleCommand.TRRANK, key, offset.ToString());
            return ResultHelper.Long(obj);
        }

        public long trrank(byte[] key, byte[] offset)
        {
            var obj = getRedis().Execute(ModuleCommand.TRRANK, key, offset);
            return ResultHelper.Long(obj);
        }
    }
}