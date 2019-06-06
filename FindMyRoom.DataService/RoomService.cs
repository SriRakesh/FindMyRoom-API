using FindMyRoom.Entities;
using FindMyRoom.Entities.Models;
using FindMyRoom.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindMyRoom.DataService
{

    public class RoomService
    {
        FindMyRoomDb roomDb = new FindMyRoomDb();
        GeoLocation geoLocation = new GeoLocation();
        FindMyRoomDb findMyRoomDb = new FindMyRoomDb();





        public virtual int AddRoom(HelperAddRoom postedroom)
        {
            try
            {
                GeoLocation geoLocation = new GeoLocation();

                FindMyRoomDb roomDb = new FindMyRoomDb();
                Room room = new Room
                {
                    Cost = postedroom.RoomCost,
                    City = postedroom.city.Trim().ToUpper(),
                    Address = postedroom.Address.Trim(),
                    Area = postedroom.Area.Trim(),
                    Furniture = postedroom.Furniture,
                    Description = postedroom.Description,
                    NoOfBeds = postedroom.NumberOfRooms,
                    Pincode = postedroom.Pincode,
                    Status = "Available",
                    RoomType = postedroom.RoomType,
                };

                foreach (var item in roomDb.FMRRooms)
                {
                    if (item.Cost == room.Cost && item.City.Equals(room.City) && item.Address.Equals(room.Address) &&
                        item.Area.Equals(room.Area) && item.Furniture.Equals(room.Furniture) &&
                        item.NoOfBeds == room.NoOfBeds && item.Pincode == room.Pincode && item.RoomType.Equals(room.RoomType))
                    {
                        throw new RoomInsertingDataException("Room Details are already present");
                    }
                }

                roomDb.FMRRooms.Add(room);
                roomDb.SaveChanges();

                geoLocation.RoomId = (from r in roomDb.FMRRooms select r.RoomId).LastOrDefault();
                geoLocation.Latitude = postedroom.Latitude;
                geoLocation.Longitude = postedroom.Longitude;

                roomDb.FMRGeolocation.Add(geoLocation);
                roomDb.SaveChanges();

                var query= roomDb.FMRRooms.LastOrDefault();
                return query.RoomId;
            }
            catch (Exception e)
            {
                throw e;
            }

        }



        public void UpdateOwners(int RoomId,int PartnerId)
        {

            Owner owner = new Owner();
            owner.RoomId = RoomId;
            owner.PartnerId = PartnerId;
            roomDb.FMROwners.Add(owner);
            roomDb.SaveChanges();   
        }

        public virtual void  RoomDelete(int roomid)
        {

            try
            {
                FindMyRoomDb roomDb = new FindMyRoomDb();
                var findRoomId = roomDb.FMRRooms.Find(roomid);
                List<Room> list = new List<Room>();
                if (findRoomId == null)
                {
                    throw new Exception();
                }

                roomDb.FMRRooms.Remove(findRoomId);
                roomDb.SaveChanges();

            }

            catch (Exception ex)
            {
                throw ex;
            }



        }
        public void updateRoom(int id, Updateroom room)
        {

            try
            {
                //loading the database table for the current id
                var updatelocation = (from r in roomDb.FMRGeolocation where r.RoomId.Equals(id) select r).FirstOrDefault();
                //var updateRoom = (from v in roomDb.FMRRooms where v.RoomId.Equals(id) select v).FirstOrDefault();
                Room updateRoom = roomDb.FMRRooms.Where(x => x.RoomId == id).Select(x => x).FirstOrDefault<Room>();

                //var updateRoom = roomDb.FMRRooms.Find(id);
                if (updateRoom != null)
                {
                    updateRoom.Cost = room.Cost;
                    updateRoom.NoOfBeds = room.NoOfBeds;
                    updateRoom.City = room.City.Trim().ToUpper();
                    updateRoom.Area = room.Area.Trim();
                    updateRoom.Address = room.Address.Trim();
                    updateRoom.Pincode = room.Pincode;
                    updateRoom.Furniture = room.Furniture;
                    updateRoom.Description = room.Description;
                    updateRoom.Status = room.Status;
                    updateRoom.RoomType = room.RoomType;
                    updatelocation.Latitude = room.Latitude;
                    updatelocation.Longitude = room.Longitude;
                    roomDb.SaveChanges();
                    

                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }




        //DLL - List of Customers for a Particular Partner
        List<GetCustomerOfPartners> GetCustomerOfPartnerslist = new List<GetCustomerOfPartners>();
        public virtual List<GetCustomerOfPartners> GetCustomers(int UserId)
        {

            //Testing 
            try
            {
                //Db Context
                FindMyRoomDb roomDb = new FindMyRoomDb();
                //Converting tables to list
                List<User> user = roomDb.FMRUsers.ToList();
                List<Room> room = roomDb.FMRRooms.ToList();
                List<Owner> owner = roomDb.FMROwners.ToList();
                List<Book> book = roomDb.FMRBookings.ToList();
                //performing join 
                var details = (from owners in roomDb.FMROwners
                               join users in roomDb.FMRUsers on owners.PartnerId equals users.UserId
                               join booking in roomDb.FMRBookings on owners.RoomId equals booking.RoomId
                               join userss in roomDb.FMRUsers on booking.RenterId equals userss.UserId
                               join rooms in roomDb.FMRRooms on owners.RoomId equals rooms.RoomId                               
                               select new GetCustomerOfPartners()
                               {
                                   PartnerId=users.UserId,
                                   UserId = userss.UserId,
                                   RoomId = rooms.RoomId,
                                   RoomType = rooms.RoomType,
                                   UserName = userss.UserName,
                                   UserEmail = userss.UserEmail,
                                   UserPhoneNumber = userss.UserPhoneNumber,
                                   UserAddress = userss.UserAddress
                               }).Distinct();
                List<GetCustomerOfPartners> temp = new List<GetCustomerOfPartners>();
                temp = details.ToList();               
                foreach (var customers in details)
                {
                    if (UserId == customers.PartnerId)
                    {
                        GetCustomerOfPartnerslist.Add(customers);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return GetCustomerOfPartnerslist;
        }
        List<Room> searchedRoomDatas = new List<Room>();
        public List<Room> GetRooms(int UserId)
        {

            //Testing 
            try
            {
                //Db Context
                FindMyRoomDb roomDb = new FindMyRoomDb();
                //Converting tables to list
                List<Room> room = roomDb.FMRRooms.ToList();
                List<Owner> owner = roomDb.FMROwners.ToList();
                List<User> user = roomDb.FMRUsers.ToList();
                //var details = (from r in room where (from o in owner where o.PartnerId==UserId select o.RoomId).Contains(r.RoomId) select r).Distinct();
                //var details = (from r in room where (from o in owner where o.PartnerId.Equals(((from u in user where u.UserStatus.Equals("valid") select u.UserId).Equals(UserId))).Equals(o.PartnerId) select o.RoomId).Contains(r.RoomId) select r).Distinct();
                
                User userdata =new User();
                foreach(User u in user)
                {
                    if(u.UserId == UserId && u.UserStatus.ToLower().Equals("valid"))
                    {
                        userdata = u;
                    }
                }
                List<int> roomIds = new List<int>();
                foreach(Owner o in owner)
                {
                    if(o.PartnerId == userdata.UserId)
                    {
                        roomIds.Add(o.RoomId);
                    }
                }
                List<Room> roomsdata = new List<Room>();
                foreach(Room r in room)
                {
                    if(roomIds.Contains(r.RoomId))
                    {
                        roomsdata.Add(r);
                    }
                }

                searchedRoomDatas = roomsdata;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return searchedRoomDatas;
        }


        public List<Room> getAllRooms()
        {
            FindMyRoomDb roomDb = new FindMyRoomDb();
            List<Room> allRooms = new List<Room>();
            try
            {
                allRooms = (from rooms in roomDb.FMRRooms
                           select rooms).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return allRooms;
        }

        public void UpdateImage(List<byte[]> convertedBytes,int roomId)
        {

            foreach(Image v in findMyRoomDb.FMRImages.ToList())
            {
                if(v.RoomId==roomId)
                {
                    v.RoomImage = convertedBytes[0];
                    findMyRoomDb.SaveChanges();
                    break;
                }
            }
            //for(int i=0;i<convertedBytes.Count;i++)
            //{
            //    if(findMyRoomDb.FMRImages==roomId)
            //    {

            //    }
            //    Image image = new Image();
            //    image.RoomImage = convertedBytes[i];
            //    findMyRoomDb.FMRImages.Add(image);
            //    findMyRoomDb.SaveChanges();
            //}
          
         

        }

        public void UploadImages(List<byte[]> convertedBytes, int roomid)
        {
            FindMyRoomDb findMyRoomDb = new FindMyRoomDb();
            for(int i=0;i<convertedBytes.Count;i++)
            {
            Image image = new Image();
            image.RoomImage = convertedBytes[i];
            image.RoomId = roomid;
            findMyRoomDb.FMRImages.Add(image);
            findMyRoomDb.SaveChanges();

            }
           
        }
        public List<Image> getRoomImages(int roomId)
        {
            FindMyRoomDb roomDb = new FindMyRoomDb();
            List<Image> images = new List<Image>();
            try
            {
                var query = (from roomImages in roomDb.FMRImages
                             where roomId == roomImages.RoomId select roomImages).ToList();

                return query;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public byte[] GetImageId(int roomId)
        {
            

            
            try
            {

                var image = roomDb.FMRImages.Where(img => img.RoomId == roomId).FirstOrDefault();
                return image.RoomImage;
                
                
              
            }
            catch(Exception ex)
            {
                throw ex;
            }
            

            
           
                
                
               
                

            
        }




    }
}