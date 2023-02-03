using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using News.Models;
using System.Linq;

namespace News.Services
{
    class APIService
    {
        public async Task<NewsListModel> GetNewsListAsync(string newscategory, int page = 1, int pageSize = 20,string keyword = "")
        {
            var WebAPIUrl = "https://newsapi.org/v2/top-headlines?country=us&apiKey=7dabe40554ad42bea9cde175feed265c"; // Set your REST API URL here.            
            WebAPIUrl = (newscategory != null && newscategory != string.Empty ? WebAPIUrl + "&category=" + newscategory : WebAPIUrl);
            WebAPIUrl = (page > 0 ? WebAPIUrl + "&page=" + page.ToString() : WebAPIUrl);
            WebAPIUrl = (pageSize > 0 ? WebAPIUrl + "&pageSize=" + pageSize.ToString() : WebAPIUrl);
            WebAPIUrl = (keyword != "" ? WebAPIUrl + "q=" + keyword : WebAPIUrl);
            var uri = new Uri(WebAPIUrl);
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("apiKey", "7dabe40554ad42bea9cde175feed265c");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.29.2");
                using (var response = await httpClient.GetAsync(uri))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    try
                    {
                        var News = JsonConvert.DeserializeObject<NewsListModel>(apiResponse);
                        return News;
                    }
                    catch (Exception ex)
                    {
                        NewsListModel obj = new NewsListModel();
                        obj.Response = new ResponseModel();                        
                        obj.Response.IsSuccess = false;
                        obj.Response.Message = ex.Message;                        
                        return obj;
                    }
                }
            }
        }
        public async Task<IEnumerable<articleModel>> GetItemsAsync(NewsListModel mdl, int page, int pageSize)
        {
            await Task.Delay(2000);

            var start = (page - 1) * pageSize;
            var count = pageSize;

            return Enumerable.Range(start, count).Select(i => mdl.articles[i]);
        }
    }
}
