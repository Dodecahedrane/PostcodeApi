using CsvHelper.Configuration.Attributes;

namespace PublicApi
{
    public class PostcodeRecord
    {
        [Name("pcd")]
        public string Postcode
        {
#pragma warning disable CS8603 // Possible null reference return. Not sure if/how this should be structured differently?
            get => _postcode;
#pragma warning restore CS8603 // Possible null reference return.
            set => _postcode = value.Replace(" ", string.Empty);
        }

        private string? _postcode;

        [Name("lat")]
        public required double Latitude { get; set; }

        [Name("long")]
        public required double Longitude { get; set; }
    }
}
