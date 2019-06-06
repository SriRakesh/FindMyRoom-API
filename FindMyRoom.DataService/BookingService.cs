using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using FindMyRoom.Entities.Models;
using System.Linq;
using FindMyRoom.Entities;
using FindMyRoom.Exceptions;

namespace FindMyRoom.DataService
{
   public class BookingService
    {
        public BookingService()
        {
        }

        FindMyRoomDb fmrDb = new FindMyRoomDb();

        public virtual List<GeoLocation> geoLocations(int RoomId)
        {
         //   FindMyRoomDb roomDb = new FindMyRoomDb();
            try
            {
                return (from g in fmrDb.FMRGeolocation where g.RoomId.Equals(RoomId) select g).ToList();
            }
            catch(Exception e)
            {
                throw e;
            }
        }



        public virtual void AddingToWishList(WishList wishList)
        {
            try
            {
              //  FindMyRoomDb fmrDb = new FindMyRoomDb();
                fmrDb.FMRWishLists.Add(wishList);
                fmrDb.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual List<Room> DisplayWishList(int renterIdReceived)
        {
            List<Room> WishListRooms;
            try
            {
             //   FindMyRoomDb fmrDb = new FindMyRoomDb();


                //            select* from FMRRooms r, FMRWishLists W
                //WHERE W.RenterId = 1 and w.RoomId = r.RoomId

                WishListRooms = (from r in fmrDb.FMRRooms
                                 from w in fmrDb.FMRWishLists
                                 where (w.RenterId == renterIdReceived) && (w.RoomId == r.RoomId)
                                 select r).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return WishListRooms;
        }


        public  List<int> RenterIdList()
        {
           // FindMyRoomDb fmrDb = new FindMyRoomDb();
            List<int> renterIdsPresent;
            
            renterIdsPresent = (from users in fmrDb.FMRUsers select users.UserId ).ToList();

            return renterIdsPresent;
        }

        public  List<int> RoomIdList()
        {
         //   FindMyRoomDb fmrDb = new FindMyRoomDb();
            List<int> roomIdsPresent;

            roomIdsPresent = (from rooms in fmrDb.FMRRooms select rooms.RoomId).ToList();

            return roomIdsPresent;
        }
        
        //   public static void RemoveFromWishList(int renterIdReceived, int roomIdReceived)
        public virtual List<WishListDuplicate> RemoveFromWishList(WishListDuplicate wishListDuplicateReceived)
        {
            //  List<Room> WishListRooms;

            int wishListId;
         //   FindMyRoomDb fmrDb = new FindMyRoomDb();
            WishList wishListToBeDeleted = new WishList();
            WishListDuplicate wishListDuplicate;
            List<WishListDuplicate> wishListDuplicateList=new List<WishListDuplicate>();
            try
            {
                //            select* from FMRRooms r, FMRWishLists W
                //WHERE W.RenterId = 1 and w.RoomId = r.RoomId

                wishListId = (from r in fmrDb.FMRWishLists
                              where (r.RenterId == wishListDuplicateReceived.RenterId) && (r.RoomId == wishListDuplicateReceived.RoomId)
                              select r.WishListId).FirstOrDefault();
                if((wishListId==null)|| (wishListId == 0))
                {
                    throw new NotThereInWishList(wishListDuplicateReceived.RoomId,wishListDuplicateReceived.RenterId);
                }
                wishListToBeDeleted = fmrDb.FMRWishLists.Find(wishListId);
                fmrDb.FMRWishLists.Remove(wishListToBeDeleted);
                fmrDb.SaveChanges();
                wishListDuplicate = new WishListDuplicate(wishListToBeDeleted.WishListId, wishListToBeDeleted.RenterId, wishListToBeDeleted.RoomId);
                wishListDuplicateList.Add(wishListDuplicate);
            }

            catch(NotThereInWishList notThere)
            {
                throw notThere;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return wishListDuplicateList;
        }


        public virtual Book AddToBooking(Book toBeBooked)
        {
         //   FindMyRoomDb fmrDb = new FindMyRoomDb();
            Room room = new Room();
        //    bool isRoomAvailable = false;
            try
            {
                room = fmrDb.FMRRooms.Find(toBeBooked.RoomId);
                if (room.Status == "unavailable")
                {
                    throw new RoomAlreadyBooked(room.RoomId);
                }
                fmrDb.FMRBookings.Add(toBeBooked);
                room.Status = "unavailable";
                fmrDb.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return toBeBooked;
        }

        public virtual User GetOwnerOfTheRoom(int roomNo)
        {
            //   FindMyRoomDb fmrDb = new FindMyRoomDb();
         
            int partnerId;
         //   string PartnerMailAddress;
            User ownerOfTheRoom = new User();
            //    bool isRoomAvailable = false;
            try
            {
                partnerId = (from eachRow in fmrDb.FMROwners
                            where eachRow.RoomId== roomNo
                            select eachRow.PartnerId).First();

                ownerOfTheRoom = (from owner in fmrDb.FMRUsers
                                     where owner.UserId == partnerId
                                     select owner).First();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ownerOfTheRoom;
        }

        public virtual Room GetRoomToCheck (Book toBeBooked)
        {
          //  FindMyRoomDb fmrDb = new FindMyRoomDb();
            Room roomToBeChecked = new Room();
            
            try
            {
                roomToBeChecked = fmrDb.FMRRooms.Find(toBeBooked.RoomId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return roomToBeChecked;
        }

        public virtual User GetUserToCheck(int userId)
        {
        //    FindMyRoomDb fmrDb = new FindMyRoomDb();
            User userToCheck = new User();
            try
            {
                userToCheck = fmrDb.FMRUsers.Find(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userToCheck;
        }
        
    }
}
