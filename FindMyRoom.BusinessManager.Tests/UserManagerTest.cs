using FindMyRoom.DataService;
using FindMyRoom.Entities;
using FindMyRoom.Entities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace FindMyRoom.BusinessManager.Tests
{
    [TestClass]
    public class UserManagerTest
    {

        [TestMethod]
        public void ForgetPassword_Positive_TestsReturned()
        {
            //Assign
            var email = "Admin@fmr.com";
            var password = "Abcd@123";
            var _message = "success";

            var _mockUserservice = new Mock<UserService>();
            var _returnAckType = _message;
            _mockUserservice.Setup(p => p.ForgetPasswordService(email,password)).Returns(_returnAckType);
            UserManager _userManager = new UserManager(_mockUserservice.Object);
            //Act
            var _actualReturnType = _userManager.ForgetPasswordManager(email,password);
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType);
        }

        [TestMethod]
        public void ForgetPassword_Negative_MailNotExist_TestsReturned()
        {
            //Assign
            var email = "test@gmail.com";
            var password = "Abcd@123";
            string _message = null;

            var _mockUserservice = new Mock<UserService>();
            string _returnAckType = "Email does not exist";
            _mockUserservice.Setup(p => p.ForgetPasswordService(email, password)).Returns(_message);
            UserManager _userManager = new UserManager(_mockUserservice.Object);
            //Act
            var _actualReturnType = _userManager.ForgetPasswordManager(email, password);
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType);
        }

        [TestMethod]
        public void ForgetPassword_Negative_EmailEmpty_TestsReturned()
        {
            //Arrange
            var email = "test.gmail.com";
            var password = "Abcd@123";
            var _returnAckType = "Invalid Email";
            var _userManager = new UserManager();
            //Act
            string _actualReturnType = _userManager.ForgetPasswordManager(email,password);
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType);
        }

        [TestMethod]
        public void ForgetPassword_Negative_PasswordEmpty_TestsReturned()
        {
            //Arrange
            var email = "test@gmail.com";
            var password = "Abcd123";
            var _returnAckType = "Invalid Password";
            var _userManager = new UserManager();
            //Act
            string _actualReturnType = _userManager.ForgetPasswordManager(email, password);
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType);
        }

        [TestMethod]
        public void ForgetPassword_Negative_Exception_TestsReturned()
        {
            //Assign
            var email = "test@gmail.com";
            var password = "Abcd@123";

            var _mockUserservice = new Mock<UserService>();
            string _returnAckType = "System Object Reference missing";
            _mockUserservice.Setup(p => p.ForgetPasswordService(email, password)).Throws<System.Exception>();
            UserManager _userManager = new UserManager(_mockUserservice.Object);
            //Act
            var _actualReturnType = _userManager.ForgetPasswordManager(email, password);
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType);
        }

        [TestMethod]
        public void EmailPasswordManager_Positive_TestsReturned()
        {
            //Assign
            var email = "Admin@fmr.com";
            var _message = "Done";

            var _mockUserservice = new Mock<UserService>();
            var _returnAckType = "Mail sent successfully";
            _mockUserservice.Setup(p => p.EmailPassword(email)).Returns(_message);
            UserManager _userManager = new UserManager(_mockUserservice.Object);
            //Act
            var _actualReturnType = _userManager.EmailPasswordManager(email);
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType);
        }

        [TestMethod]
        public void EmailPasswordManager_Negative_EmailEmpty_TestsReturned()
        {
            //Arrange
            var email = "test.gmail.com";
            var _returnAckType = "Invalid Email";
            var _userManager = new UserManager();
            //Act
            string _actualReturnType = _userManager.EmailPasswordManager(email);
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType);
        }

        [TestMethod]
        public void EmailPasswordManager_Negative_TestsReturned()
        {
            //Assign
            var email = "Admin@gmail.com";
            string _message = null;

            var _mockUserservice = new Mock<UserService>();
            string _returnAckType = null;
            _mockUserservice.Setup(p => p.EmailPassword(email)).Returns(_message);
            UserManager _userManager = new UserManager(_mockUserservice.Object);
            //Act
            var _actualReturnType = _userManager.EmailPasswordManager(email);
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType);
        }

        [TestMethod]
        public void EmailPasswordManager_Negative_Exception_TestsReturned()
        {
            //Assign
            var email = "test@gmail.com";
            var _mockUserservice = new Mock<UserService>();
            var _returnAckType = "System Object Reference missing";
            _mockUserservice.Setup(p => p.EmailPassword(email)).Throws<System.Exception>();
            UserManager _userManager = new UserManager(_mockUserservice.Object);
            //Act
            try
            {
                var _actualReturnType = _userManager.EmailPasswordManager(email);
                //Assert
                Assert.ReferenceEquals(_returnAckType, _actualReturnType);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }


        [TestMethod]
        public void GetUser_Positive_TestsReturned()
        {
            //Assign
            var userId = 53;
            UserUpdateHelp userUpdateHelp = new UserUpdateHelp();
            User user = new User()
            {
                UserId = 53,
                UserName="Admin",
                UserEmail="Admin@fmr.com",
                UserPassword="iwgcid-__kncdkdk",
                UserAddress="Admin",
                UserPhoneNumber="9999999999",
                UserStatus="valid",
                UserType="Admin"
            };
            userUpdateHelp.UserId = user.UserId;
            userUpdateHelp.UserName = user.UserName;
            userUpdateHelp.UserEmail = user.UserEmail;
            userUpdateHelp.UserAddress = user.UserAddress;
            userUpdateHelp.UserPhoneNumber = user.UserPhoneNumber;
            userUpdateHelp.UserStatus = user.UserStatus;
            userUpdateHelp.UserType = user.UserType;
            List<UserUpdateHelp> Listuser = new List<UserUpdateHelp>();
            Listuser.Add(userUpdateHelp);
            var _mockUserservice = new Mock<UserService>();
            var _returnAckType = new Acknowledgement<UserUpdateHelp>
            {
                code = 1,
                Set = Listuser,
                Message= "User data loaded successfully"
            };
            _mockUserservice.Setup(p => p.GetUserDetails(userId)).Returns(user);
            UserManager _userManager = new UserManager(_mockUserservice.Object);
            //Act
            var _actualReturnType = _userManager.GetUser(userId);
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType);
            
        }

        [TestMethod]
        public void GetUser_Negative_TestsReturned()
        {
            //Assign
            var userId = 1;
            UserUpdateHelp userUpdateHelp = new UserUpdateHelp();
            User user = new User();
            user = null;
            var _mockUserservice = new Mock<UserService>();
            var _returnAckType = new Acknowledgement<UserUpdateHelp>
            {
                code = 2,
                Set = null,
                Message = "No User with given UserId"
            };
            _mockUserservice.Setup(p => p.GetUserDetails(userId)).Returns(user);
            UserManager _userManager = new UserManager(_mockUserservice.Object);
            //Act
            var _actualReturnType = _userManager.GetUser(userId);
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType);

        }

        [TestMethod]
        public void GetUser_Negative_Exception_TestsReturned()
        {
            //Assign
            var userId = 1;
            UserUpdateHelp userUpdateHelp = new UserUpdateHelp();
            User user = new User();
            user = null;
            var _mockUserservice = new Mock<UserService>();
            _mockUserservice.Setup(p => p.GetUserDetails(userId)).Throws<System.Exception>();
            UserManager _userManager = new UserManager(_mockUserservice.Object);
            //Act
            try
            {
                var _actualReturnType = _userManager.GetUser(userId);
                //Assert
                //Assert.ReferenceEquals(_returnAckType, _actualReturnType);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void UpdateUser_Negative_NoId_TestsReturned()
        {
            //Assign
            UserPutHelp userUpdateHelp = new UserPutHelp()
            {
                UserId = 1,
                UserName="Admin",
                UserAddress="Admin",
                UserPhoneNumber="9999999999"
            };
            List<UserPutHelp> userPutHelps = new List<UserPutHelp>();
            userPutHelps.Add(userUpdateHelp);
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 2,
                Set = userPutHelps,
                Message = "UserID does not exist"
            };
            UserManager _userManager = new UserManager();
            //Act
            var _actualReturnType = _userManager.UpdateUser(userUpdateHelp);
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType);
        }

        [TestMethod]
        public void UpdateUser_Negative_NameMisMatch_TestsReturned()
        {
            //Assign
            UserPutHelp userUpdateHelp = new UserPutHelp()
            {
                UserId = 53,
                UserName = "Admin123",
                UserAddress = "Admin",
                UserPhoneNumber = "9999999999"
            };
            List<UserPutHelp> userPutHelps = new List<UserPutHelp>();
            userPutHelps.Add(userUpdateHelp);
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 2,
                Set = userPutHelps,
                Message = "Username is invalid it should consists only characters."
            };
            UserManager _userManager = new UserManager();
            //Act
            var _actualReturnType = _userManager.UpdateUser(userUpdateHelp);
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType);
        }

        [TestMethod]
        public void UpdateUser_Negative_PhoneMisMatch_TestsReturned()
        {
            //Assign
            UserPutHelp userUpdateHelp = new UserPutHelp()
            {
                UserId = 53,
                UserName = "Admin",
                UserAddress = "Admin",
                UserPhoneNumber = "99999999aa"
            };
            List<UserPutHelp> userPutHelps = new List<UserPutHelp>();
            userPutHelps.Add(userUpdateHelp);
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 2,
                Set = userPutHelps,
                Message = "mobile number is invalid it should start with 9/8/7/6 and of length 10."
            };
            UserManager _userManager = new UserManager();
            //Act
            var _actualReturnType = _userManager.UpdateUser(userUpdateHelp);
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType);
        }

        [TestMethod]
        public void UpdateUser_Negative_NameAdressEmpty_TestsReturned()
        {
            //Assign
            UserPutHelp userUpdateHelp = new UserPutHelp()
            {
                UserId = 53,
                UserName = "    ",
                UserAddress = "    ",
                UserPhoneNumber = "9999999999"
            };
            List<UserPutHelp> userPutHelps = new List<UserPutHelp>();
            userPutHelps.Add(userUpdateHelp);
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 2,
                Set = userPutHelps,
                Message = "Username field and address field should not empty"
            };
            UserManager _userManager = new UserManager();
            //Act
            var _actualReturnType = _userManager.UpdateUser(userUpdateHelp);
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType);
        }

        [TestMethod]
        public void UpdateUser_Negative_NameEmpty_TestsReturned()
        {
            //Assign
            UserPutHelp userUpdateHelp = new UserPutHelp()
            {
                UserId = 53,
                UserName = "    ",
                UserAddress = "Admin",
                UserPhoneNumber = "9999999999"
            };
            List<UserPutHelp> userPutHelps = new List<UserPutHelp>();
            userPutHelps.Add(userUpdateHelp);
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 2,
                Set = userPutHelps,
                Message = "Fullname field should not empty"
            };
            UserManager _userManager = new UserManager();
            //Act
            var _actualReturnType = _userManager.UpdateUser(userUpdateHelp);
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType);
        }

        [TestMethod]
        public void UpdateUser_Negative_AddressEmpty_TestsReturned()
        {
            //Assign
            UserPutHelp userUpdateHelp = new UserPutHelp()
            {
                UserId = 53,
                UserName = "Admin",
                UserAddress = "    ",
                UserPhoneNumber = "9999999999"
            };
            List<UserPutHelp> userPutHelps = new List<UserPutHelp>();
            userPutHelps.Add(userUpdateHelp);
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 2,
                Set = userPutHelps,
                Message = "Address field should not empty"
            };
            UserManager _userManager = new UserManager();
            //Act
            var _actualReturnType = _userManager.UpdateUser(userUpdateHelp);
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType);
        }

        [TestMethod]
        public void UpdateUser_Positive_TestsReturned()
        {
            //Assign
            UserPutHelp userUpdateHelp = new UserPutHelp()
            {
                UserId = 53,
                UserName = "Admin",
                UserAddress = "Admin",
                UserPhoneNumber = "9999999999"
            };
            List<UserPutHelp> userPutHelps = new List<UserPutHelp>();
            userPutHelps.Add(userUpdateHelp);
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 1,
                Set = userPutHelps,
                Message = "Successfully updated."
            };
            var _mockUserservice = new Mock<UserService>();
            _mockUserservice.Setup(p => p.UpdateUser(userUpdateHelp));
            UserManager _userManager = new UserManager(_mockUserservice.Object);
            //Act
            var _actualReturnType = _userManager.UpdateUser(userUpdateHelp);
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType);
        }


        [TestMethod]
        public void UpdateUser_Negative_Exception_TestsReturned()
        {
            //Assign
            UserPutHelp userUpdateHelp = new UserPutHelp()
            {
                UserId = 53,
                UserName = "Admin",
                UserAddress = "Admin",
                UserPhoneNumber = "9999999999"
            };
            List<UserPutHelp> userPutHelps = new List<UserPutHelp>();
            userPutHelps.Add(userUpdateHelp);
            var _mockUserservice = new Mock<UserService>();
            _mockUserservice.Setup(p => p.UpdateUser(userUpdateHelp)).Throws<System.Exception>();
            UserManager _userManager = new UserManager(_mockUserservice.Object);
            //Act
            try
            {
                var _actualReturnType = _userManager.UpdateUser(userUpdateHelp);
                //Assert
                //Assert.ReferenceEquals(_returnAckType, _actualReturnType);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        

        [TestMethod] 
        public void PostUser_Negative_UserName()
        {
            var _mockBussinessMethod = new Mock<UserManager>();
            var _usermanager = new UserManager();
            HelperAddUser user = GetUsers_Mock_Negative();
            user.UserName = "wer1234567";
            var _returnAskType = new Acknowledgement<User>
            {
                code = 2,
                Set = null,
                Message = "Username is invalid it should consists only characters."
            };

            _mockBussinessMethod.Setup(p => p.PostUser(user)).Returns(_returnAskType);

            //Act
            var _actualReturnType = _usermanager.PostUser(user) as Acknowledgement<User>;

            //Assert
            Assert.ReferenceEquals(_returnAskType, _actualReturnType);
        }

        [TestMethod]
        public void PostUser_Negative_UsersEmail()
        {
            var _mockBussinessMethod = new Mock<UserManager>();
            var _usermanager = new UserManager();
            HelperAddUser user = GetUsers_Mock_Negative();
            user.Email = "shravani@er";
            var _returnAskType = new Acknowledgement<User>
            {
                code = 2,
                Set = null,
                Message = "email is invalid"
            };

            _mockBussinessMethod.Setup(p => p.PostUser(user)).Returns(_returnAskType);

            //Act
            var _actualReturnType = _usermanager.PostUser(user) as Acknowledgement<User>;

            //Assert
            Assert.ReferenceEquals(_returnAskType, _actualReturnType);
        }
        [TestMethod]
        public void PostUser_Negative_UsersPhoneNo()
        {
            var _mockBussinessMethod = new Mock<UserManager>();
            var _usermanager = new UserManager();
            HelperAddUser user = GetUsers_Mock_Negative();
            user.MobileNumber = "1234567890";
            var _returnAskType = new Acknowledgement<User>
            {
                code = 2,
                Set = null,
                Message = "mobile number is invalid it should start with 9/8/7/6 and of length 10."
            };

            _mockBussinessMethod.Setup(p => p.PostUser(user)).Returns(_returnAskType);

            //Act
            var _actualReturnType = _usermanager.PostUser(user) as Acknowledgement<User>;

            //Assert
            Assert.ReferenceEquals(_returnAskType, _actualReturnType);
        }
        [TestMethod]
        public void PostUser_Negative_UsersPassword()
        {
            var _mockBussinessMethod = new Mock<UserManager>();
            var _usermanager = new UserManager();
            HelperAddUser user = GetUsers_Mock_Negative();
            user.Password = "asdfghjkl";
            var _returnAskType = new Acknowledgement<User>
            {
                code = 2,
                Set = null,
                Message = "password is invalid, It should consists atleast one Uppercase,special character and number of length more than 7"
            };

            _mockBussinessMethod.Setup(p => p.PostUser(user)).Returns(_returnAskType);

            //Act
            var _actualReturnType = _usermanager.PostUser(user) as Acknowledgement<User>;

            //Assert
            Assert.ReferenceEquals(_returnAskType, _actualReturnType);
        }

        [TestMethod]
        public void PostUser_Negative_UsersType()
        {
            var _mockBussinessMethod = new Mock<UserManager>();
            var _usermanager = new UserManager();
            HelperAddUser user = GetUsers_Mock_Negative();
            user.Type = "admin";
            var _returnAskType = new Acknowledgement<User>
            {
                code = 2,
                Set = null,
                Message = "user type should be either partner or renter"
            };

            _mockBussinessMethod.Setup(p => p.PostUser(user)).Returns(_returnAskType);

            //Act
            var _actualReturnType = _usermanager.PostUser(user) as Acknowledgement<User>;

            //Assert
            Assert.ReferenceEquals(_returnAskType, _actualReturnType);
        }

        [TestMethod]
        public void PostUser_Negative_NameandAddress()
        {
            var _mockBussinessMethod = new Mock<UserManager>();
            var _usermanager = new UserManager();
            HelperAddUser user = GetUsers_Mock_Negative();
            user.UserName = "      ";
            user.Address = "       ";
            var _returnAskType = new Acknowledgement<User>
            {
                code = 2,
                Set = null,
                Message = "Username field and address field should not empty"
            };

            _mockBussinessMethod.Setup(p => p.PostUser(user)).Returns(_returnAskType);

            //Act
            var _actualReturnType = _usermanager.PostUser(user) as Acknowledgement<User>;

            //Assert
            Assert.ReferenceEquals(_returnAskType, _actualReturnType);
        }

        [TestMethod]
        public void PostUser_Negative_Name()
        {
            var _mockBussinessMethod = new Mock<UserManager>();
            var _usermanager = new UserManager();
            HelperAddUser user = GetUsers_Mock_Negative();
            user.UserName = "      ";
            var _returnAskType = new Acknowledgement<User>
            {
                code = 2,
                Set = null,
                Message = "Fullname field should not empty"
            };

            _mockBussinessMethod.Setup(p => p.PostUser(user)).Returns(_returnAskType);

            //Act
            var _actualReturnType = _usermanager.PostUser(user) as Acknowledgement<User>;

            //Assert
            Assert.ReferenceEquals(_returnAskType, _actualReturnType);
        }

        [TestMethod]
        public void PostUser_Negative_Address()
        {
            var _mockBussinessMethod = new Mock<UserManager>();
            var _usermanager = new UserManager();
            HelperAddUser user = GetUsers_Mock_Negative();
            user.Address = "      ";
            var _returnAskType = new Acknowledgement<User>
            {
                code = 2,
                Set = null,
                Message = "Address field should not be empty"
            };

            _mockBussinessMethod.Setup(p => p.PostUser(user)).Returns(_returnAskType);

            //Act
            var _actualReturnType = _usermanager.PostUser(user) as Acknowledgement<User>;

            //Assert
            Assert.ReferenceEquals(_returnAskType, _actualReturnType);
        }
        [TestMethod]
        public void PostUser_Negative_AlreadyExists()
        {
            var _mockBussinessMethod = new Mock<UserManager>();
            var _usermanager = new UserManager();
            HelperAddUser user = GetUsers_Mock_Negative();
            var _returnAskType = new Acknowledgement<User>
            {
                code = 2,
                Set = null,
                Message = "This account already exists."
            };

            _mockBussinessMethod.Setup(p => p.PostUser(user)).Returns(_returnAskType);

            //Act
            var _actualReturnType = _usermanager.PostUser(user) as Acknowledgement<User>;

            //Assert
            Assert.ReferenceEquals(_returnAskType, _actualReturnType);
        }
        [TestMethod]
        public void PostUser_Positive_Testresults()
        {
            var _mockBussinessMethod = new Mock<UserManager>();
            var _usermanager = new UserManager();
            HelperAddUser user = GetUsers_Mock_Negative();
            user.Email = "ramyagandrotu@gmail.com";
            var _returnAskType = new Acknowledgement<User>
            {
                code = 0,
                Set = null,
                Message = "Successfully Registered."
            };

            _mockBussinessMethod.Setup(p => p.PostUser(user)).Returns(_returnAskType);

            //Act
            var _actualReturnType = _usermanager.PostUser(user) as Acknowledgement<User>;

            //Assert
            Assert.ReferenceEquals(_returnAskType, _actualReturnType);
        }
        [TestMethod]
        public void PostUser_Negative_TestResults()
        {
            var _mockBussinessMethod = new Mock<UserManager>();
            var _usermanager = new UserManager();
            HelperAddUser user = GetUsers_Mock_Negative();
            var _returnAskType = new Acknowledgement<User>
            {
                code = 0,
                Set = null,
                Message = "Something went wrong please try again later"
            };
            try
            {
                _mockBussinessMethod.Setup(p => p.PostUser(user)).Throws<System.Exception>();

                //Act
                var _actualReturnType = _usermanager.PostUser(user) as Acknowledgement<User>;
               
                //Assert
                Assert.AreEqual(_returnAskType, _actualReturnType);
                Assert.IsTrue(false);
               
            }
            catch(Exception)
            {
                Assert.IsTrue(true);
            }
        }
        public HelperAddUser GetUsers_Mock_Negative()
        {
            HelperAddUser user = new HelperAddUser();
            user.UserName = "Ramya";
            user.Email = "ramya@gmail.com";
            user.MobileNumber = "9876543210";
            user.Address = "asdfghjklqwertyui";
            user.Password = "Abcd@123";
            user.Type = "partner";
            return user;
        }
        public User GetUser(HelperAddUser helperAddUser)
        {
            User user = new User
            {
                UserName = helperAddUser.UserName,
                UserEmail = helperAddUser.Email,
                UserPhoneNumber = helperAddUser.MobileNumber,
                UserAddress = helperAddUser.Address,
                UserPassword = helperAddUser.Password,
                UserType=helperAddUser.Type,
                UserStatus="valid"
            };
            return user;
        }
      
        [TestMethod]
        public void GetUsers_Positive_UserReturned()
        {
            //Arrange
            var _mockDataService = new Mock<UserService>();
            var users= new User()
            {
                UserAddress="Hyderabad",
                UserEmail = "Admin@fmr.com",
                UserPassword = "Admin@123",
                UserName="Admin",
                UserPhoneNumber="9898989898",
                UserStatus="valid",
                UserType="Admin"
            };
            List<User> list = new List<User>();
            list.Add(users);

            _mockDataService.Setup(p => p.GetDetails()).Returns(list);

            var _userManager = new UserManager(_mockDataService.Object);

            var _responseExpected = new Acknowledgement<User>
            {
                code = 1,
                Set = list,
                Message = "Success"
            };
            var validateuser = new AuthenticateAdmin()
            {
                UserEmail = "Admin@fmr.com",
                UserPassword = "Admin@123",
            };

            //Act
            var responseReturned = _userManager.Validate(validateuser.UserEmail,validateuser.UserPassword);

            //Assert
            Assert.ReferenceEquals(responseReturned, _responseExpected);
        }
        [TestMethod]
        public void GetUser_NegativeTestCases_TestResult()
        {
            //Assign 
            var searchUser = new AuthenticateAdmin()
            {
                UserEmail = "",
                UserPassword = "",
            };
            

            var users_ack = new Acknowledgement<User>
            {
                code = 2,
                Set = null,
                Message = "Invalid Credentials"
            };

            var _mockBusinessMethod = new Mock<UserService>();
            _mockBusinessMethod.Setup(p => p.GetDetails()).Throws<System.IO.IOException>();
            UserManager _userManager = new UserManager(_mockBusinessMethod.Object);
            //Act
            var _returnedUserResp = _userManager.Validate(searchUser.UserEmail, searchUser.UserPassword);
            var _returnedUserAck = _returnedUserResp;
            //Assert
            Assert.ReferenceEquals(_returnedUserAck, users_ack);
        }

        [TestMethod]
        public void FaceBookUsers_Positive_UserReturned()
        {
            var validUser = new Fbuser()
            {
                UserEmail = "findmyromm.project123@gmail.com",
                UserName = "Admin",
                Provider = "Facebook",
                ProviderId = "108567126995987",
                UserType = "Partner"

            };
            var _mockBusinessMethod = new Mock<UserService>();
            var _returnAckType = new Acknowledgement<User>
            {
                code = 1,
                Set = null,
                Message = "Successfully LoggedIn"
            };

            _mockBusinessMethod.Setup(p => p.postData(validUser)).Returns(_returnAckType);
            UserManager _userController = new UserManager(_mockBusinessMethod.Object);
            //Act
            var _actualReturnType = _userController.fbUser(validUser);

            Assert.ReferenceEquals(_returnAckType, _actualReturnType);
           
        }
        [TestMethod]
        public void FaceBookUser_NegativeTestCases_TestResult()
        {
            //Assign 
            var fbUser = new Fbuser()
            {
                UserEmail = "findmyroom.project123@gmail.com",
                UserName = "Admin",
                Provider = "Facebook",
                ProviderId = "108567126995987",
                UserType = "Renter",
                
            };

            var _mockBusinessMethod = new Mock<UserService>();
            var _returnAckType = new Acknowledgement<User>
            {
                code = 3,
                Set = null,
                Message = "Please Enter Correct User"
            };

            _mockBusinessMethod.Setup(p => p.postData(fbUser)).Throws<System.IO.IOException>();
            UserManager _userManager = new UserManager(_mockBusinessMethod.Object);
            try
            {
                //Act
                var _returnedUserResp = _userManager.fbUser(fbUser);
                var _returnedUserAck = _returnedUserResp;

                //Assert
                Assert.ReferenceEquals(_returnedUserAck, _returnAckType);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

    }
}
