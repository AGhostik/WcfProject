using System;

namespace Host.Model.Storages
{
    public class UserLimitException : Exception
    {
        public UserLimitException()
        {
            Message = "Cant create new user - limit is reached";
        }
        public UserLimitException(string details)
        {
            Message = $"Cant create new user - limit is reached; {details}";
        }
        public override string Message { get; }
    }
}
