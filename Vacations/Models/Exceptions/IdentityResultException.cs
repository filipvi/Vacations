using System;

namespace Vacations.Models.Exceptions;

public class IdentityResultException : Exception
{
    public IdentityResultException(string message) : base(message)
    {

    }
}
