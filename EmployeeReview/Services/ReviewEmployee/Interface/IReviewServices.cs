using EmployeeReview.Models;

namespace EmployeeReview.Services.ReviewEmployee.Interface
{
    public interface IReviewServices
    {
        void CreateCategory(Category ReviewCategory);
        List<Category> GetAllCategory();
        Category GetCategoryById(string Id);
        void updateCategory(Category ReviewCategory);
        void DeleteCategory(string Id);
        EmployeeReviewModel GetEmployeeReviewModel(string UserId);

        string CreateReview(Review review);
        void createEmployeeRevieww(EmpReview empReview);

        List<getReviewWithCategory> GetReview(string UserId);


    }
}
