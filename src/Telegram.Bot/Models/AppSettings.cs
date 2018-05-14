using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelegramBotApp.Models
{
    //TODO: Create JSON
    public static class AppSettings
    {
        public static string Url { get; set; }  = "https://localhost:8080/";

        public static string Name { get; set; } = "TFSBot";

        public static string Key { get; set; }  = "put your tg secret key here";

    }
}