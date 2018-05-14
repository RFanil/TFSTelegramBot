using System.Threading.Tasks;
using Telegram.Bot.Types;
using TFSHolopBot.Model;

namespace TFSHolopBot.Services
{
    public class RequestHandler : IRequestHandler
    {
        private readonly IBotService _botService;

        public RequestHandler(IBotService botService)
        {
            _botService = botService;
        }

        public async Task Processing(object post)
        {
            var update = post as Update;
            if(update == null)
                //TODO: выделить в отдельный метод.
                await _botService.Client.SendTextMessageAsync(update.Message.Chat.Id, "External error");

            var message = update.Message;
            if (BotService.Commands.TryGetValue(message.Text, out Command command))
            {
                command = BotService.Commands[message.Text];
                await command.ExecuteAsync(message, _botService);
            }
            else
            {
                //TODO: add logging
                await _botService.Client.SendTextMessageAsync(update.Message.Chat.Id, "External error");
            }
        }
    }
}
