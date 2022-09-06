using System;

namespace Vacations.Models.Exceptions
{
    /// <summary>
    /// Input file check mime type
    /// </summary>
    public class InvalidMimeTypeException : Exception
    {
        public InvalidMimeTypeException(string message) : base(message)
        {

        }
    }
}
