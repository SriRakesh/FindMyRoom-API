using FindMyRoom.Entities;
using FindMyRoom.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FindMyRoom.Exceptions
{
   public class RoomInsertingDataException:Exception
    {

        Acknowledgement<Room> acknowledgement = new Acknowledgement<Room>();

        public RoomInsertingDataException() { }
        public RoomInsertingDataException(string msg) : base(msg) { }

        

        public void validate(HelperAddRoom room)
        {
            string roomType = room.RoomType.ToLower();
            string city = room.city.Trim();
            string area = room.Area.Trim();
            string address = room.Address.Trim();
            string pincode = room.Pincode.ToString();
            string des = room.Description.Trim();
            if (!(roomType.Equals("flat")||roomType.Equals("pg")))
            {
                throw new RoomInsertingDataException("Invalid " +
                    "Room Type Should be Flat/Pg");
            }

            else if(room.RoomCost<=1000||room.RoomCost>=50000)
            {
                throw new RoomInsertingDataException("Invalid Room Cost");

            }
            else if(room.NumberOfRooms<=0||room.NumberOfRooms>=6)
            {
                throw new RoomInsertingDataException("Bed Capacity is only 5 ");
            }
            else if(!Regex.Match(city, @"[a-zA-Z]+$").Success||String.IsNullOrWhiteSpace(city)||city.Equals(""))
            {
                throw new RoomInsertingDataException("City Name is Invalid");
            }
            else if(area.Equals(""))
            {
                throw new RoomInsertingDataException("Area Name is Invalid");
            }
          
            else if(room.Furniture.Equals("YES")||room.Furniture.Equals("NO"))
            {
                throw new RoomInsertingDataException("Invalid input for furniture field");

            }
            else if (address.Equals(""))
            {
                throw new RoomInsertingDataException("Address is Invalid");
            }


            else if (des.Length<=10||des.Length>=250||des.Equals(""))
            {
                throw new RoomInsertingDataException("Invalid description");
            }
            else if(pincode.Length<6||pincode.Length>6||!Regex.Match(pincode,@"^([0-9]+)$").Success)
            {
                throw new RoomInsertingDataException("Invalid pincode");
            }
           

        }

                   

    }


}
