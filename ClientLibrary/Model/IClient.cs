using System.Threading.Tasks;

namespace ClientLibrary.Model
{
    public interface IClient
    {
        Task<string> GetMessageAsync();
        Task SetMessageAsync(string message);
        Task Init(string url);
        void Close();
    }
}