using System.Text.RegularExpressions;

namespace PublicApi
{
    public class PostcodeValidator
    {
        public static string IsPostcodeValid(string postcode)
        {
            // Remove all whitespace
            postcode = postcode.Replace(" ", "");

            // Regex does not check for length
            if (postcode.Length > 7)
            {
                throw new InvalidPostcode($"This Postcode Is Not Valid! {postcode}");
            }

            // Regex to match a postcode
            string pattern = "^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\\s?[0-9][A-Za-z]{2})";
            
            bool isValid = Regex.IsMatch(postcode, pattern);

            if (isValid)
            {
                return postcode;
            }
            else
            {
                throw new InvalidPostcode($"This Postcode Is Not Valid! {postcode}");
            }
        }

        public class InvalidPostcode : Exception
        {
            public InvalidPostcode() { }

            public InvalidPostcode(string message) : base(message) { }

            public InvalidPostcode(string message, Exception innerException) : base(message, innerException) { }
        }
    }
}
