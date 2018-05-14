using System.Collections.Generic;
using TFSHolopBot.Settings;
using Telegram.Bot;
using TFSHolopBot.Model;
using System;
using System.Threading.Tasks;

namespace TFSHolopBot.Services
{
    public class BotService: IBotService
    {
        private static readonly Lazy<BotService> _mySingleton = new Lazy<BotService>(() => new BotService());
        private readonly BotConfiguration _config;
        public TelegramBotClient Client { get; }

        private static Dictionary<string,Command> commandsList;
        internal static IReadOnlyDictionary<string, Command> Commands => commandsList;

        private BotService()
        {
            Client = new TelegramBotClient(AppSettings.Key);
            InitializeCommands();
        }

        public static BotService Instance
        {
            get
            {
                return _mySingleton.Value;
            }
        }

        private void InitializeCommands()
        {
            commandsList = new Dictionary<string, Command>
            {
                { "some command", new SubscribeCommand() }
            };
        }
        
        public async Task SendTextMessageAsync(long chatId, string message)
        {
            await Client.SendTextMessageAsync(chatId, message);
        }
    }
}