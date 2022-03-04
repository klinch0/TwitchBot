using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.Models;

namespace TwitchBot
{
    public static class YoutubeAPi
    {
        public static HttpClient client = new HttpClient();
        public static List<string> Urls = new List<string>();
        public static IWebDriver webDriver;

        public static async Task<Root> GetVideo(string url)
        {

            try
            {
                string ID = url.Split("=").Last();

                var responce = await client.GetAsync($"https://www.googleapis.com/youtube/v3/videos?" +
                    $"part=id%2C+snippet&id={ID}&key=AIzaSyBTesLlInAl5foyuC4M1RcSA-UaWU3cwQE").Result.Content.ReadAsStringAsync();

                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responce);
                return myDeserializedClass;
            }
            catch
            {
                return new Root();
            }
        }

        public static async Task<string> GetDuration(string url)
        {
            try
            {
                // 
                var result = await client.GetAsync($"https://www.googleapis.com/youtube/v3/videos?id={url.Split("=").Last()}&part=contentDetails&key=AIzaSyBTesLlInAl5foyuC4M1RcSA-UaWU3cwQE")
                                .Result.Content.ReadAsStringAsync();

                DurationRoot myDeserializedClass = JsonConvert.DeserializeObject<DurationRoot>(result);

                return myDeserializedClass.items.FirstOrDefault().contentDetails.duration;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }





}
