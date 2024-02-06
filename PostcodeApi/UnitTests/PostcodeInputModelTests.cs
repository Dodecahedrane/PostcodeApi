using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests;

public class PostcodeInputModelTests
{
    [Test]
    public void TestModelStateWithValidPostcode()
    {
        var model = new PostcodeInputModel
        {
            Postcode = "PL48AA"
        };

        var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

        Assert.IsTrue(isValid);
        Assert.IsEmpty(validationResults);
    }

    [Test]
    public void TestModelStateWithNullPostcode()
    {
        var model = new PostcodeInputModel
        {
            Postcode = null
        };

        var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

        Assert.IsFalse(isValid);
        Assert.IsNotEmpty(validationResults);
        Assert.That(validationResults.Count, Is.EqualTo(1));
        Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("Postcode is Required"));
    }

    [Test]
    public void TestModelStateWithInvalidFormatPostcode()
    {
        var model = new PostcodeInputModel
        {
            Postcode = "ABCDE"
        };

        var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

        Assert.IsFalse(isValid);
        Assert.IsNotEmpty(validationResults);
        Assert.That(validationResults.Count, Is.EqualTo(1));
        Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("Postcode Not Valid"));
    }
}
