using System.Threading.Tasks;

namespace Receiver.Model
{
    public interface IClient
    {
        Task<string> GetMessageAsync();
        void Init(string url);
        void Close();
    }
}