# TairString

* cas

```
public long cas(string key, string oldvalue, string newvalue)
public long cas(byte[] key, byte[] oldvalue, byte[] newvalue)

Compare And Set.
Parameters:
key - the key
oldvalue - the oldvalue
newvalue - the newvalue
Returns:
Success: 1; Not exist: -1; Fail: 0.
```

```
public long cas(string key, string oldvalue, string newvalue, CasParams param)
public long cas(byte[] key, byte[] oldvalue, byte[] newvalue,CasParams param)

Compare And Set.
Parameters:
key - the key
oldvalue - the oldvalue
newvalue - thenewvalue
params - the params: [EX time] [EXAT time] [PX time] [PXAT time] `EX` - Set expire time (seconds) `EXAT` - Set expire time as a UNIX timestamp (seconds) `PX` - Set expire time (milliseconds) `PXAT` - Set expire time as a UNIX timestamp (milliseconds)
Returns:
Success: 1; Not exist: -1; Fail: 0.
```

* cad

```
public long cad(string key, string value)
public long cad(byte[] key, byte[] value)

Compare And Delete.
Parameters:
key - the key
value - the value
Returns:
Success: 1; Not exist: -1; Fail: 0.
```

* exset

``` C#
public string exset(string key, string value)
public string exset(byte[] key, byte[] value)

Set the string value of the key.
Parameters:
key - the key
value - the value
Returns:
Success: OK; Fail: error.
```

```C#
public string exset(string key, string value, ExsetParams param)
public string exset(byte[] key, byte[] value, ExsetParams param)

Set the value of the key.
Parameters:
key - the key
value - the value
params - the params: [EX time] [EXAT time] [PX time] [PXAT time] [NX|XX] [VER version | ABS version] `EX` - Set expire time (seconds) `EXAT` - Set expire time as a UNIX timestamp (seconds) `PX` - Set expire time (milliseconds) `PXAT` - Set expire time as a UNIX timestamp (milliseconds) `NX` - only set the key if it does not already exists `XX` - only set the key if it already exists `VER` - Set if version matched or not exist `ABS` - Set with abs version `FLAGS` - MEMCACHED flags
Returns:
Success: OK; Fail: error.
```
```C#
public long exsetVersion(string key, string value, ExsetParams param)
public long exsetVersion(byte[] key, byte[] value, ExsetParams param)
```
* exget

```C#
public ExgetResult<string> exget(string key)
public ExgetResult<byte[]> exget(byte[] key)

Get the value of the key.
Parameters:
key - the key
Returns:
List, Success: [value, version]; Fail: error.
```

* exgetFlags

```C#
public ExgetResult<string> exgetFlags(string key)
public ExgetResult<byte[]> exgetFlags(byte[] key)

Get the value and flags of the key.
Parameters:
key - the key
Returns:
List, Success: [value, version, flags]; Fail: error.
```
* exsetver

```C#
public long exsetver(string key, long version)
public long exsetver(byte[] key, long version)

Set the version for the key.
Parameters:
key - the key
version - the version
Returns:
Success: 1; Not exist: 0; Fail: error.
```
* exincrBy

```C#
public long exincrBy(string key, long incr)
public long exincrBy(byte[] key, long incr)

Increment the integer value of the key by the given number.
Parameters:
key - the key
incr - the incr
Returns:
Success: value of key; Fail: error.
```

```C#
public long exincrBy(string key, long incr, ExincrbyParams param)
public long exincrBy(byte[] key, long incr, ExincrbyParams param)

Increment the integer value of the key by the given number.
Parameters:
key - the key
incr - the incr
params - the params: [EX time] [EXAT time] [PX time] [PXAT time] [VER version | ABS version] [MIN minval] [MAX maxval] `EX` - Set expire time (seconds) `EXAT` - Set expire time as a UNIX timestamp (seconds) `PX` - Set expire time (milliseconds) `PXAT` - Set expire time as a UNIX timestamp (milliseconds) `VER` - Set if version matched or not exist `ABS` - Set with abs version `MIN` - Set the min value for the value. `MAX` - Set the max value for the value. `DEF` - Set DEFault value when key not exists. `NONEGATIVE` - After incr, if value less than 0, set to 0.
Returns:
Success: value of key; Fail: error.
```
* exincrByFloat

```C#
public double exincrByFloat(string key, double incr)
public double exincrByFloat(byte[] key, double incr)

Increment the float value of the key by the given number.
Parameters:
key - the key
incr - the incr
Returns:
Success: value of key; Fail: error.
```
```C#
public double exincrByFloat(string key, double incr, ExincrbyFloatParams param)
public double exincrByFloat(byte[] key, double incr, ExincrbyFloatParams param)

Increment the float value of the key by the given number.
Parameters:
key - the key
incr - the incr
params - the params: [EX time] [EXAT time] [PX time] [PXAT time] [VER version | ABS version] [MIN minval] [MAX maxval] `EX` - Set expire time (seconds) `EXAT` - Set expire time as a UNIX timestamp (seconds) `PX` - Set expire time (milliseconds) `PXAT` - Set expire time as a UNIX timestamp (milliseconds) `VER` - Set if version matched or not exist `ABS` - Set with abs version `MIN` - Set the min value for the value. `MAX` - Set the max value for the value.
Returns:
Success: value of key; Fail: error.
```
* excas

```C#
public ExcasResult<string> excas(string key, string newvalue, long version)
public ExcasResult<byte[]> excas(byte[] key, byte[] newvalue, long version)
 
Compare And Set.
Parameters:
key - the key
newvalue - the newvalue
version - the version
Returns:
List, Success: ["OK", "", version]; Fail: ["Err", value, version].
```
* excad

```C#
public long excad(string key, long version)
public long excad(byte[] key, long version)

Compare And Delete.
Parameters:
key - the key
version - the version
Returns:
Success: 1; Not exist: -1; Fail: 0.
```

# TairHash

* exhset

```C#
public long exhset(string key, string field, string value)
public long exhset(byte[] key, byte[] field, byte[] value)

Set the string value of a exhash field.
Parameters:
key - the key
field - the field type: key
value - the value
Returns:
integer-reply specifically: 1 if field is a new field in the hash and value was set. 0 if field already exists in the hash and the value was updated.
```
```C#
public long exhset(string key, string field, string value, ExhsetParams param)
public long exhset(byte[] key, byte[] field, byte[] value, ExhsetParams param)

Set the string value of a exhash field.
Parameters:
key - the key
field - the field type: key
value - the value
params - the params: [EX time] [EXAT time] [PX time] [PXAT time] [NX|XX] [VER version | ABS version] `EX` - Set expire time (seconds) `EXAT` - Set expire time as a UNIX timestamp (seconds) `PX` - Set expire time (milliseconds) `PXAT` - Set expire time as a UNIX timestamp (milliseconds) `NX` - only set the key if it does not already exists `XX` - only set the key if it already exists `VER` - Set if version matched or not exist `ABS` - Set with abs version
Returns:
integer-reply specifically: 1 if field is a new field in the hash and value was set. 0 if field already exists in the hash and the value was updated.
```
* exhsetnx

```C#
public long exhsetnx(string key, string field, string value)
public long exhsetnx(byte[] key, byte[] field, byte[] value)

Set the value of a exhash field, only if the field does not exist.
Parameters:
key - the key
field - the field type: key
value - the value
Returns:
integer-reply specifically: 1 if field is a new field in the hash and value was set. 0 if field already exists in the hash and no operation was performed.
```
* exhmset

```C#
public string exhmset(string key, Dictionary<string, string> hash)
public string exhmset(byte[] key, Dictionary<byte[], byte[]> hash)

Set multiple hash fields to multiple values.
Parameters:
key - the key
hash - the null
Returns:
String simple-string-reply
```
* exhmsetwithopts

```C#
public string exhmsetwithopts(string key, List<ExhmsetwithopsParams<string>> param)
public string exhmsetwithopts(byte[] key, List<ExhmsetwithopsParams<byte[]>> param)
 
set multiple hash fields with version
Parameters:
key - the key
params - the params
Returns:
success: OK
```

* exhpexpire

```C#
public bool exhpexpire(string key, string field, int milliseconds)
public bool exhpexpire(byte[] key, byte[] field, int milliseconds)
 
Set expire time (milliseconds).
Parameters:
key - the key
field - the field
milliseconds - time is milliseconds
Returns:
Success: true, fail: false.
 
```
* exhpexpireAt 

```C#
public bool exhpexpireAt(string key, string field, long unixTime)
public bool exhpexpireAt(byte[] key, byte[] field, long unixTime)
 
Set the expiration for a key as a UNIX timestamp (milliseconds).
Parameters:
key - the key
field - the field
unixTime - timestamp the timestamp type: posix time, time is milliseconds
Returns:
Success: true, fail: false.
```

* exhexpireAt

```C#
public bool exhexpireAt(string key, string field, long unixTime)
public bool exhexpireAt(byte[] key, byte[] field, long unixTime) 
 
Set the expiration for a key as a UNIX timestamp (seconds).
Parameters:
key - the key
field - the field
unixTime - timestamp the timestamp type: posix time, time is seconds
Returns:
Success: true, fail: false.
```
* exhexpire

```C#
public bool exhexpire(string key, string field, int seconds)
public bool exhexpire(byte[] key, byte[] field, int seconds)

Set expire time (seconds).
Parameters:
key - the key
field - the field
seconds - time is seconds
Returns:
Success: true, fail: false.
```
* exhpttl

```C#
public long exhpttl(string key, string field)
public long exhpttl(byte[] key, byte[] field)

Get ttl (milliseconds).
Parameters:
key - the key
field - the field
Returns:
ttl
```
* exhttl

```C#
public long exhttl(string key, string field)
public long exhttl(byte[] key, byte[] field)

Get ttl (seconds).
Parameters:
key - the key
field - the field
Returns:
ttl
```

* exhver

```C#
public long exhver(string key, string field)
public long exhver(byte[] key, byte[] field)

Get version
Parameters:
key - the key
field - the field
Returns:
version
```
* exhsetver

```C#
public bool exhsetver(string key, string field, long version)
public bool exhsetver(byte[] key, byte[] field, long version)

Set the field version.
Parameters:
key - the key
field - the field
version - the version
```
* exhincrBy

```C#
public long exhincrBy(string key, string field, long value)
public long exhincrBy(byte[] key, byte[] field, long value)

Increment the integer value of a hash field by the given number.
Parameters:
key - the key
field - the field type: key
Returns:
Long integer-reply the value at field after the increment operation.
```
```C#
public long exhincrBy(string key, string field, long value, ExhincrByParams param)
public long exhincrBy(byte[] key, byte[] field, long value, ExhincrByParams param)

Increment the integer value of a hash field by the given number.
Parameters:
key - the key
field - the field type: key
params - the params: [EX time] [EXAT time] [PX time] [PXAT time] `EX` - Set expire time (seconds) `EXAT` - Set expire time as a UNIX timestamp (seconds) `PX` - Set expire time (milliseconds) `PXAT` - Set expire time as a UNIX timestamp (milliseconds)
Returns:
Long integer-reply the value at field after the increment operation.
```

* exhincrByFloat

```C#
public double exhincrByFloat(string key, string field, double value)
public double exhincrByFloat(byte[] key, byte[] field, double value)

Increment the float value of a hash field by the given amount.
Parameters:
key - the key
field - the field type: key
value - the increment type: double
Returns:
Double bulk-string-reply the value of field after the increment.
```
```C#
public double exhincrByFloat(string key, string field, double value, ExhincrByFloatParams param)
public double exhincrByFloat(byte[] key, byte[] field, double value, ExhincrByFloatParams param)
 
Increment the float value of a hash field by the given amount.
Parameters:
key - the key
field - the field type: key
value - the increment type: double
params - the params: [EX time] [EXAT time] [PX time] [PXAT time] `EX` - Set expire time (seconds) `EXAT` - Set expire time as a UNIX timestamp (seconds) `PX` - Set expire time (milliseconds) `PXAT` - Set expire time as a UNIX timestamp (milliseconds)
Returns:
Double bulk-string-reply the value of field after the increment.
```

* exhget

```C#
public string exhget(string key, string field)
public byte[] exhget(byte[] key, byte[] field)

Get the value of a exhash field.
Parameters:
key - the key
field - the field type: key
Returns:
K bulk-string-reply the value associated with field or null when field is not present in the hash or key does not exist.
```

* exhgetwithver

```C#
public ExhgetwithverResult<string> exhgetwithver(string key, string field)
public ExhgetwithverResult<byte[]> exhgetwithver(byte[] key, byte[] field)

Get the value and the version of a exhash field.
Parameters:
key - the key
field - the field type: key
Returns:
ExhgetwithverResult the value and the version associated with field or null when field is not present in the hash or key does not exist.
```

* exhmget

```C#
public List<string> exhmget(string key, params string[] fields)
public List<byte[]> exhmget(byte[] key, params byte[][] fields)

Get the values of all the given hash fields.
Parameters:
key - the key
fields - the field type: key
Returns:
List<K> array-reply list of values associated with the given fields
```

* exhmgetwithve

```C#
public List<ExhgetwithverResult<string>> exhmgetwithver(string key, string[] fields)
public List<ExhgetwithverResult<byte[]>> exhmgetwithver(byte[] key, params byte[][] fields)

Get the values and version of all the given hash fields.
Parameters:
key - the key
fields - the field type: key
Returns:
List<K> array-reply list of values associated with the given fields
```
* exhdel

```C#
public long exhdel(string key, params string[] fields)
public long exhdel(byte[] key, params byte[][] fields)
 
 Parameters:
key - the key
fields - the field type: key
Returns:
Long integer-reply the number of fields that were removed from the hash not including specified but non existing fields.
```

* exhlen

```C#
public long exhlen(string key)
public long exhlen(byte[] key)

 Get the number of fields in a hash.
Parameters:
key - the key
Returns:
Long integer-reply number of fields in the hash, or 0 when key does not exist.
```

* exhexists

```C#
public bool exhexists(string key, string field)
public bool exhexists(byte[] key, byte[] field)

Determine if a hash field exists.
Parameters:
key - the key
field - the field type: key
Returns:
Boolean integer-reply specifically: true if the hash contains field. false if the hash does not contain field, or key does not exist.
```

* exhstrlen

```C#
public long exhstrlen(string key, string field)
public long exhstrlen(byte[] key, byte[] field)

Get the length of a hash field.
Parameters:
key - the key
field - the field
Returns:
the length
```

* exhkeys

```C#
public HashSet<string> exhkeys(string key)
public HashSet<byte[]> exhkeys(byte[] key)

Get all the fields in a hash.
Parameters:
key - the key
Returns:
HashSet<K> array-reply list of fields in the hash, or an empty list when key does not exist.
```

* exhvals

```C#
public List<string> exhvals(string key)
public List<byte[]> exhvals(byte[] key)

Get all the values in a hash.
Parameters:
key - the key
Returns:
List<K> array-reply list of values in the hash, or an empty list when key does not exist.
```

* exhgetAll

```C#
public Dictionary<string, string> exhgetAll(string key)
public Dictionary<byte[], byte[]> exhgetAll(byte[] key)

Get all the fields and values in a hash.
Parameters:
key - the key
Returns:
Map<K,K> array-reply list of fields and their values stored in the hash or an empty list when key does not exist.
```
* exhscan

```C#
public ExhscanResult<string> exhscan(string key, string op, string subkey)
public ExhscanResult<byte[]> exhscan(byte[] key, byte[] op, byte[] subkey)
 
 Exhscan a exhash
Parameters:
key - the key
op - the op
subkey - the subkey
Returns:
A ScanResult
```
```C#
public ExhscanResult<string> exhscan(string key, string op, string subkey, ExhscanParams param)
public ExhscanResult<byte[]> exhscan(byte[] key, byte[] op, byte[] subkey, ExhscanParams param)

Exhscan a exhash
Parameters:
key - the key
op - the op
subkey - the subkey
params - the params: [MATCH pattern] [COUNT count] `MATCH` - Set the pattern which is used to filter the results `COUNT` - Set the number of fields in a single scan (default is 10)
Returns:
A ScanResult
```

# TairSearch

* tftmappingindex

```
public string tftmappingindex(string index, string request)
public string tftmappingindex(byte[] index, byte[] request)

Create an Index and specify its schema. Note that this command will only succeed if the Index does not exist.
Parameters:
index - the index name
request - the index schema
Returns:
Success: OK; Fail: error
```

* tftcreateindex

```
public string tftcreateindex(string index, string request)
public string tftcreateindex(byte[] index, byte[] request)

Create an Index and specify its schema. Note that this command will only succeed if the Index does not exist.
Parameters:
index - the index name
request - the index schema
Returns:
Success: OK; Fail: error
```

* tftupdateindex

```
public string tftupdateindex(string index, string request)
public string tftupdateindex(byte[] index, byte[] request)

Update an existing index mapping. Note that you cannot update (append) mapping properties.
Parameters:
index - the index name
request - the index schema
Returns:
Success: OK; Fail: error
```

* tftgetindexmappings

```
public string tftgetindexmappings(string index)
public string tftgetindexmappings(byte[] index)

Get index schema information.
Parameters:
index - the index name
Returns:
Success: Schema information represented by json; Fail: error
```
* tftadddoc

```
public string tftadddoc(string index, string request)
public string tftadddoc(byte[] index, byte[] request)

Add a document to Index.
Parameters:
index - the index name
request - the json representation of a document
Returns:
Success: Json structure containing document id and version ; Fail: error.
```

```
public string tftadddoc(string index, string request, string docId)
public string tftadddoc(byte[] index, byte[] request, byte[] docId)

Similar to the above but you can manually specify the document id.
```
* tftmadddoc

```
public string tftmadddoc(string index, Dictionary<string, string> docs)
public string tftmadddoc(byte[] index, Dictionary<byte[], byte[]> docs)

Add docs in batch. This command can guarantee atomicity, that is, either all documents are added successfully, or none are added.
Parameters:
index - the index name
docs - the json representation of a document
Returns:
Success: OK ; Fail: error.
```

* tftupdatedoc

```
public string tftupdatedoc(string index, string docId, string docContent)
public string tftupdatedoc(byte[] index, byte[] docId, byte[] docContent)

Update an existing doc. You can add new fields to the document, or update an existing field.
Parameters:
index - the index name
docId - the id of the document
docContent - the content of the document
Returns:
Success: OK ; Fail: error.
```
* tftupdatedocfield

```
public string tftupdatedocfield(string index, string docId, string docContent)
public string tftupdatedocfield(byte[] index, byte[] docId, byte[] docContent)

Update doc fields. You can add new fields to the document, or update an existing field. The document is automatically created if it does not exist.
Parameters:
index - the index name
docId - the id of the document
docContent - the content of the document
Returns:
Success: OK ; Fail: error.
```
* tftincrlongdocfield

```
public long tftincrlongdocfield(string index, string docId, string field, long value)
public long tftincrlongdocfield(byte[] index, byte[] docId, byte[] field, long value)

Increment the integer value of a document field by the given number.
Parameters:
index - the index name
docId - the document id
field - The fields of the document that will be incremented
value - the value to be incremented
Returns:
Long integer-reply the value after the increment operation.
```
* tftincrfloatdocfield

```
public double tftincrfloatdocfield(string index, string docId, string field, double value)
public double tftincrfloatdocfield(byte[] index, byte[] docId, byte[] field, double value)

Increment the double value of a document field by the given number.
Parameters:
index - the index name
docId - the document id
field - The fields of the document that will be incremented
value - the value to be incremented
Returns:
Long double-reply the value after the increment operation.
```

* tftdeldocfield

```
public long tftdeldocfield(string index, string docId, params string[] field)
public long tftdeldocfield(byte[] index, byte[] docId, params byte[][] field)

Delete fields in the document.
Parameters:
index - the index name
docId - the document id
field - The fields of the document that will be deleted
Returns:
Long integer-reply the number of fields that were removed from the document.
```

* tftgetdoc

```
public string tftgetdoc(string index, string docId)
public string tftgetdoc(byte[] index, byte[] docId)

Get a document from Index.
Parameters:
index - the index name
docId - the document id
Returns:
Success: The content of the document; Not exists: null; Fail: error
```

```
public string tftgetdoc(string index, string docId, string request)
public string tftgetdoc(byte[] index, byte[] docId, byte[] request)

Same as above but you can specify some filtering rules through the request parameter.
```

* tftdeldoc

```
public string tftdeldoc(string index, params string[] docId)
public string tftdeldoc(byte[] index, params byte[][] docId)

Delete the specified document(s) from the index.
Parameters:
index - the index name
docId - the document id(s)
Returns:
Success: Number of successfully deleted documents; Fail: error
```
* tftdelall

```
public string tftdelall(string index)
public string tftdelall(byte[] index)

Delete all document(s) from the index.
Parameters:
index - the index name
Returns:
Success: OK; Fail: error
```
* tftsearch

```
public string tftsearch(string index, string request)
public string tftsearch(byte[] index, byte[] request)

Full text search in an Index.
Parameters:
index - the index name
request - Search expression, for detailed grammar, please refer to the official document
Returns:
Success: Query result in json format; Fail: error
```
* tftexists

```
public long tftexists(string index, string docId)
public long tftexists(byte[] index, byte[] docId)

Checks if the specified document exists in the index.
Parameters:
index - the index name
docId - the id of the document
Returns:
exists return 1 or return 0
```

* tftdocnum

```
public long tftdocnum(string index)
public long tftdocnum(byte[] index)

Get the number of documents contained in Index.
Parameters:
index - the index name
Returns:
the number of documents contained in Index.
```
* TFTScanResult

```
public TFTScanResult<string> tftscandocid(string index, string cursor)
public TFTScanResult<byte[]> tftscandocid(byte[] index, byte[] cursor)

Scan all document ids in index.
Parameters:
index - the index name
cursor - the cursor used for this scan
Returns:
the scan result with the results of this iteration and the new position of the cursor.
```
```
public TFTScanResult<string> tftscandocid(string index, string cursor, TFTScanParams param)
public TFTScanResult<byte[]> tftscamdocid(byte[] index, byte[] cursor, TFTScanParams param)

Scan all document ids in index. Time complexity: O(1) for every call. O(N) for a complete iteration, including enough command calls for the cursor to return back to 0. N is the number of documents inside the index.
Parameters:
index - the index name
cursor - The cursor.
params - the scan parameters. For example a glob-style match pattern
Returns:
the scan result with the results of this iteration and the new position of the cursor.
```


