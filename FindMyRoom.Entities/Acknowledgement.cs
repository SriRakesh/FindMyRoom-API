using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyRoom.Entities
{
    //This is a generic class which will used for action responce for every entity.
    public class Acknowledgement<T>
    {
        public int code { set; get; }
        public List<T> Set { set; get; }
        public string Message { set; get; }

        public Acknowledgement()
        {
        }


        public Acknowledgement(int Code, List<T> Set, string Message)
        {
            this.code = Code;
            this.Set = Set;
            this.Message = Message;

        }


        public void AcknowledgementnAsFail(int Code, string Message)
        {
            this.code = Code;

            this.Message = Message;

        }

    }
}
