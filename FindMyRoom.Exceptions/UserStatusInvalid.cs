using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Exceptions
{
  public  class UserStatusInvalid:Exception
    {
        public UserStatusInvalid():base("Sorry, the following user is not a valid user")
        {

        }
    }
}
