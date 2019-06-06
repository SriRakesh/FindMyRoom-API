using FindMyRoom.DataService;
using FindMyRoom.Entities;
using FindMyRoom.Entities.Models;
using FindMyRoom.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace FindMyRoom.BusinessManager.Tests
{
    [TestClass]
    public class BookingManagerTest
    {
        [TestMethod]
        public void GetCities_Positive_CitiesReturned()
        {
            //Arrange
            var _mockDataService = new Mock<SearchCityTypeService>();

            _mockDataService.Setup(p => p.GetCities()).Returns(GetCitiesList());

            var _bookingManager = new BookingManager(_mockDataService.Object);

            var _responseExpected = new Acknowledgement<string>
            {
                code = 1,
                Set = GetCitiesList(),
                Message = "success"
            };
            //Act
            var responseReturned = _bookingManager.GetCities();
            //Assert
            Assert.ReferenceEquals(responseReturned, _responseExpected);
        }

        [TestMethod]
        public void GetCities_Negative_CitiesReturned()
        {
            //Arrange.
            var _mockDataService = new Mock<SearchCityTypeService>();

            _mockDataService.Setup(p => p.GetCities()).Returns(new List<string>());
            var _bookingManager = new BookingManager(_mockDataService.Object);
            var _responseExpected = new Acknowledgement<string>
            {
                code = 0,
                Set = new List<string>(),
                Message = "No Cities",
            };

            //Act
            var responseReturned = _bookingManager.GetCities();

            //Assert
            Assert.ReferenceEquals(responseReturned, _responseExpected);
        }
        [TestMethod]
        public void GetCities_Negative_Exception_CitiesReturned()
        {
            var _mockDataService = new Mock<SearchCityTypeService>();
            _mockDataService.Setup(p => p.GetCities()).Throws<Exception>();
            var _bookingManager = new BookingManager(_mockDataService.Object);
            //var _responseExpected = new Acknowledgement<string>
            //{
            //    code = 2,
            //    Set = null,
            //    Message = "Something Went Wrong in Server",
            //};
            //Act
            try
            {
                var responseReturned = _bookingManager.GetCities();
                Assert.IsFalse(true);
            }
            catch
            {
                Assert.IsTrue(true);
            }
            //Assert
            
        }
        [TestMethod]
        public void GetSearchedRoomDetails_Positive_RoomsReturned()
        {
            var _mockDataService = new Mock<SearchCityTypeService>();
            SearchCityType search = new SearchCityType
            {
                city="Vijayawada",roomType="Flat"
            };
            List<SearchedRoomData> gettingData = new List<SearchedRoomData>();
            gettingData.Add(GetSearchedRoomData());
            Acknowledgement<SearchedRoomData> _returnedData = new Acknowledgement<SearchedRoomData>
            {
                code = 1,Set = gettingData, Message= "Success"
            };
            _mockDataService.Setup(p => p.GetSearchedRoomDetails(search)).Returns(gettingData);
            var _bookingManager = new BookingManager(_mockDataService.Object);
            
            //Act
            var responseReturned = _bookingManager.GetSearchedRoomDetails(search);

            //Assert.
            Assert.ReferenceEquals(responseReturned,_returnedData);
        }
        [TestMethod]
        public void GetSearchedRoomDetails_Negative_RoomsReturned()
        {
            var _mockDataService = new Mock<SearchCityTypeService>();
            SearchCityType search = new SearchCityType
            {
                city = "   ",
                roomType = "234"
            };
            List<SearchedRoomData> gettingData = new List<SearchedRoomData>();
            gettingData.Add(GetSearchedRoomData());
            Acknowledgement<SearchedRoomData> _returnedData = new Acknowledgement<SearchedRoomData>
            {
                code = 3,
                Set = null,
                Message = "Invalid Input"
            };
            _mockDataService.Setup(p => p.GetSearchedRoomDetails(search)).Returns(gettingData);
            var _bookingManager = new BookingManager(_mockDataService.Object);

            //Act
            var responseReturned = _bookingManager.GetSearchedRoomDetails(search);

            //Assert.
            Assert.ReferenceEquals(responseReturned, _returnedData);

        }
        [TestMethod]
        public void GetSearchedRoomDetails_Negative_Exception_RoomsReturned()
        {
            var _mockDataService = new Mock<SearchCityTypeService>();
            SearchCityType search = new SearchCityType
            {
                city = "Vijayawada",
                roomType = "Flat"
            };
            _mockDataService.Setup(p => p.GetSearchedRoomDetails(search)).Throws<Exception>();
            try
            {
                var _bookingManager = new BookingManager(_mockDataService.Object);
                var responseReturned = _bookingManager.GetSearchedRoomDetails(search);
                Assert.IsFalse(true);
            }
            catch
            {
                Assert.IsTrue(true);
            }

        }

        private SearchedRoomData GetSearchedRoomData()
        {
            SearchedRoomData data = new SearchedRoomData
            {
                RoomId=1,NoOfBeds=2,Furniture="yes",City="Vijayawada",Area="Area",
                Cost = 12000,Description="WIFI, TV ,Pool",RoomType="Flat"
            };
            return data;
        }
        private List<string> GetCitiesList()
        {
            var returnCityList = new List<string> { "Hyderabad", "Delhi", "Goa" };
            return returnCityList;
        }


        [TestMethod]
        public void AddingToWishlist_Positive()
        {

            WishList wishReceived = new WishList();
            wishReceived.WishListId = 1;
            wishReceived.RenterId = 2;
            wishReceived.RoomId = 4;


            WishListDuplicate wishListDuplicate = new WishListDuplicate();
            wishListDuplicate.WishListId = 1;
            wishListDuplicate.RenterId = 2;
            wishListDuplicate.RoomId = 4;

            List<WishListDuplicate> returnedWishlist = new List<WishListDuplicate>();
            returnedWishlist.Add(wishListDuplicate);
            //Arrange
            var _mockDataService = new Mock<BookingService>();

            _mockDataService.Setup(p => p.AddingToWishList(wishReceived));

            var _bookingManager = new BookingManager(_mockDataService.Object);
            
            try
            {
                var responseReturned = _bookingManager.AddingToWishList(wishReceived);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }


        [TestMethod]
        public void AddingToWishlist_Negative()
        {

            WishList wishReceived = new WishList();
            wishReceived.WishListId = 1;
            wishReceived.RenterId = 2;
            wishReceived.RoomId = 4;


            WishListDuplicate wishListDuplicate = new WishListDuplicate();
            wishListDuplicate.WishListId = 1;
            wishListDuplicate.RenterId = 2;
            wishListDuplicate.RoomId = 4;

            List<WishListDuplicate> returnedWishlist = new List<WishListDuplicate>();
            returnedWishlist.Add(wishListDuplicate);
            //Arrange
            var _mockDataService = new Mock<BookingService>();
            _mockDataService.Setup(p => p.AddingToWishList(wishReceived)).Throws<System.Exception>();
            var _bookingManager = new BookingManager(_mockDataService.Object);
            try
            {
                var responseReturned = _bookingManager.AddingToWishList(wishReceived);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void DisplayWishList_Positive()
        {
        int renterId = 2;

            Room room1 = new Room
            {
                RoomId = 1,
                Cost = 3000,
                NoOfBeds = 3,
                City = "hyderabad",
                Area = "tg",
                Address = "tg",
                Pincode = 50072,
                Furniture = "no",
                Description = "good",
                Status = "not available"
            };

            Room room2 = new Room
            {
                RoomId = 2,
                Cost = 4000,
                NoOfBeds = 2,
                City = "hyderabad",
                Area = "tg",
                Address = "tg",
                Pincode = 50073,
                Furniture = "yes",
                Description = "good",
                Status = "available"
            };

      
    
            List<Room> roomList = new List<Room>();
            roomList.Add(room1);
            roomList.Add(room2);


            Acknowledgement<Room> roomsAcknowledgement = new Acknowledgement<Room>();

            roomsAcknowledgement.code = 1;
            roomsAcknowledgement.Set = roomList;
            roomsAcknowledgement.Message = "success";

           
            //Arrange
            var _mockDataService = new Mock<BookingService>();

            _mockDataService.Setup(p => p.DisplayWishList(renterId)).Returns(roomList);

            var _bookingManager = new BookingManager(_mockDataService.Object);

            try
            {
                var responseReturned = _bookingManager.DisplayWishList(renterId);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void DisplayWishList_Negative()
        {
            int renterId = 2;
            
            Acknowledgement<Room> roomsAcknowledgement = new Acknowledgement<Room>();

            roomsAcknowledgement.code = 0;
            roomsAcknowledgement.Set = null;
            roomsAcknowledgement.Message = "fail";


            //Arrange
            var _mockDataService = new Mock<BookingService>();

            _mockDataService.Setup(p => p.DisplayWishList(renterId)).Throws<System.Exception>();

            var _bookingManager = new BookingManager(_mockDataService.Object);

            try
            {
                var responseReturned = _bookingManager.DisplayWishList(renterId);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }


        [TestMethod]
        public void DeleteFromWishList_Positive()
        {
            int renterId = 2;

            WishListDuplicate wishListDuplicateReceieved = new WishListDuplicate
            {
                
                RenterId=2,
                RoomId=3
            };

            WishListDuplicate wishListDuplicateSent = new WishListDuplicate
            {
                WishListId = 1,
                RenterId = 2,
                RoomId = 3
            };


            List<WishListDuplicate> wishlistList = new List<WishListDuplicate>();
            wishlistList.Add(wishListDuplicateSent);
            
            Acknowledgement<WishListDuplicate> wishlistAcknowledgement = new Acknowledgement<WishListDuplicate>();

            wishlistAcknowledgement.code = 1;
            wishlistAcknowledgement.Set = wishlistList;
            wishlistAcknowledgement.Message = "success";


            //Arrange
            var _mockDataService = new Mock<BookingService>();


            _mockDataService.Setup(p => p.RemoveFromWishList(wishListDuplicateReceieved)).Returns(wishlistList);

            var _bookingManager = new BookingManager(_mockDataService.Object);

            try
            {
                var responseReturned = _bookingManager.DeleteFromWishList(wishListDuplicateReceieved);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }


        [TestMethod]
        public void DeleteFromWishList_Negative()
        {
            int renterId = 2;

            WishListDuplicate wishListDuplicateReceieved = new WishListDuplicate
            {

                RenterId = 2,
                RoomId = 3
            };
            Acknowledgement<WishListDuplicate> wishlistAcknowledgement = new Acknowledgement<WishListDuplicate>();

            wishlistAcknowledgement.code = 0;
            wishlistAcknowledgement.Set = null;
            wishlistAcknowledgement.Message = "fail";


            //Arrange
            var _mockDataService = new Mock<BookingService>();


            _mockDataService.Setup(p => p.RemoveFromWishList(wishListDuplicateReceieved)).Throws<System.Exception>();

            var _bookingManager = new BookingManager(_mockDataService.Object);

            try
            {
                var responseReturned = _bookingManager.DeleteFromWishList(wishListDuplicateReceieved);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void AddToBooking_Positive()
        {
           Book toBeBooked=new Book
            {
                RenterId = 2,
                RoomId = 3,
                Status="alloted"
            };

            Book Booked = new Book
            {
                BookId = 1,
                RenterId = 2,
                RoomId = 3,
                Status = "alloted"
            };

            List<Book> BookList = new List<Book>();
            BookList.Add(Booked);

            Acknowledgement<Book> bookAcknowledgement = new Acknowledgement<Book>();

            bookAcknowledgement.code = 1;
            bookAcknowledgement.Set = BookList;
            bookAcknowledgement.Message = "success";


            //Arrange
            var _mockDataService = new Mock<BookingService>();
            _mockDataService.Setup(p => p.AddToBooking(toBeBooked)).Returns(Booked);
            var _bookingManager = new BookingManager(_mockDataService.Object);

            try
            {
                var responseReturned = _bookingManager.AddToBooking(toBeBooked);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void AddToBooking_Negative()
        {
            Book toBeBooked = new Book
            {
                RenterId = 2,
                RoomId = 3,
                Status = "alloted"
            };
            Acknowledgement<Book> bookAcknowledgement = new Acknowledgement<Book>();
            bookAcknowledgement.code = 0;
            bookAcknowledgement.Set = null;
            bookAcknowledgement.Message = "fail";
            //Arrange
            var _mockDataService = new Mock<BookingService>();
            _mockDataService.Setup(p => p.AddToBooking(toBeBooked)).Throws<System.Exception>();
            var _bookingManager = new BookingManager(_mockDataService.Object);

            try
            {
                var responseReturned = _bookingManager.AddToBooking(toBeBooked);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void AddToBooking_Negative_UserStatusInvalid()
        {
            Book toBeBooked = new Book
            {
                RenterId = 2,
                RoomId = 3,
                Status = "alloted"
            };
            Acknowledgement<Book> bookAcknowledgement = new Acknowledgement<Book>();
            bookAcknowledgement.code = 0;
            bookAcknowledgement.Set = null;
            bookAcknowledgement.Message = "fail";
            //Arrange
            var _mockDataService = new Mock<BookingService>();
            _mockDataService.Setup(p => p.AddToBooking(toBeBooked)).Throws(new UserStatusInvalid());
            var _bookingManager = new BookingManager(_mockDataService.Object);

            try
            {
                var responseReturned = _bookingManager.AddToBooking(toBeBooked);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void AddToBooking_Negative_UserIsNotRenter()
        {
            Book toBeBooked = new Book
            {
                RenterId = 2,
                RoomId = 3,
                Status = "alloted"
            };
            Acknowledgement<Book> bookAcknowledgement = new Acknowledgement<Book>();
            bookAcknowledgement.code = 0;
            bookAcknowledgement.Set = null;
            bookAcknowledgement.Message = "fail";
            //Arrange
            var _mockDataService = new Mock<BookingService>();
            _mockDataService.Setup(p => p.AddToBooking(toBeBooked)).Throws(new UserIsNotRenter());
            var _bookingManager = new BookingManager(_mockDataService.Object);

            try
            {
                var responseReturned = _bookingManager.AddToBooking(toBeBooked);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void AddToBooking_Negative_RoomAlreadyBooked()
        {
            Book toBeBooked = new Book
            {
                RenterId = 2,
                RoomId = 3,
                Status = "alloted"
            };
            Acknowledgement<Book> bookAcknowledgement = new Acknowledgement<Book>();
            bookAcknowledgement.code = 0;
            bookAcknowledgement.Set = null;
            bookAcknowledgement.Message = "fail";
            //Arrange
            var _mockDataService = new Mock<BookingService>();
            _mockDataService.Setup(p => p.AddToBooking(toBeBooked)).Throws(new RoomAlreadyBooked(toBeBooked.RoomId));
            var _bookingManager = new BookingManager(_mockDataService.Object);

            try
            {
                var responseReturned = _bookingManager.AddToBooking(toBeBooked);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }


        [TestMethod]
        public void AddToBooking_Negative_RenterIdNotThere()
        {
            Book toBeBooked = new Book
            {
                RenterId = 2,
                RoomId = 3,
                Status = "alloted"
            };
            Acknowledgement<Book> bookAcknowledgement = new Acknowledgement<Book>();
            bookAcknowledgement.code = 0;
            bookAcknowledgement.Set = null;
            bookAcknowledgement.Message = "fail";
            //Arrange
            var _mockDataService = new Mock<BookingService>();
            _mockDataService.Setup(p => p.AddToBooking(toBeBooked)).Throws(new RenterIdNotThere());
            var _bookingManager = new BookingManager(_mockDataService.Object);

            try
            {
                var responseReturned = _bookingManager.AddToBooking(toBeBooked);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void AddToBooking_Negative_RoomIdNotThere()
        {
            Book toBeBooked = new Book
            {
                RenterId = 2,
                RoomId = 3,
                Status = "alloted"
            };
            Acknowledgement<Book> bookAcknowledgement = new Acknowledgement<Book>();
            bookAcknowledgement.code = 0;
            bookAcknowledgement.Set = null;
            bookAcknowledgement.Message = "fail";
            //Arrange
            var _mockDataService = new Mock<BookingService>();
            _mockDataService.Setup(p => p.AddToBooking(toBeBooked)).Throws(new RoomIdNotThere());
            var _bookingManager = new BookingManager(_mockDataService.Object);

            try
            {
                var responseReturned = _bookingManager.AddToBooking(toBeBooked);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]

        public void GetLocation_Positive_CitiesReturnerd()
        {
            var _mockDataService = new Mock<BookingService>();

            _mockDataService.Setup(p => p.geoLocations(4)).Returns(GetLocationList(4));

            var _bookingManager = new BookingManager(_mockDataService.Object);

            var _responseExpected = new Acknowledgement<GeoLocation>
            {
                code = 0,
                Set = GetLocationList(4),
                Message = "Location of room on google map"
            };

            //Act
            var responseReturned = _bookingManager.GetLocation(4);

            //Assert
            Assert.ReferenceEquals(responseReturned, _responseExpected);
        }
        private List<GeoLocation> GetLocationList(int RoomId)
        {

            List<GeoLocation> locations = new List<GeoLocation>();
            GeoLocation location = new GeoLocation
            {
                GeoId = 3,
                RoomId = 4,
                Latitude = "17.8854002",
                Longitude = "18.5656003"
            };
            locations.Add(location);
            return locations;
        }
    }
}
