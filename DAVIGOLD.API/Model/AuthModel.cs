using System.ComponentModel.DataAnnotations;

namespace DAVIGOLD.API.Model
{
    public class AuthModel
    {  
       [Required]
        public string? Login { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
