namespace EmployeeReview.Models
{
    public class EmployeeReviewModel
    {
        public EmployeeReviewModel()
        {
            userinfo = new UserInfo();
            category = new List<Category>();
            Reviews = new List<Review>();
        }
       public UserInfo userinfo { get; set; }

        public List<Category> category { get; set; }

        public List<Review> Reviews { get; set; }


    }
}
