using CsvHelper.Configuration.Attributes;

namespace PublicApi
{
    public class PostcodeRecord
    {
        [Name("pcd")]
        public required string Postcode { get; set; }

        [Name("lat")]
        public required double Latitude { get; set; }

        [Name("long")]
        public required double Longitude { get; set; }
    }
}
