using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace PostcodeApi;

public class PostcodeLoader : IPostcodeLoader
{
    public Dictionary<string, PostcodeRecord> Records { get; private set; }

    public PostcodeLoader(string path)
    {
        Records = GetPostcodes(path);
    }

    private static Dictionary<string, PostcodeRecord> GetPostcodes(string path)
    {
        try
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

            // Read the records into a list
            List<PostcodeRecord> records = csv.GetRecords<PostcodeRecord>().ToList();

            // Create a dictionary to hold the postcode records
            var postcodeDictionary = new Dictionary<string, PostcodeRecord>();

            // Iterate through the records and populate the dictionary
            foreach (var record in records)
            {
                if (!string.IsNullOrEmpty(record.Postcode))
                {
                    // Use the postcode as the key
                    postcodeDictionary[record.Postcode] = record;
                }
            }

            return postcodeDictionary;
        }
        catch (Exception ex)
        {
            throw new FailedToLoadPostcodeData("Failed To Load The Postcode Data: ", ex);
        }
    }
}

public interface IPostcodeLoader
{
    Dictionary<string, PostcodeRecord> Records { get; }
}

public class FailedToLoadPostcodeData : Exception
{
    public FailedToLoadPostcodeData() { }

    public FailedToLoadPostcodeData(string message) : base(message) { }

    public FailedToLoadPostcodeData(string message, Exception innerException) : base(message, innerException) { }
}
