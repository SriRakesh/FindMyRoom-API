using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Exceptions
{
        public class RetrievingDataException : Exception
        {
            public RetrievingDataException(string msg) : base(msg) { }
        }
}
