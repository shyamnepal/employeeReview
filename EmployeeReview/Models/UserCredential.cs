using System.ComponentModel.DataAnnotations;

namespace EmployeeReview.Models
{
    public class UserCredential
    {
        public string UserId { get; set; }
        [Required(ErrorMessage ="Password is required")]
        public string passwordHash { get; set; }
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [Required(ErrorMessage = "confirm Password is required")]
        [Compare("PasswordHash", ErrorMessage ="Password does not match")]
        public string confirmPassword { get; set; }
        public string PasswordSalt { get; set; }
        [Required(ErrorMessage ="User Name is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string UserName { get; set; }
    }
}
