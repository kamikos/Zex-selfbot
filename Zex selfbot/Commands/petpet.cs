using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Reflection;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace Zex_selfbot.Commands
{
    [Command("petpet")]
    public class Command : CommandBase
    {
        [Parameter("id")]
        public string Id { get; private set; }

        public override void Execute()
        {
            if (Message.Author.User.Id != Program.userID) return;
            Message.Delete();
            List<Thread> startedThreads = new List<Thread>();
            Thread myThread = new Thread(() => Start(Message));
            myThread.Start();
            startedThreads.Add(myThread);
            myThread.Join();
        }
        public void Start(DiscordMessage Message)
        {
            DiscordUser ofiara = Program.client.GetUser(ulong.Parse(Id));
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            var option = new ChromeOptions();
            option.AddArguments("--headless", "--no-sandbox", "--disable-web-security", "--disable-gpu", "--incognito", "--proxy-bypass-list=*", "--proxy-server='direct://'", "--log-level=3", "--hide-scrollbars", "--silent");
            var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), option);
            driver.Navigate().GoToUrl("http://antialt.xyz/api/petpet?image=" + ofiara.Avatar.Url);
            var element = driver.FindElement(By.XPath("//img"));
            string imageSrc = element.GetAttribute("src");
            driver.Close();
            WebClient wc = new WebClient();
            wc.DownloadFile(imageSrc, "petpet.gif");
            //string paht = Path.Combine(Path.GetTempPath(), );
            //File.WriteAllText("petpet.gif", img);
            Message.Channel.SendFile("petpet.gif");
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            Message.Delete();
            Message.Channel.SendMessage("Invalid value on " + parameterName);

        }


    }
}
