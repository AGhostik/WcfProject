using System.Threading.Tasks;

namespace Host.Model
{
    public interface IHostService
    {
        Task Close();
        Task Open();
    }
}