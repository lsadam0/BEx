using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BEx
{
    //class OrderRejectedException
    //{
    //}

    public class OrderRejectedException : Exception, ISerializable
    {
        public OrderRejectedException()
        {
        }

        public OrderRejectedException(string message)
            : base(message)
        {
        }

        public OrderRejectedException(string message, Exception inner)
            : base(message, inner)
        {

        }

        protected OrderRejectedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
    /*
    public class NewException : BaseException, ISerializable
{
    public NewException()
    {
        // Add implementation.
    }
    public NewException(string message)
    {
        // Add implementation.
    }
    public NewException(string message, Exception inner)
    {
        // Add implementation.
    }

    // This constructor is needed for serialization.
   protected NewException(SerializationInfo info, StreamingContext context)
   {
        // Add implementation.
   }*/
}
