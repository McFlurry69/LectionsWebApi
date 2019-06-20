using System;

namespace HelperLibrary.CustomExceptions
{
    public class InvalidMailExeption : Exception
    {
        public InvalidMailExeption(){}

        public InvalidMailExeption(string mail)
            : base(String.Format("Mail does not match pattern xxx@xxx.xxx: {0}", mail)) {}
    }
}