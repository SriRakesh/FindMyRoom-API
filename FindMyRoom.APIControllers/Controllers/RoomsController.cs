using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FindMyRoom.BusinessManager;
using FindMyRoom.Entities.Models;
using FindMyRoom.Entities;
using FindMyRoom.Exceptions;
using System.IO;

namespace FindMyRoom.APIControllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        RoomManager roomManager = new RoomManager();

        public RoomsController(RoomManager roomManager)
        {
            this.roomManager = roomManager;
        }

        Acknowledgement<Room> acknowledgement = new Acknowledgement<Room>();
        Acknowledgement<Image> imageAcknowledgement = new Acknowledgement<Image>();
        
        
        [HttpPost]
        [Route("AddRoom")]
        public IActionResult PostRooms(HelperAddRoom room)
        {
            RoomInsertingDataException roomInsertingDataException = new RoomInsertingDataException();
            try
            {
                roomInsertingDataException.validate(room);

                return Ok(roomManager.AddingRoom(room));

            }
            catch (RoomInsertingDataException ex)
            {
                acknowledgement.code = 2;
                acknowledgement.Set = null;
                acknowledgement.Message = ex.Message;
                return Ok(acknowledgement);
            }
            catch (Exception)
            {
                acknowledgement.code = 2;
                acknowledgement.Set = null;
                acknowledgement.Message = "Something went wrong. Please try again later";
                return Ok(acknowledgement);
            }
        }

        

        [HttpDelete]
        [Route("DeleteRoom/{RoomId}")]
        public IActionResult DeletePropertListing(int RoomId)
        {
            try
            {

               // RoomManager addRoom = new RoomManager();
                return Ok(roomManager.deleteRoomService(RoomId));

            }
            catch (Exception)
            {
                acknowledgement.code = 2;
                acknowledgement.Set = null;
                acknowledgement.Message = "No Room found with given roomid to delete";
                return Ok(acknowledgement);
            }

            
        }
        //Partner to Get his Customer Details 
        [HttpGet]
        [Route("PartnerCustomer/{PartnerId}")]
        public IActionResult GetCustomer([FromRoute] int PartnerId)
       {
            Acknowledgement<Room> aknowledgement = new Acknowledgement<Room>();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = roomManager.GetCustomer(PartnerId);
                if (user.Count == 0)
                {
                    //return NotFound();
                    throw new Exception();
                }
                return Ok(user);
            }
            catch (Exception)
            {
                aknowledgement.code = 2;
                aknowledgement.Set = null;
                aknowledgement.Message = "No records to display for given PartnerId";
                return Ok(aknowledgement);
            }
        }

        [HttpPut]
        [Route("UpdateRoom/{RoomId}")]
        public IActionResult UpdateRoomDetails(int RoomId, [FromBody] Updateroom room)
        {
            try
            {
                RoomUpdateDataException roomUpdateDataException = new RoomUpdateDataException();
                roomUpdateDataException.validate(room);
                return Ok(roomManager.updateRoom(RoomId, room));
                
            }
            catch (RoomUpdateDataException ex)
            {
                acknowledgement.code = 2;
                acknowledgement.Set = null;
                acknowledgement.Message = ex.Message;
                return Ok(acknowledgement);

            }
            catch(Exception)
            {
                acknowledgement.code = 2;
                acknowledgement.Set = null;
                acknowledgement.Message = "something went wrong.Please Try again later";
                return Ok(acknowledgement);
            }


        }

        [HttpGet]
        [Route("PartnerRooms/{PartnerId}")]
        public IActionResult GetRooms([FromRoute] int PartnerId)
        {
            Acknowledgement<Room> aknowledgement = new Acknowledgement<Room>();
            try
            {
                if (PartnerId.Equals(null))
                {
                    // return BadRequest(ModelState);
                    throw new Exception();
                }
                var room = roomManager.GetRooms(PartnerId);
                if (room.Count == 0)
                {
                    //return NotFound();
                    throw new Exception();
                }
                return Ok(room);
            }
            catch (Exception)
            {
                aknowledgement.code = 2;
                aknowledgement.Set = null;
                aknowledgement.Message = "No rooms for Given PartnerId";
                return Ok(aknowledgement);
            }
        }

        [Route("/api/Photos/photos")]
        [HttpPost]
        public IActionResult Upload()
        {
            try
            {

                List<byte[]> convertedBytes = new List<byte[]>();
                var images = Request.Form.Files;
                byte[] imageBytes = null;
                foreach (var file in images)
                {
                    using (var memoryStream = new MemoryStream())
                    {


                        file.CopyTo(memoryStream);
                        imageBytes = memoryStream.ToArray();
                        convertedBytes.Add(imageBytes);
                    }

                }
                // var result = roomManager.AddImage(convertedBytes,roomid);
                var result = roomManager.AddImage(convertedBytes);
                return Ok(result);
            }
            catch (Exception)
            {
                imageAcknowledgement.code = 2;
                imageAcknowledgement.Set = null;
                imageAcknowledgement.Message = "Failed to upload Images";
                return Ok(imageAcknowledgement);
            }
        }

        [Route("/api/Photos/photos/{roomId}")]
        [HttpPost]

        public IActionResult UpdateImage(int roomId,IFormFile file)
        {
            try
            {

            List<byte[]> convertedBytes = new List<byte[]>();
            var images = Request.Form.Files;
            byte[] imageBytes = null;
            foreach (var files in images)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    imageBytes = memoryStream.ToArray();
                    convertedBytes.Add(imageBytes);
                }
            }

            var result = roomManager.AddImage(roomId, convertedBytes);
            return Ok(result);
            }
            catch(Exception)
            {
            
                imageAcknowledgement.code = 2;
                imageAcknowledgement.Set = null;
                imageAcknowledgement.Message = "Failed to update Images";
                return Ok(imageAcknowledgement);

            }
        }


        [HttpGet]
        [Route("/api/Photos/{roomId}")]
        public ActionResult <IEnumerable<Image>> GetImage([FromRoute] int roomId) 
        {
            Acknowledgement<Image> ImageAcknowledgement = new Acknowledgement<Image>();
            List<Image> images = new List<Image>();
            try
            {
               return  roomManager.GetImages(roomId);

            }
            catch(Exception)
            {
                ImageAcknowledgement.code = 2;
                ImageAcknowledgement.Set = null;
                ImageAcknowledgement.Message = "Failed to get details of images";
                return Ok(ImageAcknowledgement);
            }
           
        }

        [HttpGet]
        [Route("/api/{roomId}")]
        public ActionResult GetImageById([FromRoute] int roomId)
        {
           
           
            Acknowledgement<Image> ImageAcknowledgement = new Acknowledgement<Image>();
            try
            {
                byte[] image = roomManager.GetImagesId(roomId);
                var result = File(image, "image/jpeg", "image/png");
                return result;

            }
            catch(Exception)
            {
                ImageAcknowledgement.code = 2;
                imageAcknowledgement.Set = null;
                ImageAcknowledgement.Message = "No Images Found";
                return Ok(ImageAcknowledgement);
            }
        }



        //[HttpGet]
        //[Route("GetAllRoom")]
        //public IActionResult GetAllRooms()
        //{
        //    Acknowledgement<Room> aknowledgement = new Acknowledgement<Room>();
        //    List<Room> allRooms = new List<Room>();
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        RoomManager roomManager = new RoomManager();

        //        allRooms=roomManager.getAllRooms();
        //        aknowledgement.code = 1;
        //        aknowledgement.Set = allRooms;
        //        aknowledgement.Message = "successfully retrieved all rooms";

        //    }
        //    catch (Exception ex)
        //    {
        //        aknowledgement.code = 0;
        //        aknowledgement.Set = null;
        //        aknowledgement.Message = "Something went wrong. Please try again later";
        //        return Ok(aknowledgement);
        //    }

        //    return Ok(aknowledgement);
        //}

    }
}