using EmployeeReview.Models;
using EmployeeReview.Services.ReviewEmployee.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.InteropServices;

namespace EmployeeReview.Services.ReviewEmployee.Implementation
{
    public class ReviewServices : IReviewServices
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly ReviewSystemContext _reviewSystemContext;
        public ReviewServices(IHttpContextAccessor httpContext, ReviewSystemContext reviewSystemContext)
        {
            _httpContext = httpContext;
            _reviewSystemContext = reviewSystemContext;

        }

        //Create Category of the review 
        public void CreateCategory(Category ReviewCategory)
        {
            //get the login user. 

            try
            {
                var userName = _httpContext.HttpContext.User.Identity.Name;
                SqlParameter categoryNameParam = new SqlParameter("@CategoryName", ReviewCategory.CategoryName);
                SqlParameter categoryMarksParam = new SqlParameter("@CategoryMarks", ReviewCategory.CategoryMarks);
                SqlParameter createdDateParam = new SqlParameter("@CreatedDate", DateTime.Now);
                SqlParameter createdByParam = new SqlParameter("@CreatedBy", userName);

                _reviewSystemContext.Database.ExecuteSqlRaw("EXEC CreateCategory @CategoryName,@CategoryMarks, @CreatedDate,@CreatedBy", categoryNameParam, categoryMarksParam, createdDateParam, createdByParam);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void createEmployeeRevieww(EmpReview empReview)
        {
            try
            {
                SqlParameter ReviewIdParam = new SqlParameter("@ReviewId", empReview.ReviewId);
                SqlParameter CategoryIdParam = new SqlParameter("@CategoryId", empReview.CategoryId);

                _reviewSystemContext.Database.ExecuteSqlRaw("EXEC createEmployeeReview @ReviewId, @CategoryId", ReviewIdParam, CategoryIdParam);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
           
        }

        //Review the User. 
        public string CreateReview(Review review)
        {
            try
            {
                //get the first value from stirng 
                var rating = review.SkillRating.Substring(0, 1);
                
                //Insert review 
                SqlParameter ReviewIdParam = new SqlParameter("@ReviewId", SqlDbType.UniqueIdentifier);
                ReviewIdParam.Direction = ParameterDirection.Output;
                string sql = "EXEC CreateReview @UserId, @CategoryId, @SkillRating, @ReviewDate, @ReviewBy, @Comment, @ReviewId Out";

                _reviewSystemContext.Database.ExecuteSqlRaw(sql,
                    new SqlParameter("@UserId", review.UserId),
                    new SqlParameter("@CategoryId", review.CategoryId),
                    new SqlParameter("@SkillRating", review.SkillRating),
                    new SqlParameter("@ReviewDate", review.ReviewDate),
                    new SqlParameter("@ReviewBy", review.ReviewBy),
                    new SqlParameter("@Comment", review.Comment),
                   ReviewIdParam


                );
                Guid ReviewId = (Guid)ReviewIdParam.Value;
                return ReviewId.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //Delte the category.
        public void DeleteCategory(string Id)
        {
            //Sql parameter. 
            var CategoryIdParam = new SqlParameter("@CategoryId", Id);
            _reviewSystemContext.Database.ExecuteSqlRaw("EXEC DeleteCategory @CategoryId", CategoryIdParam);
        }

        //Get all the category.
        public List<Category> GetAllCategory()
        {
            try
            {
                var Category = _reviewSystemContext.Categoryies.FromSqlRaw("EXEC GetAllCategory").ToList();
                if (Category != null || Category.Count > 0)
                {
                    return Category;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Get Category using id. 
        public Category GetCategoryById(string id)
        {
            try
            {
                //Sql parameter.
                var categoryIdParam = new SqlParameter("@CategoryId", id);
                var category = _reviewSystemContext.Categoryies.FromSqlRaw("EXEC getCategoryById @CategoryId", categoryIdParam).ToList();
                if (category != null || category.Count > 0)
                {
                    return category.FirstOrDefault();
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public EmployeeReviewModel GetEmployeeReviewModel(string UserId)
        {
            try
            {
                //Get the user info.
                var UserIdParam = new SqlParameter("@UserId", UserId);
                var result = _reviewSystemContext.UserInfos.FromSqlRaw("EXEC getUserById @UserId", UserIdParam).ToList();
                UserInfo data = null;
                if (result != null || result.Count > 0)
                {
                    data = result.FirstOrDefault();
                }

                //Get all Category
                var Category = _reviewSystemContext.Categoryies.FromSqlRaw("EXEC GetAllCategory").ToList();

                var EmployeeCategory = _reviewSystemContext.EmployeeSkills.FromSqlRaw("EXEC GetAllEmployeeCategory").ToList();
                var Allinfo = new EmployeeReviewModel()
                {
                    userinfo = data,
                    category = Category,
                    Reviews = EmployeeCategory,




                };
                return Allinfo;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Update the Review category.
        public void updateCategory(Category ReviewCategory)
        {
            try
            {
                var userName = _httpContext.HttpContext.User.Identity.Name;
                var categoryIdParam = new SqlParameter("@CategoryId", ReviewCategory.CategoryId);

                SqlParameter categoryNameParam = new SqlParameter("@CategoryName", ReviewCategory.CategoryName);
                SqlParameter categoryMarksParam = new SqlParameter("@CategoryMarks", ReviewCategory.CategoryMarks);
                SqlParameter updatedDateParam = new SqlParameter("@UpdatedDate", DateTime.Now);
                SqlParameter updatedByParam = new SqlParameter("@UpdatedBy", userName);
                _reviewSystemContext.Database.ExecuteSqlRaw("EXEC EditCategory @CategoryId, @CategoryName,@CategoryMarks,@UpdatedDate, @UpdatedBy ",
                    categoryIdParam, categoryNameParam, categoryMarksParam, updatedDateParam, updatedByParam);
            }
            catch (Exception ex)
            {

            }
        }

        public List<getReviewWithCategory> GetReview(string UserId)
        {
            try
            {

             
             
                var UserIdParam = new SqlParameter("@UserId", UserId);
               var review= _reviewSystemContext.GetReviewWithCategories.FromSqlRaw("EXEC checkReview @UserId",UserIdParam).ToList();
                return review;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
