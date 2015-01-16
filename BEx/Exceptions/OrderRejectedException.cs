using System;
using System.Runtime.Serialization;

namespace BEx
{
    //class OrderRejectedException
    //{
    //}

    public class InsufficientFundsException : Exception, ISerializable
    {
        public InsufficientFundsException()
        {
        }

        public InsufficientFundsException(string message)
            : base(message)
        {
        }

        public InsufficientFundsException(string message, Exception inner)
            : base(message, inner)
        {

        }

        protected InsufficientFundsException(SerializationInfo info, StreamingContext context) : base(info, context)
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
