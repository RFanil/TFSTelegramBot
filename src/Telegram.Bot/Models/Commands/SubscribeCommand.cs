using Telegram.Bot.Types;
using System.Threading.Tasks;
using TFSHolopBot.Services;

namespace TFSHolopBot.Model
{
    public class SubscribeCommand : Command
    {
        public override string Name => "/subscribe";

        public override async Task ExecuteAsync(Message message, IBotService _botService)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            

            await _botService.Client.SendTextMessageAsync(chatId, "Hello!");
        }
    }
}