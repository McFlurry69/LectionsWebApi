using System;

namespace HelperLibrary.CustomExceptions
{
    public class InvalidNameException : Exception
    {
        public InvalidNameException(){}

        public InvalidNameException(string name)
            : base(String.Format("Invalid Student Name: {0}", name)) {}
    }
}