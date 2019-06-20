using System;

namespace HelperLibrary.CustomExceptions
{
    public class InvalidSubjectNameException : Exception
    {
        public InvalidSubjectNameException(){}

        public InvalidSubjectNameException(string name)
            : base(String.Format("Subject: {0} does not exist in Db", name)) {}
    }
}