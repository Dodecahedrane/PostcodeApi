using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using CsvHelper;
using PostcodeApi;
using CsvHelper.Configuration;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Routing.Constraints;
using static PostcodeApi.PostcodeValidator;

namespace PostcodeApi.Controllers
{
    [Route("")]
    public class PostcodeController : Controller
    {
        private readonly CsvLoader _csvLoader;

        public PostcodeController(CsvLoader csvLoader)
        {
            _csvLoader = csvLoader;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Visit https://github.com/Dodecahedrane/PublicApi to learn about this API!");
        }

        [HttpGet("Postcode")]
        public IActionResult GetPostcodeLatLong([FromBody] PostcodeInputModel input)
        {
            string postcode = input.Postcode;

            if (postcode == null)
            {
                return BadRequest($"Postcode URL Parameter is Null");
            }

            try
            {
                PostcodeRecord? result = _csvLoader.Records.FirstOrDefault(
                    x => x.Postcode == PostcodeValidator.IsPostcodeValid(postcode)
                    );
                
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound($"Postcode {postcode} not found.");
                }
            }
            catch (InvalidPostcode ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, $"An error occurred");
            }
        }

        [HttpGet("AllPostcodes")]
        public IActionResult GetAllPostcodes()
        {
            try
            {
                return Ok(_csvLoader.Records);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("PartialPostcode")]
        public IActionResult GetPartialPostcode([FromBody] PostcodeInputModel input)
        {
            string partialPostcode = PostcodeValidator.PostcodeFormatter(input.Postcode);

            if (partialPostcode == null)
            {
                return BadRequest($"Postcode URL Parameter is Null");
            }

            try
            {
                List<PostcodeRecord> result = _csvLoader.Records
                    .Where(record => record.Postcode.Contains(partialPostcode))
                    .ToList();

                if (result.Count > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound($"No partial matches for {partialPostcode} found.");
                }
            }
            catch
            {
                return StatusCode(500, $"An error occurred");
            }
        }
    }
}
