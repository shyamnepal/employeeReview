using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeReview.Models;
using EmployeeReview.Services.Account.Interface;
using EmployeeReview.Services.ReviewEmployee.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace EmployeeReview.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewServices _review;
        private readonly INotyfService _notyf;
        private readonly IAccountInfoServices _accountInfo;
        private readonly IHttpContextAccessor _Httpcontext;
        public ReviewController(IReviewServices review, INotyfService notyf, IAccountInfoServices accountInfo, IHttpContextAccessor httpContext)
        {
            _review = review;
            _notyf = notyf;
            _accountInfo = accountInfo;
            _Httpcontext = httpContext;
        }
        public IActionResult Index()
        {
            return RedirectToAction("GetAllCategory");
        }

        public IActionResult CreateCategoryPage()
        {
            return View("Index");
        }

        public IActionResult CreateCategory([Bind(include: "CategoryName, CategoryMarks")] Category EmployeeCategory)
        {
            if (!ModelState.IsValid)
                return View("Index");

            _review.CreateCategory(EmployeeCategory);
            _notyf.Success("Successfully create Review Category");
            return RedirectToAction("GetAllCategory");

        }

        public IActionResult GetAllCategory()
        {
            //get all the category 
            var category = _review.GetAllCategory();
            if (category != null)
            {
                return View(category);
            }
            return View();
        }

        public IActionResult Edit(string CategoryId)
        {
            //get all the category 
            var category = _review.GetCategoryById(CategoryId);
            if (category != null)
            {
                return View("EditCategory", category);
            }
            return View("EditCategory");
        }

        public IActionResult EditCategory(Category category)
        {
            if (!ModelState.IsValid)
                return View("Index");

            _review.updateCategory(category);
            _notyf.Success("Successfyll Edit the category");
            return RedirectToAction("GetAllCategory");
        }

        public IActionResult DeleteCategory(string CategoryId)
        {
            _review.DeleteCategory(CategoryId);
            _notyf.Success("Successfully Delete the category");
            return RedirectToAction("GetAllCategory");
        }

        //get the review
        public IActionResult GetReview(string UserId)
        {
            EmployeeReviewModel model = new EmployeeReviewModel();
            model.userinfo.UserId = Guid.Parse(UserId);
            model= _review.GetEmployeeReviewModel(UserId);
            return View("EmployeeReview", model);

        }

        public IActionResult CreateReviewEmployee(EmployeeReviewModel employee)
        {
            var allUser = _accountInfo.GetAllUser();
            if (allUser.Count > 0)
            {
                var dropdown = allUser.Select(x => new SelectListItem()
                {
                    Value = x.UserId.ToString(),
                    Text = x.UserName


                }).ToList();
                ViewData["allUser"] = dropdown;
            }
            return View("SelectEmployee");

        }

        public IActionResult CreateReview(EmployeeReviewModel allCategory)
        {

            //get the user Name of the login user. 
            var userName = _Httpcontext.HttpContext.User.Identity.Name;
            // Insert the review 

            for (int i=0; i<allCategory.category.Count;i++)
            {
                Review emp = new Review()
                {
                    UserId = Guid.Parse(allCategory.userinfo.UserId.ToString()),
                    CategoryId = Guid.Parse(allCategory.category[i].CategoryId.ToString()),
                    Comment = allCategory.category[i].employee.Comment,
                    SkillRating = allCategory.category[i].employee.SkillRating,
                    ReviewDate = DateTime.Now,
                    ReviewBy = userName

                };

               




               var ReviewId= _review.CreateReview(emp);

                EmpReview empReview = new EmpReview()
                {
                    CategoryId = Guid.Parse(allCategory.category[i].CategoryId.ToString()),
                    ReviewId = Guid.Parse(ReviewId)
                };

                _review.createEmployeeRevieww(empReview);
            }


            _notyf.Success("Successfully create Review");
            return RedirectToAction("GetReview", new {Userid=allCategory.userinfo.UserId});

        }

        public IActionResult GetEmployeeReview()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            

             var result = _review.GetReview(userId);
            return View(result);
        }


       



    }
}
