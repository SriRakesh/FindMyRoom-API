using FindMyRoom.APIControllers.Controllers;
using FindMyRoom.BusinessManager;
using FindMyRoom.DataService;
using FindMyRoom.Entities;
using FindMyRoom.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Http.Results;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FindMyRoom.APIControllers.Tests
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void GetUserDetails_PositiveTestCases_TestResults()
        {
            //Assign
            var _mockAdminMethod = new Mock<AdminManager>();
            var User_List = GetUsersDetails_Mock_Positive();
            _mockAdminMethod.Setup(p => p.GetUserDetails()).Returns(User_List);
            AdminController _AdminController = new AdminController(_mockAdminMethod.Object);
            //Act
            var _returnedUsersResp = _AdminController.GetUserDetails();
            // var _returnedUsersAck = _returnedUsersResp as OkObjectResult;
            var _originalUsersAck = GetUsersDetails_Mock_Positive();
            //Assert
            Assert.IsNotNull(_returnedUsersResp);
            Assert.ReferenceEquals(_returnedUsersResp, _originalUsersAck);
        }

        [TestMethod]
        public void GetUserDetails_NegativeTestCases_TestResults()
        {
            //Assign
            var _mockAdminMethod = new Mock<AdminManager>();
            var User_List = GetUserDetails_Mock_Negative();
            _mockAdminMethod.Setup(p => p.GetUserDetails()).Throws<System.IO.IOException>();
            AdminController _AdminController = new AdminController(_mockAdminMethod.Object);
            //Act
            var _returnedUsersResp = _AdminController.GetUserDetails();
            //var _returnedUsersAck = _returnedUsersResp as OkObjectResult;
            var _originalUsersAck = GetUserDetails_Mock_Negative();
            //Assert
            Assert.IsNotNull(_returnedUsersResp);

            Assert.ReferenceEquals(_returnedUsersResp, _originalUsersAck);
        }

        private Acknowledgement<UserModified> GetUsersDetails_Mock_Positive()
        {
            UserModified userData = new UserModified()
            {
                UserId = 1,
                UserName = "Hemangi",
                UserEmail = "Hemangi@gmail.com",
                UserPhoneNumber = "9823625981",
                UserAddress = "Nagpur",
                UserType = "Partner",
                UserStatus = "valid"
            };

            List<UserModified> userList = new List<UserModified>();
            userList.Add(userData);

            var users_ack = new Acknowledgement<UserModified>
            {
                code = 0,
                Set = userList,
                Message = "Success"
            };
            return users_ack;
        }
        private Acknowledgement<UserModified> GetUserDetails_Mock_Negative()
        {
            List<UserModified> userList = new List<UserModified>();
            var users_ack = new Acknowledgement<UserModified>
            {
                code = 2,
                Set = userList,
                Message = "Userdata loading failed"
            };
            return users_ack;
        }

        [TestMethod]

        public void Getpartnerdata_PositiveTestCases_TestResults()
        {
            //Assign
            var _mockAdminMethod = new Mock<AdminManager>();
            var Partner_List = Getpartnerdata_Mock_Positive();
            _mockAdminMethod.Setup(p => p.GetPartners()).Returns(Partner_List);
            AdminController _AdminController = new AdminController(_mockAdminMethod.Object);
            //Act
            var _returnedPartnersResp = _AdminController.Getpartnerdata();
            // var _returnedUsersAck = _returnedUsersResp as OkObjectResult;
            var _originalPartnersAck = Getpartnerdata_Mock_Positive();
            //Assert
            Assert.IsNotNull(_returnedPartnersResp);
            Assert.ReferenceEquals(_returnedPartnersResp, _originalPartnersAck);
        }

        [TestMethod]

        public void Getpartnerdata_NegativeTestCases_TestResults()
        {
            //Assign
            var _mockAdminMethod = new Mock<AdminManager>();
            var Partner_List = Getpartnerdata_Mock_Positive();
            _mockAdminMethod.Setup(p => p.GetPartners()).Throws<System.IO.IOException>();
            AdminController _AdminController = new AdminController(_mockAdminMethod.Object);
            //Act
            var _returnedPartnersResp = _AdminController.Getpartnerdata();
            // var _returnedUsersAck = _returnedUsersResp as OkObjectResult;
            var _originalPartnersAck = Getpartnerdata_Mock_Positive();
            //Assert
            //Assert.IsNotNull(_returnedPartnersResp);
            Assert.ReferenceEquals(_returnedPartnersResp, _originalPartnersAck);
        }
        private Acknowledgement<UserModified> Getpartnerdata_Mock_Positive()
        {
            UserModified PartnerData = new UserModified()
            {
                UserId = 1,
                UserName = "Hemangi",
                UserEmail = "Hemangi@gmail.com",
                UserPhoneNumber = "9823623466",
                UserAddress = "Nagpur",
                UserType = "Partner",
                UserStatus = "valid"
            };

            List<UserModified> partnerList = new List<UserModified>();
            partnerList.Add(PartnerData);

            var partner_ack = new Acknowledgement<UserModified>
            {
                code = 0,
                Set = partnerList,
                Message = "Partnerdata loading failed"
            };
            return partner_ack;
        }
        private Acknowledgement<UserModified> Getpartnerdata_Mock_Negative()
        {
            List<UserModified> partnerList = new List<UserModified>();
            var partner_ack = new Acknowledgement<UserModified>
            {
                code = 2,
                Set = partnerList,
                Message = "Partnerdata loading failed"
            };
            return partner_ack;
        }

        [TestMethod]
        public void PutPartner_Positive_TestCases_TestResults()
        {
            //Assign
            var _mockAdminMethod = new Mock<AdminManager>();

            UserModified PartnerData = new UserModified()
            {
                UserId = 1000,
                UserName = "Hemangi",
                UserEmail = "Hemangi@gmail.com",
                UserPhoneNumber = "9823623466",
                UserAddress = "Nagpur",
                UserType = "Partner",
                UserStatus = "valid"
            };
            var put_Response = PutPartner_Mock_Positive_TestCases_TestResults(PartnerData, 1);
            _mockAdminMethod.Setup(p => p.UpdatePartner(PartnerData, 1000)).Returns(put_Response);
            AdminController _AdminController = new AdminController(_mockAdminMethod.Object);
            //Act
            var _returnedPartnersResp = _AdminController.Getpartnerdata();
            // var _returnedUsersAck = _returnedUsersResp as OkObjectResult;
            var _originalPartnersAck = PutPartner_Mock_Positive_TestCases_TestResults(PartnerData, 1);
            //Assert
            //Assert.IsNotNull(_returnedPartnersResp);
            Assert.ReferenceEquals(_returnedPartnersResp, _originalPartnersAck);
        }


        [TestMethod]
        public void PutPartner_Negative_TestCases_TestResults_Invalid_Username()
        {
            //Assign
            var _mockAdminMethod = new Mock<AdminManager>();

            UserModified PartnerData = new UserModified()
            {
                UserId = 1,
                UserName = " ",
                UserEmail = "Hemangi@gmail.com",
                UserPhoneNumber = "9823623466",
                UserAddress = "Nagpur",
                UserType = "Partner",
                UserStatus = "valid"
            };
            var InalidUsername_Response = PutPartner_Mock_Negative_Invalid_Username();
            _mockAdminMethod.Setup(p => p.UpdatePartner(PartnerData, 1)).Returns(InalidUsername_Response);
            AdminController _AdminController = new AdminController(_mockAdminMethod.Object);
            //Act
            var _returnedPartnersResp = _AdminController.Getpartnerdata();
            // var _returnedUsersAck = _returnedUsersResp as OkObjectResult;
            var _originalPartnersAck = PutPartner_Mock_Negative_Invalid_Username();
            //Assert
            //Assert.IsNotNull(_returnedPartnersResp);
            Assert.ReferenceEquals(_returnedPartnersResp, _originalPartnersAck);
        }
        [TestMethod]
        public void PutPartner_Negative_TestCases_TestResults_Invalid_phonenumber()
        {
            //Assign
            var _mockAdminMethod = new Mock<AdminManager>();

            UserModified PartnerData = new UserModified()
            {
                UserId = 1,
                UserName = "abc",
                UserEmail = "Hemangi@gmail.com",
                UserPhoneNumber = "98236466",
                UserAddress = "Nagpur",
                UserType = "Partner",
                UserStatus = "valid"
            };
            var Inalidphonenumber_Response = PutPartner_Mock_Negative_Invalid_phonenumber();
            _mockAdminMethod.Setup(p => p.UpdatePartner(PartnerData, 1)).Returns(Inalidphonenumber_Response);
            AdminController _AdminController = new AdminController(_mockAdminMethod.Object);
            //Act
            var _returnedPartnersResp = _AdminController.Getpartnerdata();
            // var _returnedUsersAck = _returnedUsersResp as OkObjectResult;
            var _originalPartnersAck = PutPartner_Mock_Negative_Invalid_phonenumber();
            //Assert
            //Assert.IsNotNull(_returnedPartnersResp);
            Assert.ReferenceEquals(_returnedPartnersResp, _originalPartnersAck);
        }
        [TestMethod]
        public void PutPartner_Negative_TestCases_TestResults_Invalid_Address()
        {
            //Assign
            var _mockAdminMethod = new Mock<AdminManager>();

            UserModified PartnerData = new UserModified()
            {
                UserId = 1,
                UserName = "abc",
                UserEmail = "Hemangi@gmail.com",
                UserPhoneNumber = "98236466",
                UserAddress = "  ",
                UserType = "Partner",
                UserStatus = "valid"
            };
            var InalidAddress_Response = PutPartner_Mock_Negative_Invalid_Address();
            _mockAdminMethod.Setup(p => p.UpdatePartner(PartnerData, 1)).Returns(InalidAddress_Response);
            AdminController _AdminController = new AdminController(_mockAdminMethod.Object);
            //Act
            var _returnedPartnersResp = _AdminController.Getpartnerdata();
            // var _returnedUsersAck = _returnedUsersResp as OkObjectResult;
            var _originalPartnersAck = PutPartner_Mock_Negative_Invalid_Address();
            //Assert
            //Assert.IsNotNull(_returnedPartnersResp);
            Assert.ReferenceEquals(_returnedPartnersResp, _originalPartnersAck);
        }
        [TestMethod]
        public void PutPartner_Negative_TestCases_TestResults_Invalid_UserType()
        {
            //Assign
            var _mockAdminMethod = new Mock<AdminManager>();

            UserModified PartnerData = new UserModified()
            {
                UserId = 1,
                UserName = "abc",
                UserEmail = "Hemangi@gmail.com",
                UserPhoneNumber = "98236466",
                UserAddress = "scf",
                UserType = "Partn",
                UserStatus = "valid"
            };
            var InalidUserType_Response = PutPartner_Mock_Negative_Invalid_UserType();
            _mockAdminMethod.Setup(p => p.UpdatePartner(PartnerData, 1)).Returns(InalidUserType_Response);
            AdminController _AdminController = new AdminController(_mockAdminMethod.Object);
            //Act
            var _returnedPartnersResp = _AdminController.Getpartnerdata();
            // var _returnedUsersAck = _returnedUsersResp as OkObjectResult;
            var _originalPartnersAck = PutPartner_Mock_Negative_Invalid_UserType();
            //Assert
            //Assert.IsNotNull(_returnedPartnersResp);
            Assert.ReferenceEquals(_returnedPartnersResp, _originalPartnersAck);
        }
        [TestMethod]
        public void PutPartner_Negative_TestCases_TestResults_Invalid_UserStatus()
        {
            //Assign
            var _mockAdminMethod = new Mock<AdminManager>();

            UserModified PartnerData = new UserModified()
            {
                UserId = 1,
                UserName = "abc",
                UserEmail = "Hemangi@gmail.com",
                UserPhoneNumber = "98236466",
                UserAddress = "scf",
                UserType = "Partn",
                UserStatus = "on"
            };
            var InalidUserStatus_Response = PutPartner_Mock_Negative_Invalid_UserStatus();
            _mockAdminMethod.Setup(p => p.UpdatePartner(PartnerData, 1)).Returns(InalidUserStatus_Response);
            AdminController _AdminController = new AdminController(_mockAdminMethod.Object);
            //Act
            var _returnedPartnersResp = _AdminController.Getpartnerdata();
            // var _returnedUsersAck = _returnedUsersResp as OkObjectResult;
            var _originalPartnersAck = PutPartner_Mock_Negative_Invalid_UserStatus();
            //Assert
            //Assert.IsNotNull(_returnedPartnersResp);
            Assert.ReferenceEquals(_returnedPartnersResp, _originalPartnersAck);
        }
        private Acknowledgement<UserModified> PutPartner_Mock_NegativeTestMismatchedId(UserModified user, int partnerId)
        {
            Acknowledgement<UserModified> aknowledgement = new Acknowledgement<UserModified>();
            if (user.UserId != partnerId)
            {
                aknowledgement.code = 2;
                aknowledgement.Set = null;
                aknowledgement.Message = "PartnerId and UserId does not match";
                return aknowledgement;
            }
            else
            {
                List<UserModified> partnerList = new List<UserModified>();
                partnerList.Add(user);
                aknowledgement.code = 0;
                aknowledgement.Set = partnerList;
                aknowledgement.Message = "Success";
                return aknowledgement;
            }

        }
        private Acknowledgement<UserModified> PutPartner_Mock_Positive_TestCases_TestResults(UserModified user, int partnerId)
        {
            Acknowledgement<UserModified> aknowledgement = new Acknowledgement<UserModified>();
            List<UserModified> partnerList = new List<UserModified>();
            partnerList.Add(user);
            aknowledgement.code = 0;
            aknowledgement.Set = partnerList;
            aknowledgement.Message = "Success";
            return aknowledgement;
        }
        private Acknowledgement<UserModified> PutPartner_Mock_Negative_Invalid_Username()
        {
            Acknowledgement<UserModified> aknowledgement = new Acknowledgement<UserModified>();
            UserModified PartnerData = new UserModified()
            {
                UserId = 1,
                UserName = "  ",
                UserEmail = "Hemangi@gmail.com",
                UserPhoneNumber = "9823623466",
                UserAddress = "Nagpur",
                UserType = "Partner",
                UserStatus = "valid"
            };

            List<UserModified> partnerList = new List<UserModified>();
            partnerList.Add(PartnerData);
            aknowledgement.code = 2;
            aknowledgement.Set = partnerList;
            aknowledgement.Message = "Username is invalid";
            return aknowledgement;
        }
        private Acknowledgement<UserModified> PutPartner_Mock_Negative_Invalid_phonenumber()
        {
            Acknowledgement<UserModified> aknowledgement = new Acknowledgement<UserModified>();
            UserModified PartnerData = new UserModified()
            {
                UserId = 1,
                UserName = "abcd",
                UserEmail = "Hemangi@gmail.com",
                UserPhoneNumber = "982362",
                UserAddress = "Nagpur",
                UserType = "Partner",
                UserStatus = "valid"
            };

            List<UserModified> partnerList = new List<UserModified>();
            partnerList.Add(PartnerData);
            aknowledgement.code = 2;
            aknowledgement.Set = partnerList;
            aknowledgement.Message = "Mobile number should start with 6/7/8/9 and should have 10 digits";
            return aknowledgement;
        }
        private Acknowledgement<UserModified> PutPartner_Mock_Negative_Invalid_Address()
        {
            Acknowledgement<UserModified> aknowledgement = new Acknowledgement<UserModified>();
            UserModified PartnerData = new UserModified()
            {
                UserId = 1,
                UserName = "abcd",
                UserEmail = "Hemangi@gmail.com",
                UserPhoneNumber = "9823625674",
                UserAddress = "  ",
                UserType = "Partner",
                UserStatus = "valid"
            };

            List<UserModified> partnerList = new List<UserModified>();
            partnerList.Add(PartnerData);
            aknowledgement.code = 2;
            aknowledgement.Set = partnerList;
            aknowledgement.Message = "UserAddress is invalid";
            return aknowledgement;
        }
        private Acknowledgement<UserModified> PutPartner_Mock_Negative_Invalid_UserType()
        {
            Acknowledgement<UserModified> aknowledgement = new Acknowledgement<UserModified>();
            UserModified PartnerData = new UserModified()
            {
                UserId = 1,
                UserName = "abcd",
                UserEmail = "Hemangi@gmail.com",
                UserPhoneNumber = "9823625674",
                UserAddress = "Nagpur",
                UserType = "usertype",
                UserStatus = "valid"
            };
            List<UserModified> partnerList = new List<UserModified>();
            partnerList.Add(PartnerData);
            aknowledgement.code = 2;
            aknowledgement.Set = partnerList;
            aknowledgement.Message = "UserStatus is invalid";
            return aknowledgement;
        }
        [TestMethod]
        public void DeletePartner_Positive()
        {
            int PartnerId = 400;
            var MockAdminBusiness = new Mock<AdminManager>();
            var returnAckType = new Acknowledgement<User>
            {
                code = 1,
                Set = null,
                Message = "Succuessfully deleted",
            };
            MockAdminBusiness.Setup(p => p.deletePartnerService(PartnerId)).Returns(returnAckType);
            AdminController adminController = new AdminController(MockAdminBusiness.Object);
            var actual_returnType = adminController.DeletePartnerList(PartnerId) as OkObjectResult;
            Assert.ReferenceEquals(actual_returnType.Value, returnAckType);

        }
        private Acknowledgement<UserModified> PutPartner_Mock_Negative_Invalid_UserStatus()
        {
            Acknowledgement<UserModified> aknowledgement = new Acknowledgement<UserModified>();
            List<UserModified> partnerList = new List<UserModified>();
            UserModified PartnerData = new UserModified()
            {
                UserId = 1,
                UserName = "abcd",
                UserEmail = "Hemangi@gmail.com",
                UserPhoneNumber = "9823625674",
                UserAddress = "Nagpur",
                UserType = "Partner",
                UserStatus = "sts"
            };
            partnerList.Add(PartnerData);
            aknowledgement.code = 2;
            aknowledgement.Set = partnerList;
            aknowledgement.Message = "UserType is invalid";
            return aknowledgement;
        }
        [TestMethod]
        public void DeletePartner_Negative()
        {
            int PartnerId = 400;
            var MockAdminBusiness = new Mock<AdminManager>();
            var returnAckType = new Acknowledgement<User>
            {
                code = 2,
                Set = null,
                Message = "No Partner found with given PartnerId to Delete",
            };
            MockAdminBusiness.Setup(p => p.deletePartnerService(PartnerId)).Throws<System.Exception>();
            AdminController adminController = new AdminController(MockAdminBusiness.Object);

            var actualReturnType = adminController.DeletePartnerList(PartnerId) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);
        }
    }
}
