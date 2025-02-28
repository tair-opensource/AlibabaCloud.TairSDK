using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading.Tasks;
using AlibabaCloud.TairSDK.TairZset.Param;
using AlibabaCloud.TairSDK.Util;
using StackExchange.Redis;

namespace AlibabaCloud.TairSDK.TairZset
{
    public class TairZsetAsync
    {
        private readonly ConnectionMultiplexer _conn;
        private readonly IDatabase _redis;

        public TairZsetAsync(ConnectionMultiplexer conn, int num = 0)
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
        /// <param name="member"></param>
        /// <param name="scores"></param>
        /// <returns></returns>
        public Task<RedisResult> exzadd(string key, string member, params double[] scores)
        {
            string mscore = ZsetHelper.joinScoresToString(scores);
            return exzadd(key, mscore, member);
        }

        public Task<RedisResult> exzadd(byte[] key, byte[] member, params double[] scores)
        {
            string mscore = ZsetHelper.joinScoresToString(scores);
            return exzadd(key, Encoding.UTF8.GetBytes(mscore), member);
        }

        public Task<RedisResult> exzadd(string key, string score, string member)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZADD, key, score, member);
            return obj;
        }

        public Task<RedisResult> exzadd(byte[] key, byte[] score, byte[] member)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZADD, key, score, member);
            return obj;
        }

        public Task<RedisResult> exzadd(string key, string score, string member, ExzaddParams param)
        {
            return exzadd(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(score), Encoding.UTF8.GetBytes(member),
                param);
        }

        public Task<RedisResult> exzadd(byte[] key, byte[] score, byte[] member, ExzaddParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZADD, param.getByteParams(key, score, member));
            return obj;
        }

        public Task<RedisResult> exzadd(string key, Dictionary<string, double> members)
        {
            var param = new List<byte[]> { Encoding.UTF8.GetBytes(key) };
            foreach (var member in members)
            {
                param.Add(Encoding.UTF8.GetBytes(member.Value.ToString()));
                param.Add(Encoding.UTF8.GetBytes(member.Key));
            }

            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZADD, param.ToArray());
            return obj;
        }

        public Task<RedisResult> exzadd(string key, Dictionary<string, double> elements, ExzaddParams param)
        {
            var byteParams = new List<byte[]>(elements.Count * 2);
            foreach (var element in elements)
            {
                byteParams.Add(Encoding.UTF8.GetBytes(element.Value.ToString()));
                byteParams.Add(Encoding.UTF8.GetBytes(element.Key));
            }

            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZADD,
                param.getByteParams(Encoding.UTF8.GetBytes(key), byteParams.ToArray()));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="increment"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public Task<RedisResult> exzincrBy(string key, string increment, string member)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZINCRBY, key, increment, member);
            return obj;
        }

        public Task<RedisResult> exzincrBy(byte[] key, byte[] increment, byte[] member)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZINCRBY, key, increment, member);
            return obj;
        }

        public Task<RedisResult> exincrBy(string key, string member, params double[] scores)
        {
            string mscore = ZsetHelper.joinScoresToString(scores);
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZINCRBY, key, mscore, member);
            return obj;
        }

        public Task<RedisResult> exincrBy(byte[] key, byte[] member, params double[] scores)
        {
            string mscore = ZsetHelper.joinScoresToString(scores);
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZINCRBY, key, Encoding.UTF8.GetBytes(mscore), member);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public Task<RedisResult> exzscore(string key, string member)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZSCORE, key, member);
            return obj;
        }

        public Task<RedisResult> exzscore(byte[] key, byte[] member)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZSCORE, key, member);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public Task<RedisResult> exzrange(string key, long min, long max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZRANGE, key, min, max);
            return obj;
        }

        public Task<RedisResult> exzrange(byte[] key, long min, long max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZRANGE, key, Encoding.UTF8.GetBytes(min.ToString()),
                Encoding.UTF8.GetBytes(max.ToString()));
            return obj;
        }

        public Task<RedisResult> exzrangeWithScores(string key, long min, long max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZRANGE, key, min, max, "WITHSCORES");
            return obj;
        }

        public Task<RedisResult> exzrangeWithScores(byte[] key, long min, long max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZRANGE, key, Encoding.UTF8.GetBytes(min.ToString()),
                Encoding.UTF8.GetBytes(max.ToString()), Encoding.UTF8.GetBytes("WITHSCORES"));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public Task<RedisResult> exzrevrange(string key, long min, long max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREVRANGE, key, min, max);
            return obj;
        }

        public Task<RedisResult> exzrevrange(byte[] key, long min, long max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREVRANGE, key, Encoding.UTF8.GetBytes(min.ToString()),
                Encoding.UTF8.GetBytes(max.ToString()));
            return obj;
        }

        public Task<RedisResult> exzrevrangeWithScores(string key, long min, long max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREVRANGE, key, min, max, "WITHSOCRES");
            return obj;
        }

        public Task<RedisResult> exzrevrangeWithScores(byte[] key, long min, long max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREVRANGE, key, Encoding.UTF8.GetBytes(min.ToString()),
                Encoding.UTF8.GetBytes(max.ToString()), Encoding.UTF8.GetBytes("WITHSCORES"));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public Task<RedisResult> exzrangeByScore(string key, string min, string max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZRANGEBYSCORE, key, min, max);
            return obj;
        }

        public Task<RedisResult> exzrangeByScore(byte[] key, string min, string max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZRANGEBYSCORE, key, Encoding.UTF8.GetBytes(min),
                Encoding.UTF8.GetBytes(max));
            return obj;
        }

        public Task<RedisResult> exzrangeByScore(string key, string min, string max, ExzrangeParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZRANGEBYSCORE,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(min),
                    Encoding.UTF8.GetBytes(max)));
            return obj;
        }

        public Task<RedisResult> exzrangeByScore(byte[] key, byte[] min, byte[] max, ExzrangeParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZRANGEBYSCORE, param.getByteParams(key, min, max));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public Task<RedisResult> exzrevrangeByScore(string key, string min, string max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREVRANGEBYSCORE, key, min, max);
            return obj;
        }

        public Task<RedisResult> exzrevrangeByScore(byte[] key, string min, string max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREVRANGEBYSCORE, key, Encoding.UTF8.GetBytes(min),
                Encoding.UTF8.GetBytes(max));
            return obj;
        }

        public Task<RedisResult> exzrevrangeByScore(string key, string min, string max, ExzrangeParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREVRANGEBYSCORE,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(min),
                    Encoding.UTF8.GetBytes(max)));
            return obj;
        }

        public Task<RedisResult> exzrevrangeByScore(byte[] key, byte[] min, byte[] max, ExzrangeParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREVRANGEBYSCORE, param.getByteParams(key, min, max));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public Task<RedisResult> exzrangeByLex(string key, string min, string max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZRANGEBYLEX, key, min, max);
            return obj;
        }

        public Task<RedisResult> exzrangeByLex(byte[] key, string min, string max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZRANGEBYLEX, key, Encoding.UTF8.GetBytes(min),
                Encoding.UTF8.GetBytes(max));
            return obj;
        }

        public Task<RedisResult> exzrangeByLex(string key, string min, string max, ExzrangeParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZRANGEBYLEX,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(min),
                    Encoding.UTF8.GetBytes(max)));
            return obj;
        }

        public Task<RedisResult> exzrangeByLex(byte[] key, byte[] min, byte[] max, ExzrangeParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZRANGEBYLEX, param.getByteParams(key, min, max));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public Task<RedisResult> exzrevrangeByLex(string key, string min, string max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREVRANGEBYLEX, key, min, max);
            return obj;
        }

        public Task<RedisResult> exzrevrangeByLex(byte[] key, string min, string max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREVRANGEBYLEX, key, Encoding.UTF8.GetBytes(min),
                Encoding.UTF8.GetBytes(max));
            return obj;
        }

        public Task<RedisResult> exzrevrangeByLex(string key, string min, string max, ExzrangeParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREVRANGEBYLEX,
                param.getByteParams(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(min),
                    Encoding.UTF8.GetBytes(max)));
            return obj;
        }

        public Task<RedisResult> exzrevrangeByLex(byte[] key, byte[] min, byte[] max, ExzrangeParams param)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREVRANGEBYLEX, param.getByteParams(key, min, max));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="members"></param>
        /// <returns></returns>
        public Task<RedisResult> exzrem(string key, params string[] members)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREM, JoinParameters.joinParameters(key, members));
            return obj;
        }

        public Task<RedisResult> exzrem(byte[] key, params byte[][] members)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREM, JoinParameters.joinParameters(key, members));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public Task<RedisResult> exzremrangeByScore(string key, string min, string max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREMRANGEBYSCORE, key, min, max);
            return obj;
        }

        public Task<RedisResult> exzremrangeByScore(byte[] key, byte[] min, byte[] max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREMRANGEBYSCORE, key, min, max);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public Task<RedisResult> exzremrangeByRank(string key, long start, long stop)
        {
            return exzremrangeByRank(Encoding.UTF8.GetBytes(key), start, stop);
        }

        public Task<RedisResult> exzremrangeByRank(byte[] key, long start, long stop)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREMRANGEBYRANK, key,
                Encoding.UTF8.GetBytes(start.ToString()),
                Encoding.UTF8.GetBytes(stop.ToString()));
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public Task<RedisResult> exzremrangeByLex(string key, string min, string max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREMRANGEBYLEX, key, min, max);
            return obj;
        }

        public Task<RedisResult> exzremrangeByLex(byte[] key, byte[] min, byte[] max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREMRANGEBYLEX, key, min, max);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<RedisResult> exzcard(string key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZCARD, key);
            return obj;
        }

        public Task<RedisResult> exzcard(byte[] key)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZCARD, key);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public Task<RedisResult> exzrank(string key, string member)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZRANK, key, member);
            return obj;
        }

        public Task<RedisResult> exzrank(byte[] key, byte[] member)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZRANK, key, member);
            return obj;
        }

        public Task<RedisResult> exzrevrank(string key, string member)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREVRANK, key, member);
            return obj;
        }

        public Task<RedisResult> exzrevrank(byte[] key, byte[] member)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREVRANK, key, member);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public Task<RedisResult> exzcount(string key, string min, string max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZCOUNT, key, min, max);
            return obj;
        }

        public Task<RedisResult> exzcount(byte[] key, byte[] min, byte[] max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZCOUNT, key, min, max);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public Task<RedisResult> exzlexcount(string key, string min, string max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZLEXCOUNT, key, min, max);
            return obj;
        }

        public Task<RedisResult> exzlexcount(byte[] key, byte[] min, byte[] max)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZLEXCOUNT, key, min, max);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public Task<RedisResult> exzrankByScore(string key, string score)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZRANKBYSCORE, key, score);
            return obj;
        }

        public Task<RedisResult> exzrankByScore(byte[] key, byte[] score)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZRANKBYSCORE, key, score);
            return obj;
        }

        public Task<RedisResult> exzrevrankByScore(string key, string score)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREVRANKBYSCORE, key, score);
            return obj;
        }

        public Task<RedisResult> exzrevrankByScore(byte[] key, byte[] score)
        {
            var obj = getRedis().ExecuteAsync(ModuleCommand.EXZREVRANKBYSCORE, key, score);
            return obj;
        }
    }
}