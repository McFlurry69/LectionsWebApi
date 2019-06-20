using System;

namespace HelperLibrary.CustomExceptions
{
    public class InvalidLecturerNameException : Exception
    {
        public InvalidLecturerNameException(){}

        public InvalidLecturerNameException(string name)
            : base(String.Format("Lecturer: {0} does not exist in Db", name)) {}
    }
}