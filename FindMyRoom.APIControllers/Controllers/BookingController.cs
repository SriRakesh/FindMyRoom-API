using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindMyRoom.BusinessManager;
using FindMyRoom.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FindMyRoom.Entities.Models;
using System.Data.SqlClient;
using FindMyRoom.Exceptions;


namespace FindMyRoom.APIControllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
         BookingManager search =  new BookingManager();
        //public readonly BookingManager search;
        public BookingController(BookingManager bookingManager)
        {
            search = bookingManager;
        }

        //GET : api/Booking/Cities
        //Gives all distinct Cities.
        [Route("Cities")]
        [HttpGet]
        public IActionResult GetCities()
        {
            Acknowledgement<string> payload = new Acknowledgement<string>();
            try
            {
                //BookingManager search = new BookingManager();
                payload = search.GetCities();
            }
            catch (Exception)
            {
                payload.code = 2;
                payload.Set = null;
                payload.Message = "Something Went Wrong in Server";
            }
            return Ok(payload);
        }

        //POST : api/Booking
        //gives the searched roomDetails 
        [Route("SearchedRooms")]
        [HttpPost]
        public IActionResult SearchedRooms([FromBody] SearchCityType searchdata)
        {
            Acknowledgement<SearchedRoomData> payload = new Acknowledgement<SearchedRoomData>();
            try
            {
                //BookingManager searchManager = new BookingManager();
                payload = search.GetSearchedRoomDetails(searchdata);
            }
            catch (Exception exception)
            {
                payload.code = 2;
                payload.Set = null;
                payload.Message = exception.Message;
            }
            return Ok(payload);
        }



        //To get location of room
        [HttpGet]
        [Route("Getlocation/{RoomId}")]
        public IActionResult GetLocation(int RoomId)
        {
            Acknowledgement<GeoLocation> payload = new Acknowledgement<GeoLocation>();
            //BookingManager bookingManager = new BookingManager();
            try
            {
                payload = search.GetLocation(RoomId);
                return Ok(payload);
            }
            catch (Exception)
            {
                payload.code = 1;
                payload.Set = null;
                payload.Message = "Something went wrong please try again later";
                return Ok(payload);
            }
        }

        //POST: api/Booking/AddToWishList
        [HttpPost]
        [Route("AddToWishList")]
        //   public IActionResult PostWishList(WishList wishListReceived)
        public IActionResult PostWishList(WishListToReceive wishListReceived)
        {
            //BookingManager bookingManager = new BookingManager();

            WishList wishListToBeAdded = new WishList();

            wishListToBeAdded.RenterId = wishListReceived.RenterId;
            wishListToBeAdded.RoomId = wishListReceived.RoomId;

            Acknowledgement<WishListDuplicate> returnData = new Acknowledgement<WishListDuplicate>();
            //to avoid nulls that r present due to foreign key in model class "wishlist" we are using generic for duplicate wishlist.

            if (!ModelState.IsValid)
            {
                returnData = search.WhenErrorOccurs("", type: "modelState");  //this will call a method in bookingManager
                return Ok(returnData);
            }

            try
            {
                returnData = search.AddingToWishList(wishListToBeAdded);
            }
            catch (AlreadyThereInWishList alreadyThere)
            {

                returnData = search.WhenErrorOccurs(alreadyThere.Message, type: "post");  //this will call a overloaded method in bookingManager
                return Ok(returnData);
            }

            catch (SqlException)
            {

                returnData = search.WhenErrorOccurs("", type: "post");  //this will call a overloaded method in bookingManager
                return Ok(returnData);
            }
            catch (Exception)
            {
                returnData = search.WhenErrorOccurs("", type: "post");
                return Ok(returnData);
            }
            return Ok(returnData);
        }



        //GET: api/Booking/DisplayWishList/1 or 2 or 3(renterId i.e.,renter who is viewing his wishlist his id will come and we can display his wishlist ....
        [HttpGet]
        [Route("ShowWishList/{renterIdReceived}")]
        public IActionResult GetWishListByHisId(int renterIdReceived)
        {
            // List<Room> DisplayWishListRooms;
            Acknowledgement<Room> returnData = new Acknowledgement<Room>();
            Acknowledgement<WishListDuplicate> returnFailData = new Acknowledgement<WishListDuplicate>();//actually we don't use WishListDuplicate in this method but
            //as we are calling a method that is returning  "ReturningData<WishListDuplicate>" we r using this.

            //BookingManager bookingManager = new BookingManager();


            if (!ModelState.IsValid)
            {

                return Ok(returnFailData);
            }
            try
            {
                returnData = search.DisplayWishList(renterIdReceived);
            }
            catch (Exception)
            {
                returnFailData = search.WhenErrorOccurs("", type: "get");
                return Ok(returnFailData);
            }
            return Ok(returnData);
        }


        [HttpPost]
        [Route("RemoveFromWishList")]
        public IActionResult RemoveFromWishList(WishListToReceive wishListReceived)
        {
            //BookingManager bookingManager = new BookingManager();

            WishListDuplicate wishListToBeRemoved = new WishListDuplicate();
            wishListToBeRemoved.RoomId = wishListReceived.RoomId;
            wishListToBeRemoved.RenterId = wishListReceived.RenterId;

            Acknowledgement<WishListDuplicate> returnData = new Acknowledgement<WishListDuplicate>();
          
            if (!ModelState.IsValid)
            {
                returnData = search.WhenErrorOccurs("", type: "modelState");  
                return Ok(returnData);
            }

            try
            {
                // returnData = BookingManager.(wishListReceived);
                returnData = search.DeleteFromWishList(wishListToBeRemoved);
            }
            catch (NotThereInWishList notThereInWishList)
            {

                returnData = search.WhenErrorOccurs(notThereInWishList.Message, type: "delete");  //this will call a overloaded method in bookingManager
                return Ok(returnData);
            }
            catch (Exception)
            {
                returnData = search.WhenErrorOccurs("", type: "delete");
                return Ok(returnData);
            }
            return Ok(returnData);
        }


        //[Route("Filter")]
        //[HttpPost]
        //public IActionResult GetFilteredData(SearchedRoomData data)
        //{

        //    Acknowledgement<Room> payload = new Acknowledgement<Room>();
        //    try
        //    {
        //        BookingManager manager = new BookingManager();
        //        payload = manager.GetFilteredData(data);
        //    }
        //    catch(Exception error)
        //    {
        //        payload.code = 2;
        //        payload.Set = null;
        //        payload.Message = "Server Under Maintainence"+error.Message;
        //    }
        //    return Ok(payload);
        //}



        [HttpPost]
        [Route("BookThisRoom")]
        public IActionResult AddToBooking(BookingReceived toBeBookedRecieved)
        {
            Book toBeBooked = new Book();
            toBeBooked.RenterId = toBeBookedRecieved.RenterId;
            toBeBooked.RoomId = toBeBookedRecieved.RoomId;

            //BookingManager bookingManager = new BookingManager();
            string wrongMessage = "Sorry, something went wrong";
            Acknowledgement<Book> returnData = new Acknowledgement<Book>();
          
            if (!ModelState.IsValid)
            {
                returnData.code = 0;   
                returnData.Message = "model state invalid";
                return Ok(returnData);
            }

            try
            {
                returnData = search.AddToBooking(toBeBooked);
            }
            //renterIdNotThereException
            //    roomIdNotThereException

            catch (UserStatusInvalid userStatusInvalidEx)
            {
                returnData.code = 0;
                returnData.Message = userStatusInvalidEx.Message;
                return Ok(returnData);
            }
            catch (UserIsNotRenter userIsNotRenterEx)
            {
                returnData.code = 0;
                returnData.Message = userIsNotRenterEx.Message;
                return Ok(returnData);
            }
            catch (RoomIdNotThere roomIdNotThereEx)
            {
                returnData.code = 0;
                returnData.Message = roomIdNotThereEx.Message;
                return Ok(returnData);
            }
            catch (RenterIdNotThere renterIdNotThereEx)
            {
                returnData.code = 0;
                returnData.Message = renterIdNotThereEx.Message;
                return Ok(returnData);
            }
            catch(RoomAlreadyBooked roomAlreadyBookedEx)
            {
                returnData.code = 0;
                returnData.Message = roomAlreadyBookedEx.Message;
                return Ok(returnData);
            }
            catch (Exception)
            {
                returnData.code = 0;
                returnData.Message = wrongMessage;
                return Ok(returnData);
            }
            
            return Ok(returnData);
        }
    }
}
