using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;
using AlibabaCloud.TairSDK.TairZset.Param;
using AlibabaCloud.TairSDK.Util;


namespace AlibabaCloud.TairSDK.TairZset
{
    public class TairZset
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairZset(ConnectionMultiplexer conn, int num = 0)
        {
            _conn = conn;
            _redis = _conn.GetDatabase(num);
        }

        private IDatabase getRedis()
        {
            return _redis;
        }

        /// <summary>
        /// Adds all the specified members with the specified (multi)scores to the tairzset stored at key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <param name="scores"></param>
        /// <returns></returns>
        public long exzadd(string key, string member, params double[] scores)
        {
            string mscore = ZsetHelper.joinScoresToString(scores);
            return exzadd(key, mscore, member);
        }

        public long exzadd(byte[] key, byte[] member, params double[] scores)
        {
            string mscore = ZsetHelper.joinScoresToString(scores);
            return exzadd(key, Encoding.UTF8.GetBytes(mscore), member);
        }

        public long exzadd(string key, string score, string member)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZADD, key, score, member);
            return ResultHelper.Long(obj);
        }

        public long exzadd(byte[] key, byte[] score, byte[] member)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZADD, key, score, member);
            return ResultHelper.Long(obj);
        }

        public long exzadd(string key, string score, string member, ExzaddParams param)
        {
            return exzadd(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(score), Encoding.UTF8.GetBytes(member),
                param);
        }

        public long exzadd(byte[] key, byte[] score, byte[] member, ExzaddParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZADD, param.getByteParams(key, score, member));
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Increments the score of member int the tairzset stored at key by increment.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="increment"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public string exzincrBy(string key, string increment, string member)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZINCRBY, key, increment, member);
            return ResultHelper.String(obj);
        }

        public byte[] exzincrBy(byte[] key, byte[] increment, byte[] member)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZINCRBY, key, increment, member);
            return ResultHelper.ByteArray(obj);
        }

        public string exincrBy(string key, string member, params double[] scores)
        {
            string mscore = ZsetHelper.joinScoresToString(scores);
            var obj = getRedis().Execute(ModuleCommand.EXZINCRBY, key, mscore, member);
            return ResultHelper.String(obj);
        }

        public byte[] exincrBy(byte[] key, byte[] member, params double[] scores)
        {
            string mscore = ZsetHelper.joinScoresToString(scores);
            var obj = getRedis().Execute(ModuleCommand.EXZINCRBY, key, Encoding.UTF8.GetBytes(mscore), member);
            return ResultHelper.ByteArray(obj);
        }

        /// <summary>
        /// Return the score of member in the tairzset at key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public string exzscore(string key, string member)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZSCORE, key, member);
            return ResultHelper.String(obj);
        }

        public byte[] exzscore(byte[] key, byte[] member)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZSCORE, key, member);
            return ResultHelper.ByteArray(obj);
        }

        /// <summary>
        /// Returns the specified range of elements in the sorted set stored at key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public List<string> exzrange(string key, long min, long max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZRANGE, key, min, max);
            return ResultHelper.ListString(obj);
        }

        public List<byte[]> exzrange(byte[] key, long min, long max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZRANGE, key, Encoding.UTF8.GetBytes(min.ToString()),
                Encoding.UTF8.GetBytes(max.ToString()));
            return ResultHelper.ListByte(obj);
        }

        public List<string> exzrangeWithScores(string key, long min, long max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZRANGE, key, min, max, "WITHSCORES");
            return ResultHelper.ListString(obj);
        }

        public List<byte[]> exzrangeWithScores(byte[] key, long min, long max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZRANGE, key, Encoding.UTF8.GetBytes(min.ToString()),
                Encoding.UTF8.GetBytes(max.ToString()), Encoding.UTF8.GetBytes("WITHSCORES"));
            return ResultHelper.ListByte(obj);
        }

        /// <summary>
        /// Returns the specified range of elements in the sorted set stored set stored at key.
        /// The elements are considered to ordered from the highest to the lowest score.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public List<string> exzrevrange(string key, long min, long max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREVRANGE, key, min, max);
            return ResultHelper.ListString(obj);
        }

        public List<byte[]> exzrevrange(byte[] key, long min, long max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREVRANGE, key, Encoding.UTF8.GetBytes(min.ToString()),
                Encoding.UTF8.GetBytes(max.ToString()));
            return ResultHelper.ListByte(obj);
        }

        public List<string> exzrevrangeWithScores(string key, long min, long max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREVRANGE, key, min, max, "WITHSOCRES");
            return ResultHelper.ListString(obj);
        }

        public List<byte[]> exzrevrangeWithScores(byte[] key, long min, long max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREVRANGE, key, Encoding.UTF8.GetBytes(min.ToString()),
                Encoding.UTF8.GetBytes(max.ToString()), Encoding.UTF8.GetBytes("WITHSCORES"));
            return ResultHelper.ListByte(obj);
        }

        /// <summary>
        /// Returns all the elements in the tairzset at key with a score between min and max(including elements with
        /// scores equal to min or max).The elements are considered to be ordered from low to high scores.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public List<string> exzrangeByScore(string key, string min, string max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZRANGEBYSCORE, key, min, max);
            return ResultHelper.ListString(obj);
        }

        public List<byte[]> exzrangeByScore(byte[] key, string min, string max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZRANGEBYSCORE, key, Encoding.UTF8.GetBytes(min),
                Encoding.UTF8.GetBytes(max));
            return ResultHelper.ListByte(obj);
        }

        public List<string> exzrangeByScore(string key, string min, string max, ExzrangeParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZRANGEBYSCORE,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(min),
                    Encoding.UTF8.GetBytes(max)));
            return ResultHelper.ListString(obj);
        }

        public List<byte[]> exzrangeByScore(byte[] key, byte[] min, byte[] max, ExzrangeParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZRANGEBYSCORE, param.getByteParams(key, min, max));
            return ResultHelper.ListByte(obj);
        }

        /// <summary>
        /// Returns all the elements in the tairzset at key with a score between max and min (including elements with score
        /// equal to max or min).In contrary to the default ordering of tairzsets,for this command the elements are
        /// considered to be ordered from high to low scores.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public List<string> exzrevrangeByScore(string key, string min, string max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREVRANGEBYSCORE, key, min, max);
            return ResultHelper.ListString(obj);
        }

        public List<byte[]> exzrevrangeByScore(byte[] key, string min, string max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREVRANGEBYSCORE, key, Encoding.UTF8.GetBytes(min),
                Encoding.UTF8.GetBytes(max));
            return ResultHelper.ListByte(obj);
        }

        public List<string> exzrevrangeByScore(string key, string min, string max, ExzrangeParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREVRANGEBYSCORE,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(min),
                    Encoding.UTF8.GetBytes(max)));
            return ResultHelper.ListString(obj);
        }

        public List<byte[]> exzrevrangeByScore(byte[] key, byte[] min, byte[] max, ExzrangeParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREVRANGEBYSCORE, param.getByteParams(key, min, max));
            return ResultHelper.ListByte(obj);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public List<string> exzrangeByLex(string key, string min, string max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZRANGEBYLEX, key, min, max);
            return ResultHelper.ListString(obj);
        }

        public List<byte[]> exzrangeByLex(byte[] key, string min, string max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZRANGEBYLEX, key, Encoding.UTF8.GetBytes(min),
                Encoding.UTF8.GetBytes(max));
            return ResultHelper.ListByte(obj);
        }

        public List<string> exzrangeByLex(string key, string min, string max, ExzrangeParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZRANGEBYLEX,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(min),
                    Encoding.UTF8.GetBytes(max)));
            return ResultHelper.ListString(obj);
        }

        public List<byte[]> exzrangeByLex(byte[] key, byte[] min, byte[] max, ExzrangeParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZRANGEBYLEX, param.getByteParams(key, min, max));
            return ResultHelper.ListByte(obj);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public List<string> exzrevrangeByLex(string key, string min, string max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREVRANGEBYLEX, key, min, max);
            return ResultHelper.ListString(obj);
        }

        public List<byte[]> exzrevrangeByLex(byte[] key, string min, string max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREVRANGEBYLEX, key, Encoding.UTF8.GetBytes(min),
                Encoding.UTF8.GetBytes(max));
            return ResultHelper.ListByte(obj);
        }

        public List<string> exzrevrangeByLex(string key, string min, string max, ExzrangeParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREVRANGEBYLEX,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(min),
                    Encoding.UTF8.GetBytes(max)));
            return ResultHelper.ListString(obj);
        }

        public List<byte[]> exzrevrangeByLex(byte[] key, byte[] min, byte[] max, ExzrangeParams param)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREVRANGEBYLEX, param.getByteParams(key, min, max));
            return ResultHelper.ListByte(obj);
        }


        /// <summary>
        /// Removes the specified members from the tairzset sorted at key.Non existing members are ignored.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="members"></param>
        /// <returns></returns>
        public long exzrem(string key, params string[] members)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREM, JoinParameters.joinParameters(key, members));
            return ResultHelper.Long(obj);
        }

        public long exzrem(byte[] key, params byte[][] members)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREM, JoinParameters.joinParameters(key, members));
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Removes all elements in the tairzset stored at key with a score between min and max(inclusive).
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public long exzremrangeByScore(string key, string min, string max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREMRANGEBYSCORE, key, min, max);
            return ResultHelper.Long(obj);
        }

        public long exzremrangeByScore(byte[] key, byte[] min, byte[] max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREMRANGEBYSCORE, key, min, max);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Removes all elements in the tairzset stored at key with rank between start and stop.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public long exzremrangeByRank(string key, long start, long stop)
        {
            return exzremrangeByRank(Encoding.UTF8.GetBytes(key), start, stop);
        }

        public long exzremrangeByRank(byte[] key, long start, long stop)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREMRANGEBYRANK, key, Encoding.UTF8.GetBytes(start.ToString()),
                Encoding.UTF8.GetBytes(stop.ToString()));
            return ResultHelper.Long(obj);
        }


        /// <summary>
        /// When all the elements in a sorted set are inserted with the same score, in order to force lexicographical
        /// ordering, this command removes all elements in the sorted set stored at key between the lexicographical range
        /// specified by min and max.
        ///
        /// The meaning of min and max are the same of the ZRANGEBYLEX command. Similarly, this command actually removes
        /// * the same elements that ZRANGEBYLEX would return if called with the same min and max arguments.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public long exzremrangeByLex(string key, string min, string max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREMRANGEBYLEX, key, min, max);
            return ResultHelper.Long(obj);
        }

        public long exzremrangeByLex(byte[] key, byte[] min, byte[] max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREMRANGEBYLEX, key, min, max);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Returns the tairzset cardinality(number of elements) of the tairzset stored at key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long exzcard(string key)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZCARD, key);
            return ResultHelper.Long(obj);
        }

        public long exzcard(byte[] key)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZCARD, key);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Returns the rank of member in the tairzset stored at key,with the scores ordered from low to high.
        /// The rank (or index) is 0-based,which means that the member with the lowest score has rank 0.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public long exzrank(string key, string member)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZRANK, key, member);
            return ResultHelper.Long(obj);
        }

        public long exzrank(byte[] key, byte[] member)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZRANK, key, member);
            return ResultHelper.Long(obj);
        }

        public long exzrevrank(string key, string member)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREVRANK, key, member);
            return ResultHelper.Long(obj);
        }

        public long exzrevrank(byte[] key, byte[] member)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREVRANK, key, member);
            return ResultHelper.Long(obj);
        }


        /// <summary>
        /// Returns the number of elements in the tairzset at key with a score between min and max.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public long exzcount(string key, string min, string max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZCOUNT, key, min, max);
            return ResultHelper.Long(obj);
        }

        public long exzcount(byte[] key, byte[] min, byte[] max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZCOUNT, key, min, max);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// When all the elements in a tairzset are inserted with the same score, in order to force lexicographical ordering,
        /// this command returns the number of elements in the tairzset at key with a value between min and max.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public long exzlexcount(string key, string min, string max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZLEXCOUNT, key, min, max);
            return ResultHelper.Long(obj);
        }

        public long exzlexcount(byte[] key, byte[] min, byte[] max)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZLEXCOUNT, key, min, max);
            return ResultHelper.Long(obj);
        }

        /// <summary>
        /// Same with zrank,but use score to get rank,when the field corresponding to score does not exist,an estimate is used.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public long exzrankByScore(string key, string score)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZRANKBYSCORE, key, score);
            return ResultHelper.Long(obj);
        }

        public long exzrankByScore(byte[] key, byte[] score)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZRANKBYSCORE, key, score);
            return ResultHelper.Long(obj);
        }

        public long exzrevrankByScore(string key, string score)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREVRANKBYSCORE, key, score);
            return ResultHelper.Long(obj);
        }

        public long exzrevrankByScore(byte[] key, byte[] score)
        {
            var obj = getRedis().Execute(ModuleCommand.EXZREVRANKBYSCORE, key, score);
            return ResultHelper.Long(obj);
        }
    }
}