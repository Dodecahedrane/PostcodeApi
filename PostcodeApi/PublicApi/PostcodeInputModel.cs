using System.ComponentModel.DataAnnotations;

namespace PublicApi
{
    public class PostcodeInputModel
    {
        [Required]
        [MaxLength(7)]
        [MinLength(6)]
        public required string Postcode { get; set; }
    }
}
