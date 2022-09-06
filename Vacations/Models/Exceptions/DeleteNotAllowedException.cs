﻿using System;


namespace Vacations.Models.Exceptions
{
    public class DeleteNotAllowedException : Exception
    {
        public DeleteNotAllowedException(string message) : base(message)
        {

        }
    }
}
