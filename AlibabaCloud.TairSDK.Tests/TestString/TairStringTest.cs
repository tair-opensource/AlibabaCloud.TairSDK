using System;
using System.Text;
using System.Threading;
using AlibabaCloud.TairSDK.TairString;
using AlibabaCloud.TairSDK.TairString.Param;
using AlibabaCloud.TairSDK.TairString.Result;
using NUnit.Framework;
using StackExchange.Redis;

namespace TestStringTest
{
    public class StringTest
    {
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
        private readonly TairString tair = new(connDC, 0);


        [Test]
        public void exsetTest()
        {
            var bkey = Encoding.UTF8.GetBytes("bkey" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bvalue = Encoding.UTF8.GetBytes("bvalue" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var key = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var value = "value" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var ret = "";

            //string
            ret = tair.exset(key, value);
            Assert.AreEqual("OK", ret);
            var result = tair.exget(key);
            Assert.AreEqual(value, result.getValue());
            Assert.AreEqual(1, result.getVersion());

            //binary
            ret = tair.exset(bkey, bvalue);
            Assert.AreEqual("OK", ret);
            var bresult = tair.exget(bkey);
            Assert.AreEqual(bvalue, bresult.getValue());
            Assert.AreEqual(1, bresult.getVersion());
        }

        [Test]
        public void exgetflagsTest()
        {
            var bkey = Encoding.UTF8.GetBytes("bkey" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bvalue = Encoding.UTF8.GetBytes("bvalue" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var key = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var value = "value" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            ExgetResult<string> result;
            var ret = tair.exset(key, value);
            Assert.AreEqual("OK", ret);
            result = tair.exgetFlags(key);
            Assert.AreEqual(0, result.getFlags());
        }

        [Test]
        public void exsetParamsTest()
        {
            var bkey = Encoding.UTF8.GetBytes("bkey" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bvalue = Encoding.UTF8.GetBytes("bvalue" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var key = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var value = "value" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");

            var params_nx = new ExsetParams();
            params_nx.nx();
            var params_xx = new ExsetParams();
            params_xx.xx();
            var ret_xx = "";
            var ret_nx = "";

            //string
            ret_xx = tair.exset(key, value, params_xx);
            Assert.IsEmpty(ret_xx);
            ret_nx = tair.exset(key, value, params_nx);
            Assert.AreEqual("OK", ret_nx);
            ret_xx = tair.exset(key, value, params_xx);
            Assert.AreEqual("OK", ret_xx);

            //binary
            ret_xx = tair.exset(bkey, bvalue, params_xx);
            Assert.IsEmpty(ret_xx);
            ret_nx = tair.exset(bkey, bvalue, params_nx);
            Assert.AreEqual("OK", ret_nx);
            ret_xx = tair.exset(bkey, bvalue, params_xx);
            Assert.AreEqual("OK", ret_xx);
        }

        [Test]
        public void exsetverTest()
        {
            var bkey = Encoding.UTF8.GetBytes("bkey" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bvalue = Encoding.UTF8.GetBytes("bvalue" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var key = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var value = "value" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var ret = "";
            long ret_var = 0;

            //string
            ret = tair.exset(key, value);
            Assert.AreEqual("OK", ret);
            var result = tair.exget(key);
            Assert.NotNull(result);
            Assert.AreEqual(1, result.getVersion());
            Assert.AreEqual(value, result.getValue());

            ret_var = tair.exsetver(key, 10);
            Assert.AreEqual(1, ret_var);
            Assert.AreEqual(10, tair.exget(key).getVersion());

            //binary
            ret = tair.exset(bkey, bvalue);
            Assert.AreEqual("OK", ret);
            var bresult = tair.exget(bkey);
            Assert.NotNull(bresult);
            Assert.AreEqual(1, bresult.getVersion());
            ret_var = tair.exsetver(bkey, 10);
            Assert.AreEqual(1, ret_var);
            Assert.AreEqual(10, tair.exget(bkey).getVersion());
        }

        [Test]
        public void exincrbyTest()
        {
            var bkey = Encoding.UTF8.GetBytes("bkey" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bvalue = Encoding.UTF8.GetBytes("bvalue" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var key = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var value = "value" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var ret = "";

            var num_string_value = "100";
            var num_byte_value = Encoding.UTF8.GetBytes("100");
            long incr_value = 100;
            var new_string_value = "200";
            var new_byte_value = Encoding.UTF8.GetBytes("200");
            long new_long_value = 200;
            long ret_var = 0;

            //string
            ret = tair.exset(key, num_string_value);
            Assert.AreEqual("OK", ret);
            var result = tair.exget(key);
            Assert.NotNull(result);
            Assert.AreEqual(num_string_value, result.getValue());

            ret_var = tair.exincrBy(key, incr_value);
            Assert.AreEqual(new_long_value, ret_var);
            result = tair.exget(key);
            Assert.AreEqual(new_string_value, result.getValue());
            Assert.AreEqual(2, result.getVersion());

            //binary
            ret = tair.exset(bkey, num_byte_value);
            Assert.AreEqual("OK", ret);
            var bresult = tair.exget(bkey);
            Assert.AreEqual(num_byte_value, bresult.getValue());
            ret_var = tair.exincrBy(bkey, incr_value);
            Assert.AreEqual(new_long_value, ret_var);
            bresult = tair.exget(bkey);
            Assert.AreEqual(new_byte_value, bresult.getValue());
        }

        [Test]
        public void exincrbyParamsTest()
        {
            var bkey = Encoding.UTF8.GetBytes("bkey" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bvalue = Encoding.UTF8.GetBytes("bvalue" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var key = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var value = "value" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");

            var num_string_value = "100";
            var num_byte_value = Encoding.UTF8.GetBytes("100");
            long incr_value = 100;
            long ret_var = 0;

            var params_nx_px = new ExincrbyParams();
            params_nx_px.nx();
            params_nx_px.px(1000);
            var params_xx_ex = new ExincrbyParams();
            params_xx_ex.xx();
            params_xx_ex.ex(1);
            var params_xx_pxat = new ExincrbyParams();
            params_xx_pxat.xx();
            params_xx_pxat.pxat(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + 1000);
            var param_ver = new ExincrbyParams();
            param_ver.ver(10);
            param_ver.max(5);
            param_ver.min(2);


            //string
            ret_var = tair.exincrBy(key, incr_value, params_nx_px);
            Assert.AreEqual(incr_value, ret_var);
            var result = tair.exget(key);
            Assert.AreEqual(1, result.getVersion());
            Assert.AreEqual(num_string_value, result.getValue());
            Thread.Sleep(1000);
            result = tair.exget(key);
            Assert.Null(result);
            try
            {
                ret_var = tair.exincrBy(key, incr_value, param_ver);
            }
            catch (Exception e)
            {
                Assert.AreEqual("ERR increment or decrement would overflow", e.Message);
            }

            //binary
            ret_var = tair.exincrBy(bkey, incr_value, params_nx_px);
            Assert.AreEqual(incr_value, ret_var);
            var bresult = tair.exget(bkey);
            Assert.AreEqual(1, bresult.getVersion());
            Assert.AreEqual(num_byte_value, bresult.getValue());
            Thread.Sleep(1500);
            bresult = tair.exget(bkey);
            Assert.Null(bresult);
        }

        [Test]
        public void exincrbyfloatTest()
        {
            var bkey = Encoding.UTF8.GetBytes("bkey" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bvalue = Encoding.UTF8.GetBytes("bvalue" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var key = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var value = "value" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var ret = "";

            var num_string_value = "100";
            var num_byte_value = Encoding.UTF8.GetBytes("100");
            double incr_value = 100;
            var new_string_value = "200";
            var new_byte_value = Encoding.UTF8.GetBytes("200");
            double new_float_value = 200;
            double ret_var = 0;

            //string
            ret = tair.exset(key, num_string_value);
            Assert.AreEqual("OK", ret);
            ret_var = tair.exincrByFloat(key, incr_value);
            Assert.AreEqual(new_float_value, ret_var);
            var result = tair.exget(key);
            Assert.AreEqual(2, result.getVersion());
            Assert.AreEqual(new_string_value, result.getValue());

            //binary
            ret = tair.exset(bkey, num_byte_value);
            Assert.AreEqual("OK", ret);
            ret_var = tair.exincrByFloat(bkey, incr_value);
            Assert.AreEqual(new_float_value, ret_var);
            var bresult = tair.exget(bkey);
            Assert.AreEqual(2, result.getVersion());
            Assert.AreEqual(new_byte_value, bresult.getValue());
        }

        [Test]
        public void exincrbyfloatParamsTest()
        {
            var bkey = Encoding.UTF8.GetBytes("bkey" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bvalue = Encoding.UTF8.GetBytes("bvalue" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var key = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var value = "value" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");

            var num_string_value = "100";
            var num_byte_value = Encoding.UTF8.GetBytes("100");
            double incr_value = 100;

            double ret_var = 0;
            ExgetResult<string> result = null;
            ExgetResult<byte[]> bresult = null;

            var params_nx_px = new ExincrbyFloatParams();
            params_nx_px.nx();
            params_nx_px.px(1000);

            //string
            ret_var = tair.exincrByFloat(key, incr_value, params_nx_px);
            Assert.AreEqual(incr_value, ret_var);
            result = tair.exget(key);
            Assert.AreEqual(1, result.getVersion());
            Assert.AreEqual(num_string_value, result.getValue());
            Thread.Sleep(1000);
            result = tair.exget(key);
            Assert.Null(result);

            //binary
            ret_var = tair.exincrByFloat(bkey, incr_value, params_nx_px);
            Assert.AreEqual(incr_value, ret_var);
            bresult = tair.exget(bkey);
            Assert.AreEqual(1, bresult.getVersion());
            Assert.AreEqual(num_byte_value, bresult.getValue());
            Thread.Sleep(1000);
            bresult = tair.exget(bkey);
            Assert.Null(result);
        }

        [Test]
        public void excasTest()
        {
            var bkey = Encoding.UTF8.GetBytes("bkey" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bvalue = Encoding.UTF8.GetBytes("bvalue" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var key = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var value = "value" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var ret = "";
            ExcasResult<string> ret2 = null;
            ExcasResult<byte[]> ret3 = null;

            //string
            ret = tair.exset(key, value);
            Assert.AreEqual("OK", ret);
            ret2 = tair.excas(key, "new" + value, 2);
            Assert.AreEqual(1, ret2.getVersion());
            ret2 = tair.excas(key, "new" + value, 1);
            Assert.AreEqual("OK", ret2.getMsg());
            Assert.AreEqual("", ret2.getValue());

            //binary
            ret = tair.exset(bkey, bvalue);
            Assert.AreEqual("OK", ret);
            ret3 = tair.excas(bkey, Encoding.UTF8.GetBytes("new" + value), 2);
            Assert.AreEqual(1, ret3.getVersion());
            ret3 = tair.excas(bkey, Encoding.UTF8.GetBytes("new" + value), 1);
            Assert.AreEqual(2, ret3.getVersion());
        }

        [Test]
        public void excadTest()
        {
            var bkey = Encoding.UTF8.GetBytes("bkey" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var bvalue = Encoding.UTF8.GetBytes("bvalue" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N"));
            var key = "key" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var value = "value" + Thread.CurrentThread.Name + Guid.NewGuid().ToString("N");
            var ret = "";
            long ret2 = 0;

            //string
            ret = tair.exset(key, value);
            Assert.AreEqual("OK", ret);
            ret2 = tair.excad(key, 2);
            Assert.AreEqual(0, ret2);
            ret2 = tair.excad(key, 1);
            Assert.AreEqual(1, ret2);


            //binary
            ret = tair.exset(bkey, bvalue);
            Assert.AreEqual("OK", ret);
            ret2 = tair.excad(bkey, 2);
            Assert.AreEqual(0, ret2);
            ret2 = tair.excad(bkey, 1);
            Assert.AreEqual(1, ret2);
        }
    }
}