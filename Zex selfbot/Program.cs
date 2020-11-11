using Discord.Gateway;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Console = Colorful.Console;
using System.Drawing;

namespace Zex_selfbot
{
    class Program
    {
        private static string token;
        public static ulong userID;

        public static  DiscordSocketClient client = new DiscordSocketClient();
        static void Main(string[] args)
        {
            Console.BackgroundColor = Color.FromArgb(14, 14, 14);
            Console.Clear();
            Watermark();
            settoken:
            if (args.Length == 0)
            {
                Console.Write("Token: ");
                token = Console.ReadLine();
            } else
            {
                token = args[0];
            }

  
            client.OnLoggedIn += Client_OnLoggedIn;
            client.CreateCommandHandler("z!");
            try
            {
                client.Login(token);    

            }
            catch 
            {
                Console.WriteLine("invalid token", Color.Red);
                goto settoken;
            }
            Console.ReadLine();
            Console.ReadLine();
        }
        private static void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            Console.WriteLine("Logged in as: " + args.User.Username, Color.Lime);
            userID = args.User.Id;
        }

        public static void Watermark()
        {
            Typewriter.CenterText(" ▄███████▄     ▄████████ ▀████     ████▀ ", Color.FromArgb(4, 168, 183));
            Typewriter.CenterText("██▀     ▄██   ███    ███   ███     ███▀  ", Color.FromArgb(4, 168, 183));
            Typewriter.CenterText("      ▄███▀   ███    █▀     ███   ███    ", Color.FromArgb(4, 168, 183));
            Typewriter.CenterText(" ▀█▀▄███▀▄▄  ▄███▄▄▄        ▀███▄███▀    ", Color.FromArgb(4, 168, 183));
            Typewriter.CenterText("  ▄███▀   ▀ ▀▀███▀▀▀         ███▀██▄     ", Color.FromArgb(4, 168, 183));
            Typewriter.CenterText("▄███▀         ███    █▄     ███  ▀███    ", Color.FromArgb(4, 168, 183));
            Typewriter.CenterText("███▄     ▄█   ███    ███  ▄███     ███▄  ", Color.FromArgb(4, 168, 183));
            Typewriter.CenterText(" ▀████████▀   ██████████ ████       ███▄ ", Color.FromArgb(4, 168, 183));
            Typewriter.CenterText("                                         ", Color.FromArgb(4, 168, 183));
        }
    }
}
