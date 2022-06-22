using System.Collections.Generic;

namespace AlibabaCloud.TairSDK
{
    public abstract class Param
    {
        private Dictionary<string, object> param;

        public object getParams(string name)
        {
            if (param == null) return null;

            return param[name];
        }

        protected void addParam(string name)
        {
            if (param == null) param = new Dictionary<string, object>();

            param.Add(name, null);
        }

        protected void addParam(string name, object value)
        {
            if (param == null) param = new Dictionary<string, object>();

            if (param.ContainsKey(name))
                param[name] = value;
            else
                param.Add(name, value);
        }

        protected bool contains(string name)
        {
            if (param == null) return false;

            return param.ContainsKey(name);
        }
    }
}