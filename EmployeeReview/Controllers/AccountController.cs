using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeReview.Models;
using EmployeeReview.Services.Account.Interface;
using EmployeeReview.Services.RoleService.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace EmployeeReview.Controllers
{
    public class Account : Controller
    {
        private readonly IAccountInfoServices _AccountInfoServices;
        private readonly IHttpContextAccessor _Httpcontext;
        private readonly INotyfService _notyf;
        private readonly IRoleServices _roleServices;

        public Account(IAccountInfoServices AccountInfoServices, IHttpContextAccessor httpContext, INotyfService notyf, IRoleServices roleService)
        {
            _AccountInfoServices = AccountInfoServices;
            _Httpcontext = httpContext;
            _notyf = notyf;
            _roleServices = roleService;
        }
        public ActionResult Index()
        {
            return View("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        public ActionResult UserRegisterInfo(UserInfo info)
        {

            if (!ModelState.IsValid)
                return View("Register");
            var response = _AccountInfoServices.createUserInfo(info);

            var contactId = _Httpcontext.HttpContext.Session.GetString("contactId");
            if (contactId != null)
            {
                return RedirectToAction("GetContact", new { Id = contactId });
            }

            return RedirectToAction("ContactInfo", new { id = response });
        }

        public ActionResult ContactInfo(string id)
        {
            _Httpcontext.HttpContext.Session.SetString("userId", id);
            ViewBag.Id = _Httpcontext.HttpContext.Session.GetString("userId");
            return View();
        }


        public ActionResult CreateContactInfo(CantactInfo contact)
        {
            if (!ModelState.IsValid)
                return View("ContactInfo");
            var contactId = _AccountInfoServices.createContactInfo(contact);

            ViewBag.Id = _Httpcontext.HttpContext.Session.GetString("userId");


            if (contactId == null)
            {
                _notyf.Error("Email is already exist");
                return View("ContactInfo");
            }

            _Httpcontext.HttpContext.Session.SetString("contactId", contactId);
            ViewBag.contactId = _Httpcontext.HttpContext.Session.GetString("contactId");
            


            var docId = _Httpcontext.HttpContext.Session.GetString("documentId");
            if (docId != null)
            {
                return RedirectToAction("getDocument", new { id = docId });
            }

            return View("DocumentInfo");
        }



        public ActionResult CreateDocumentInfo(DocumentInfo document)
        {
            string documentId = _AccountInfoServices.createDocumentInfo(document);

            ViewBag.Id = _Httpcontext.HttpContext.Session.GetString("userId");
            ViewBag.contactId = _Httpcontext.HttpContext.Session.GetString("contactId");
            _Httpcontext.HttpContext.Session.SetString("documentId", documentId);
            ViewBag.docId = _Httpcontext.HttpContext.Session.GetString("documentId");


            return View("UserCredential");
        }

        public ActionResult CreateUserCredential(UserCredential user)
        {
            //Check the model is valid 
            if (ModelState.IsValid)
                return View("UserCredential");

            //first userName exist 
            var checkUser = _AccountInfoServices.GetUserInfo(user.UserName);
            if (checkUser != null)
            {              
                return View("UserCredential");
            }

            _AccountInfoServices.CreateUserCredential(user);

            //Set the session to empty.
            _Httpcontext.HttpContext.Session.SetString("userId", "");
            _Httpcontext.HttpContext.Session.SetString("contactId", "");
            _Httpcontext.HttpContext.Session.SetString("documentId", "");
            _notyf.Success("Successfylly Create account");
            return View("Login");
        }

        //Login Credential 

        public async Task<IActionResult> LoginUser(LoginUser user)
        {
            //first check if the userName is exist 
            var checkUser=   _AccountInfoServices.GetUserInfo(user.UserName);
            
            if (checkUser == null)
            {
                _notyf.Error("UserName not found");
                return View("Login");
            }            
            
            bool result = _AccountInfoServices.Login(user.UserName, user.password);
            if (result)
            {
               

                //Create the claims for the authenticated user.
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,user.UserName),

                };

                // Get the UserId by userName.
                string userId = null;
                var getUser = _AccountInfoServices.GetUserInfo(user.UserName);
                if (getUser != null)
                {
                    userId = getUser.UserId.ToString();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));

                }

                //get the RoleId from UserId
                var GetMultipleRoleId = _roleServices.GetRoleIdByUserId(userId);
                if (GetMultipleRoleId != null)
                {
                    foreach(var roleId in GetMultipleRoleId)
                    {
                      var roleName=  _roleServices.RoleNameById(roleId.RoleId.ToString());
                        if (roleName != null)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, roleName));
                        }
                        
                    }
                }

                // Create the authentication properties
                var authProperties = new AuthenticationProperties
                {
                    // Set whether the cookie should be persistent
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Set the expiration time for the cookie
                };


                // Create the claims identity
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Sign in the user
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);


                _notyf.Success("Successfully login");
                return RedirectToAction("Index","Dashboard");
                
            }
                
            _notyf.Error("Password does not match");
            return View("Login");
        }

        //Get the user By id
        public ActionResult GetUser(string Id)
        {
            var result = _AccountInfoServices.GetUserById(Id);

            if (result != null)
            {
                return View("Register", result);
            }
            return null;
        }



        //Get the Contact By id 
        public ActionResult GetContact(string Id)
        {
            var result = _AccountInfoServices.getContactById(Id);
            ViewBag.Id = _Httpcontext.HttpContext.Session.GetString("userId");
            if (result != null)
            {
                return View("ContactInfo", result);
            }
            return null;
        }

        //Get the Document By Id 

        public ActionResult getDocument(string id)
        {
            var result = _AccountInfoServices.getDocumentById(id);
            ViewBag.Id = _Httpcontext.HttpContext.Session.GetString("userId");
            ViewBag.contactId = _Httpcontext.HttpContext.Session.GetString("contactId");
            if (result != null)
            {
                return View("DocumentInfo", result);
            }
            return null;
        }

        //Logout
        public IActionResult Logout()
        {
            Response.Cookies.Delete(".AspNetCore.Cookies");
            return RedirectToAction("Index", "Account");
        }

    }
}
