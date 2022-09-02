using System.IO.Pipelines;

namespace AlibabaCloud.TairSDK
{
    public static class ModuleCommand
    {
        // CAS & CAD
        public const string CAS = "CAS";
        public const string CAD = "CAD";

        // TairString command
        public const string EXSET = "EXSET";
        public const string EXGET = "EXGET";
        public const string EXSETVER = "EXSETVER";
        public const string EXINCRBY = "EXINCRBY";
        public const string EXINCRBYFLOAT = "EXINCRBYFLOAT";
        public const string EXCAS = "EXCAS";
        public const string EXCAD = "EXCAD";

        // TairHash command
        public const string EXHSET = "EXHSET";
        public const string EXHSETNX = "EXHSETNX";
        public const string EXHMSET = "EXHMSET";
        public const string EXHMSETWITHOPTS = "EXHMSETWITHOPTS";
        public const string EXHPEXPIREAT = "EXHPEXPIREAT";
        public const string EXHPEXPIRE = "EXHPEXPIRE";
        public const string EXHEXPIRE = "EXHEXPIRE";
        public const string EXHPTTL = "EXHPTTL";
        public const string EXHTTL = "EXHTTL";
        public const string EXHVER = "EXHVER";
        public const string EXHSETVER = "EXHSETVER";
        public const string EXHGET = "EXHGET";
        public const string EXHGETWITHVER = "EXHGETWITHVER";
        public const string EXHMGET = "EXHMGET";
        public const string EXHMGETWITHVER = "EXHMGETWITHVER";
        public const string EXHINCRBY = "EXHINCRBY";
        public const string EXHINCRBYFLOAT = "EXHINCRBYFLOAT";
        public const string EXHLEN = "EXHLEN";
        public const string EXHEXISTS = "EXHEXISTS";
        public const string EXHSTRLEN = "EXHSTRLEN";
        public const string EXHKEYS = "EXHKEYS";
        public const string EXHVALS = "EXHVALS";
        public const string EXHDEL = "EXHDEL";
        public const string EXHGETALL = "EXHGETALL";
        public const string EXHSCAN = "EXHSCAN";

        //TairSearch
        public const string TFTMAPPINGINDEX = "TFT.MAPPINGINDEX";
        public const string TFTCREATEINDEX = "TFT.CREATEINDEX";
        public const string TFTUPDATEINDEX = "TFT.UPDATEINDEX";
        public const string TFTADDDOC = "TFT.ADDDOC";
        public const string TFTMADDDOC = "TFT.MADDDOC";
        public const string TFTUPDATEDOC = "TFT.UPDATEDOC";
        public const string TFTUPDATEDOCFIELD = "TFT.UPDATEDOCFIELD";
        public const string TFTDELDOC = "TFT.DELDOC";
        public const string TFTDELALL = "TFT.DELALL";
        public const string TFTGETINDEX = "TFT.GETINDEX";
        public const string TFTGETDOC = "TFT.GETDOC";
        public const string TFTSEARCH = "TFT.SEARCH";
        public const string TFTMSEARCH = "TFT.MSEARCH";
        public const string TFTEXISTS = "TFT.EXISTS";
        public const string TFTSCANDOCID = "TFT.SCANDOCID";
        public const string TFTDOCNUM = "TFT.DOCNUM";
        public const string TFTINCRLONGDOCFIELD = "TFT.INCRLONGDOCFIELD";
        public const string TFTINCRFLOATDOCFIELD = "TFT.INCRFLOATDOCFIELD";
        public const string TFTDELDOCFIELD = "TFT.DELDOCFIELD";

        //TairDoc
        public const string JSONDEL = "JSON.DEL";
        public const string JSONGET = "JSON.GET";
        public const string JSONMGET = "JSON.MGET";
        public const string JSONSET = "JSON.SET";
        public const string JSONTYPE = "JSON.TYPE";
        public const string JSONNUMINCRBY = "JSON.NUMINCRBY";
        public const string JSONSTRAPPEND = "JSON.STRAPPEND";
        public const string JSONSTRLEN = "JSON.STRLEN";
        public const string JSONARRAPPEND = "JSON.ARRAPPEND";
        public const string JSONARRPOP = "JSON.ARRPOP";
        public const string JSONARRINSERT = "JSON.ARRINSERT";
        public const string JSONARRLEN = "JSON.ARRLEN";
        public const string JSONARRTRIM = "JSON.ARRTRIM";

        //TairGis
        public const string GISADD = "GIS.ADD";
        public const string GISGET = "GIS.GET";
        public const string GISSEARCH = "GIS.SEARCH";
        public const string GISDEL = "GIS.DEL";
        public const string GISGETALL = "GIS.GETALL";
        public const string GISCONTAINS = "GIS.CONTAINS";
        public const string GISWITHIN = "GIS.WITHIN";
        public const string GISINTERSECTS = "GIS.INTERSECTS";

        //TairBloom
        public const string BFRESERVE = "BF.RESERVE";
        public const string BFADD = "BF.ADD";
        public const string BFMADD = "BF.MADD";
        public const string BFEXISTS = "BF.EXISTS";
        public const string BFMEXISTS = "BF.MEXISTS";
        public const string BFINSERT = "BF.INSERT";
        public const string BFDEBUG = "BF.DEBUG";

        //tairzset
        public const string EXZADD = "EXZADD";
        public const string EXZINCRBY = "EXZINCRBY";
        public const string EXZSCORE = "EXZSCORE";
        public const string EXZREVRANGE = "EXZREVRANGE";
        public const string EXZRANGE = "EXZRANGE";
        public const string EXZRANGEBYSCORE = "EXZRANGEBYSCORE";
        public const string EXZREVRANGEBYSCORE = "EXZREVRANGEBYSCORE";
        public const string EXZRANGEBYLEX = "EXZRANGEBYLEX";
        public const string EXZREVRANGEBYLEX = "EXZREVRANGEBYLEX";
        public const string EXZREM = "EXZREM";
        public const string EXZREMRANGEBYSCORE = "EXZREMRANGEBYSCORE";
        public const string EXZREMRANGEBYRANK = "EXZREMRANGEBYRANK";
        public const string EXZREMRANGEBYLEX = "EXZREMRANGEBYLEX";
        public const string EXZCARD = "EXZCARD";
        public const string EXZRANK = "EXZRANK";
        public const string EXZREVRANK = "EXZREVRANK";
        public const string EXZCOUNT = "EXZCOUNT";
        public const string EXZLEXCOUNT = "EXZLEXCOUNT";
        public const string EXZRANKBYSCORE = "EXZRANKBYSCORE";
        public const string EXZREVRANKBYSCORE = "EXZREVRANKBYSCORE";


        //tairroaring
        public const string TRSETBIT = "TR.SETBIT";
        public const string TRSETBITS = "TR.SETBITS";
        public const string TRCLEARBITS = "TR.CLEARBITS";
        public const string TRSETRANGE = "TR.SETRANGE";
        public const string TRAPPENDBITARRAY = "TR.APPENDBITARRAY";
        public const string TRFLIPRANGE = "TR.FLIPRANGE";
        public const string TRAPPENDINTARRAY = "TR.APPENDINTARRAY";
        public const string TRSETINTARRAY = "TR.SETINTARRAY";
        public const string TRSETBITARRAY = "TR.SETBITARRAY";
        public const string TRBITOP = "TR.BITOP";
        public const string TRBITOPCARD = "TR.BITOPCARD";
        public const string TROPTIMIZE = "TR.OPTIMIZE";
        public const string TRGETBIT = "TR.GETBIT";
        public const string TRGETBITS = "TR.GETBITS";
        public const string TRBITCOUNT = "TR.BITCOUNT";
        public const string TRBITPOS = "TR.BITPOS";
        public const string TRSCAN = "TR.SCAN";
        public const string TRRANGE = "TR.RANGE";
        public const string TRRANGEBITARRAY = "TR.RANGEBITARRAY";
        public const string TRMIN = "TR.MIN";
        public const string TRMAX = "TR.MAX";
        public const string TRSTAT = "TR.STAT";
        public const string TRJACCARD = "TR.JACCARD";
        public const string TRCONTAINS = "TR.CONTAINS";
        public const string TRRANK = "TR.RANK";


        //taircpc
        public const string CPCUPDATE = "CPC.UPDATE";
        public const string CPCESTIMATE = "CPC.ESTIMATE";
        public const string CPCUPDATE2EST = "CPC.UPDATE2EST";
        public const string CPCUPDATE2JUD = "CPC.UPDATE2JUD";
        public const string CPCARRAYUPDATE = "CPC.ARRAY.UPDATE";
        public const string CPCARRAYESTIMATE = "CPC.ARRAY.ESTIMATE";
        public const string CPCARRAYESTIMATERANGE = "CPC.ARRAY.ESTIMATE.RANGE";
        public const string CPCARRAYESTIMATERANGEMERGE = "CPC.ARRAY.ESTIMATE.RANGE.MERGE";
        public const string CPCARRAYUPDATE2EST = "CPC.ARRAY.UPDATE2EST";
        public const string CPCARRAYUPDATE2JUD = "CPC.ARRAY.UPDATE2JUD";

        //tairts
        public const string TSPCREATE = "EXTS.P.CREATE";
        public const string TSSCREATE = "EXTS.S.CREATE";
        public const string TSSALTER = "EXTS.S.ALTER";
        public const string TSSADD = "EXTS.S.ADD";
        public const string TSSMADD = "EXTS.S.MADD";
        public const string TSSINCRBY = "EXTS.S.INCRBY";
        public const string TSSMINCRBY = "EXTS.S.MINCRBY";
        public const string TSSDEL = "EXTS.S.DEL";
        public const string TSSGET = "EXTS.S.GET";
        public const string TSSINFO = "EXTS.S.INFO";
        public const string TSSQUERYINDEX = "EXTS.S.QUERYINDEX";
        public const string TSSRANGE = "EXTS.S.RANGE";
        public const string TSSMRANGE = "EXTS.S.MRANGE";
        public const string TSPRANGE = "EXTS.P.RANGE";
        public const string TSSRAWMODIFY = "EXTS.S.RAW_MODIFY";
        public const string TSSRAWMULTIMODIFY = "EXTS.S.RAW_MMODIFY";
        public const string TSSRAWINCTBY = "EXTS.S.RAW_INCRBY";
        public const string TSSRAWMULTIINCRBY = "EXTS.S.RAW_MINCRBY";
    }
}