using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;
using Colorful;

namespace Zex_selfbot
{
    class Typewriter
    {
        public static void Typewrite(string message, System.Drawing.Color color)
        {
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i].ToString()  ,color);
                System.Threading.Thread.Sleep(60);
            }

        }
        public static void TypeWriteTitle(string message, int speed)
        {
            Console.Title = "";
            for (int i = 0; i < message.Length; i++)
            {
                Console.Title += message[i].ToString();
                System.Threading.Thread.Sleep(speed);
            }

        }

        public static void CenterText(String text, System.Drawing.Color color)
        {
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
            Console.WriteLine(text, color);
        }
    }
}
