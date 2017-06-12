using System;

namespace CheCoxshall.Battleship.Core
{
    public class CannotPlaceException : Exception
    {
        public CannotPlaceException()
        {
        }

        public CannotPlaceException(string message) : base(message)
        {
        }

        public CannotPlaceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class InvalidShotException : Exception
    {
        public InvalidShotException()
        {
        }

        public InvalidShotException(string message) : base(message)
        {
        }

        public InvalidShotException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}