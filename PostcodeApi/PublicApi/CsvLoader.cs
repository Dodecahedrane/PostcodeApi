using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace PublicApi
{
    public class CsvLoader
    {
        private const string CsvFilePath = "wwwroot\\Data\\PostcodesLatLong.csv";

        public List<PostcodeRecord> Records { get; private set; }

        public CsvLoader() 
        {
            Records = GetRecords();
        }

        private List<PostcodeRecord> GetRecords()
        {
            try
            {
                using var reader = new StreamReader(CsvFilePath);
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
                return csv.GetRecords<PostcodeRecord>().ToList();
            }
            catch
            {
                throw new FailedToLoadPostcodeData("Failed To Load The Postcode Data");
            }
            
        }
    }

    public interface ICsvLoader
    {
        List<PostcodeRecord> Records { get; set; }
        List<PostcodeRecord> GetRecords();
    }

    public class FailedToLoadPostcodeData : Exception
    {
        public FailedToLoadPostcodeData() { }

        public FailedToLoadPostcodeData(string message) : base(message) { }

        public FailedToLoadPostcodeData(string message, Exception innerException) : base(message, innerException) { }
    }
}
