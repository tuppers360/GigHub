using System.Threading.Tasks;

namespace GigHub.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
