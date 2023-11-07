using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;

namespace PostcodeApi
{
    public class PostcodeHelper
    {
        string const pattern = "^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\\s?[0-9][A-Za-z]{2})";

        public static bool IsPostcodeValid(string postcode)
        {
            // Regex does not check for length
            if (postcode.Length > 7)
            {
                return false;
            }

            // Regex to match a postcode
            
            bool isValid = Regex.IsMatch(postcode, pattern);

            return isValid;
        }

        public static string PostcodeFormatter(string postcode)
        {
            return postcode.Replace(" ", "").ToUpper();
        }
    }
}
