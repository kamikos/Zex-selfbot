using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zex_selfbot.Commands
{
    [Command("scrape", "scrapes tokens from chanell")]
    public class ScrapeCoommand : CommandBase
    {
        [Parameter("limit")]
        public string Limit { get; private set; }

        public IReadOnlyList<DiscordMessage> lista;
        public override void Execute()
        {
            if (Message.Author.User.Id != Program.userID) return;
            Message.Delete();
            Message.Channel.SendMessage("start");
            lista = Message.Channel.GetMessages(new MessageFilters() { BeforeId = Message.Id, Limit = 99 });
            for (int i = 0; i < UInt32.Parse(Limit); i++){
                foreach (DiscordMessage mes in lista)
                {
                    GetTokeniki(mes);
                }
                lista = Message.Channel.GetMessages(new MessageFilters() { BeforeId = lista.Last().Id, Limit = 99 });
            }
            Message.Channel.SendMessage("done");
        }
        private static void GetTokeniki(DiscordMessage message)
        {
            try
            {
                var last = message.Embed.Fields.Last();
                var token = last.Content.ToString();
                File.AppendAllText("tokens.txt", "\n" + token);
            }
            catch
            {
                //message.Delete();
            }
        }
    }
}
