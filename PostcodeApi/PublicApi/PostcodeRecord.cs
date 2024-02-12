using CsvHelper.Configuration.Attributes;

namespace PostcodeApi;

public class PostcodeRecord
{
    [Name("pcd")]
    public string? Postcode
    {
        get => _postcode;
        set => _postcode = value.Replace(" ", string.Empty);
    }

    private string? _postcode;

    [Name("lat")]
    public required double Latitude { get; set; }

    [Name("long")]
    public required double Longitude { get; set; }
}
