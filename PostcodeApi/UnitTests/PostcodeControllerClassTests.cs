using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PostcodeApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
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
        public void GetPostcodeOk()
        {
            var result = _controller.GetPostcode(new PostcodeInputModel { Postcode = "PL48AA" });

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void GetPostcodeOk()
        {
            var result = _controller.GetPostcode(new PostcodeInputModel { Postcode = "pl48aa" });

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void GetPostcodeBadRequestInvalidPostcode()
        {
            var result = _controller.GetPostcode(new PostcodeInputModel { Postcode = "ABC" });

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void GetPostcodeBadRequestNullPostcode()
        {
            var result = _controller.GetPostcode(new PostcodeInputModel { Postcode = "" });

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
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
            var result = _controller.GetPartialPostcode(new PostcodeInputModel { Postcode = "PL4" });

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void GetPartialPostcodeBadRequest()
        {
            var result = _controller.GetPartialPostcode(new PostcodeInputModel { Postcode = "" });

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void GetPartialPostcodeNotFound()
        {
            var result = _controller.GetPartialPostcode(new PostcodeInputModel { Postcode = "ZZ999" });

            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}
