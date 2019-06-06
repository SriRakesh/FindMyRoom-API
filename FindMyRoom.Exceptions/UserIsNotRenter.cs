using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Exceptions
{
   public class UserIsNotRenter:Exception
    {
        public UserIsNotRenter():base("Sorry, the following user is not a renter")
        {

        }
    }
}
