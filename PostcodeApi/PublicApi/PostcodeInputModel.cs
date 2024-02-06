using System.ComponentModel.DataAnnotations;

namespace PostcodeApi;

public class PostcodeInputModel
{
    [Required(ErrorMessage = "Postcode is Required")]
    [IsPostcode(ErrorMessage = "Postcode Not Valid")]
    public required string Postcode { get; set; }
}

public class PartialPostcodeInputModel
{
    [Required(ErrorMessage = "Postcode is Required")]
    [MaxLength(8, ErrorMessage = "Max Length of 8 Chars")]
    public required string Postcode { get; set; }

}

public class IsPostcode : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is string postcode)
        {
            if (PostcodeHelper.IsPostcodeValid(PostcodeHelper.PostcodeFormatter(postcode)))
            {
                return true;
            }

            return false;
        }
        return false;
    }
}
