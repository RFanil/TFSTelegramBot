using Telegram.Bot.Types;
using System.Threading.Tasks;
using TFSHolopBot.Services;

namespace TFSHolopBot.Model
{
    public class UnsubscribeCommand : Command
    {
        public override string Name => "/unsubscribe";

        public override async Task ExecuteAsync(Message message, IBotService _botService)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            //TODO: Command logic -_-

            await _botService.Client.SendTextMessageAsync(chatId, "Hello!");
        }
    }
}