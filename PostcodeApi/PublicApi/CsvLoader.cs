using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace PublicApi
{
    public class CsvLoader
    {
        private const string CsvFilePath = "C:\\Users\\Oliver\\Downloads\\ONSPD_AUG_2023_UK\\Data\\PostcodesLatLong.csv";

        public List<PostcodeRecord> Records { get; private set; }

        public CsvLoader() 
        {
            Records = GetRecords();
        }

        private List<PostcodeRecord> GetRecords()
        {
            using var reader = new StreamReader(CsvFilePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            return csv.GetRecords<PostcodeRecord>().ToList();
        }
    }

    public interface ICsvLoader
    {
        List<PostcodeRecord> Records { get; set; }
        List<PostcodeRecord> GetRecords();
    }
}
