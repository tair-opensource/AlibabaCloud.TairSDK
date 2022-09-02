namespace AlibabaCloud.TairSDK.TairTs.Result
{
    public class ExtsLabelResult
    {
        private string name;
        private string value;

        public ExtsLabelResult(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getValue()
        {
            return value;
        }

        public void setValue(string value)
        {
            this.value = value;
        }
    }
}