using Telegram.Bot.Types;
using System;
using TFSHolopBot.Services;
using System.Threading.Tasks;

namespace TFSHolopBot.Model
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public string TargetRepositoryName { get; set; }

        public abstract Task ExecuteAsync(Message message, IBotService _botService);
    }
}