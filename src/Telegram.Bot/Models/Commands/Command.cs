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


        public bool Compare(string text)
        {
            int positionOfNewLine = text.IndexOf("to");
            string commandName = "";
            if (positionOfNewLine >= 0)
            {
                commandName = text.Substring(0, positionOfNewLine);
                TargetRepositoryName = text.Substring(positionOfNewLine + 2, text.Length - positionOfNewLine - 2);
            }
            return Name.Equals(commandName, StringComparison.OrdinalIgnoreCase);
        }
    }
}