using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zex_selfbot.Commands
{
    [Command("help","Shows help menu")]
    public class Help : CommandBase
    {
        public override void Execute()
        {
            if (Message.Author.User.Id != Program.userID) return;
            EmbedMaker embed = new EmbedMaker()
            {
                Title = "Help",
                Description = "A list of commands",
                Color = Color.FromArgb(4, 168, 183),
                Footer = new EmbedFooter() { Text = "ZEX", IconUrl = "https://anarchyteam.dev/favicon.ico" }
            };

            foreach (var cmd in Client.CommandHandler.Commands.Values)
            {
                StringBuilder args = new StringBuilder();

                foreach (var arg in cmd.Parameters)
                {
                    if (arg.Optional)
                        args.Append($" <{arg.Name}>");
                    else
                        args.Append($" [{arg.Name}]");
                }
                embed.AddField(Client.CommandHandler.Prefix + cmd.Name, $"{cmd.Name}{args}\n" + cmd.Description);
            }
            Message.Delete();
            Message.Channel.SendMessage("", false, embed);
        }
    }
}
