// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

namespace AppCore.Tools
{
    public class closureCode
    {
        public static string CompleteINC_CRQ_TAS(string num, string prefix)
        {
            if ((num.Length < 1) || (num.Length >= 15))
                return num;

            num = num.Replace(prefix, string.Empty);
            if (!Tools.IsAllDigits(num))
                return "error";

            if (num.Length < 13)
            {
                int num2 = 15 - (num.Length + 3);
                for (int i = 1; i <= num2; i++)
                {
                    num = "0" + num;
                }
            }
            return (prefix + num);
        }
    }
}
