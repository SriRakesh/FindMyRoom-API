using FindMyRoom.DataService;
using FindMyRoom.Entities;
using FindMyRoom.Entities.Models;
using FindMyRoom.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindMyRoom.BusinessManager
{
    public class RoomManager
    {

        public static int id;
        RoomService roomService = new RoomService();
        public RoomManager(RoomService service)
        {
            this.roomService = service;
        }

        public RoomManager()
        {
        }

        public static int roomImageId;
        Acknowledgement<Room> Acknowledgement = new Acknowledgement<Room>();
        public virtual Acknowledgement<Room> AddingRoom(HelperAddRoom room)
        {
            try
            {
               
                int RoomId = roomService.AddRoom(room);
                // roomImageId = RoomId;
                roomService.UpdateOwners(RoomId, room.PartnerId);
                Acknowledgement.code = 1;
                Acknowledgement.Set = null;
                Acknowledgement.Message = "Successfully Added";
                return Acknowledgement;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public virtual Acknowledgement<Room> deleteRoomService(int roomid)
        {
            try
            {



                roomService.RoomDelete(roomid);
                Acknowledgement.code = 1;
                Acknowledgement.Set = null;
                Acknowledgement.Message = "Successfully deleted";
                return Acknowledgement;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public virtual Acknowledgement<Room> updateRoom(int id, Updateroom room)
        {
            try
            {
                roomService.updateRoom(id, room);
                Acknowledgement.code = 1;
                Acknowledgement.Set = null;
                Acknowledgement.Message = "Successfully Updated";
                return Acknowledgement;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public List<Room> GetRooms(int UserId)
        {

            try
            {
                id = UserId;
                return roomService.GetRooms(UserId);
            }
            catch (RetrievingDataException ex)
            {
                throw new RetrievingDataException(ex.Message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //List of Customers for a particular Partner
        public virtual List<GetCustomerOfPartners> GetCustomer(int UserId)
        {
            Acknowledgement<Room> aknowledgement = new Acknowledgement<Room>();
            try
            {     
                List<GetCustomerOfPartners> getCustomerOfPartners= roomService.GetCustomers(UserId);
                if(getCustomerOfPartners.Count>0)
                {
                    Acknowledgement.code = 1;
                    Acknowledgement.Set = null;
                    Acknowledgement.Message = "Successfully Displayed";
                }
                else
                {
                    Acknowledgement.code = 0;
                    Acknowledgement.Set = null;
                    Acknowledgement.Message = "There is no Partner with this PartnerId";
                }
                return getCustomerOfPartners;
            }
            catch (RetrievingDataException ex)
            {
                throw new RetrievingDataException(ex.Message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //public virtual Acknowledgement<string> GetCities()
        //{
        //    Acknowledgement<string> payload = new Acknowledgement<string>();
        //    try
        //    {
        //        List<string> list;
        //        SearchCityTypeService service = new SearchCityTypeService();
        //        list = service.GetCities();
        //        if (list.Count > 0)
        //        {
        //            payload.code = 1;
        //            payload.Set = list;
        //            payload.Message = "Success";
        //        }
        //        else if (list.Count == 0)
        //        {
        //            payload.code = 0;
        //            payload.Set = list;
        //            payload.Message = "No Cities";
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        throw exception;
        //    }
        //    return payload;
        //}


        public List<Room> getAllRooms()
        {
            List<Room> allRooms = new List<Room>();
            try
            {
                allRooms = roomService.getAllRooms();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return allRooms;
        }



        public virtual Acknowledgement<Image> AddImage(List<byte[]> convertedBytes)
        {
            Acknowledgement<Image> ImageAknowledgement = new Acknowledgement<Image>();

            try
            {
            FindMyRoomDb roomDb = new FindMyRoomDb();
          
                var query = roomDb.FMRRooms.LastOrDefault();
                roomImageId = query.RoomId;
                roomService.UploadImages(convertedBytes, roomImageId);
                ImageAknowledgement.code = 1;
                ImageAknowledgement.Set = null;
                ImageAknowledgement.Message = "Images are Uploaded";
                return ImageAknowledgement;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public virtual Acknowledgement<Image> AddImage(int roomId,List<byte[]> convertedBytes)
        {
            Acknowledgement<Image> ImageAcknowledgement = new Acknowledgement<Image>();
            try
            {
                FindMyRoomDb roomDb = new FindMyRoomDb();
               // RoomService roomService = new RoomService();
                roomService.UpdateImage(convertedBytes, roomId);
                ImageAcknowledgement.code = 1;
                ImageAcknowledgement.Set = null;
                ImageAcknowledgement.Message = "Images are Updated";
                return ImageAcknowledgement;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public  virtual List<Image> GetImages(int roomId)
        {
            try
            {

          //  RoomService roomService = new RoomService();
            return roomService.getRoomImages(roomId);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public virtual byte[] GetImagesId(int roomId)
        {
            try
            {
               // RoomService roomService = new RoomService();
                return roomService.GetImageId(roomId);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
