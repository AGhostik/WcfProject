using System.Threading.Tasks;

namespace Sender.Model
{
    public interface IClient
    {
        Task SetMessageAsync(string message);
        void Init(string url);
        void Close();
    }
}