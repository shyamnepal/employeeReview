using System.ComponentModel.DataAnnotations;

namespace EmployeeReview.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage ="User Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string password { get; set; }
    }
}
