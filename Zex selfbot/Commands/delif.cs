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
    [Command("delif", "delete messages if contains")]
    public class DelIfCommand : CommandBase
    {
        [Parameter("limit")]
        public string Limit { get; private set; }

        [Parameter("string")]
        public string Str { get; private set; }

        public IReadOnlyList<DiscordMessage> lista;
        public override void Execute()
        {
            if (Message.Author.User.Id != Program.userID) return;
            Message.Delete();
            Message.Channel.SendMessage("start");
            lista = Message.Channel.GetMessages(new MessageFilters() { BeforeId = Message.Id, Limit = 99 });
            for (int i = 0; i < UInt32.Parse(Limit); i++)
            {
                foreach (DiscordMessage mes in lista)
                {
                    GetTokens(mes);
                }
                lista = Message.Channel.GetMessages(new MessageFilters() { BeforeId = lista.Last().Id, Limit = 99 });
            }
            Message.Channel.SendMessage("done");
        }
        private void GetTokens(DiscordMessage message)
        {
            try
            {
                if (message.Embed.Footer.Text.Contains(Str))
                {
                    message.Delete();
                }
            }
            catch
            {
                //message.Delete();
            }
        }
    }
}
