# AlibabaCloud-CSharp-sdk

English | [简体中文](./README-CN.md)

A client packaged based on [StackExchange.Redis](https://stackexchange.github.io/StackExchange.Redis/) that operates [Tair](https://www.alibabacloud.com/help/en/apsaradb-for-redis/latest/apsaradb-for-redis-enhanced-edition-overview) For Redis Modules.

* [TairString](https://www.alibabacloud.com/help/en/apsaradb-for-redis/latest/tairstring-commands), is a string that contains s version number.([Open sourced](https://github.com/alibaba/TairString))
* [TairHash](https://www.alibabacloud.com/help/en/apsaradb-for-redis/latest/tairhash-commands), is a hash that allows you to specify the expiration time and verison number of a field.([Open sourced](https://github.com/alibaba/TairHash))
* [TairZset](https://www.alibabacloud.com/help/en/apsaradb-for-redis/latest/tairzset-commands), allows you to sort data of the double type based on multiple dimensions. ([Open sourced](https://github.com/alibaba/TairZset))
* [TairDoc](https://www.alibabacloud.com/help/en/apsaradb-for-redis/latest/tairdoc-commands), to perform create, read, update, and delete (CRUD) operations on JSON data. (Coming soon)
* [TairGis](https://www.alibabacloud.com/help/en/apsaradb-for-redis/latest/tairgis-commands), allowing you to query points, linestrings, and polygons. (Coming soon)
* [TairBloom](https://www.alibabacloud.com/help/en/apsaradb-for-redis/latest/tairbloom-commands), is a Bloom filter that supports dynamic scaling. (Coming soon)
* [TairRoaring](https://www.alibabacloud.com/help/en/apsaradb-for-redis/latest/tairroaring-commands), is a more efficient and balanced type of compressed bitmaps recognized by the industry. (Coming soon)
* [TairSearch](https://www.alibabacloud.com/help/en/apsaradb-for-redis/latest/tairsearch-command), is a full-text search module developed in-house based on Redis modules.(Coming soon)
* [TairCpc](https://www.alibabacloud.com/help/en/apsaradb-for-redis/latest/taircpc-commands), is a data structure developed based on the compressed probability counting (CPC) sketch. (Coming soon)
* [TairTs](https://www.alibabacloud.com/help/en/apsaradb-for-redis/latest/tairts-commands), is a time series data structure that is developed on top of Redis modules. (Coming soon)


## Example

### Connection

```
private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost");
private readonly TairString tair = new(connDC, 0);
```
Use `ConnectionMultiplexer.Connect` to create a `ConnectionMultiplexer` instance. Connection can be passed a configuration string or a `ConfigurationOptions` object.
Once a `ConnectionMultiplexer` instance is created, a redis database (whether standalone or clustered) can be accessed.

Next, create the corresponding Module object, and then access the database. In addition, redis supports multiple databases, so when `new` an object, specify the corresponding database through the second parameter. (Clusters do not support this feature)

### Async
For `StackExchange.Redis`, use `ExecuteAsync` to return the result as `Task<RedisResult>` type, you can use the following methods to get the real result

```
var ret1 = tairStringAsync.exset("key", "value");
Console.WriteLine(ResultHelper.Long(ret1.Result));//output "OK"
```

`.Result` is used to get the result of Async execution, `ResultHelper.Long` is used to convert `RedisResult` to the result required by the user.

### API

An example of TairString is as follows:

```
using System.Text;
using Alibaba.TairSDK.TairString;
using StackExchange.Redis;

namespace TestString
{
    public class Program
    {
        private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost:6379");
        private static readonly TairString tair = new(connDC, 0);
        
        static void Main(string[] args)
        {
            var bkey = Encoding.UTF8.GetBytes("bkey");
            var bvalue = Encoding.UTF8.GetBytes("bvalue");
            var key = "key";
            var value = "value";

            //string
            var ret = tair.exset(key, value);
            Console.WriteLine(ret);
            var result = tair.exget(key);
            Console.WriteLine(result.getValue());
            Console.WriteLine(result.getVersion());
            

            //binary
            ret = tair.exset(bkey, bvalue);
            Console.WriteLine(ret);
            var bresult = tair.exget(bkey);
            Console.WriteLine(Encoding.UTF8.GetString(bresult.getValue()));
            Console.WriteLine(bresult.getVersion());
        }
    } 

}
```

## Package Status

| Package | NuGet Stable | Downloads |
| ------- | ------------ | --------- |
| [AlibabaCloud.TairSDK](https://www.nuget.org/packages/AlibabaCloud.TairSDK) | [![AlibabaCloud.TairSDK](https://img.shields.io/nuget/vpre/AlibabaCloud.TairSDK.svg)](https://www.nuget.org/packages/AlibabaCloud.TairSDK/) | [![AlibabaCloud.TairSDK](https://img.shields.io/nuget/dt/AlibabaCloud.TairSDK.svg)](https://www.nuget.org/packages/AlibabaCloud.TairSDK/) 

## Tair All SDK

| language | GitHub |
|----------|---|
| Java     |https://github.com/alibaba/alibabacloud-tairjedis-sdk|
| Python   |https://github.com/alibaba/tair-py|
| Go       |https://github.com/alibaba/tair-go|
| .Net     |https://github.com/alibaba/AlibabaCloud.TairSDK|