using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TFSHolopBot.Services
{
    public interface IRequestHandler
    {
        Task Processing(object update);
    }
}
