using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Exceptions
{
   public class RenterIdNotThere:Exception
    {
        public RenterIdNotThere():base("Sorry, the following renter is not there in records.")
        {

        }
    }
}
