using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TFSHolopBot.Services
{
    public interface IBotService
    {
        Task SendTextMessageAsync(long chatId, string message);
        TelegramBotClient Client { get; }
    }
}