using EmployeeReview.Models;

namespace EmployeeReview.Services.Account.Interface
{
    public interface IAccountInfoServices
    {
        string createUserInfo(UserInfo user);
        string createContactInfo(CantactInfo contact);

        CantactInfo getUserByEmail(string email);

        string createDocumentInfo(DocumentInfo document);

        void CreateUserCredential(UserCredential user);
        UserInfo GetUserInfo(string userName);
        bool Login(string UserName, string Password);

        UserInfo GetUserById(string id);

        CantactInfo getContactById(string id);

        List<UserInfo> GetAllUser();

        DocumentInfo getDocumentById(string id);

  


       

        
    }
}
