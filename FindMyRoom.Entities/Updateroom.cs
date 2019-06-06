using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Entities
{
   public class Updateroom
    {
        public int RoomId { get; set; }  //Primary Key in the Database
    
        public int Cost { get; set; }  //Cost of respective Flat/PG
    
        public int NoOfBeds { get; set; } // 2-bhk/3-bhk IF Flat else 2-beds/3-beds in the PG
  
        public string City { get; set; }  //City name of the Flat/PG
 
        public string Area { get; set; }  //Area in the City of the Flat/PG
     
        public string Address { get; set; } //Address of the Flat/PG
    
        public int Pincode { get; set; }  //Pincode of the Flat/PG

        public string Furniture { get; set; } //furniture can be Boolean also- lets c

        public string Description { get; set; } //Description of the Flat/PG
    
        public string Status { get; set; } //Status of the Flat/PG
        public string RoomType { get; set; }

        public string Latitude { set; get; }
        public string Longitude { set; get; }

    }
}
