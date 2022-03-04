using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace TwitchBot
{

    public class Program
    {
        
        public static void Main(string[] args)
        {
            try
            {
                YoutubeAPi.webDriver = new FirefoxDriver();
                YoutubeAPi.webDriver.Navigate().GoToUrl("https://www.youtube.com/");
                Bot bot = new Bot();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                File.AppendAllLines("error.txt", new string[] { ex.Message });
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();

        }
    }


    class Bot
    {
        TwitchClient client;

        public Bot()
        {
            ConnectionCredentials credentials = new ConnectionCredentials("igoryamba488", "oauth:i3agvvblh8jppymzpqfwlajf83niud");
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30),
               
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            client = new TwitchClient(customClient);
            client.Initialize(credentials, "igoryamba488");

            client.OnLog += Client_OnLog;
            client.OnMessageReceived += Client_OnMessageReceived;

            client.Connect();
        }


        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }


        private async void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            File.AppendAllLines("log.txt", new string[] { e.ChatMessage.Message });

            // id награды
            if (e.ChatMessage.CustomRewardId == "12c331bb-bd98-49eb-82c6-32ef5993f6e3")
            {
                try
                {
                    Console.WriteLine($"ЗАКАЗАНА МУЗЫКА: {e.ChatMessage.Message}");

                    var duration = await YoutubeAPi.GetDuration(e.ChatMessage.Message);
                    duration = duration.Replace("PT", "");

                    var mins = duration.Split("M").FirstOrDefault();
                    var secs = duration.Split("M").Last().Replace("S","");

                    var video = await YoutubeAPi.GetVideo(e.ChatMessage.Message);
                    client.SendMessage(e.ChatMessage.Channel, $"Заказана музыка: {video.items.First().snippet.title}, длительность: {mins} минута(ы), {secs} секунд");

                    YoutubeAPi.Urls.Add(e.ChatMessage.Message);
                    File.AppendAllLines("Music.txt", new string[] { $"{DateTime.Now}: Sender: {e.ChatMessage.Username}, URL: {e.ChatMessage.Message}" });
                    StartVideo(e.ChatMessage.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


            }

            if (e.ChatMessage.CustomRewardId == "962f6227-1e5f-45c2-bf1d-434294cea0ef")
            {
                // скример
            }

            if (e.ChatMessage.Message.Contains("badword"))
                client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromMinutes(30), "Bad word! 30 minute timeout!");
        }

        public async Task StartVideo(string url)
        {
            YoutubeAPi.webDriver.Navigate().GoToUrl(url);
            await Task.Delay(5000);
            var el = YoutubeAPi.webDriver.FindElement(By.XPath("*"));
            el.SendKeys(Keys.Space);
        }
    }
}
