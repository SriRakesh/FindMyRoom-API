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
using System.IO;
using FindMyRoom.Exceptions;

namespace FindMyRoom.APIControllers.Tests
{
    [TestClass]
    public class RoomsControllerTest
    {
        [TestMethod]
        public void PostRooms_Positive()
        {

            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = HelperMockRooms();
            var returnAckType = new Acknowledgement<Room>
            {
                code = 1,
                Set = null,
                Message = "Successfully Added",

            };

            MockRoomBusiness.Setup(p => p.AddingRoom(room_List)).Returns(returnAckType);
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);

            //Act
            var actualReturnType = roomsController.PostRooms(room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);


        }
        [TestMethod]
        public void PostRooms_Negative()
        {

            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = HelperMockRooms();

            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Something went wrong.Please try again",

            };
            MockRoomBusiness.Setup(p => p.AddingRoom(room_List)).Throws<System.Exception>();

            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.PostRooms(room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);


        }
        [TestMethod]
        public void PostRoomsNegative_Address()
        {

            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = HelperMockRooms();
            room_List.Address = " ";
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Address"
            };

            MockRoomBusiness.Setup(p => p.AddingRoom(room_List)).Throws(new RoomInsertingDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.PostRooms(room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);
        }
        [TestMethod]
        public void PostRoomsNegative_Cost()
        {

            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = HelperMockRooms();
            room_List.RoomCost = 5000;
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Room Cost"
            };
            MockRoomBusiness.Setup(p => p.AddingRoom(room_List)).Throws(new RoomInsertingDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualRetunType = roomsController.PostRooms(room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType.Message, actualRetunType.Value );
        }
        [TestMethod]
        public void PostRoomsNegative_Area()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = HelperMockRooms();
            room_List.Area = " ";
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Area"
            };
            MockRoomBusiness.Setup(p => p.AddingRoom(room_List)).Throws(new RoomInsertingDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualRetunType = roomsController.PostRooms(room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualRetunType.Value);
        }
        [TestMethod]

        public void PostRoomsNegative_City()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = HelperMockRooms();
            room_List.city = " ";
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid City"
            };
            MockRoomBusiness.Setup(p => p.AddingRoom(room_List)).Throws(new RoomInsertingDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualRetunType = roomsController.PostRooms(room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualRetunType.Value);
        }
        [TestMethod]

        public void PostRoomsNegative_Furniture()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = HelperMockRooms();
            room_List.Furniture = "yes";
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Furniture details"
            };
            MockRoomBusiness.Setup(p => p.AddingRoom(room_List)).Throws(new RoomInsertingDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualRetunType = roomsController.PostRooms(room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualRetunType.Value);
        }
        [TestMethod]
        public void PostRoomsNegative_Description()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = HelperMockRooms();
            room_List.Description = " ";
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Description"
            };
            MockRoomBusiness.Setup(p => p.AddingRoom(room_List)).Throws(new RoomInsertingDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualRetunType = roomsController.PostRooms(room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualRetunType.Value);
        }

        [TestMethod]
        public void PostRoomsNegative_NumberOfRooms()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = HelperMockRooms();
            room_List.NumberOfRooms = 0;
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Furniture details"
            };
            MockRoomBusiness.Setup(p => p.AddingRoom(room_List)).Throws(new RoomInsertingDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualRetunType = roomsController.PostRooms(room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualRetunType.Value);
        }

        [TestMethod]
        public void PostRoomsNegative_Pincode()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = HelperMockRooms();
            room_List.Pincode = 12;
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Pincode"
            };
            MockRoomBusiness.Setup(p => p.AddingRoom(room_List)).Throws(new RoomInsertingDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualRetunType = roomsController.PostRooms(room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualRetunType.Value);
        }

        [TestMethod]
        public void PostRoomsNegative_Latitude()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = HelperMockRooms();
            room_List.Latitude = "wwwwe";
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Latitude"
            };
            MockRoomBusiness.Setup(p => p.AddingRoom(room_List)).Throws(new RoomInsertingDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualRetunType = roomsController.PostRooms(room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualRetunType.Value);
        }

        [TestMethod]
        public void PostRoomsNegative_RoomType()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = HelperMockRooms();
            room_List.RoomType = "it is a new Room";
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Room Type"
            };
            MockRoomBusiness.Setup(p => p.AddingRoom(room_List)).Throws(new RoomInsertingDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualRetunType = roomsController.PostRooms(room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualRetunType.Value);
        }

        [TestMethod]
        public void PostRoomsNegative_Longitude()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = HelperMockRooms();
            room_List.Longitude = "yyryry";
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Longitude"
            };
            MockRoomBusiness.Setup(p => p.AddingRoom(room_List)).Throws(new RoomInsertingDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualRetunType = roomsController.PostRooms(room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualRetunType.Value);
        }

        public HelperAddRoom HelperMockRooms()
        {
            HelperAddRoom room = new HelperAddRoom();
            room.PartnerId = 12;
            room.Address = "abcdefghijkl";
            room.Area = "abdjkieijr";
            room.city = "Bangalore";
            room.Description = "tough night";
            room.Furniture = "yes";
            room.NumberOfRooms = 5;
            room.Pincode = 123456;
            room.RoomCost = 5000;
            room.Latitude = "500";
            room.RoomType = "flat";
            room.Longitude = "600";
            return room;
        }

        [TestMethod]
        public void DeleteRoom_Positive()
        {

            int RoomId = 10;
            var MockRoomBusiness = new Mock<RoomManager>();
            var returnAckType = new Acknowledgement<Room>
            {
                code = 1,
                Set = null,
                Message = "Successfully deleted",

            };


            //Act
            MockRoomBusiness.Setup(p => p.deleteRoomService(RoomId)).Returns(returnAckType);
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);

            var actualReturnType = roomsController.DeletePropertListing(RoomId) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);



        }
        [TestMethod]
        public void DeleteRoom_Negative()
        {
            int RoomId = 10;
            var MockRoomBusiness = new Mock<RoomManager>();
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "No Room found with given roomid to delete",

            };

            MockRoomBusiness.Setup(p => p.deleteRoomService(RoomId)).Throws<System.Exception>();
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);

            //Act
            var actualReturnType = roomsController.DeletePropertListing(RoomId) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);

        }

        public Updateroom MockUpdateRoom()
        {
            Updateroom room = new Updateroom();

            
            room.RoomId = 30;
            room.Cost = 8000;
            room.NoOfBeds = 5;
            room.City = "Bellary";
            room.Area = "Parvati Nagar";
            room.Address = "1st Link Road";
            room.Pincode = 123456;
            room.Furniture = "yes";
            room.Description = "Present in Karnataka";
            room.Status = "Valid";
            room.RoomType = "pg";
            room.Latitude = "12.2";
            room.Longitude = "12.4";

            return room;

        }

        //[TestMethod]
        //public void UpdateRoom_Cost()
        //{
        //    var MockRoomBusiness = new Mock<RoomManager>();
        //    var room_List = MockUpdateRoom();
        //    room_List.Cost = 0;
        //    var returnAckType = new Acknowledgement<Room>
        //    {
        //        code = 2,
        //        Set = null,
        //        Message = "Invalid Cost",
        //    };
        //    MockRoomBusiness.Setup(p => p.updateRoom(room_List.RoomId, room_List)).Throws(new RoomUpdateDataException(returnAckType.Message));
        //    RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
        //    var actualReturnType = roomsController.UpdateRoomDetails(room_List.RoomId, room_List) as OkObjectResult;
        //    Assert.ReferenceEquals(returnAckType, actualReturnType.Value);

        //}
        [TestMethod]
        public void UpdateRoom_Beds()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = MockUpdateRoom();
            room_List.NoOfBeds = 0;
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Beds",
            };
            MockRoomBusiness.Setup(p => p.updateRoom(room_List.RoomId, room_List)).Throws(new RoomUpdateDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.UpdateRoomDetails(room_List.RoomId, room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);

        }
        [TestMethod]
        public void UpdateRoom_Area()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = MockUpdateRoom();
            room_List.Address = " ";
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Area",
            };
            MockRoomBusiness.Setup(p => p.updateRoom(room_List.RoomId, room_List)).Throws(new RoomUpdateDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.UpdateRoomDetails(room_List.RoomId, room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);

        }
        [TestMethod]
        public void UpdateRoom_Address()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = MockUpdateRoom();
            room_List.Address = " ";
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Cost",
            };
            MockRoomBusiness.Setup(p => p.updateRoom(room_List.RoomId, room_List)).Throws(new RoomUpdateDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.UpdateRoomDetails(room_List.RoomId, room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);

        }
        [TestMethod]
        public void UpdateRoom_City()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = MockUpdateRoom();
            room_List.City = "";
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid City",
            };
            MockRoomBusiness.Setup(p => p.updateRoom(room_List.RoomId, room_List)).Throws(new RoomUpdateDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.UpdateRoomDetails(room_List.RoomId, room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);

        }

        [TestMethod]
        public void UpdateRoom_Pincode()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = MockUpdateRoom();
            room_List.Pincode = 1;
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Pincode",
            };
            MockRoomBusiness.Setup(p => p.updateRoom(room_List.RoomId, room_List)).Throws(new RoomUpdateDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.UpdateRoomDetails(room_List.RoomId, room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);

        }
        [TestMethod]
        public void UpdateRoom_Furniture()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = MockUpdateRoom();
            room_List.Furniture = "i dont know";
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Furniture",
            };
            MockRoomBusiness.Setup(p => p.updateRoom(room_List.RoomId, room_List)).Throws(new RoomUpdateDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.UpdateRoomDetails(room_List.RoomId, room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);

        }
        [TestMethod]
        public void UpdateRoom_Description()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = MockUpdateRoom();
            room_List.Description = "";
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Description",
            };
            MockRoomBusiness.Setup(p => p.updateRoom(room_List.RoomId, room_List)).Throws(new RoomUpdateDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.UpdateRoomDetails(room_List.RoomId, room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);
        }
        [TestMethod]
        public void GetPartnerCustmer_Positive()
        {
            int PartnerId = 394;
            var MockRoomBusiness = new Mock<RoomManager>();
            var returnAckType = new Acknowledgement<User>
            {
                code = 1,
                Set = null,
                Message = "Successfully Displayed",
            };
            var customers = new GetCustomerOfPartners()
            {
                PartnerId = 394,
                UserId = 392,
                RoomId = 610,
                RoomType = "flat",
                UserAddress = "Hyderabad",
                UserEmail = "manoranjan@gmail.com",
                UserName = "karuna",
                UserPhoneNumber = "9987657689"
            };
            List<GetCustomerOfPartners> par = new List<GetCustomerOfPartners>();
            par.Add(customers);
            MockRoomBusiness.Setup(p => p.GetCustomer(PartnerId)).Returns(par);
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);

            var actualReturnType = roomsController.GetCustomer(PartnerId) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);
        }
        [TestMethod]
        public void GetPartnerCustmer_Negative()
        {
            int PartnerId = 410;
            var MockRoomBusiness = new Mock<RoomManager>();
            var returnAckType = new Acknowledgement<User>
            {
                code = 2,
                Set = null,
                Message = "No Customers for a given particular PartnerId",
            };
            var customers = new GetCustomerOfPartners()
            {
                PartnerId = 411,
                UserId = 200,
                RoomId = 340,
                RoomType = "flat",
                UserAddress = "Hyderabad",
                UserEmail = "manoranjan@gmail.com",
                UserName = "karuna",
                UserPhoneNumber = "9987657689"
            };
            List<GetCustomerOfPartners> par = new List<GetCustomerOfPartners>();
            par.Add(customers);
            MockRoomBusiness.Setup(p => p.GetCustomer(PartnerId)).Throws<System.Exception>();
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);

        }
        [TestMethod]
        public void UpdateRoom_Status()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = MockUpdateRoom();
            room_List.Status = "not invalid";
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Cost",
            };
            MockRoomBusiness.Setup(p => p.updateRoom(room_List.RoomId, room_List)).Throws(new RoomUpdateDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.UpdateRoomDetails(room_List.RoomId, room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);

        }
        [TestMethod]
        public void UpdateRoom_RoomType()
        {
            var MockRoomBusiness = new Mock<RoomManager>();
            var room_List = MockUpdateRoom();
            room_List.RoomType = "flat and pg";
            var returnAckType = new Acknowledgement<Room>
            {
                code = 2,
                Set = null,
                Message = "Invalid Room Type",
            };
            MockRoomBusiness.Setup(p => p.updateRoom(room_List.RoomId, room_List)).Throws(new RoomUpdateDataException(returnAckType.Message));
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.UpdateRoomDetails(room_List.RoomId, room_List) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);

        }








        [TestMethod]
        public void UpdateRoom_Positive()
        {
            int RoomId = 30;
            Updateroom room = new Updateroom()
            {
                RoomId = 30,
                Cost = 8000,
                NoOfBeds = 5,
                City = "Bellary",
                Area = "Parvati Nagar",
                Address = "1st Link Road",
                Pincode = 123456,
                Furniture = "yes",
                Description = "Present in Karnataka",
                Status = "Valid",
                RoomType = "pg",
                Latitude="12.4",
                Longitude="33.4"

            };
            var MockRoomBusiness = new Mock<RoomManager>();
            var returnAckType = new Acknowledgement<Room>
            {
                code = 1,
                Set = null,
                Message = "Room Details are Updated",
            };

            MockRoomBusiness.Setup(p => p.updateRoom(RoomId, room)).Returns(returnAckType);
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            //Act
            var actualReturnType = roomsController.UpdateRoomDetails(RoomId, room) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);
        }
        [TestMethod]
        public void UpdateRoom_Negative()
        {
            int RoomId = 30;
            Updateroom room = new Updateroom()
            {
                RoomId = 30,
                Cost = 8000,
                NoOfBeds = 5,
                City = "Bellary",
                Area = "Parvati Nagar",
                Address = "1st Link Road",
                Pincode = 123456,
                Furniture = "yes",
                Description = "Present in Karnataka",
                Status = "Valid",
                RoomType = "pg",
                Latitude = "12.4",
                Longitude = "33.4"

            };
            var MockRoomBusiness = new Mock<RoomManager>();
            var returnAckType = new Acknowledgement<Room>
            {
                code = 1,
                Set = null,
                Message = "something went wrong.Please Try again later",
            };
            MockRoomBusiness.Setup(p => p.updateRoom(RoomId, room)).Throws<System.Exception>();
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.UpdateRoomDetails(RoomId, room) as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);

        }
        [TestMethod]
        public void UploadImage_Positive()
        {
            Image image = new Image()
            {
                ImageId = 10,
                RoomId = 20,
                RoomImage = new Byte[1] { Convert.ToByte(12) }
            };
            var MockRoomBusiness = new Mock<RoomManager>();
            var returnAckType = new Acknowledgement<Image>
            {
                code = 1,
                Set = null,
                Message = "Images are Updated",
            };
            List<byte[]> list = new List<byte[]>();
            list.Add(image.RoomImage);
            MockRoomBusiness.Setup(p => p.AddImage(list)).Returns(returnAckType);
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.Upload() as OkObjectResult;
            Assert.ReferenceEquals(returnAckType, actualReturnType.Value);


        }
        [TestMethod]
        public void UploadImage_Negative()
        {
            Image image = new Image()
            {
                ImageId = 20,
                RoomId = 10,
                RoomImage = new Byte[1] { Convert.ToByte(12) }

            };
            var MockRoomBusiness = new Mock<RoomManager>();
            var returnType = new Acknowledgement<Image>
            {
                code = 2,
                Set = null,
                Message = "Failed to Upload Images",
            };


            List<byte[]> list = new List<byte[]>();
            list.Add(image.RoomImage);
            MockRoomBusiness.Setup(p => p.AddImage(list)).Throws<System.Exception>();
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.Upload() as OkObjectResult;
            Assert.ReferenceEquals(returnType, actualReturnType.Value);


        }
        //[TestMethod]
        //public void UpdateImage_Positive()
        //{
        //    int RoomId = 12;
        //    Image image = new Image()
        //    {
        //        ImageId = 10,
        //        RoomId = 12,
        //        RoomImage = new Byte[1] { Convert.ToByte(12) }
        //    };
        //    var MockRoomBusiness = new Mock<RoomManager>();
        //    var returnType = new Acknowledgement<Image>
        //    {
        //        code = 1,
        //        Set = null,
        //        Message = "Images are Updated",
        //    };

        //    //  File file;
        //    List<byte[]> list = new List<byte[]>();
        //    list.Add(image.RoomImage);
        //    MockRoomBusiness.Setup(p => p.AddImage(RoomId, list)).Returns(returnType);
        //    RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
        //    var actualReturnType = roomsController.UpdateImage(RoomId, file) as OkObjectResult;
        //    Assert.ReferenceEquals(returnType, actualReturnType.Value);
        //}
        //[TestMethod]
        //public void UpdateImage_Negative()
        //{
        //    int RoomId = 12;
        //    Image image = new Image()
        //    {
        //        ImageId = 10,
        //        RoomId = 12,
        //        RoomImage = new Byte[1] { Convert.ToByte(12) }

        //    };
        //    var MockRoomBusiness = new Mock<RoomManager>();
        //    var returnType = new Acknowledgement<Image>
        //    {
        //        code = 2,
        //        Set = null,
        //        Message = "Failed to upload Images"

        //    };
        //    List<byte[]> list = new List<byte[]>();
        //    list.Add(image.RoomImage);
        //    MockRoomBusiness.Setup(p => p.AddImage(RoomId, list)).Throws<System.Exception>();
        //    list.Add(image.RoomImage);
        //    RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
        //    var actualReturnType = roomsController.UpdateImage(RoomId, file) as OkObjectResult;
        //    Assert.ReferenceEquals(returnType, actualReturnType.Value);
        //}


        [TestMethod]
        public void GetImage_Positive()
        {
            int roomId = 10;
            Image image = new Image()
            {
                ImageId = 10,
                RoomId = 12,
                RoomImage = new Byte[1] { Convert.ToByte(12) }
            };
            var imageList = new List<Image>();
            imageList.Add(image);

            var MockRoomBusiness = new Mock<RoomManager>();
            MockRoomBusiness.Setup(p => p.GetImages(image.RoomId)).Returns(imageList);
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.GetImage(roomId);
            //Assert.ReferenceEquals(actualReturnType, actualReturnType.Value);
            Assert.ReferenceEquals(imageList, actualReturnType.Value);
    }
        [TestMethod]
        public void GetImage_Negative()
        {
            int roomId = 10;
            Image image = new Image()
            {
                ImageId = 10,
                RoomId = 12,
                RoomImage = new byte[1] { Convert.ToByte(12) }

            };
            var returnType = new Acknowledgement<Image>
            {
                code = 2,
                Set = null,
                Message = "Failed to get details of Images"

            };
            var imageList = new List<Image>();
            imageList.Add(image);
           
            var MockRoomBusiness = new Mock<RoomManager>();
            MockRoomBusiness.Setup(p => p.GetImages(image.RoomId)).Throws<System.Exception>();
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.GetImage(roomId);
            Assert.ReferenceEquals(returnType, actualReturnType.Value);


        }
        [TestMethod]
        public void GetImageId_Positive()
        {
            int roomId = 10;
            Image image = new Image()
            {
                ImageId = 10,
                RoomId = 12,
                RoomImage = new byte[1] { Convert.ToByte(12) }

            };
            
            //var imageList = new List<Image>();
            //imageList.Add(image);
            var MockRoomBusiness = new Mock<RoomManager>();
            MockRoomBusiness.Setup(p => p.GetImagesId(roomId)).Returns(image.RoomImage);
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.GetImageById(roomId);
            Assert.ReferenceEquals(image.RoomImage, actualReturnType.GetHashCode());
         }
        [TestMethod]
        public void GetImage_Id_Negative()
        {
            int roomId = 10;
            Image image = new Image()
            {
                ImageId = 10,
                RoomId = 12,
                RoomImage = new byte[1] { Convert.ToByte(12) }
            };
            var returnType = new Acknowledgement<Image>
            {
                code = 2,
                Set = null,
                Message = "No Images Found"

            };

            var MockRoomBusiness = new Mock<RoomManager>();
            MockRoomBusiness.Setup(p=>p.GetImagesId(roomId)).Throws<System.Exception>();
            RoomsController roomsController = new RoomsController(MockRoomBusiness.Object);
            var actualReturnType = roomsController.GetImage(roomId);
            Assert.ReferenceEquals(returnType, actualReturnType.Value);



        }




    }
}

    

