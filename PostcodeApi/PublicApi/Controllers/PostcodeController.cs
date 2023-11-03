﻿using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using CsvHelper;
using PublicApi;
using CsvHelper.Configuration;
using System.Reflection.PortableExecutable;

namespace PublicApi.Controllers
{
    public class PostcodeController : Controller
    {
        private readonly CsvLoader _csvLoader;

        public PostcodeController(CsvLoader csvLoader)
        {
            _csvLoader = csvLoader;
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
    }
}