using static PublicApi.PostcodeValidator;

namespace UnitTests
{
    public class PostcodeValidatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PostcodeFormatterTest()
        {

            Dictionary<string, string> testCases = new()
            {
                ["ba211aa"] = "BA211AA",
                ["Ba111Aa"] = "BA111AA",
                ["BA20 1AA"] = "BA201AA",
                ["BN4 5HD"] = "BN45HD",
                ["bN4 5hd"] = "BN45HD",
                ["bN45hd"] = "BN45HD"
            };


            foreach (var kvp in testCases)
            {
                if (PostcodeValidator.PostcodeFormatter(kvp.Key) != kvp.Value)
                {
                    Assert.Fail($"Postcode Formatter Not Formatting {kvp.Value} to {kvp.Key} Correctly");
                }
            }

            Assert.Pass();
        }

        [Test]
        public void IsPostcodeValid()
        {
            Dictionary<string, bool> testCases = new()
            {
                ["BA491AA"] = true,
                ["BN3 1PD"] = true,
                ["BN3 3WE"] = true,
                ["BS217DB"] = true,
                ["AA111AA"] = true,
                ["abcdecg"] = false,
                ["1234567"] = false,
                ["       "] = false,
                ["11AA12"] = false
            };

            foreach (var kvp in testCases)
            {
                bool valid = true;
                try
                {
                    string postcode = PostcodeValidator.IsPostcodeValid(kvp.Key);
                }
                catch (InvalidPostcode)
                {
                    valid = false;
                }

                if (kvp.Value != valid)
                {
                    Assert.Fail($"Postcode Validator Incorect For {kvp.Key} giving {valid} instead of {kvp.Value}");
                }
            }

            Assert.Pass();
        }
    }
}