using Discord;
using Discord.Commands;
using Discord.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zex_selfbot.Commands
{
    [Command("purge", "clears x of your messages")]
    public class PurgeCommand : CommandBase
    {
        [Parameter("limit")]
        public string Limit { get; private set; }


        public override void Execute()
        {
            if (Message.Author.User.Id != Program.userID) return;
            Message.Delete();
            foreach (var mes in Message.Channel.GetMessages(new MessageFilters() { BeforeId = Message.Id, Limit = UInt32.Parse(Limit) }))
            {
                if (mes.Author.User.Id == Program.userID)
                {
                    mes.Delete();
                }
            }
        }
        private static void Purge(string limit, MessageEventArgs args, DiscordSocketClient client)
        {
            int min = 0; int max = Int32.Parse(limit);
            foreach (var mes in args.Message.Channel.GetMessages(new MessageFilters() { BeforeId = args.Message.Id }))
            {
                if (mes.Author.User.Id == client.User.Id)
                {
                    mes.Delete();
                    min++;
                }
                if (min == max)
                    return;
            }
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            if (providedValue == null)
                Message.Channel.SendMessage("Please provide a value for " + parameterName);
            else
                Message.Channel.SendMessage("Invalid value on " + parameterName);
        }
    }
}
