using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace News.Models
{
    public class articleModel
    {
        /*"source": {
        "id": "associated-press",
        "name": "Associated Press"
        },
        "author": "Nick Perry",
        "title": "Australia is removing British monarchy from its bank notes - The Associated Press - en Español",
        "description": "CANBERRA, Australia (AP) — Australia is removing the British monarchy from its bank notes. The nation's central bank said Thursday its new $5 bill would feature an Indigenous design rather than an image of King Charles III.",
        "url": "https://apnews.com/article/queen-elizabeth-ii-king-charles-iii-australia-business-25f05caba7d4d71b6952e52d695f4107",
        "urlToImage": "https://storage.googleapis.com/afs-prod/media/9442b5c913414821ae190ede9ffa4a10/3000.webp",
        "publishedAt": "2023-02-02T06:12:03Z",
        "content": "CANBERRA, Australia (AP) Australia is removing the British monarchy from its bank notes.\r\nThe nations central bank said Thursday its new $5 bill would feature an Indigenous design rather than an imag… [+3792 chars]"
        }*/

        public sourceModel source { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string urlToImage { get; set; }
        public string publichedAt { get; set; }
        public string content { get; set; }
    }
    public class sourceModel
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
