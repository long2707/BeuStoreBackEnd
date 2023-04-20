using System.ComponentModel.DataAnnotations;

namespace BeuStoreApi.Models.StaffDTO
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
