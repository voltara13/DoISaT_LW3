using System;

namespace LW1.Models
{
    public class IncorrectCarException : SystemException
    {
        public IncorrectCarException() { }
        public IncorrectCarException(string message) : base(message) { }
        public IncorrectCarException(string message, Exception inner) : base(message, inner) { }
        protected IncorrectCarException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
