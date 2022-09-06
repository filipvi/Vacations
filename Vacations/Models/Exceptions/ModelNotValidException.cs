using System;

namespace Vacations.Models.Exceptions
{
    public class ModelNotValidException : Exception
    {
        public ModelNotValidException(string message) : base(message)
        {
        }

        public ModelNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ModelNotValidException()
        {
        }
    }
}
