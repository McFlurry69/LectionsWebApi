using System;

namespace HelperLibrary.CustomExceptions
{
    public class InvalidPhoneException : Exception
    {
        public InvalidPhoneException(){}

        public InvalidPhoneException(string number)
            : base(String.Format("Phone does not match pattern +X (XXX) XXX-XX-XX or X XXX XXX-XX-XX or +XXX (XX) XXX-XXXX: {0}", number)) {}
    }
}