namespace AlibabaCloud.TairSDK.TairZset.Param
{
    public class ZsetHelper
    {
        public static string joinScoresToString(params double[] scores)
        {
            string mscore = "";
            foreach (var score in scores)
            {
                mscore += score;
                mscore += '#';
            }

            return mscore.Substring(0, mscore.Length - 1);
        }
    }
}