using FindMyRoom.APIControllers.Controllers;
using FindMyRoom.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using FindMyRoom.BusinessManager;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Results;
using System;
//using FindMyRoom.Entities;
using FindMyRoom.Entities.Models;
using FindMyRoom.Exceptions;

namespace FindMyRoom.APIControllers.Tests
{
    [TestClass]
    public class BookingControllerTest
    {
        [TestMethod]
        public void GetCities_PositiveTestCases_TestResults()
        {
            //Assign
            var _mockBusinessMethod = new Mock<BookingManager>();
            var city_List = GetCities_Mock_Positive();
            _mockBusinessMethod.Setup(p => p.GetCities()).Returns(city_List);
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);
            //Act
            var _returnedCitiesResp = _bookingController.GetCities();
            var _returnedCitiesAck = _returnedCitiesResp as OkObjectResult;
            var _originalCitiesAck = GetCities_Mock_Positive();
            //Assert
            //Assert.IsNotNull(_returnedCitiesAck);
            Assert.ReferenceEquals(_returnedCitiesAck.Value,_originalCitiesAck);
        }

        [TestMethod]
        public void GetCities_NegativeTestCases_TestResult()
        {
            //Assign 
            var _mockBusinessMethod = new Mock<BookingManager>();
            var city_List = GetCities_Mock_Negative();
            //    _mockBusinessMethod.Setup(p => p.GetCities()).Throws<System.IO.IOException>();
            _mockBusinessMethod.Setup(p => p.GetCities()).Throws<System.Exception>();
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);

            //Act
            var _returnedCitiesResp = _bookingController.GetCities();
            var _returnedCitiesAck = _returnedCitiesResp as OkObjectResult;
            var _originalCitiesAck = GetCities_Mock_Negative();
            //Assert
            Assert.ReferenceEquals(_returnedCitiesAck.Value, _originalCitiesAck);
        }
        [TestMethod]
        public void SearchedRooms_PositiveTestCases_InvalidInput()
        {
            //Assign
            var searchCityType = new SearchCityType()
            {
                city = "Delhi",
                roomType =  "flat"
            };
            var _mockBusinessMethod = new Mock<BookingManager>();
            var _returnAckType = new Acknowledgement<SearchedRoomData>
            {
                code = 1,
                Set = null,
                Message = "success"
            };

            _mockBusinessMethod.Setup(p => p.GetSearchedRoomDetails(searchCityType)).Returns(_returnAckType);
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);
            //Act
            var _actualReturnType = _bookingController.SearchedRooms(searchCityType) as OkObjectResult;

            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);
        }
        [TestMethod]
        public void SearchedRooms_NegativeTestCases_InvalidInput()
        {
            //Assign.
            var searchCityType = new SearchCityType()
            {
                city = "",
                roomType = ""
            };
            var _mockBusinessMethod = new Mock<BookingManager>();
            var _returnAckType = new Acknowledgement<SearchedRoomData>
            {
                code = 2,
                Set = null,
                Message = "Failure"
            };
            _mockBusinessMethod.Setup(p => p.GetSearchedRoomDetails(searchCityType)).Throws<Exception>();
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);
            //Act.
            var _actualReturnType = _bookingController.SearchedRooms(searchCityType) as OkObjectResult;
            Assert.ReferenceEquals(_returnAckType, _actualReturnType.Value);

        }
        
        private  Acknowledgement<string> GetCities_Mock_Positive()
        {
            var cities_ack = new Acknowledgement<string>
            {
                code = 1,
                Set = new List<string> { "Hyderabad", "Delhi", "Goa" },
                Message = "Success"
            };
            return cities_ack;
        }
        private Acknowledgement<string> GetCities_Mock_Negative()
        {
            var cities_ack = new Acknowledgement<string>
            {
                code = 2,
                Set = null,
                Message = "Something Went Wrong in Server"
            };
            return cities_ack;
        }


        [TestMethod]
        public void GetWishListByHisId_PositiveTestCases_TestResults()
        {
            int id=5;
            //Assign
            var _mockBusinessMethod = new Mock<BookingManager>();
            var Wish_List = GetWishList_Mock_Positive();
            _mockBusinessMethod.Setup(p => p.DisplayWishList(id)).Returns(Wish_List);
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);
            //Act
            var _returnedWishListRoomsResp = _bookingController.GetWishListByHisId(id);
            var _returnedWishListRoomsAck = _returnedWishListRoomsResp as OkObjectResult;
            var _originalWishListRoomsAck = GetWishList_Mock_Positive();
            //Assert
            Assert.IsNotNull(_returnedWishListRoomsAck);
            Assert.ReferenceEquals(_returnedWishListRoomsAck.Value, _originalWishListRoomsAck);
        }


        [TestMethod]
        public void GetWishListByHisId_NegativeTestCases_TestResult()
        {
            int id = 5;
            //Assign 
            var _mockBusinessMethod = new Mock<BookingManager>();
            var Wish_List = GetWishList_Mock_Negative();
            _mockBusinessMethod.Setup(p => p.DisplayWishList(id)).Throws<System.IO.IOException>();
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);
            //Act
            var _returnedWishListRoomsResp = _bookingController.GetWishListByHisId(id);
            var _returnedWishListRoomsAck = _returnedWishListRoomsResp as OkObjectResult;
            var _originalWishListRoomsAck = GetWishList_Mock_Negative();
            //Assert
            Assert.ReferenceEquals(_returnedWishListRoomsAck.Value, _originalWishListRoomsAck);
        }

        private Acknowledgement<Room> GetWishList_Mock_Positive()
        {
            var room = new Room(){
                RoomId=1,Cost=2000,NoOfBeds=3,City= "hyderabad",Area= "kphb",Address= "tg",
                Pincode =500072,Furniture= "no",Description= "good",Status= "available",
                RoomType = "flat"
            };
            List<Room> roomsList = new List<Room>();
            roomsList.Add(room);
            var wishListRooms_Ack = new Acknowledgement<Room>{
                code = 1,
               Set = roomsList,
                Message = "Success"
            };
            return wishListRooms_Ack;
        }


        private Acknowledgement<Room> GetWishList_Mock_Negative()
        {
            var wishListRooms_Ack = new Acknowledgement<Room>
            {
                code = 0,
                Set = null,
                Message = "failed"
            };
            return wishListRooms_Ack;
        }



        [TestMethod]
        public void PostWishList_PositiveTestCases_TestResults()
        {
            WishListToReceive wishListToReceive = new WishListToReceive();
            wishListToReceive.RenterId = 2;
            wishListToReceive.RoomId = 4;
            
            WishList wishReceived = new WishList();
            wishReceived.RenterId = 2;
            wishReceived.RoomId = 4;
            
            //Assign
            var _mockBusinessMethod = new Mock<BookingManager>();
            var Wish_List = AddingToWishList_Mock_Positive();
            _mockBusinessMethod.Setup(p => p.AddingToWishList(wishReceived)).Returns(Wish_List);
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);
            //Act
            var _returnedAddedWishList = _bookingController.PostWishList(wishListToReceive);
            var _returnedAddedWishListAck = _returnedAddedWishList as OkObjectResult;
            var _originalReturnedAddedWishListAck = AddingToWishList_Mock_Positive();
            //Assert
            Assert.IsNotNull(_returnedAddedWishListAck);
            Assert.ReferenceEquals(_returnedAddedWishListAck.Value, _originalReturnedAddedWishListAck);
        }


        private Acknowledgement<WishListDuplicate> AddingToWishList_Mock_Positive()
        {
            var wishListDuplicate = new WishListDuplicate()
            {
                WishListId = 4,
                RoomId = 4,
              RenterId=2
            };
            List<WishListDuplicate> wishList = new List<WishListDuplicate>();
            wishList.Add(wishListDuplicate);
            var wishList_Ack = new Acknowledgement<WishListDuplicate>
            {
                code = 1,
                Set = wishList,
                Message = "Success"
            };
            return wishList_Ack;
        }

        [TestMethod]
        public void PostWishList_NegativeTestCases_TestResults()
        {
            WishListToReceive wishListToReceive = new WishListToReceive();
            wishListToReceive.RenterId = 2;
            wishListToReceive.RoomId = 4;

            WishList wishReceived = new WishList();
            wishReceived.RenterId = 2;
            wishReceived.RoomId = 4;

            //Assign
            var _mockBusinessMethod = new Mock<BookingManager>();
            var Wish_List = AddingToWishList_Mock_Negative();
            _mockBusinessMethod.Setup(p => p.AddingToWishList(wishReceived)).Throws<System.IO.IOException>();
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);
            //Act
            var _returnedAddedWishList = _bookingController.PostWishList(wishListToReceive);
            var _returnedAddedWishListAck = _returnedAddedWishList as OkObjectResult;
            var _originalReturnedAddedWishListAck = AddingToWishList_Mock_Negative();
            //Assert
            Assert.IsNotNull(_returnedAddedWishListAck);
            Assert.ReferenceEquals(_returnedAddedWishListAck.Value, _originalReturnedAddedWishListAck);
        }


        private Acknowledgement<WishListDuplicate> AddingToWishList_Mock_Negative()
        {
            var wishList_Ack = new Acknowledgement<WishListDuplicate>
            {
                code = 0,
                Set = null,
                Message = "fail"
            };
            return wishList_Ack;
        }

        //RemoveFromWishList
        [TestMethod]
        public void RemoveFromWishList_NegativeTestCases_TestResults()
        {
            WishListToReceive wishListToReceive = new WishListToReceive();
            wishListToReceive.RenterId = 2;
            wishListToReceive.RoomId = 4;

            WishList wishReceived = new WishList();
            wishReceived.RenterId = 2;
            wishReceived.RoomId = 4;

            //Assign
            var _mockBusinessMethod = new Mock<BookingManager>();
            var Wish_List = RemoveFromWishList_Mock_Negative();
            _mockBusinessMethod.Setup(p => p.AddingToWishList(wishReceived)).Throws<System.IO.IOException>();
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);
            //Act
            var _returnedAddedWishList = _bookingController.PostWishList(wishListToReceive);
            var _returnedAddedWishListAck = _returnedAddedWishList as OkObjectResult;
            var _originalReturnedAddedWishListAck = RemoveFromWishList_Mock_Negative();
            //Assert
            Assert.IsNotNull(_returnedAddedWishListAck);
            Assert.ReferenceEquals(_returnedAddedWishListAck.Value, _originalReturnedAddedWishListAck);
        }

        private Acknowledgement<WishListDuplicate> RemoveFromWishList_Mock_Negative()
        {
            var wishList_Ack = new Acknowledgement<WishListDuplicate>
            {
                code = 0,
                Set = null,
                Message = "fail"
            };
            return wishList_Ack;
        }



        [TestMethod]
        public void RemoveFromWishList_PositiveTestCases_TestResults()
        {
            WishListToReceive wishListToReceive = new WishListToReceive();
            wishListToReceive.RenterId = 2;
            wishListToReceive.RoomId = 4;

            WishList wishReceived = new WishList();
            wishReceived.RenterId = 2;
            wishReceived.RoomId = 4;

            //Assign
            var _mockBusinessMethod = new Mock<BookingManager>();
            var Wish_List = RemoveFromWishList_Mock_Negative();
            _mockBusinessMethod.Setup(p => p.AddingToWishList(wishReceived)).Returns(Wish_List);
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);
            //Act
            var _returnedAddedWishList = _bookingController.PostWishList(wishListToReceive);
            var _returnedAddedWishListAck = _returnedAddedWishList as OkObjectResult;
            var _originalReturnedAddedWishListAck = RemoveFromWishList_Mock_Negative();
            //Assert
            Assert.IsNotNull(_returnedAddedWishListAck);
            Assert.ReferenceEquals(_returnedAddedWishListAck.Value, _originalReturnedAddedWishListAck);
        }

        private Acknowledgement<WishListDuplicate> RemoveFromWishList_Mock_Positive()
        {
            WishListDuplicate wishListDuplicate = new WishListDuplicate
            {
                WishListId=1,RenterId=2,RoomId=4
            };
            List<WishListDuplicate> wishList = new List<WishListDuplicate>();
            wishList.Add(wishListDuplicate);
            var wishList_Ack = new Acknowledgement<WishListDuplicate>
            {
                code = 1,
                Set = wishList,
                Message = "Success "
            };
            return wishList_Ack;
        }


        

        [TestMethod]
        public void RemoveFromWishList_NegativeTestCases_NotThereEx()
        {
            WishListToReceive wishListToReceive = new WishListToReceive();
            wishListToReceive.RenterId = 2;
            wishListToReceive.RoomId = 4;

            WishList wishReceived = new WishList();
            wishReceived.RenterId = 2;
            wishReceived.RoomId = 4;

            //Assign
            var _mockBusinessMethod = new Mock<BookingManager>();
            var Wish_List = RemoveFromWishList_Mock_Negative();
            _mockBusinessMethod.Setup(p => p.AddingToWishList(wishReceived)).Throws(new NotThereInWishList(wishReceived.RoomId,wishReceived.RenterId));
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);
            //Act
            var _returnedAddedWishList = _bookingController.PostWishList(wishListToReceive);
            var _returnedAddedWishListAck = _returnedAddedWishList as OkObjectResult;
            var _originalReturnedAddedWishListAck = RemoveFromWishList_Mock_Negative();
            //Assert
            Assert.IsNotNull(_returnedAddedWishListAck);
            Assert.ReferenceEquals(_returnedAddedWishListAck.Value, _originalReturnedAddedWishListAck);
        }

        [TestMethod]
        public void AddToBooking_PositiveTestCases_TestResults()
        {
            Book toBeBooked = new Book();
            toBeBooked.RenterId = 2;
            toBeBooked.RoomId = 3;

            Book booked = new Book();
            booked.BookId = 1;
            booked.RenterId = 2;
            booked.RoomId = 3;

            BookingReceived bookingReceived = new BookingReceived();
            bookingReceived.RenterId = 2;
            bookingReceived.RoomId = 3;

            Acknowledgement<Book> bookAcknowledgement = new Acknowledgement<Book>();
            List<Book> bookingList = new List<Book>();
            bookingList.Add(booked);

            bookAcknowledgement.code = 1;
            bookAcknowledgement.Set = bookingList;
            bookAcknowledgement.Message = "successfully booked the room";


            //Assign
            var _mockBusinessMethod = new Mock<BookingManager>();
          //  var Wish_List = RemoveFromWishList_Mock_Negative();
            _mockBusinessMethod.Setup(p => p.AddToBooking(toBeBooked)).Returns(bookAcknowledgement);
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);
            //Act
            var _returnedBooking = _bookingController.AddToBooking(bookingReceived);

            var _returnedBookingAck = _returnedBooking as OkObjectResult;
            var _originalReturnedBookingAck = bookAcknowledgement;
            //Assert
            Assert.IsNotNull(_returnedBookingAck);
            Assert.ReferenceEquals(_returnedBookingAck.Value, _originalReturnedBookingAck);
        }


        [TestMethod]
        public void AddToBooking_NegativeTestCases_Ex()
        {
            Book toBeBooked = new Book();
            toBeBooked.RenterId = 2;
            toBeBooked.RoomId = 3;

            Book booked = new Book();
            booked.BookId = 1;
            booked.RenterId = 2;
            booked.RoomId = 3;

            BookingReceived bookingReceived = new BookingReceived();
            bookingReceived.RenterId = 2;
            bookingReceived.RoomId = 3;

            Acknowledgement<Book> bookAcknowledgement = new Acknowledgement<Book>();
            

            bookAcknowledgement.code = 0;
            bookAcknowledgement.Set = null;
            bookAcknowledgement.Message = "failed to book";


            //Assign
            var _mockBusinessMethod = new Mock<BookingManager>();
            //  var Wish_List = RemoveFromWishList_Mock_Negative();
            _mockBusinessMethod.Setup(p => p.AddToBooking(toBeBooked)).Throws<System.Exception>();
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);
            //Act
            var _returnedBooking = _bookingController.AddToBooking(bookingReceived);

            var _returnedBookingAck = _returnedBooking as OkObjectResult;
            var _originalReturnedBookingAck = bookAcknowledgement;
            //Assert
            Assert.IsNotNull(_returnedBookingAck);
            Assert.ReferenceEquals(_returnedBookingAck.Value, _originalReturnedBookingAck);
        }

        [TestMethod]
        public void AddToBooking_NegativeTestCases_UserStatusInvalid()
        {
            Book toBeBooked = new Book();
            toBeBooked.RenterId = 2;
            toBeBooked.RoomId = 3;

            Book booked = new Book();
            booked.BookId = 1;
            booked.RenterId = 2;
            booked.RoomId = 3;

            BookingReceived bookingReceived = new BookingReceived();
            bookingReceived.RenterId = 2;
            bookingReceived.RoomId = 3;

            Acknowledgement<Book> bookAcknowledgement = new Acknowledgement<Book>();


            bookAcknowledgement.code = 0;
            bookAcknowledgement.Set = null;
            bookAcknowledgement.Message = "failed to book";


            //Assign
            var _mockBusinessMethod = new Mock<BookingManager>();
            //  var Wish_List = RemoveFromWishList_Mock_Negative();
            _mockBusinessMethod.Setup(p => p.AddToBooking(toBeBooked)).Throws(new UserStatusInvalid()); 
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);
            //Act
            var _returnedBooking = _bookingController.AddToBooking(bookingReceived);

            var _returnedBookingAck = _returnedBooking as OkObjectResult;
            var _originalReturnedBookingAck = bookAcknowledgement;
            //Assert
            Assert.IsNotNull(_returnedBookingAck);
            Assert.ReferenceEquals(_returnedBookingAck.Value, _originalReturnedBookingAck);
        }

        [TestMethod]
        public void AddToBooking_NegativeTestCases_UserIsNotRenter()
        {
            Book toBeBooked = new Book();
            toBeBooked.RenterId = 2;
            toBeBooked.RoomId = 3;

            Book booked = new Book();
            booked.BookId = 1;
            booked.RenterId = 2;
            booked.RoomId = 3;

            BookingReceived bookingReceived = new BookingReceived();
            bookingReceived.RenterId = 2;
            bookingReceived.RoomId = 3;

            Acknowledgement<Book> bookAcknowledgement = new Acknowledgement<Book>();


            bookAcknowledgement.code = 0;
            bookAcknowledgement.Set = null;
            bookAcknowledgement.Message = "failed to book";


            //Assign
            var _mockBusinessMethod = new Mock<BookingManager>();
            //  var Wish_List = RemoveFromWishList_Mock_Negative();
            _mockBusinessMethod.Setup(p => p.AddToBooking(toBeBooked)).Throws(new UserIsNotRenter());
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);
            //Act
            var _returnedBooking = _bookingController.AddToBooking(bookingReceived);
            var _returnedBookingAck = _returnedBooking as OkObjectResult;
            var _originalReturnedBookingAck = bookAcknowledgement;
            //Assert
            Assert.IsNotNull(_returnedBookingAck);
            Assert.ReferenceEquals(_returnedBookingAck.Value, _originalReturnedBookingAck);
        }

        [TestMethod]
        public void AddToBooking_NegativeTestCases_RoomAlreadyBooked()
        {
            Book toBeBooked = new Book();
            toBeBooked.RenterId = 2;
            toBeBooked.RoomId = 3;

            Book booked = new Book();
            booked.BookId = 1;
            booked.RenterId = 2;
            booked.RoomId = 3;

            BookingReceived bookingReceived = new BookingReceived();
            bookingReceived.RenterId = 2;
            bookingReceived.RoomId = 3;

            Acknowledgement<Book> bookAcknowledgement = new Acknowledgement<Book>();


            bookAcknowledgement.code = 0;
            bookAcknowledgement.Set = null;
            bookAcknowledgement.Message = "failed to book";


            //Assign
            var _mockBusinessMethod = new Mock<BookingManager>();
            //  var Wish_List = RemoveFromWishList_Mock_Negative();
            _mockBusinessMethod.Setup(p => p.AddToBooking(toBeBooked)).Throws(new RoomAlreadyBooked(toBeBooked.RoomId));
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);
            //Act
            var _returnedBooking = _bookingController.AddToBooking(bookingReceived);
            var _returnedBookingAck = _returnedBooking as OkObjectResult;
            var _originalReturnedBookingAck = bookAcknowledgement;
            //Assert
            Assert.IsNotNull(_returnedBookingAck);
            Assert.ReferenceEquals(_returnedBookingAck.Value, _originalReturnedBookingAck);
        }

        [TestMethod]
        public void AddToBooking_NegativeTestCases_RenterIdNotThere()
        {
            Book toBeBooked = new Book();
            toBeBooked.RenterId = 2;
            toBeBooked.RoomId = 3;

            Book booked = new Book();
            booked.BookId = 1;
            booked.RenterId = 2;
            booked.RoomId = 3;

            BookingReceived bookingReceived = new BookingReceived();
            bookingReceived.RenterId = 2;
            bookingReceived.RoomId = 3;

            Acknowledgement<Book> bookAcknowledgement = new Acknowledgement<Book>();


            bookAcknowledgement.code = 0;
            bookAcknowledgement.Set = null;
            bookAcknowledgement.Message = "failed to book";


            //Assign
            var _mockBusinessMethod = new Mock<BookingManager>();
            //  var Wish_List = RemoveFromWishList_Mock_Negative();
            _mockBusinessMethod.Setup(p => p.AddToBooking(toBeBooked)).Throws(new RenterIdNotThere());
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);
            //Act
            var _returnedBooking = _bookingController.AddToBooking(bookingReceived);
            var _returnedBookingAck = _returnedBooking as OkObjectResult;
            var _originalReturnedBookingAck = bookAcknowledgement;
            //Assert
            Assert.IsNotNull(_returnedBookingAck);
            Assert.ReferenceEquals(_returnedBookingAck.Value, _originalReturnedBookingAck);
        }


        [TestMethod]
        public void AddToBooking_NegativeTestCases_RoomIdNotThere()
        {
            Book toBeBooked = new Book();
            toBeBooked.RenterId = 2;
            toBeBooked.RoomId = 3;

            Book booked = new Book();
            booked.BookId = 1;
            booked.RenterId = 2;
            booked.RoomId = 3;

            BookingReceived bookingReceived = new BookingReceived();
            bookingReceived.RenterId = 2;
            bookingReceived.RoomId = 3;

            Acknowledgement<Book> bookAcknowledgement = new Acknowledgement<Book>();


            bookAcknowledgement.code = 0;
            bookAcknowledgement.Set = null;
            bookAcknowledgement.Message = "failed to book";


            //Assign
            var _mockBusinessMethod = new Mock<BookingManager>();
            //  var Wish_List = RemoveFromWishList_Mock_Negative();
            _mockBusinessMethod.Setup(p => p.AddToBooking(toBeBooked)).Throws(new RoomIdNotThere());
            BookingController _bookingController = new BookingController(_mockBusinessMethod.Object);
            //Act
            var _returnedBooking = _bookingController.AddToBooking(bookingReceived);
            var _returnedBookingAck = _returnedBooking as OkObjectResult;
            var _originalReturnedBookingAck = bookAcknowledgement;
            //Assert
            Assert.IsNotNull(_returnedBookingAck);
            Assert.ReferenceEquals(_returnedBookingAck.Value, _originalReturnedBookingAck);
        }
    }

}



