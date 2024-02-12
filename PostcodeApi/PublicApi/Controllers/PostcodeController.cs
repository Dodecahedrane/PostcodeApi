using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using CsvHelper;
using PostcodeApi;
using CsvHelper.Configuration;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Routing.Constraints;
using static PostcodeApi.PostcodeHelper;

namespace PostcodeApi.Controllers;

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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            string postcode = PostcodeHelper.PostcodeFormatter(input.Postcode);

            PostcodeRecord? result = _postcodeLoader.Records[postcode];

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound($"Postcode {input.Postcode} not found.");
            }
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound($"Postcode {input.Postcode} not found.");
        }
        catch
        {
            return StatusCode(500, $"An error occurred");
        }
    }

    [HttpGet("AllPostcodes")]
    public IActionResult GetAllPostcode()
    {
        try
        {
            return Ok(_postcodeLoader.Records.Values.ToList());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("ValidatePostcode")]
    public IActionResult GetPostcodeValidation([FromQuery] PartialPostcodeInputModel input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            if (!IsPostcodeValid(PostcodeFormatter(input.Postcode)))
            {
                return Ok(false);
            }
            else
            {
                return Ok(true);
            }
        }
        catch
        {
            return StatusCode(500, $"An error occurred");
        }
        
    } 

    [HttpGet("PartialPostcode")]
    public IActionResult GetPartialPostcode([FromQuery] PartialPostcodeInputModel input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            string partialPostcode = PostcodeHelper.PostcodeFormatter(input.Postcode);

            List<PostcodeRecord> result = _postcodeLoader.Records
                .Values
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
