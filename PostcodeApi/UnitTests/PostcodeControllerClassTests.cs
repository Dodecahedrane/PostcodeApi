using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PostcodeApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests;

public class PostcodeControllerClassTests
{
    private PostcodeController _controller;

    [SetUp]
    public void Setup()
    {
        const string path = "..\\..\\..\\..\\PublicApi\\wwwroot\\Data\\PostcodesLatLong.csv";
        PostcodeLoader loader = new(path);
        _controller = new PostcodeController(loader);
    }

    [Test]
    public void GetPostcodeUppercaseOk()
    {
        var result = _controller.GetPostcode(new PostcodeInputModel { Postcode = "PL48AA" });

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void GetPostcodeLowercaseOk()
    {
        var result = _controller.GetPostcode(new PostcodeInputModel { Postcode = "pl48aa" });

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void GetPostcodeNotFound()
    {
        var result = _controller.GetPostcode(new PostcodeInputModel { Postcode = "ZZ999ZZ" });

        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
    }

    [Test]
    public void GetAllPostcodeOk()
    {
        var result = _controller.GetAllPostcode();

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void GetPartialPostcodeOk()
    {
        var result = _controller.GetPartialPostcode(new PartialPostcodeInputModel { Postcode = "PL4" });

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void GetPartialPostcodeNotFound()
    {
        var result = _controller.GetPartialPostcode(new PartialPostcodeInputModel { Postcode = "ZZ999" });

        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
    }

    [Test]
    public void GetPostcodeValidation()
    {
        Dictionary<string, bool> testCases = new()
        {
            ["ba211aa"] = true,
            ["Ba111Aa"] = true,
            ["BA20 1AA"] = true,
            ["BN4 5HD"] = true,
            ["bN4 5hd"] = true,
            ["bN45hd"] = true,
            ["aqerrq"] = false,
            ["1234"] = false,

        };


        foreach (var kvp in testCases)
        {
            var result = _controller.GetPostcodeValidation(new PartialPostcodeInputModel { Postcode = kvp.Key });

            Assert.That(result, Is.InstanceOf<OkObjectResult>());

            bool resultBool = (bool)((OkObjectResult)result).Value;

            if (resultBool != kvp.Value)
            {
                Assert.Fail($"{kvp.Value} gave the result {resultBool} instead of {kvp.Key}");
            }
        }

        Assert.Pass();
    }
}
