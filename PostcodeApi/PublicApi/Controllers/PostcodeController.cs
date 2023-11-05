using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using CsvHelper;
using PublicApi;
using CsvHelper.Configuration;
using System.Reflection.PortableExecutable;

namespace PublicApi.Controllers
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
            return Ok("Visit https://github.com/Dodecahedrane/PostcodeApi to learn about this API!");
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
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("AllPostcodes")]
        public IActionResult GetPostcodeLatLong()
        {
            try
            {
                return Ok(_csvLoader.Records);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("PartialPostcode")]
        public IActionResult GetPartialPostcode([FromBody] PostcodeInputModel input)
        {
            string partialPostcode = input.Postcode;

            if (partialPostcode == null)
            {
                return BadRequest($"Postcode URL Parameter is Null");
            }

            try
            {
                List<PostcodeRecord> result = _csvLoader.Records
                    .Where(record => record.Postcode.Contains(partialPostcode))
                    .ToList();

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound($"No partial matches for {partialPostcode} found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
