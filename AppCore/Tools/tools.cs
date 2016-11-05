// AMTRevolution
// Hugo Gonçalves
// Rui Gonçalves

namespace AppCore.Tools
{
    public class Tools
    {
        public static bool IsAllDigits(string s)
        {
            foreach (char ch in s)
                if (!char.IsDigit(ch))
                    return false;

            return true;
        }
    }
}
