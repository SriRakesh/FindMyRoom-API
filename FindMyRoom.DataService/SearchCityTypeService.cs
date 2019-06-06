using System;
using System.Collections.Generic;
using System.Text;
using FindMyRoom.Entities;
using FindMyRoom.Entities.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FindMyRoom.DataService
{
    public class SearchCityTypeService
    {
        //Brings all the Distinct Cities from Rooms Table
        public virtual List<string> GetCities()
        {
            List<string> cityList;
            try
            {
                using (FindMyRoomDb context = new FindMyRoomDb())
                {
                    cityList =(from room in context.FMRRooms select room.City).Distinct().ToList();
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
            return cityList;
        }

        //Brings all the Display details of the Room after searching.
        public virtual List<SearchedRoomData> GetSearchedRoomDetails(SearchCityType search)
        {
            List<SearchedRoomData> roomDetails = new List<SearchedRoomData>();
            try
            {
                using (FindMyRoomDb context = new FindMyRoomDb())
                {
                    foreach (Room room in context.FMRRooms)
                    {
                        if (string.Equals(room.City, search.city.Trim(), StringComparison.OrdinalIgnoreCase)
                            && string.Equals(room.RoomType, search.roomType, StringComparison.OrdinalIgnoreCase)
                            && string.Equals(room.Status,"Available", StringComparison.OrdinalIgnoreCase))
                        {
                            SearchedRoomData data = new SearchedRoomData
                            {
                                Cost = room.Cost,
                                City = room.City,
                                RoomType = room.RoomType,
                                Area = room.Area,
                                Description = room.Description,
                                Furniture = room.Furniture,
                                NoOfBeds = room.NoOfBeds,
                                RoomId = room.RoomId
                            };
                            roomDetails.Add(data);
                        }
                    }
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }
            return roomDetails;
        }

        //Filtered list.
        //public List<Room> GetFilteredRooms(SearchedRoomData data)
        //{
        //    List<Room> list = new List<Room>();
        //    try
        //    {
        //        using (FindMyRoomDb context = new FindMyRoomDb())
        //        {
        //            foreach(Room room in context.FMRRooms)
        //            {
        //                if ((string.Equals(room.City, data.City, StringComparison.OrdinalIgnoreCase) &&
        //                    string.Equals(room.RoomType, data.RoomType, StringComparison.OrdinalIgnoreCase)
        //                    && room.Status.Equals("available"))
        //                    &&( room.Furniture.Equals(data.Furniture) && room.Cost<=data.Cost &&
        //                    room.NoOfBeds == data.NoOfBeds &&
        //                    string.Equals(room.Area, data.Area, StringComparison.OrdinalIgnoreCase)))
        //                {
        //                    list.Add(room);
        //                }
        //            }
        //        }
        //    }
        //    catch(Exception error)
        //    {
        //        throw error;
        //    }
        //    return list;
        //}
    }
}
