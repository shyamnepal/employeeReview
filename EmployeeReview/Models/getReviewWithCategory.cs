using System.ComponentModel.DataAnnotations;

namespace EmployeeReview.Models
{
    public class getReviewWithCategory
    {
        //public string CategoryName { get; set; } = null!;


        //public int CategoryMarks { get; set; }

        //public string? UpdatedDate { get; set; }
        //public string? UpdatedBy { get; set; }

        //public Guid ReviewId { get; set; }
        //public Guid UserId { get; set; }

        //public string SkillRating { get; set; } = null!;

        //public DateTime? ReviewDate { get; set; }

        //public string? ReviewBy { get; set; }
        //public string Comment { get; set; }
        public string Rating { get; set; } = null!;
        public string CategoryName { get; set; }
        public string  SkillRating { get; set; }
        public int CategoryMarks { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }




    }
}
