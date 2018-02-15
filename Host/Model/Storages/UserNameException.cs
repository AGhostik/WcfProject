using System;

namespace Host.Model.Storages
{
    public class UserNameException : Exception
    {
        public UserNameException()
        {
            Message = "Cant create new user - username not available";
        }

        public UserNameException(string details)
        {
            Message = $"Cant create new user - username not available; {details}";
        }

        public override string Message { get; }
    }
}
