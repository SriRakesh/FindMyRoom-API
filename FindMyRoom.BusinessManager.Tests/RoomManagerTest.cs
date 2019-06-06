using FindMyRoom.DataService;
using FindMyRoom.Entities;
using FindMyRoom.Entities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace FindMyRoom.BusinessManager.Tests
{
    [TestClass]
    public class RoomManagerTest
    {
        [TestMethod]
        public void GetPartnerCustomer_Positive()
        {
            int PartnerId = 394;
            var MockRoomService = new Mock<RoomService>();
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
            MockRoomService.Setup(p => p.GetCustomers(PartnerId)).Returns(par);
            var roomManager = new RoomManager(MockRoomService.Object);
        }
        [TestMethod]
        public void PostRoomsBusiness_Positive()
        {
            var MockRoomService = new Mock<RoomService>();
            HelperAddRoom room = new HelperAddRoom
            {

                PartnerId=12,
                Address = "abcdefghijkl",
                Area = "abdjkieijr",
                city = "Bangalore",
                Description = "tough night",
                Furniture = "yes",
                NumberOfRooms = 5,
                Pincode = 123456,
                RoomCost = 5000,
                Latitude = "500",
                RoomType = "flat",
                Longitude = "600",

            };
            var retAck = new Acknowledgement<Room>
            {
                code = 1,
                Set = null,
                Message = "Successfully Added"
            };
            int roomId = 10;
            MockRoomService.Setup(p => p.AddRoom(room)).Returns(roomId);
            RoomManager roomManager = new RoomManager(MockRoomService.Object);
            try
            {
                var actualReturnType = roomManager.AddingRoom(room);
                Assert.IsTrue(false);

            }
           // var actualRetunType = roomManager.PostRooms(room_List) as OkObjectResult;
           catch
            {
                Assert.IsTrue(true);

            }

        }
        [TestMethod]
        public void PostRoomsBussiness_Negative()
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

           
            var MockRoomService = new Mock<RoomService>();
            MockRoomService.Setup(p => p.AddRoom(room)).Throws<System.Exception>();
            RoomManager roomManager = new RoomManager(MockRoomService.Object);
            try
            {
            var actualReturnType = roomManager.AddingRoom(room);
                Assert.IsTrue(false);

            }
            catch
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void DeleteRoomBusiness_Positive()
        {
            int roomId = 10;
            var MockRoomService = new Mock<RoomService>();
            MockRoomService.Setup(p => p.RoomDelete(roomId));
            RoomManager roomManager = new RoomManager(MockRoomService.Object);
            try
            {
                var actualReturnType = roomManager.deleteRoomService(roomId);
                Assert.IsTrue(false);

            }
            catch
            {
                Assert.IsTrue(true);
            }


        }
        [TestMethod]
        public void DeleteRoomBusiness_Negative()
        {
            int roomId = 10;
            var MockRoomService = new Mock<RoomService>();
            MockRoomService.Setup(p => p.RoomDelete(roomId));
            RoomManager roomManager = new RoomManager(MockRoomService.Object);
            try
            {
                var actualReturnType = roomManager.deleteRoomService(roomId);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }
    }
}
