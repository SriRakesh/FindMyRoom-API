using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Exceptions
{
        public class InsertingDataException : Exception
        {
            public InsertingDataException(string msg) : base(msg) { }
        }
}
