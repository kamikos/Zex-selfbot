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
    [Command("test", "test")]
    public class TestCommand : CommandBase
    {
        [Parameter("test")]
        public object Test { get; private set; }


        public override void Execute()
        {
            if (Message.Author.User.Id != Program.userID) return;
            Message.Delete();
            try
            {
                Message.Channel.SendMessage(Test.ToString());
            }
            catch (Exception ex)
            {
                Message.Channel.SendMessage(ex.ToString());
            }
            
        }

    }
}
