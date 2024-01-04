using System.ComponentModel.DataAnnotations;

namespace AppKursProject.ViewModels.Users
{
    public class CreateUserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        [Required]
        [Range(1950, 2100)]
        public int Year { get; set; }
    }
}
