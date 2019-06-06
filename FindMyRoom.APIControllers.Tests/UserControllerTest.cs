using FindMyRoom.APIControllers.Controllers;
using FindMyRoom.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using FindMyRoom.BusinessManager;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Results;
using System;
using FindMyRoom.Entities.Models;

namespace FindMyRoom.APIControllers.Tests
{
    [TestClass]
    public class UserControllerTest
    {
        //[TestMethod]
        //public void GetUsers_PositiveTestCases_TestResults()
        //{
        //    //Assign
        //    var _mockBusinessMethod = new Mock<UserManager>();
        //    var city_List = GetUsers_Mock_Positive();
        //    _mockBusinessMethod.Setup(p => p.Validate()).Returns(city_List);
        //    UserController _bookingController = new UserController(_mockBusinessMethod.Object);
        //    //Act
        //    var _returnedCitiesResp = _bookingController.Validate();
        //    var _returnedCitiesAck = _returnedCitiesResp as OkObjectResult;
        //    var _originalCitiesAck = GetUsers_Mock_Positive();
        //    //Assert
        //    Assert.IsNotNull(_returnedCitiesAck);
        //    Assert.ReferenceEquals(_returnedCitiesAck.Value, _originalCitiesAck);
        //}
        //private Acknowledgement<string> GetUsers_Mock_Positive()
        //{
        //    var cities_ack = new Acknowledgement<string>
        //    {
        //        code = 1,
        //        Set = new List<string> { "Admin@fmr.com", "Admin@123" },
        //        Message = "Success"
        //    };
        //    return cities_ack;
        //}
        [TestMethod]
        public void Users_PositiveTestCases_ValidInput()
        {
            //Assign
            var validUser = new AuthenticateAdmin()
            {
                UserEmail = "Admin@fmr.com",
                UserPassword="Admin@123",
            };
            var _mockBusinessMethod = new Mock<UserManager>();
            var _returnAckType = new Acknowledgement<User>
            {
                code = 1,
                Set = null,
                Message = "Success"
            };

            _mockBusinessMethod.Setup(p => p.Validate(validUser.UserEmail,validUser.UserPassword)).Returns(_returnAckType);
            UserController _userController = new UserController(_mockBusinessMethod.Object);
            //Act
            var _actualReturnType = _userController.Validate(validUser) as OkObjectResult;

            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);
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

            var _mockBusinessMethod = new Mock<UserManager>();
            _mockBusinessMethod.Setup(p => p.Validate(searchUser.UserEmail,searchUser.UserPassword)).Throws<System.IO.IOException>();
            UserController _userController = new UserController(_mockBusinessMethod.Object);
            //Act
            var _returnedUserResp = _userController.Validate(searchUser);
            var _returnedUserAck = _returnedUserResp as OkObjectResult;
            //Assert
            Assert.ReferenceEquals(_returnedUserAck.Value, users_ack);
        }
        
        [TestMethod]
        public void FacebookUsers_PositiveTestCases_ValidInput()
        {
            //Assign
            var validUser = new Fbuser()
            { 
                UserEmail = "findmyromm.project123@gmail.com",
                UserName ="Admin",
                Provider ="Facebook",
                ProviderId= "108567126995987",
                UserType="Partner"
                
            };
            var _mockBusinessMethod = new Mock<UserManager>();
            var _returnAckType = new Acknowledgement<User>
            {
                code = 1,
                Set = null,
                Message = "Success"
            };

            _mockBusinessMethod.Setup(p => p.fbUser(validUser)).Returns(_returnAckType);
            UserController _userController = new UserController(_mockBusinessMethod.Object);
            //Act
            var _actualReturnType = _userController.FbUser(validUser) as OkObjectResult;

            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);
        }
        [TestMethod]
        public void FaceBookUser_NegativeTestCases_TestResult()
        {
            //Assign 
            var fbUser = new Fbuser()
            {
                UserEmail = "Admin@fmr.com",
                UserName = "Admin",
                Provider = "Facebook",
                ProviderId = "345678909876545678",
                UserType = "Renter"
            };

            var _mockBusinessMethod = new Mock<UserManager>();
            var _returnAckType = new Acknowledgement<User>
            {
                code = 3,
                Set = null,
                Message = "Please Enter Correct User"
            };

            _mockBusinessMethod.Setup(p => p.fbUser(fbUser)).Throws<System.IO.IOException>();
            UserController _userController = new UserController(_mockBusinessMethod.Object);
            //Act
            var _returnedUserResp = _userController.FbUser(fbUser);
            var _returnedUserAck = _returnedUserResp as OkObjectResult;
            
            //Assert
            Assert.ReferenceEquals(_returnedUserAck.Value, _returnAckType);
        }
        
        [TestMethod]
        public void GetUserdata_PositiveTestCases_TestResults()
        {
            //Assign
            var userId = 53;
            var user = new UserUpdateHelp()
            {
                UserId = 53,
                UserName = "Admin",
                UserEmail = "Admin@fmr.com",
                UserAddress = "Admin",
                UserPhoneNumber="9999999999",
                UserStatus="valid",
                UserType="Admin"
            };
            var UserList = new List<UserUpdateHelp>();
            UserList.Add(user);
            
            var _mockUserMethod = new Mock<UserManager>();
            var _returnAckType = new Acknowledgement<UserUpdateHelp>
            {
                code = 1,
                Set = UserList,
                Message = "User data loaded successfully"
            };
            _mockUserMethod.Setup(p => p.GetUser(userId)).Returns(_returnAckType);
            UserController _userController = new UserController(_mockUserMethod.Object);
            //Act
            var _actualReturnType = _userController.GetUserdata(userId) as OkObjectResult;
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);
            
        }

        [TestMethod]
        public void GetUserdata_NegativeTestCases_TestResults()
        {
            //Assign
            var userId = 1;
            var _returnAckType = new Acknowledgement<UserUpdateHelp>
            {
                code = 3,
                Set = null,
                Message = "Something went wrong. Please try again later"
            };
            var _mockUserMethod = new Mock<UserManager>();
            _mockUserMethod.Setup(p => p.GetUser(userId)).Throws<System.Exception>();
            UserController _userController = new UserController(_mockUserMethod.Object);
            //Act
            var _actualReturnType = _userController.GetUserdata(userId) as OkObjectResult;
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);

        }

        [TestMethod]
        public void UpdateUserData_PositiveUserTestCases_TestResults()
        {
            //Assign
            var user = new UserPutHelp()
            {
                UserId = 53,
                UserName = "Admin",
                UserAddress = "Admin",
                UserPhoneNumber = "9999999999",
            };
            var UserList = new List<UserPutHelp>();
            UserList.Add(user);

            var _mockUserMethod = new Mock<UserManager>();
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 1,
                Set = UserList,
                Message = "Successfully updated."
            };
            _mockUserMethod.Setup(p => p.UpdateUser(user)).Returns(_returnAckType);
            UserController _userController = new UserController(_mockUserMethod.Object);
            //Act
            var _actualReturnType = _userController.UpdateUserData(user) as OkObjectResult;
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);

        }

        [TestMethod]
        public void UpdateUserData_NegativeTestCases_TestResults()
        {
            //Assign
            var user = new UserPutHelp()
            {
                UserId = 1,
                UserName = "Admin",
                UserAddress = "Admin",
                UserPhoneNumber = "9999999999",
            };
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 3,
                Set = null,
                Message = "Something went wrong. Please try again later"
            };
            var _mockUserMethod = new Mock<UserManager>();
            _mockUserMethod.Setup(p => p.UpdateUser(user)).Throws<System.Exception>();
            UserController _userController = new UserController(_mockUserMethod.Object);
            //Act
            var _actualReturnType = _userController.UpdateUserData(user) as OkObjectResult;
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);

        }


        [TestMethod]
        public void EmailPassword_PositiveUserTestCases_TestResults()
        {
            //Assign
            var email = "Admin@fmr.com";

            var _mockUserMethod = new Mock<UserManager>();
            var _message = "Mail sent successfully";
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 1,
                Set = null,
                Message = _message
            };

            _mockUserMethod.Setup(p => p.EmailPasswordManager(email)).Returns(_message);
            UserController _userController = new UserController(_mockUserMethod.Object);
            //Act
            var _actualReturnType = _userController.EmailPassword(email) as OkObjectResult;
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);

        }
        [TestMethod]
        public void EmailPassword_NegativeUserTestCases_EmptyMail_TestResults()
        {
            //Assign
            string email = null;
            var _message = "Email should not be empty";
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 2,
                Set = null,
                Message = _message
            };
            var _mockUserMethod = new Mock<UserManager>();
            _mockUserMethod.Setup(p => p.EmailPasswordManager(email)).Returns(_message);
            UserController _userController = new UserController(_mockUserMethod.Object);
            //Act
            var _actualReturnType = _userController.EmailPassword(email) as OkObjectResult;
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);

        }

        [TestMethod]
        public void EmailPassword_NegativeUserTestCases_NotExist_TestResults()
        {
            //Assign
            string email = "test@gmail.com";
            string _message = null;
            var _mockUserMethod = new Mock<UserManager>();
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 2,
                Set = null,
                Message = "Email does not exist"
            };
            _mockUserMethod.Setup(p => p.EmailPasswordManager(email)).Returns(_message);
            UserController _userController = new UserController(_mockUserMethod.Object);
            //Act
            var _actualReturnType = _userController.EmailPassword(email) as OkObjectResult;
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);

        }

        [TestMethod]
        public void EmailPassword_NegativeUserTestCases_InvalidInput_TestResults()
        {
            //Assign
            string email = "test.gmail.com";
            string _message = "Invalid Input";
            var _mockUserMethod = new Mock<UserManager>();
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 2,
                Set = null,
                Message = _message
            };
            _mockUserMethod.Setup(p => p.EmailPasswordManager(email)).Returns(_message);
            UserController _userController = new UserController(_mockUserMethod.Object);
            //Act
            var _actualReturnType = _userController.EmailPassword(email) as OkObjectResult;
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);

        }

        [TestMethod]
        public void EmailPassword_NegativeUserTestCases_Exception_TestResults()
        {
            //Assign
            string email = "test@gmail.com";
            var _mockUserMethod = new Mock<UserManager>();
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 2,
                Set = null,
                Message = "System Object Reference missing"
            };
            _mockUserMethod.Setup(p => p.EmailPasswordManager(email)).Throws<System.Exception>();
            UserController _userController = new UserController(_mockUserMethod.Object);
            //Act
            var _actualReturnType = _userController.EmailPassword(email) as OkObjectResult;
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);
        }

        [TestMethod]
        public void ForgetPassword_PositiveUserTestCases_TestResults()
        {
            //Assign
            var email = "Admin@fmr.com";
            var password = "Admin@123";
            var _mockUserMethod = new Mock<UserManager>();
            var _message = "Updated Successfully";
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 1,
                Set = null,
                Message = _message
            };

            _mockUserMethod.Setup(p => p.ForgetPasswordManager(email,password)).Returns(_message);
            UserController _userController = new UserController(_mockUserMethod.Object);
            //Act
            var _actualReturnType = _userController.ForgetPassword(email,password) as OkObjectResult;
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);

        }

        [TestMethod]
        public void ForgetPassword_NegativeUserTestCases_MailEmpty_TestResults()
        {
            //Assign
            var email = "";
            var password = "Admin@123";
            var _message = "Email should not be empty";
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 2,
                Set = null,
                Message = _message
            };
            var _mockUserMethod = new Mock<UserManager>();
            _mockUserMethod.Setup(p => p.ForgetPasswordManager(email, password)).Returns(_message);
            UserController _userController = new UserController(_mockUserMethod.Object);
            //Act
            var _actualReturnType = _userController.ForgetPassword(email, password) as OkObjectResult;
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);

        }


        [TestMethod]
        public void ForgetPassword_NegativeUserTestCases_PasswordEmpty_TestResults()
        {
            //Assign
            var email = "Admin@fmr.com";
            var password = "";
            var _message = "Password should not be empty";
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 2,
                Set = null,
                Message = _message
            };
            var _mockUserMethod = new Mock<UserManager>();
            _mockUserMethod.Setup(p => p.ForgetPasswordManager(email, password)).Returns(_message);
            UserController _userController = new UserController(_mockUserMethod.Object);
            //Act
            var _actualReturnType = _userController.ForgetPassword(email, password) as OkObjectResult;
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);

        }

        [TestMethod]
        public void ForgetPassword_NegativeUserTestCases_NotExist_TestResults()
        {
            //Assign
            var email = "test@gmail.com";
            var password = "Admin@123";
            var _mockUserMethod = new Mock<UserManager>();
            var _message = "Email does not exist";
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 2,
                Set = null,
                Message = _message
            };

            _mockUserMethod.Setup(p => p.ForgetPasswordManager(email, password)).Returns(_message);
            UserController _userController = new UserController(_mockUserMethod.Object);
            //Act
            var _actualReturnType = _userController.ForgetPassword(email, password) as OkObjectResult;
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);

        }

        [TestMethod]
        public void ForgetPassword_NegativeUserTestCases_InvalidEmail_TestResults()
        {
            //Assign
            var email = "test.gmail.com";
            var password = "Admin@123";
            var _mockUserMethod = new Mock<UserManager>();
            var _message = "Invalid Email";
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 2,
                Set = null,
                Message = _message
            };

            _mockUserMethod.Setup(p => p.ForgetPasswordManager(email, password)).Returns(_message);
            UserController _userController = new UserController(_mockUserMethod.Object);
            //Act
            var _actualReturnType = _userController.ForgetPassword(email, password) as OkObjectResult;
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);

        }

        [TestMethod]
        public void ForgetPassword_NegativeUserTestCases_InvalidPassword_TestResults()
        {
            //Assign
            var email = "test@gmail.com";
            var password = "Admin123";
            var _mockUserMethod = new Mock<UserManager>();
            var _message = "Invalid Password";
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 2,
                Set = null,
                Message = _message
            };

            _mockUserMethod.Setup(p => p.ForgetPasswordManager(email, password)).Returns(_message);
            UserController _userController = new UserController(_mockUserMethod.Object);
            //Act
            var _actualReturnType = _userController.ForgetPassword(email, password) as OkObjectResult;
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);

        }

        [TestMethod]
        public void ForgetPassword_NegativeUserTestCases_Exception_TestResults()
        {
            //Assign
            var email = "test@gmail.com";
            var password = "Admin123";
            var _mockUserMethod = new Mock<UserManager>();
            var _returnAckType = new Acknowledgement<UserPutHelp>
            {
                code = 2,
                Set = null,
                Message = "System Object Reference missing"
            };
            _mockUserMethod.Setup(p => p.ForgetPasswordManager(email,password)).Throws<System.Exception>();
            UserController _userController = new UserController(_mockUserMethod.Object);
            //Act
            var _actualReturnType = _userController.ForgetPassword(email, password) as OkObjectResult;
            //Assert
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);

        }
    
        [TestMethod]
        public void PostUser_PositiveTestCases_TestResults()
        {
            //Assign
            var _mockBusinessMethod = new Mock<UserManager>();
            var user_List = GetUsers_Mock_Positive();
          
            var _returnAckType = new Acknowledgement<User>
            {
                code = 1,
                Set = null,
                Message = "successfully registered"
            };

            _mockBusinessMethod.Setup(p => p.PostUser(user_List)).Returns(_returnAckType);
            UserController _userController = new UserController(_mockBusinessMethod.Object);
            //Act
            var _actualReturnType = _userController.PostUser(user_List) as OkObjectResult;
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);
        }

        [TestMethod]
        public void PostUser_NegativeTestCases_TestResults()
        {
            //Assign
            var _mockBussinessMethod = new Mock<UserManager>();
            var user_List = GetUsers_Mock_Negative();
            var _returnAskType = new Acknowledgement<User>
            {
                code = 2,
                Set = null,
                Message = "Fullname field should not empty"
            };

            _mockBussinessMethod.Setup(p => p.PostUser(user_List)).Returns(_returnAskType);
            UserController _userController = new UserController(_mockBussinessMethod.Object);

            var _actualReturnType = _userController.PostUser(user_List) as OkObjectResult;
            Assert.ReferenceEquals(_returnAskType, _actualReturnType);
        }
        public HelperAddUser GetUsers_Mock_Positive()
        {
            HelperAddUser user = new HelperAddUser();
            user.UserName = "shravani";
            user.Email = "edaramshravani@gmail.com";
            user.MobileNumber = "9876543210";
            user.Address = "asdfghjklqwertyui";
            user.Password= "Abcd@123";
            user.Type = "partner";
            return user;
        }
        
        public HelperAddUser GetUsers_Mock_Negative()
        {
            HelperAddUser user = new HelperAddUser();
            user.UserName = "     ";
            user.Email = "edaramshravani@gmail.com";
            user.MobileNumber = "9876543210";
            user.Address = "asdfghjklqwertyui";
            user.Password = "Abcd@123";
            user.Type = "partner";
            return user;
        }

      
    }
}
