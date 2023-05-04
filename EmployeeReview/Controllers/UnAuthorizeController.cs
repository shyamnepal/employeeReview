using Microsoft.AspNetCore.Mvc;

namespace EmployeeReview.Controllers
{
    public class UnAuthorizeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
