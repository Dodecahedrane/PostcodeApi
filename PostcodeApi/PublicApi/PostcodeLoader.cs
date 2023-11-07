using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace PostcodeApi
{
    public class PostcodeLoader
    {
        // TODO Move this to an env variable
        private const string CsvFilePath = "wwwroot\\Data\\PostcodesLatLong.csv";

        public HashSet<PostcodeRecord> Records { get; private set; }

        public PostcodeLoader() 
        {
            Records = GetPostcodes();
        }

        private static HashSet<PostcodeRecord> GetPostcodes()
        {
            try
            {
                using var reader = new StreamReader(CsvFilePath);
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
                return csv.GetRecords<PostcodeRecord>().ToHashSet();
            }
            catch (Exception ex)
            {
                throw new FailedToLoadPostcodeData("Failed To Load The Postcode Data: ", ex);
            }
        }
    }


    public class FailedToLoadPostcodeData : Exception
    {
        public FailedToLoadPostcodeData() { }

        public FailedToLoadPostcodeData(string message) : base(message) { }

        public FailedToLoadPostcodeData(string message, Exception innerException) : base(message, innerException) { }
    }
}
