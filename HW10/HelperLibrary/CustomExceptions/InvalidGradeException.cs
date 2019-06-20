using System;

namespace HelperLibrary.CustomExceptions
{
    public class InvalidGradeException : Exception
    {
        public InvalidGradeException(){}

        public InvalidGradeException(int grade)
            : base(String.Format("Grade must be from 0 to 5: {0}", grade)) {}
    }
}