using FindMyRoom.Entities;
using FindMyRoom.Entities.Models;
using FindMyRoom.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace FindMyRoom.DataService
{
    public class AdminService
    {
        //use this db instance
        FindMyRoomDb roomDb = new FindMyRoomDb();
        FindMyRoomDb partnerDb = new FindMyRoomDb();
        //This method will get the partner data from database

        public List<UserModified> Getpartnerdata()
        {
            try
            {
                IList<User> lstUsers = roomDb.FMRUsers.ToList();
                List<UserModified> PartnerData = new List<UserModified>();
                foreach (var item in lstUsers)
                {
                    if (item.UserType == "Partner")
                    {
                        UserModified user = new UserModified();
                        user.UserId = item.UserId;
                        user.UserName = item.UserName;
                        user.UserEmail = item.UserEmail;
                        user.UserPhoneNumber = item.UserPhoneNumber;
                        user.UserStatus = item.UserStatus;
                        user.UserType = item.UserType;
                        user.UserAddress = item.UserAddress;
                        PartnerData.Add(user);
                    
                    }
                }


                return PartnerData;
            }
            catch (Exception ex)
            {
                throw new RetrievingDataException(ex.Message);
            }
        }
        //This method will update partner data 
         public List<UserModified> GetUserDetails()
         {
            IList<User> lstUsers = roomDb.FMRUsers.ToList();
            List<UserModified> UserData = new List<UserModified>();
            try
            {
                using (var context = new FindMyRoomDb())
                {
                    foreach (var item in lstUsers)
                    {
                        if (item.UserType == "Partner")
                        {
                            UserModified user = new UserModified();
                            user.UserId = item.UserId;
                            user.UserName = item.UserName;
                            user.UserEmail = item.UserEmail;
                            user.UserPhoneNumber = item.UserPhoneNumber;
                            user.UserAddress = item.UserAddress;
                            user.UserStatus = item.UserStatus;
                            user.UserAddress = item.UserAddress;
                            user.UserType = item.UserType;
                            UserData.Add(user);

                        }
                    }
                }
                return UserData;
            }
            catch (Exception ex)
            {
                throw new InsertingDataException(ex.Message);
            }
        }

        public User GetPartner(int id)
        {
           User user = (from u in roomDb.FMRUsers where u.UserId.Equals(id) select u).FirstOrDefault();
           return user;
        }
        public void UpdatePartner(UserModified user, int id)
        {
            try
            {
                //var toUpdate = (from users in roomDb.FMRUsers where user.UserId == id select users).FirstOrDefault();
                //toUpdate = user;

                //   (from p in roomDb.FMRUsers
                //where p.UserId == id
                //select p).ToList().ForEach(x => {
                //    x.UserStatus = user.UserStatus;
                //    x.UserPhoneNumber = user.UserPhoneNumber;
                //    });

                //roomDb.FMRUsers.Update(user);
                User updatePartner = (User)roomDb.FMRUsers.Find(id);
                if (updatePartner == null)
                {
                    return;
                }

                updatePartner.UserName = user.UserName;

                updatePartner.UserAddress = user.UserAddress;
                updatePartner.UserPhoneNumber = user.UserPhoneNumber;
                updatePartner.UserStatus = user.UserStatus;

                roomDb.SaveChanges();
                if (user.UserStatus.Equals("invalid"))
                {
                    List<int> list = new List<int>();
                    foreach (Owner own in roomDb.FMROwners)
                    {
                        if (own.PartnerId == user.UserId)
                        {
                            list.Add(own.RoomId);
                        }
                    }
                    foreach (Room room in roomDb.FMRRooms)
                    {
                        if (list.Contains(room.RoomId))
                        {
                            room.Status = "unavailable";
                        }
                    }
                    //roomDb.SaveChanges();
                }
                else if(user.UserStatus.Equals("valid"))
                {
                    List<int> listtomakeavailable = new List<int>();
                    foreach (Owner own in roomDb.FMROwners)
                    {
                        if (own.PartnerId == user.UserId)
                        {
                            listtomakeavailable.Add(own.RoomId);
                        }
                    }
                    List<int> listofbookedrooms = new List<int>();
                    foreach (Book book in roomDb.FMRBookings)
                    {
                        if (listtomakeavailable.Contains(book.RoomId))
                        {
                            listofbookedrooms.Add(book.RoomId);
                        }
                    }
                    foreach (Room room in roomDb.FMRRooms)
                    {
                        if (listtomakeavailable.Contains(room.RoomId) && !listofbookedrooms.Contains(room.RoomId))
                        {
                            room.Status = "Available";
                        }

                    }
                    //roomDb.SaveChanges();

                }
                 roomDb.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InsertingDataException(ex.Message);
            }

        }
        public virtual void PartnerDelete(int UserId)
        {
            try
            {
                var findPartnerId = partnerDb.FMRUsers.Find(UserId);
                List<User> list = new List<User>();
                if (findPartnerId == null)
                {
                    throw new Exception();
                }
                if (findPartnerId.UserStatus == "Partner")
                {
                    partnerDb.FMRUsers.Remove(findPartnerId);
                    partnerDb.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
