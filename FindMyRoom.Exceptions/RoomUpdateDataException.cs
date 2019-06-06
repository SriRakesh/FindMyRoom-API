using FindMyRoom.Entities;
using FindMyRoom.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FindMyRoom.Exceptions
{
   public class RoomUpdateDataException:Exception
    {

        Acknowledgement<Room> acknowledgement = new Acknowledgement<Room>();

        public RoomUpdateDataException() { }
        public RoomUpdateDataException(string msg) : base(msg) { }

        public void validate(Updateroom room)
        {
            string city = room.City.Trim(), area = room.Area.Trim(), address = room.Address.Trim(), roomType = room.RoomType.ToLower(), status = room.Status.ToLower();
            string pincode = room.Pincode.ToString();
            string des = room.Description.Trim();

            if (room.Cost <= 1000 || room.Cost >= 50000)
             {
                 throw new RoomUpdateDataException("Invalid Room Cost");

             }
             else if(room.NoOfBeds<=0||room.NoOfBeds>=6)
            {
                throw new RoomUpdateDataException("Bed Capacity is only 5");

            }
             else if(!Regex.Match(city, @"^[a-zA-Z]+$").Success || String.IsNullOrWhiteSpace(city)||city.Equals(""))
            {
                throw new RoomUpdateDataException("City Name is Invalid");

            }
             else if(area.Equals(""))
            {
                throw new RoomUpdateDataException("Area Name is Invalid");
            }
            else if(address.Equals(""))
            {
                throw new RoomUpdateDataException("Address is Invalid");
            }
            
             else if(room.Furniture.Equals("YES") || room.Furniture.Equals("NO"))
            {
                throw new RoomUpdateDataException("Invalid input for furniture field");
            }
             else if(des.Length <= 10 || des.Length >= 250 ||des.Equals(""))
            {
                throw new RoomUpdateDataException("Invalid description");
            }
             else if(!(roomType.Equals("flat") || roomType.Equals("pg")))
            {
                throw new RoomUpdateDataException("Invalid " + "Room Type Should be Flat/Pg");
            }
            else if(!(status.Equals("available") || status.Equals("not available")))
            {
                throw new RoomUpdateDataException("Invalid room status");
            }
            else if ( pincode.Length < 6|| pincode.Length>6 || !Regex.Match(pincode, @"^([0-9]+)$").Success)
            {
                throw new RoomUpdateDataException("Invalid pincode");
            }
        }

    }
}
