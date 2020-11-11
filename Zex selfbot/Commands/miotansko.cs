using Discord;
using Discord.Commands;
using Discord.Gateway;
using Discord.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zex_selfbot.Commands
{
    [Command("movevc", "moves turbo rapidly pog")]
    public class Miotansko : CommandBase
    {
        [Parameter("user id")]
        public string UserId { get; private set; }

        [Parameter("guild id")]
        public string GuildId { get; private set; }

        [Parameter("channel 1 id")]
        public string Ch1Id { get; private set; }

        [Parameter("channel 2 id")]
        public string Ch2Id { get; private set; }


        public override void Execute()
        {
            if (Message.Author.User.Id != Program.userID) return;
            Message.Delete();
            List<Thread> startedThreads = new List<Thread>();
            Thread myThread = new Thread(() => Start());
            myThread.Start();
            startedThreads.Add(myThread);
            myThread.Join();
        }
        public void Start()
        {
            DiscordGuild gildia = Program.client.GetGuild(ulong.Parse(GuildId));
            GuildMember ofiara = gildia.GetMember(ulong.Parse(UserId));
            try
            {
                while (true)
                {
                    ofiara.Modify(new GuildMemberProperties() { ChannelId = ulong.Parse(Ch1Id) });
                    Thread.Sleep(100);
                    ofiara.Modify(new GuildMemberProperties() { ChannelId = ulong.Parse(Ch2Id) });
                }
            }
            catch
            {
                return;
            }
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            Message.Delete();
            Message.Channel.SendMessage("Invalid value on " + parameterName);

        }
    }
}
