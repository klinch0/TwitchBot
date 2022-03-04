using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Models
{
    // DurationRoot myDeserializedClass = JsonConvert.DeserializeObject<DurationRoot>(myJsonResponse);
    public class ContentRating
    {
    }

    public class ContentDetails
    {
        public string duration { get; set; }
        public string dimension { get; set; }
        public string definition { get; set; }
        public string caption { get; set; }
        public bool licensedContent { get; set; }
        public ContentRating contentRating { get; set; }
        public string projection { get; set; }
    }

    public class Item
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string id { get; set; }
        public ContentDetails contentDetails { get; set; }
    }

    public class PageInfo
    {
        public int totalResults { get; set; }
        public int resultsPerPage { get; set; }
    }

    public class DurationRoot
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public List<Item> items { get; set; }
        public PageInfo pageInfo { get; set; }
    }


}
