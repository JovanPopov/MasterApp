using Angular5TF1.Data.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Compat.Web;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using static Angular5TF1.Data.DTO.AlleventsDto;
using static Angular5TF1.Data.DTO.WikipediaSearchDto;

namespace Angular5TF1.Services
{
    public class ApisService : IApisService
    {
        private readonly IConfiguration _configuration;

        public ApisService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Event> Allevents(string data)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            string key = _configuration["AllEventsKey"];
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

            // Request parameters
            queryString["query"] = data;
            //queryString["latitude"] = "{string}";
            //queryString["longitude"] = "{string}";
            //queryString["city"] = "{string}";
            //queryString["page"] = "{string}";

            string q = String.Join("&", queryString.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(queryString[a])));
            var uri = "https://api.allevents.in/events/search/?" + q;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{body}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = client.PostAsync(uri, content).Result;


                List<Event> allevents = new List<Event>();
                if ((int)response.StatusCode == 200)
                {
                    string stringData = response.Content.ReadAsStringAsync().Result;
                    List<Event> apiData = JsonConvert.DeserializeObject<AlleventsDto.RootObject>(stringData).data;
                    if (apiData != null) allevents = apiData;
                }

                return allevents;
            }

        }


        public async Task<string> Wikipedia(string data)
        {
            var client = new HttpClient();

            string searchUrl = $"https://en.wikipedia.org/w/api.php?action=query&format=json&list=search&utf8=1&srsearch=intitle:{data}";
            HttpResponseMessage searchResponse = await client.GetAsync(searchUrl);
            string stringData = searchResponse.Content.ReadAsStringAsync().Result;
            WikipediaSearchDto.RootObject apiData = JsonConvert.DeserializeObject<WikipediaSearchDto.RootObject>(stringData);

            List<Search> pages = new List<Search>();

            if(apiData != null && apiData.query != null)
            {
                pages = apiData.query.search;
            }

            string searchTermFromResult = pages[0] != null ? pages[0].title : data;


            string uri = $"https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&titles={searchTermFromResult}&exintro&redirects=1&utf8";
            

            HttpResponseMessage response = await client.GetAsync(uri);
            var resultText = response.Content.ReadAsStringAsync().Result;
            var index = resultText.IndexOf("extract");
            if (index == -1)
            {
                return "no results";
            }
            else
            {
                string result = resultText.Substring(index + 10);
                var index2 = result.IndexOf("extract");
                string result2 = result.Substring(index2 + 10);
                result2 = result2.Remove(result2.Length - 5);
                result2 = result2.Replace(@"\n", "<br />");

                return result2;

            }
        }


        public List<TwitterStatus> Twitter(string data)
        {
            //TwitterService("consumer key", "consumer secret");
            var service = new TwitterService(_configuration["TwConsumerKey"], _configuration["TwConsumerSecret"]);

            //AuthenticateWith("Access Token", "AccessTokenSecret");
            service.AuthenticateWith(_configuration["TwAccessToken"], _configuration["TwAccessTokenSecret"]);

            TwitterSearchResult tweets = service.Search(options: new SearchOptions { Q = data, Count = 100 });
            List<TwitterStatus> status = tweets.Statuses.ToList();
            return status;
        }
    }
}
