using Discord;
using Discord.Commands;
using Discord.Gateway;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace Zex_selfbot.Commands
{
    [Command("status", "sets status to play/watch/listen/stream or restet")]
    public class StatusCommand : CommandBase
    {
        [Parameter("commandName")]
        public string CommandName { get; private set; }

        [Parameter("Context", true)]
        public string Thing { get; private set; }


        public override void Execute()
        {
            if (Message.Author.User.Id != Program.userID) return;
            Message.Delete();
            if (CommandName == "play")
                SetStatus(Program.client, ActivityType.Game);

            if (CommandName == "watch")
                SetStatus(Program.client, ActivityType.Watching);

            if (CommandName == "listen")
                SetStatus(Program.client, ActivityType.Listening);

            if (CommandName == "stream")
                SetStatus(Program.client, ActivityType.Streaming);

            if (CommandName == "reset")
                SetStatus(Program.client, ActivityType.Game, true);

            
        }


        private void SetStatus(DiscordSocketClient client, ActivityType type, bool resetStatus = false)
        {

            //string commandName = "";
            //
            //switch (type)
            //{
            //    case ActivityType.Game: commandName = "play"; break;
            //    case ActivityType.Watching: commandName = "watch"; break;
            //    case ActivityType.Listening: commandName = "listen"; break;
            //    case ActivityType.Streaming: commandName = "stream"; break;
            //}

            if (resetStatus)
            {
                try
                {
                    client.SetActivity(null);
                }
                catch
                {
                    
                }
                return;
            }
            try
            {
                if (type == ActivityType.Streaming)
                {
                    client.SetActivity(new ActivityProperties()
                    {
                        Name = Thing,
                        Type = ActivityType.Streaming
                    });
                }
                else
                {
                    client.SetActivity(new ActivityProperties()
                    {
                        Name = Thing,
                        Type = type
                    });
                }
                Console.WriteLine($"[LOGS] Set {CommandName}ing to {Thing} status", Color.GreenYellow);
            }
            catch
            {
                Console.WriteLine($"[LOGS] Could not set {CommandName}ing status", Color.Red);
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
