# AlibabaCloud-CSharp-sdk

## 概述
基于 StackExchange.Redis 封装，用于操作 [云数据库Redis企业版](https://help.aliyun.com/document_detail/145957.html) 的客户端，支持企业版多种 [Module](https://help.aliyun.com/document_detail/146579.html) 的操作命令即部分高级特性。

* [TairString](https://help.aliyun.com/document_detail/145902.html)，支持 string 设置 version，增强`cas`和`cad`命令可轻松实现分布式锁。（已[开源](https://github.com/alibaba/TairString)）
* [TairHash](https://help.aliyun.com/document_detail/145970.html)，可实现 field 级别的过期。（已[开源](https://github.com/alibaba/TairHash)）
* [TairZset](https://help.aliyun.com/document_detail/292812.html), 支持多维排序。(已[开源](https://github.com/alibaba/TairZset))
* [TairDoc](https://help.aliyun.com/document_detail/145940.html), 支持存储`JSON`类型。（待开源）
* [TairGis](https://help.aliyun.com/document_detail/145971.html), 支持地理位置点、线、面的相交、包含等关系判断。（已[开源](https://github.com/tair-opensource/TairGis)）
* [TairBloom](https://help.aliyun.com/document_detail/145972.html), 支持动态扩容的布隆过滤器。（待开源）
* [TairRoaring](https://help.aliyun.com/document_detail/311433.html), Roaring Bitmap, 使用少量的存储空间来实现海量数据的查询优化。（待开源）
* [TairSearch](https://help.aliyun.com/document_detail/417908.html)，支持 ES-LIKE 语法的全文索引和搜索模块。（待开源） 
* [TairCpc](https://help.aliyun.com/document_detail/410587.html), 基于CPC（Compressed Probability Counting）压缩算法开发的数据结构，支持仅占用很小的内存空间对采样数据进行高性能计算。（待开源）
* [TairTs](https://help.aliyun.com/document_detail/408954.html), 时序数据结构，提供低时延、高并发的内存读写访问。（待开源）

## 安装
本组件是基于 .NET Core 5.0 和 StackExchange.Redis2.5.61 开发，所以用户在使用过程注意安装的 .NET 版本。

该组件已上传至 Nuget，且依赖于 StackExchange.Redis，可直接在Nuget控制界面搜索`AlibabaCloud.TairSDK`以及 `StackExchange.Redis`进行下载安装。


## Example
### 连接

```
private static readonly ConnectionMultiplexer connDC = ConnectionMultiplexer.Connect("localhost");
private readonly TairString tair = new(connDC, 0);
```
使用`ConnectionMultiplexer.Connect` 创建一个`ConnectionMultiplexer`实例用来使用。`Connect`可以传入配置字符串或者是`ConfigurationOptions`对象。
一旦创建了`ConnectionMultiplexer`实例，就可以访问一个`redis`数据库(无论是单机还是集群)。

接下来创建相应`Module`的对象，进而访问数据库。此外`redis`支持多个数据库，所以在`new`一个对象时，通过第二个参数指定对应的数据库。（集群不支持此功能）

### Async
对于`StackExchange.Redis`，使用	`ExecuteAsync`返回结果为`Task<RedisResult>`类型，可使用如下方式获取真实结果：

```
var ret1 = tairStringAsync.exset("key", "value");
Console.WriteLine(ResultHelper.Long(ret1.Result));//输出为 "OK"
```
`.Result`用于得到Async执行完毕的结果，`ResultHelper.Long`用于将`RedisResult`转换为用户需要的结果。

### API使用
TairString示例如下

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

更多示例请参考`Alibaba.TairSDK.Tests`。

## Tair 所有的 SDK

| language | GitHub |
|----------|---|
| Java     |https://github.com/alibaba/alibabacloud-tairjedis-sdk|
| Python   |https://github.com/alibaba/tair-py|
| Go       |https://github.com/alibaba/tair-go|
| .Net     |https://github.com/alibaba/AlibabaCloud.TairSDK|
