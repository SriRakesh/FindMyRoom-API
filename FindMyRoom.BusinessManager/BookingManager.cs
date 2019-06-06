using FindMyRoom.DataService;
using FindMyRoom.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using FindMyRoom.Entities.Models;
using System.Data.SqlClient;
using FindMyRoom.Exceptions;
using System.Text.RegularExpressions;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace FindMyRoom.BusinessManager
{
    public class BookingManager
    {
        public BookingManager()
        {

        }
        SearchCityTypeService service = new SearchCityTypeService();
        BookingService bookingService = new BookingService();
        public  BookingManager(SearchCityTypeService _serviceInstance)
        {
        }


        public BookingManager(BookingService _bookingServiceInstance)
        {
            this.bookingService = _bookingServiceInstance;
        }

        //Pass the searched details from Controller (i.e Angular home Search) to the 
        //DataAccessLayer -  SearchCityType Class.
        public virtual Acknowledgement<SearchedRoomData> GetSearchedRoomDetails(SearchCityType search)
        {
            Acknowledgement<SearchedRoomData> payload = new Acknowledgement<SearchedRoomData>();
            try
            {
                Regex matcher = new Regex(@"^[a-zA-Z]+$");
                if((!matcher.IsMatch(search.city.Trim())) 
                    || (!search.roomType.ToLower().Equals("flat")
                    && (!search.roomType.ToLower().Equals("pg"))))
                {
                    payload.code = 3;
                    payload.Set = null;
                    payload.Message = "Invalid Input";
                    return payload;
                }
                List<SearchedRoomData> roomDetails;

                roomDetails = service.GetSearchedRoomDetails(search);
                if(roomDetails.Count>0)
                {
                    payload.code = 1;
                    payload.Set = roomDetails;
                    payload.Message = "Success";
                }
                else if(roomDetails.Count==0)
                {
                    payload.code = 0;
                    payload.Set = roomDetails;
                    payload.Message = "No Records";
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
            return payload;
        }

        //Pass the dEtails of all the Cities that are in the Rooms Table.
        public virtual Acknowledgement<string> GetCities()
        {
            Acknowledgement<string> payload = new Acknowledgement<string>();
            try
            {
                List<string> list;
                //SearchCityTypeService service = new SearchCityTypeService();
                list = service.GetCities();
                if(list.Count>0)
                {
                    payload.code = 1;
                    payload.Set = list;
                    payload.Message = "Success";
                }
                else if(list.Count==0)
                {
                    payload.code = 0;
                    payload.Set = list;
                    payload.Message = "No Cities";
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
            return payload;
        }




        public virtual Acknowledgement<GeoLocation> GetLocation(int RoomId)
        {
            
            Acknowledgement<GeoLocation> payload = new Acknowledgement<GeoLocation>();
            try
            {
                if (bookingService.geoLocations(RoomId).Count > 0)
                {
                    payload.code = 0;
                    payload.Set = bookingService.geoLocations(RoomId);
                    payload.Message = "Location of room on google map";

                    return payload;
                }
                else
                {
                    payload.code = 1;
                    payload.Set = null;
                    payload.Message = "Sorry no records found";
                    return payload;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }




        public virtual Acknowledgement<WishListDuplicate> AddingToWishList(WishList wishListReceived)
        {
            WishListDuplicate wishListDuplicate;
            List<WishListDuplicate> WishlistDuplicateList = new List<WishListDuplicate>();
            Acknowledgement<WishListDuplicate> returnData;
           // BookingService bookingService = new BookingService();
            List<Room> roomsInWishList = new List<Room>();
            try
            {
                roomsInWishList= bookingService.DisplayWishList(wishListReceived.RenterId);
                foreach(var v in roomsInWishList)
                {
                    if (v.RoomId == wishListReceived.RoomId)
                        throw new AlreadyThereInWishList(wishListReceived.RoomId);
                }
                bookingService.AddingToWishList(wishListReceived);
                wishListDuplicate = new WishListDuplicate(wishListReceived.WishListId, wishListReceived.RenterId, wishListReceived.RoomId);
                WishlistDuplicateList.Add(wishListDuplicate);
                returnData = new Acknowledgement<WishListDuplicate>(1, WishlistDuplicateList, "Added to wishlist");
            }
            catch(AlreadyThereInWishList alreadyThere)
            {
                throw alreadyThere;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnData;
        }


        public virtual Acknowledgement<Room> DisplayWishList(int renterIdReceived)
        {
            List<Room> DisplayWishListRooms = new List<Room>();
            Acknowledgement<Room> returnData;
            string message;
            List<int> renterIdsPresent;
            bool isRenterPresent = false;
            try
            {
                DisplayWishListRooms = bookingService.DisplayWishList(renterIdReceived);
                if (DisplayWishListRooms.Count == 0)
                {
               renterIdsPresent = bookingService.RenterIdList();
                    foreach(int i in renterIdsPresent)
                    {
                        if (i == renterIdReceived)
                            isRenterPresent = true;
                    }
                    if(isRenterPresent==false)
                    {
                        returnData= new Acknowledgement<Room>(0, DisplayWishListRooms, "Sorry, No renter present with that Id");
                        return returnData;
                    }
                    
                   message = "Your wishlist is empty";
                }
                else
                    message = "Successfully retrieved wishlist";
                returnData = new Acknowledgement<Room>(1, DisplayWishListRooms, message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnData;
        }



        public virtual Acknowledgement<WishListDuplicate> DeleteFromWishList(WishListDuplicate wishListDuplicateReceived)
        {
            Acknowledgement<WishListDuplicate> returnData;
            List<WishListDuplicate> wishListDuplicateRooms = new List<WishListDuplicate>();
            try
            {
                // wishListDuplicateRooms = bookingService.RemoveFromWishList(wishListDuplicateReceived);
                wishListDuplicateRooms = bookingService.RemoveFromWishList(wishListDuplicateReceived);
                returnData = new Acknowledgement<WishListDuplicate>(1, wishListDuplicateRooms, "Removed from wishlist");
               }
            catch(NotThereInWishList notThere)
            {
                throw notThere;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnData;
        }


        
        public Acknowledgement<WishListDuplicate> WhenErrorOccurs(string exception, string type)
        {
            Acknowledgement<WishListDuplicate> returnAsFail = new Acknowledgement<WishListDuplicate>();
            if (type == "post")
                returnAsFail.AcknowledgementnAsFail(0, "Sorry, failed to add to wishlist. " + exception);
            else if (type == "get")
                returnAsFail.AcknowledgementnAsFail(0, "Sorry, can't fetch wishlist. " + exception);
            else if (type == "modelState")
                returnAsFail.AcknowledgementnAsFail(0, "Model state invalid");
            else if (type == "delete")
                returnAsFail.AcknowledgementnAsFail(0, "Failed to remove from wishlist. " + exception);
            return returnAsFail;
        }

        //filtered list.
        //public Acknowledgement<Room> GetFilteredData(SearchedRoomData data)
        //{
        //    Acknowledgement<Room> payload = new Acknowledgement<Room>(); 
        //    try
        //    {
        //        List<Room> list;
        //        SearchCityTypeService service = new SearchCityTypeService();
        //        list = service.GetFilteredRooms(data);
        //        if(list.Count>0)
        //        {
        //            payload.code = 1;
        //            payload.Set = list;
        //            payload.Message = "Success";
        //        }
        //        else if(list.Count==0)
        //        {
        //            payload.code = 0;
        //            payload.Set = list;
        //            payload.Message = "No records found";
        //        }
        //        return payload;

        //    }
        //    catch(Exception error)
        //    {
        //        throw error;
        //    }
        //}

        public virtual Acknowledgement<Book> AddToBooking(Book toBeBooked)
        {
            Acknowledgement<Book> returnBooked = new Acknowledgement<Book>();
            List<Book> bookingList = new List<Book>();
       //     bookingService bookingService = new bookingService();
            List<int> roomIds = new List<int>();
            List<int> userIds = new List<int>();
            bool isRoomIdPresent = false;
            bool isRenterIdPresent = false;
            User userToCheck = new User();
            Room roomToCheck = new Room();
            string renterConfirmationMessage;
            string partnerConfirmationMessage;
            string partnerEmailAddress;
            User ownerOfThisRoom = new User();
            try
            {
                roomIds = bookingService.RoomIdList();
                userIds = bookingService.RenterIdList();
                foreach(int i in roomIds)
                {
                    if (i == toBeBooked.RoomId)
                    {
                        isRoomIdPresent = true;
                        break;
                    }
                }
                foreach(int i in userIds)
                {
                    if (i == toBeBooked.RenterId)
                    { 
                        isRenterIdPresent = true;
                        break;
                    }
                }
                 if (isRenterIdPresent == false)
                    throw new RenterIdNotThere();

                userToCheck = bookingService.GetUserToCheck(toBeBooked.RenterId);
                
                if (!(userToCheck.UserType.ToLower().Equals("renter")))
                    throw new UserIsNotRenter();

                if (userToCheck.UserStatus.ToLower().Equals("invalid"))
                    throw new UserStatusInvalid();

                if (isRoomIdPresent==false)
                    throw new RoomIdNotThere();
                
                roomToCheck = bookingService.GetRoomToCheck(toBeBooked);

                if (roomToCheck.Status.ToLower().Equals("unavailable"))
                    throw new RoomAlreadyBooked(toBeBooked.RoomId);
                
                toBeBooked = bookingService.AddToBooking(toBeBooked);
                bookingList.Add(toBeBooked);
                
                returnBooked.code = 1;
                returnBooked.Set = bookingList;
                returnBooked.Message = "Successfully, booked the room. ";

                ownerOfThisRoom = bookingService.GetOwnerOfTheRoom(toBeBooked.RoomId);
                renterConfirmationMessage = ConfirmationMailForRenter(userToCheck,roomToCheck,ownerOfThisRoom);
                returnBooked.Message = returnBooked.Message + renterConfirmationMessage;

                partnerConfirmationMessage = ConfirmationMailForPartner(ownerOfThisRoom, roomToCheck, userToCheck);

                returnBooked.Message = returnBooked.Message + partnerConfirmationMessage;

            }
            catch(UserStatusInvalid userStatusInvalidEx)
            {
                throw userStatusInvalidEx;
            }
            catch (UserIsNotRenter userIsNotRenterEx)
            {
                throw userIsNotRenterEx;
            }
            catch (RoomAlreadyBooked roomAlreadyBookedEx)
            {
                throw roomAlreadyBookedEx;
            }
            catch (RenterIdNotThere renterIdNotThereException)
            {
                throw renterIdNotThereException;
            }
            catch(RoomIdNotThere roomIdNotThereException)
            {
                throw roomIdNotThereException;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
          //  partnerConfirmationMessage= ConfirmationMailForPartner()

            return returnBooked;
        }


        public string ConfirmationMailForRenter(User userToCheck,Room roomBooked,User ownerOfRoom)
        {
            try
            {
                if (userToCheck.UserEmail == null)
                {
                    return null;
                }
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Find My Room", "findmyroom.project123@gmail.com"));
                message.To.Add(new MailboxAddress("Renter", userToCheck.UserEmail));
               
                message.Subject = "Booking confirmation";
                message.Body = new TextPart("plain")
                {
                    Text = "Hi "+ userToCheck .UserName+ "\n\n\n\nCongrats, successfully booked the room.\n\n" +
                    "room details are: \n"+
                    "Txn Id: " + roomBooked.RoomId + "\nno of beds: " + roomBooked.NoOfBeds + "\ncity: " + roomBooked.City +
                    "\narea: " + roomBooked.Area + "\naddress: " + roomBooked.Address + "\npincode: " + roomBooked.Pincode +
                    "\nfurniture: " + roomBooked.Furniture + "\nroom description: " + roomBooked.Description +
                    "\nroom Type: " + roomBooked.RoomType + "\n\n" +
                    "Owner details are: \n" +
                    "name: " + ownerOfRoom.UserName + "\nphone no:" + ownerOfRoom.UserPhoneNumber + "\nmail:" + ownerOfRoom.UserEmail
                    + "\naddress:" + ownerOfRoom.UserAddress+
                    "\n\n\n\nThank You"

                };
                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("findmyroom.project123@gmail.com", "Findmyroom@123");
                    client.Send(message);
                    client.Disconnect(true);
                }
                return "Confirmation Mail sent successfully to renter. ";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ConfirmationMailForPartner(User ownerOfRoom,Room bookedRoom,User customer )
        {
            try
            {
                if (ownerOfRoom.UserEmail == null)
                {
                    return null;
                }
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Find My Room", "findmyroom.project123@gmail.com"));
                message.To.Add(new MailboxAddress("Owner", ownerOfRoom.UserEmail));

                message.Subject = "Booking confirmation";
                message.Body = new TextPart("plain")
                {
                    Text = "Hi " + ownerOfRoom.UserName + "\n\n\n\n Hello\n\n"+" Txn Id: " + bookedRoom.RoomId +"\ntype: "+bookedRoom.RoomType+"\ncity: "+bookedRoom.City
                    + "\n\n booked by : "+"\nName:"+customer.UserName+"\nphoneNo="+customer.UserPhoneNumber+"\nmail="+customer.UserEmail
                    +"\nAddress="+customer.UserAddress
                    +"\n\n\n\nThank You"
                };
                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("findmyroom.project123@gmail.com", "Findmyroom@123");
                    client.Send(message);
                    client.Disconnect(true);
                }
                return "Confirmation Mail sent successfully to Owner. ";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
