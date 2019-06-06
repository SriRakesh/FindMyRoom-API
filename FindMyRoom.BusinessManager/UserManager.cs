using FindMyRoom.DataService;
using FindMyRoom.Entities;
using FindMyRoom.Entities.Models;
using FindMyRoom.Exceptions;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FindMyRoom.BusinessManager
{
    public class UserManager
    {
        UserService signup = new UserService();
        public UserManager()
        {
        }
        public UserManager(UserService userService)
        {
            this.signup = userService;
        }
        Acknowledgement<User> acknowledgement = new Acknowledgement<User>();

        //This method will call the get method in UserService 
        public virtual IEnumerable<User> GetUsers()
        {
            return signup.Getdata();
        }

        public static string validations(User user)
        {
            Regex Name = new Regex(@"^[a-zA-Z\s]*$");
            Regex Email = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");
            Regex MobileNumber = new Regex(@"[6/7/8/9]{1}[0-9]{9}$");
            Regex Password=new Regex(@"(?=^.{6,20}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$");
            if(!Name.IsMatch(user.UserName))
            {
                return "Username is invalid it should consists only characters.";
            }
            else if(!Email.IsMatch(user.UserEmail))
            {
                return "email is invalid";
            }
            else if(!MobileNumber.IsMatch(user.UserPhoneNumber))
            {
                return "mobile number is invalid it should start with 9/8/7/6 and of length 10.";
            }
            else if(!Password.IsMatch(user.UserPassword))
            {
                return "password is invalid, It should consists atleast one Uppercase,special character and number of length more than 7";
            }
            else if (!(user.UserType.ToLower().Equals("partner"))&&(!(user.UserType.ToLower().Equals("renter"))))
            {
                return "user type should be either partner or renter";
            }
            else
            {
                return "success";
            }
            
        }
        //This method will do validations for existance of email and type of partner and if not exists it inserts data.
        public virtual Acknowledgement<User> PostUser(HelperAddUser adduser)
        {
           
                User user = new User();
            int count=0;
            try
            {
                user.UserName = adduser.UserName;
                user.UserEmail = adduser.Email;
                user.UserPhoneNumber = adduser.MobileNumber;
                user.UserAddress = adduser.Address;
                user.UserPassword = adduser.Password;
                user.UserType = adduser.Type;
                user.UserStatus = "valid";
                List<User> users = new List<User>();
                users.Add(user);
                string validator = validations(user);
                if (!validator.Equals("success"))
                {
                    acknowledgement.code = 2;
                    acknowledgement.Set = null;
                    acknowledgement.Message = validator;
                    return acknowledgement;
                }
                else
                {
                    string name = user.UserName.Trim();
                    string address = user.UserAddress.Trim();
                    if((name.Equals(""))&&(address.Equals("")))
                    {
                        acknowledgement.code = 2;
                        acknowledgement.Set = null;
                        acknowledgement.Message = "Username field and address field should not empty";
                        return acknowledgement;

                    }
                    if (name.Equals(""))
                    {
                        acknowledgement.code = 2;
                        acknowledgement.Set = null;
                        acknowledgement.Message = "Fullname field should not empty";
                        return acknowledgement;
                    }
                    if (address.Equals(""))
                    {
                        acknowledgement.code = 2;
                        acknowledgement.Set = null;
                        acknowledgement.Message = "Address field should not be empty";
                        return acknowledgement;
                    }
                    foreach (var item in signup.Getdata())
                    {
                        if ((item.UserEmail.Equals(user.UserEmail,StringComparison.OrdinalIgnoreCase)))
                        {
                            count++;
                        }
                    }
                    if (count == 0)
                    {
                        PasswordHashing hash = new PasswordHashing();
                        string password = "";
                        acknowledgement.code = 0;
                        acknowledgement.Set = null;
                        acknowledgement.Message = "Successfully Registered.";
                        password = hash.CreatePasswordSalt(user.UserPassword);
                        user.UserPassword = password;
                        signup.PostUser(user);
                        return acknowledgement;
                    }

                    else
                    {
                        acknowledgement.code = 2;
                        acknowledgement.Set = null;
                        acknowledgement.Message = "This account already exists.";
                        return acknowledgement;
                    }
                    //return acknowledgement;
                }
            }
          
            catch(Exception e)
            {
                throw e;
            }

        }

        public virtual Acknowledgement<User> fbUser(Fbuser fbuser)
        {
            Acknowledgement<User> payload = new Acknowledgement<User>();
            try
            {
                //UserService userService = new UserService();
                payload  = signup.postData(fbuser);
                
            }
            catch(Exception e)
            {
                throw e;
            }
            return payload;
        }

        public virtual Acknowledgement<User> Validate(string UserEmail, string UserPassword)
        {
            Regex Email = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");
            Regex Password = new Regex(@"(?=^.{6,20}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$");
            try
            {
                if(!Email.IsMatch(UserEmail) || !Password.IsMatch(UserPassword))
                {
                    acknowledgement.code = 3;
                    acknowledgement.Set = null;
                    acknowledgement.Message = "Invalid Credentials";
                    return acknowledgement;
                }
                //UserService userService = new UserService();
                List<User> list = signup.GetDetails();
                List<User> users = new List<User>();
                PasswordHashing hash = new PasswordHashing();
                foreach (User user in list)
                {
                    if (user.UserEmail.Equals(UserEmail,
                        StringComparison.OrdinalIgnoreCase) && (hash.IsPasswordValid(UserPassword,user.UserPassword) && !user.UserType.Equals("Admin")))
                    {
                        users.Add(user);
                    }
                    else if(user.UserEmail.Equals(UserEmail,
                        StringComparison.OrdinalIgnoreCase) && UserPassword.Equals(user.UserPassword) && user.UserType.Equals("Admin"))
                    {
                        users.Add(user);
                    }
                }
                if(users.Count==1 && users[0].UserStatus.ToLower().Equals("invalid"))
                {
                    acknowledgement.code = 0;
                    acknowledgement.Set = users;
                    acknowledgement.Message = "Sorry! Account is Blocked";
                }
                else if(users.Count == 1)
                {
                    acknowledgement.code = 1;
                    acknowledgement.Set = users;
                    acknowledgement.Message = "Successfully LoggedIn";
                }
                else if(users.Count != 1)
                {
                    acknowledgement.code = 0;
                    acknowledgement.Set = users;
                    acknowledgement.Message = "Invalid Credentials";
                }
            return acknowledgement;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual string ForgetPasswordManager(string email, string password)
        {
            Regex Email = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");
            Regex Password = new Regex(@"(?=^.{6,20}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$");
            try
            {
                if (!Email.IsMatch(email))
                {
                    return "Invalid Email";
                }
                if (!Password.IsMatch(password))
                {
                    return "Invalid Password";
                }

                string tempPassword;
                PasswordHashing hash = new PasswordHashing();
                tempPassword = hash.CreatePasswordSalt(password);
                string output =signup.ForgetPasswordService(email, tempPassword);
                if (output == null)
                {
                    return "Email does not exist";
                }
                return "Updated Successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual string EmailPasswordManager(string email)
        {
            Regex Email = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");

            try
            {
                if(!Email.IsMatch(email))
                {
                    return "Invalid Input";
                }
                string output = signup.EmailPassword(email);
                if (output==null)
                {
                    return null;
                }
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Password Reset", "findmyroom.project123@gmail.com"));
                message.To.Add(new MailboxAddress("Customer", email));
                message.Subject = "Password Reset";
                message.Body = new TextPart("plain")
                {
                    //Text = "Your password is : " + password
                    Text= "https://findmyroomclinetapp.azurewebsites.net/#/features/updatepassword"
                };
                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("findmyroom.project123@gmail.com", "Findmyroom@123");
                    client.Send(message);
                    client.Disconnect(true);
                }
                return "Mail sent successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual Acknowledgement<UserUpdateHelp> GetUser(int UserId)
        {
            Acknowledgement<UserUpdateHelp> acknowledgement = new Acknowledgement<UserUpdateHelp>();
            try
            {
                User users = signup.GetUserDetails(UserId);
                if (users==null)
                {
                    acknowledgement.code = 2;
                    acknowledgement.Set = null;
                    acknowledgement.Message = "No User with given UserId";
                    return acknowledgement;
                }
                UserUpdateHelp usersdetails = new UserUpdateHelp();
                usersdetails.UserId = users.UserId;
                usersdetails.UserName = users.UserName;
                usersdetails.UserEmail = users.UserEmail;
                usersdetails.UserPhoneNumber = users.UserPhoneNumber;
                usersdetails.UserAddress = users.UserAddress;
                usersdetails.UserStatus = users.UserStatus;
                usersdetails.UserType = users.UserType;

                List<UserUpdateHelp> userUpdateHelps = new List<UserUpdateHelp>();
                userUpdateHelps.Add(usersdetails);
                acknowledgement.code = 1;
                acknowledgement.Set= userUpdateHelps;
                acknowledgement.Message = "User data loaded successfully";
                return acknowledgement;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ValidateUser(UserPutHelp user)
        {
            
            Regex Name = new Regex(@"^[a-zA-Z\s]*$");
            Regex MobileNumber = new Regex(@"[6/7/8/9]{1}[0-9]{9}$");
            
            int Count1 = 0;
            foreach (var item in signup.Getdata())
            {
                if ((item.UserId == user.UserId))
                {
                    Count1++;
                }
            }
            if (Count1 == 0)
            {
                return "UserID does not exist";
            }
            if (!Name.IsMatch(user.UserName))
            {
                return "Username is invalid it should consists only characters.";
            }
            else if (!MobileNumber.IsMatch(user.UserPhoneNumber))
            {
                return "mobile number is invalid it should start with 9/8/7/6 and of length 10.";
            }
            else
            {
                return "success";
            }

        }

        public virtual Acknowledgement<UserPutHelp> UpdateUser(UserPutHelp user)
        {
            Acknowledgement<UserPutHelp> acknowledgement = new Acknowledgement<UserPutHelp>();
            try
            {
                List<UserPutHelp> users = new List<UserPutHelp>();
                users.Add(user);
                string validator = ValidateUser(user);
                if (!validator.Equals("success"))
                {
                    acknowledgement.code = 2;
                    acknowledgement.Set = users;
                    acknowledgement.Message = validator;
                    return acknowledgement;
                }
                else
                {
                    string name = user.UserName.Trim();
                    string address = user.UserAddress.Trim();
                    if ((name.Equals("")) && (address.Equals("")))
                    {
                        acknowledgement.code = 2;
                        acknowledgement.Set = users;
                        acknowledgement.Message = "Username field and address field should not empty";
                        return acknowledgement;

                    }
                    if (name.Equals(""))
                    {
                        acknowledgement.code = 2;
                        acknowledgement.Set = users;
                        acknowledgement.Message = "Fullname field should not empty";
                        return acknowledgement;
                    }
                    if (address.Equals(""))
                    {
                        acknowledgement.code = 2;
                        acknowledgement.Set = users;
                        acknowledgement.Message = "Address field should not be empty";
                        return acknowledgement;
                    }

                        acknowledgement.code = 1;
                        acknowledgement.Set = users;
                        acknowledgement.Message = "Successfully updated.";
                        signup.UpdateUser(user);
                        return acknowledgement;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
        
}

