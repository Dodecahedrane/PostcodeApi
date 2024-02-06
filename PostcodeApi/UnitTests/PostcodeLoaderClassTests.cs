using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests;

public class PostcodeLoaderClassTests
{
    // Path to CSV
    const string path = "..\\..\\..\\..\\PublicApi\\wwwroot\\Data\\PostcodesLatLong.csv";

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PostcodeLoaderSuccess()
    {
        try
        {
            PostcodeLoader postcodeLoader = new PostcodeLoader(path);
        }
        catch (FailedToLoadPostcodeData)
        {
            Assert.Fail("Failed To Load Postcode Data Exception");
        }

        Assert.Pass();
    }

    [Test]
    public void PostcodeLoaderFail()
    {
        Assert.Throws<FailedToLoadPostcodeData>(() => new PostcodeLoader("Non Existant Path"));
    }
}
