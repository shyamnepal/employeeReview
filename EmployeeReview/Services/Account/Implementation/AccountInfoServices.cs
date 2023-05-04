using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeReview.Models;
using EmployeeReview.Services.Account.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Cryptography;

namespace EmployeeReview.Services.Account.Implementation
{

    public class AccountInfoServices : IAccountInfoServices
    {
        private readonly ReviewSystemContext _reviewSystemContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
     
        public AccountInfoServices(ReviewSystemContext reviewSystemContext, IWebHostEnvironment webHostEnvironment)
        {
            _reviewSystemContext = reviewSystemContext;
            _webHostEnvironment = webHostEnvironment;
           
        }

        public string createContactInfo(CantactInfo contact)
        {
            try
            {
                //Check if address is not null then update the form.
                if (contact.ContactId != null)
                {
                    var UserIdParam = new SqlParameter("@UserId", contact.UserId);
                    var PermanentAddressParam = new SqlParameter("@PermanentAddress", contact.PermanentAddress);
                    var PermanentCountryParam = new SqlParameter("@PermanentCountry", contact.PermanentCountry);
                    var PermanentStateParam = new SqlParameter("@PermanentState", contact.PermanentState);
                    var TemporaryAddressParam = new SqlParameter("@TemporaryAddress", contact.TemporaryAddress);
                    var TemporaryCountryParam = new SqlParameter("@TemporaryCountry", contact.TemporaryCountry);
                    var TemporaryStateParam = new SqlParameter("@TemporaryState", contact.TemporaryState);
                    var EmailParam = new SqlParameter("@Email", contact.Email);
                    var AlternativeEmailParam = new SqlParameter("@AlternativeEmail", contact.AlternativeEmail ?? (object)DBNull.Value);
                    var ContactNumberParam = new SqlParameter("@ContactNumber", contact.ContactNumber);
                    var EmergencyContactNumberParam = new SqlParameter("@EmergencyContactNumber", contact.EmergencyContactNumber ?? (object)DBNull.Value);
                    var ContactNameParam = new SqlParameter("@ContactName", contact.ContactName ?? (object)DBNull.Value);
                    var ContactRelationParam = new SqlParameter("@ContactRelation", contact.ContactRelation ?? (object)DBNull.Value);
                    var FacebookParam = new SqlParameter("@Facebook", contact.Facebook ?? (object)DBNull.Value);
                    var SkypeParam = new SqlParameter("@Skype", contact.Skype ?? (object)DBNull.Value);
                    var LinkedInParam = new SqlParameter("@LinkedIn", contact.LinkedIn ?? (object)DBNull.Value);
                    var CreatedDateParam = new SqlParameter("@CreatedDate", DateTime.Now);

                    _reviewSystemContext.Database.ExecuteSqlRaw("EXEC UpdateContactInfo @UserId, @PermanentAddress, @PermanentCountry, @PermanentState, @TemporaryAddress, @TemporaryCountry, @TemporaryState, @Email, @AlternativeEmail, @ContactNumber, @EmergencyContactNumber, @ContactName, @ContactRelation, @Facebook, @Skype, @LinkedIn, @CreatedDate",
                         UserIdParam,
                         PermanentAddressParam,
                         PermanentCountryParam,
                         PermanentStateParam,
                         TemporaryAddressParam,
                         TemporaryCountryParam,
                         TemporaryStateParam,
                         EmailParam,
                         AlternativeEmailParam,
                         ContactNumberParam,
                         EmergencyContactNumberParam,
                         ContactNameParam,
                         ContactRelationParam,
                         FacebookParam,
                         SkypeParam,
                         LinkedInParam,
                         
                         CreatedDateParam
                       
                         );

                    return contact.ContactId.ToString();
                }
                else
                {

                    //First check the email is already exist for the user. 
                    var check= getUserByEmail(contact.Email);
                    if (check != null)
                    {
                        return null;
                    }
                    else
                    {
                        var UserIdParam = new SqlParameter("@UserId", contact.UserId);
                        var PermanentAddressParam = new SqlParameter("@PermanentAddress", contact.PermanentAddress);
                        var PermanentCountryParam = new SqlParameter("@PermanentCountry", contact.PermanentCountry);
                        var PermanentStateParam = new SqlParameter("@PermanentState", contact.PermanentState);
                        var TemporaryAddressParam = new SqlParameter("@TemporaryAddress", contact.TemporaryAddress);
                        var TemporaryCountryParam = new SqlParameter("@TemporaryCountry", contact.TemporaryCountry);
                        var TemporaryStateParam = new SqlParameter("@TemporaryState", contact.TemporaryState);
                        var EmailParam = new SqlParameter("@Email", contact.Email);
                        var AlternativeEmailParam = new SqlParameter("@AlternativeEmail", contact.AlternativeEmail ?? (object)DBNull.Value);
                        var ContactNumberParam = new SqlParameter("@ContactNumber", contact.ContactNumber);
                        var EmergencyContactNumberParam = new SqlParameter("@EmergencyContactNumber", contact.EmergencyContactNumber ?? (object)DBNull.Value);
                        var ContactNameParam = new SqlParameter("@ContactName", contact.ContactName ?? (object)DBNull.Value);
                        var ContactRelationParam = new SqlParameter("@ContactRelation", contact.ContactRelation ?? (object)DBNull.Value);
                        var FacebookParam = new SqlParameter("@Facebook", contact.Facebook ?? (object)DBNull.Value);
                        var SkypeParam = new SqlParameter("@Skype", contact.Skype ?? (object)DBNull.Value);
                        var LinkedInParam = new SqlParameter("@LinkedIn", contact.LinkedIn ?? (object)DBNull.Value);
                        var CreatedDateParam = new SqlParameter("@CreatedDate", DateTime.Now);
                        SqlParameter addressIdParam = new SqlParameter("@ContactId", SqlDbType.UniqueIdentifier);
                        addressIdParam.Direction = ParameterDirection.Output;


                        //Execute the Sql Raw. 
                        _reviewSystemContext.Database.ExecuteSqlRaw("EXEC CreateContactInfo @UserId, @PermanentAddress, @PermanentCountry, @PermanentState, @TemporaryAddress, @TemporaryCountry, @TemporaryState, @Email, @AlternativeEmail, @ContactNumber, @EmergencyContactNumber, @ContactName, @ContactRelation, @Facebook, @Skype, @LinkedIn, @CreatedDate,@ContactId OUT",
                             UserIdParam,
                             PermanentAddressParam,
                             PermanentCountryParam,
                             PermanentStateParam,
                             TemporaryAddressParam,
                             TemporaryCountryParam,
                             TemporaryStateParam,
                             EmailParam,
                             AlternativeEmailParam,
                             ContactNumberParam,
                             EmergencyContactNumberParam,
                             ContactNameParam,
                             ContactRelationParam,
                             FacebookParam,
                             SkypeParam,
                             LinkedInParam,

                             CreatedDateParam,
                             addressIdParam
                             );

                        Guid contactId = (Guid)addressIdParam.Value;
                        return contactId.ToString();

                    }


                }



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string createDocumentInfo(DocumentInfo document)
        {
            try
            {

                if (document.DocumnetId != null)
                {

                
                    
                    if (document.Doc != null)
                    {

                        string fileName = UploadedFile(document.Doc);
                        DeleteFile(document.DocName);
                        //SQL parameter for create Document Info.
                        var UserIdParam = new SqlParameter("@UserId", document.UserId);
                        var CitizenshipNumberParam = new SqlParameter("@CitizenshipNumber", document.CitizenshipNumber);
                        var CitizenshipIssuePlaceParam = new SqlParameter("@CitizenshipIssuePlace", document.CitizenshipIssuePlace);
                        var PanNumberParam = new SqlParameter("@PanNumber", document.PanNumber ?? (object)DBNull.Value);
                        var CITNumberParam = new SqlParameter("@CitNumber", document.Citnumber ?? (object)DBNull.Value);
                        var PFNumberParam = new SqlParameter("@PfNumber", document.Pfnumber ?? (object)DBNull.Value);
                        var SFFNumberParam = new SqlParameter("@SffNumber", document.Sffnumber ?? (object)DBNull.Value);
                        var AcademicQualificationParam = new SqlParameter("@AcademicQualification", document.AcademicQualification);
                        var SelectDocTypeParam = new SqlParameter("@DocumentType", document.SelectDocType);
                        var DocNameParam = new SqlParameter("@DocumentName", fileName);
                        var createdDateParam = new SqlParameter("@createdDate", DateTime.Now);

                        //Execute the Sql Raw. 
                        _reviewSystemContext.Database.ExecuteSqlRaw("EXEC UpdateDocument @UserId, @CitizenshipNumber, @CitizenshipIssuePlace, @PanNumber, @CitNumber, @PfNumber, @SffNumber, @AcademicQualification,@DocumentType,@DocumentName , @createdDate",
                            UserIdParam,
                            CitizenshipNumberParam,
                            CitizenshipIssuePlaceParam,
                            PanNumberParam,
                            CITNumberParam,
                            PFNumberParam,
                            SFFNumberParam,
                            AcademicQualificationParam,
                            SelectDocTypeParam,
                            DocNameParam,
                            createdDateParam
                            

                            );

                        return document.DocumnetId.ToString();
                    }
                    else
                    {
                     
                        //SQL parameter for create Document Info.
                        var UserIdParam = new SqlParameter("@UserId", document.UserId);
                        var CitizenshipNumberParam = new SqlParameter("@CitizenshipNumber", document.CitizenshipNumber);
                        var CitizenshipIssuePlaceParam = new SqlParameter("@CitizenshipIssuePlace", document.CitizenshipIssuePlace);
                        var PanNumberParam = new SqlParameter("@PanNumber", document.PanNumber ?? (object)DBNull.Value);
                        var CITNumberParam = new SqlParameter("@CitNumber", document.Citnumber ?? (object)DBNull.Value);
                        var PFNumberParam = new SqlParameter("@PfNumber", document.Pfnumber ?? (object)DBNull.Value);
                        var SFFNumberParam = new SqlParameter("@SffNumber", document.Sffnumber ?? (object)DBNull.Value);
                        var AcademicQualificationParam = new SqlParameter("@AcademicQualification", document.AcademicQualification);
                        var SelectDocTypeParam = new SqlParameter("@DocumentType", document.SelectDocType);
                        var DocNameParam = new SqlParameter("@DocumentName", document.DocName);
                        var createdDateParam = new SqlParameter("@createdDate", DateTime.Now);

                        //Execute the Sql Raw. 
                        _reviewSystemContext.Database.ExecuteSqlRaw("EXEC UpdateDocument @UserId, @CitizenshipNumber, @CitizenshipIssuePlace, @PanNumber, @CitNumber, @PfNumber, @SffNumber, @AcademicQualification,@DocumentType,@DocumentName , @createdDate",
                            UserIdParam,
                            CitizenshipNumberParam,
                            CitizenshipIssuePlaceParam,
                            PanNumberParam,
                            CITNumberParam,
                            PFNumberParam,
                            SFFNumberParam,
                            AcademicQualificationParam,
                            SelectDocTypeParam,
                            DocNameParam,
                            createdDateParam
                            

                            );

                        return document.DocumnetId.ToString();
                    }
                }
                else
                {

                    //Get the Unique Name of the documnet 
                    string file = UploadedFile(document.Doc);

                    //SQL parameter for create Document Info.
                    var UserIdParam = new SqlParameter("@UserId", document.UserId);
                    var CitizenshipNumberParam = new SqlParameter("@CitizenshipNumber", document.CitizenshipNumber);
                    var CitizenshipIssuePlaceParam = new SqlParameter("@CitizenshipIssuePlace", document.CitizenshipIssuePlace);
                    var PanNumberParam = new SqlParameter("@PanNumber", document.PanNumber ?? (object)DBNull.Value);
                    var CITNumberParam = new SqlParameter("@CitNumber", document.Citnumber ?? (object)DBNull.Value);
                    var PFNumberParam = new SqlParameter("@PfNumber", document.Pfnumber ?? (object)DBNull.Value);
                    var SFFNumberParam = new SqlParameter("@SffNumber", document.Sffnumber ?? (object)DBNull.Value);
                    var AcademicQualificationParam = new SqlParameter("@AcademicQualification", document.AcademicQualification);
                    var SelectDocTypeParam = new SqlParameter("@DocumentType", document.SelectDocType);
                    var DocNameParam = new SqlParameter("@DocumentName", file);
                    var createdDateParam = new SqlParameter("@createdDate", DateTime.Now);
                    SqlParameter documentIdParam = new SqlParameter("@DocumnetId", SqlDbType.UniqueIdentifier);
                    documentIdParam.Direction = ParameterDirection.Output;

                    //Execute the Sql Raw. 
                    _reviewSystemContext.Database.ExecuteSqlRaw("EXEC CreateDocumentInfo @UserId, @CitizenshipNumber, @CitizenshipIssuePlace, @PanNumber, @CitNumber, @PfNumber, @SffNumber, @AcademicQualification,@DocumentType,@DocumentName , @createdDate,@DocumnetId OUT",
                        UserIdParam,
                        CitizenshipNumberParam,
                        CitizenshipIssuePlaceParam,
                        PanNumberParam,
                        CITNumberParam,
                        PFNumberParam,
                        SFFNumberParam,
                        AcademicQualificationParam,
                        SelectDocTypeParam,
                        DocNameParam,
                        createdDateParam,
                        documentIdParam
                        );


                    Guid DocumentId = (Guid)documentIdParam.Value;
                    return DocumentId.ToString();
                }


            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void CreateUserCredential(UserCredential user)
        {
            try
            {

                //Auto Generate salt 
                byte[] salt = GenerateSalt();
                string hashPassword= HashPassword(user.passwordHash, salt);
                //SQL parameter for create User Credential.
                var UserIdParam = new SqlParameter("@UserId", user.UserId);
                var UserNameParam = new SqlParameter("@UserName", user.UserName);
                var PasswordHashParam = new SqlParameter("@Password", hashPassword);
                var CreatedByParam = new SqlParameter("@CreatedBy", user.UserName);
                var StatusParam = new SqlParameter("@Status", 1);

                 _reviewSystemContext.Database.ExecuteSqlRaw("Exec UserCredential @UserId, @UserName, @Password,@Status,@CreatedBy", 
                     UserIdParam,
                     UserNameParam, 
                     PasswordHashParam,
                     StatusParam, 
                     CreatedByParam
                     
                     );

                //update the userName to the created by in each table of contactInfo and documentInfo
                //ContactInfo
                _reviewSystemContext.Database.ExecuteSqlRaw("EXEC UpdateCreatedByAddressInfo @UserId, @CreatedBy", UserIdParam, CreatedByParam);

                //DocumentInfo
                _reviewSystemContext.Database.ExecuteSqlRaw("EXEC UpdateCreatedByDocumentInfo @UserId, @CreatedBy", UserIdParam, CreatedByParam);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }

        public string createUserInfo(UserInfo user)
        {

            try
            {

                

                if (user.UserId != null)
                {

                    //SQL parameter for create Document Info.
                    var UserIdParam = new SqlParameter("@UserId", user.UserId);
                    var firstNameParam = new SqlParameter("@firstName", user.FirstName);
                    var middleNameParam = new SqlParameter("@middleName", user.MiddleName);
                    var lastNameParam = new SqlParameter("@lastName", user.LastName);
                    var dateOfBirthParam = new SqlParameter("@dateOfBirth", user.DateOfBirth);
                    var genderParam = new SqlParameter("@gender", user.Gender);
                    var maritalStatusParam = new SqlParameter("@maritalStatus", user.MaritalStatus);
                    var religionParam = new SqlParameter("@religion", user.Religion);
                    var bloodGroupParam = new SqlParameter("@bloodGroup", user.BloodGroup);
                    var createdDateParam = new SqlParameter("@createdDate", DateTime.Now);
                    

                    //Execute the Sql Raw.
                    var t = _reviewSystemContext.Database.ExecuteSqlRaw("EXEC UpdateUserInfo @UserId, @firstName, @middleName, @lastName, @dateOfBirth, @gender, @maritalStatus, @religion, @bloodGroup,@createdDate",
                        UserIdParam,
                        firstNameParam,
                        middleNameParam,
                        lastNameParam,
                        dateOfBirthParam,
                        genderParam,
                        maritalStatusParam,
                        religionParam,
                        bloodGroupParam,
                        createdDateParam
                        
                        );
                    return user.UserId.ToString();
                }
                else
                {
                    //SQL parameter for create Document Info.
                    var firstNameParam = new SqlParameter("@firstName", user.FirstName);
                    var middleNameParam = new SqlParameter("@middleName", user.MiddleName);
                    var lastNameParam = new SqlParameter("@lastName", user.LastName);
                    var dateOfBirthParam = new SqlParameter("@dateOfBirth", user.DateOfBirth);
                    var genderParam = new SqlParameter("@gender", user.Gender);
                    var maritalStatusParam = new SqlParameter("@maritalStatus", user.MaritalStatus);
                    var religionParam = new SqlParameter("@religion", user.Religion);
                    var bloodGroupParam = new SqlParameter("@bloodGroup", user.BloodGroup);
                    var createdDateParam = new SqlParameter("@createdDate", DateTime.Now);
                    SqlParameter userIdParam = new SqlParameter("@UserId", SqlDbType.UniqueIdentifier);
                    userIdParam.Direction = ParameterDirection.Output;

                    //Execute the Sql Raw.
                    var t = _reviewSystemContext.Database.ExecuteSqlRaw("EXEC CreateUserInfo @firstName, @middleName, @lastName, @dateOfBirth, @gender, @maritalStatus, @religion, @bloodGroup,@createdDate, @UserId OUT",
                        firstNameParam,
                        middleNameParam,
                        lastNameParam,
                        dateOfBirthParam,
                        genderParam,
                        maritalStatusParam,
                        religionParam,
                        bloodGroupParam,
                        createdDateParam,
                        userIdParam);

                    Guid userId = (Guid)userIdParam.Value;
                    return userId.ToString();
                }

               

                
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }


        }   

        public CantactInfo getUserByEmail(string email)
        {
            var parameter = new SqlParameter("@Email", email);
            var user = _reviewSystemContext.CantactInfos.FromSqlRaw("EXEC GetUserByEmail @Email", parameter).ToList();
            if (user != null && user.Count > 0)
            {
                return user.FirstOrDefault();
            }
            return null;
        }
        
      
     
       



        //Stored the file in static file in wwwroot folder and
        //return the file name and stored file name in database. 

        private string UploadedFile(IFormFile file)
        {
            string uniqueFileName = null;
            if (file != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "File");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        //Delete the file from the file system for edit.
        private void DeleteFile(string fileName)
        {
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "File", fileName);
            if (System.IO.File.Exists(imagePath))
            {
                try
                {
                    System.IO.File.Delete(imagePath);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                    //_notyf.Error("Error while Delete the image form the system");
                }
            }

        }


        //Hash the passwor and return the hash. 
        private string HashPassword(string password, byte[] salt)
        {
            int iterations = 10000; // number of iterations
            int hashSize = 32; // desired hash size in bytes

            // generate the hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(hashSize);

            // combine the salt and hash into a single string
            byte[] hashBytes = new byte[hashSize + salt.Length];
            Array.Copy(salt, 0, hashBytes, 0, salt.Length);
            Array.Copy(hash, 0, hashBytes, salt.Length, hash.Length);
            string hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }


        //Auto Generate the salt 
        private  byte[] GenerateSalt(int saltSize = 16)
        {
            byte[] salt = new byte[saltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        //verify Password 
        private  bool VerifyPassword(string password, string hashedPassword)
        {
            // get the salt and hash from the hashed password
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            int iterations = 10000;
            int hashSize = 32;

            // compute the hash of the user-supplied password
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(hashSize);

            // compare the computed hash with the stored hash
            for (int i = 0; i < hashSize; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }

        public bool Login(string UserName, string Password)
        {
           
            var GetUser = GetUserInfo(UserName);
            if (GetUser != null)
            {
               bool result= VerifyPassword(Password, GetUser.PasswordHash);
                return result;
            }
            return false;
        }

        public UserInfo GetUserInfo(string userName)
        {
            var userNameParam = new SqlParameter("@UserName", userName);
            var user = _reviewSystemContext.UserInfos.FromSqlRaw("EXEC GetByUserName @UserName", userNameParam).ToList();
            if (user != null && user.Count > 0)
            {
                return user.FirstOrDefault();
            }
            return null;
        }

        public UserInfo GetUserById(string id)
        {
            var UserIdParam = new SqlParameter("@UserId", id);
            var result = _reviewSystemContext.UserInfos.FromSqlRaw("EXEC getUserById @UserId", UserIdParam).ToList();
            if(result!=null && result.Count> 0)
            {
                return result.FirstOrDefault();
            }
            return null;
        }

        public CantactInfo getContactById(string id)
        {
            var UserIdParam = new SqlParameter("@ContactId", id);
            var result = _reviewSystemContext.CantactInfos.FromSqlRaw("EXEC GetContactById @ContactId", UserIdParam).ToList();
            if (result != null && result.Count > 0)
            {
                return result.FirstOrDefault();
            }
            return null;
        }

        public DocumentInfo getDocumentById(string id)
        {
            var UserIdParam = new SqlParameter("@DocumentId", id);
            var result = _reviewSystemContext.DocumentInfos.FromSqlRaw("EXEC GetDocumentById @DocumentId", UserIdParam).ToList();
            if (result != null && result.Count > 0)
            {
                return result.FirstOrDefault();
            }
            return null;
        }

        public List<UserInfo> GetAllUser()
        {
            try
            {
                var result = _reviewSystemContext.UserInfos.FromSqlRaw("EXEC getAllUser").ToList();
                return result;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
        }


    }
}
