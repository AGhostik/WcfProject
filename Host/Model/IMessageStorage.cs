namespace Host.Model
{
    public interface IMessageStorage
    {
        string GetMessage();
        void SetMessage(string message);
    }
}