using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using CsvHelper;
using PostcodeApi;
using CsvHelper.Configuration;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Routing.Constraints;
using static PostcodeApi.PostcodeHelper;

namespace PostcodeApi.Controllers
{
    [Route("")]
    public class PostcodeController : Controller
    {
        private readonly PostcodeLoader _postcodeLoader;

        public PostcodeController(PostcodeLoader postcodeLoader)
        {
            _postcodeLoader = postcodeLoader;
        }

        [HttpGet("Postcode")]
        public IActionResult GetPostcode([FromQuery] PostcodeInputModel input)
        {
            string postcode = input.Postcode;

            if (postcode == null)
            {
                return BadRequest($"Postcode URL Parameter is Null");
            }

            if (!PostcodeHelper.IsPostcodeValid(postcode))
            {
                return BadRequest("Invalid Postcode");
            }

            try
            {
                PostcodeRecord? result = _postcodeLoader.Records.FirstOrDefault(
                    x => x.Postcode == postcode
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
            catch
            {
                return StatusCode(500, $"An error occurred");
            }
        }

        [HttpGet("AllPostcodes")]
        public IActionResult GetPostcode()
        {
            try
            {
                return Ok(_postcodeLoader.Records);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("PartialPostcode")]
        public IActionResult GetPartialPostcode([FromQuery] PostcodeInputModel input)
        {
            string partialPostcode = PostcodeHelper.PostcodeFormatter(input.Postcode);

            if (partialPostcode == null)
            {
                return BadRequest($"Postcode URL Parameter is Null");
            }

            try
            {
                List<PostcodeRecord> result = _postcodeLoader.Records
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
