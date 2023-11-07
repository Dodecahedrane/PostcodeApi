using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace PostcodeApi
{
    public class PostcodeLoader : IPostcodeLoader
    {
        public HashSet<PostcodeRecord> Records { get; private set; }

        public PostcodeLoader(string path) 
        {
            Records = GetPostcodes(path);
        }

        private static HashSet<PostcodeRecord> GetPostcodes(string path)
        {
            try
            {
                using var reader = new StreamReader(path);
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
                return csv.GetRecords<PostcodeRecord>().ToHashSet();
            }
            catch (Exception ex)
            {
                throw new FailedToLoadPostcodeData("Failed To Load The Postcode Data: ", ex);
            }
        }
    }

    public interface IPostcodeLoader
    {
        HashSet<PostcodeRecord> Records { get; }
    }

    public class FailedToLoadPostcodeData : Exception
    {
        public FailedToLoadPostcodeData() { }

        public FailedToLoadPostcodeData(string message) : base(message) { }

        public FailedToLoadPostcodeData(string message, Exception innerException) : base(message, innerException) { }
    }
}
