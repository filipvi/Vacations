using System;

namespace Vacations.Models.Exceptions;

public class UserNotAllowedException : Exception
{
    public UserNotAllowedException(string message) : base(message)
    {

    }
}
