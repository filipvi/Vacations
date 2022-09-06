using System;

namespace Vacations.Models.Exceptions
{
    public class AlreadyInProcessException : Exception
    {
        public AlreadyInProcessException(string message) : base(message)
        {

        }
    }
}
