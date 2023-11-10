using System.ComponentModel.DataAnnotations;

namespace PostcodeApi
{
    public class PostcodeInputModel
    {
        [Required]
        [MaxLength(8)]
        [MinLength(5)]
        public required string Postcode { get; set; }
    }
}
