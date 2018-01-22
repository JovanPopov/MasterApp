using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Angular5TF1.Data;
using Angular5TF1.Data.DTO;
using Angular5TF1.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static Angular5TF1.Data.DTO.AlleventsDto;

namespace Angular5TF1.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class SearchController : BaseController
    {
        private readonly IConfiguration _configuration;
        private  DataContext context;

        public SearchController(IConfiguration configuration, DataContext dataContext)
        {
            context = dataContext;
            _configuration = configuration;
        }

        public IActionResult Search(string data)
        {
            try
            {
                SearchTerm searchTerm = context.SearchTerms.Include(x => x.Wikipedia).Include(x => x.Events).Where(x => x.Term == data).FirstOrDefault();

                if(searchTerm != null && (DateTime.UtcNow - searchTerm.SearchDate).TotalMinutes > 1)
                {
                    context.SearchTerms.Remove(searchTerm);
                    //context.SaveChanges();
                    searchTerm = null;
                }

                if (searchTerm == null)
                {
                    //search term does not exist in database
                    searchTerm = new SearchTerm();
                    searchTerm.Term = data;
                    searchTerm.SearchDate = DateTime.UtcNow;

                    //apis
                    List<Data.DTO.AlleventsDto.Event> events = AlleventsApi(data);
                    string wikiText = WikipediaApi(data).Result;

                    //database persisting
                    persistWiki(searchTerm, wikiText);
                    persistEvents(searchTerm, events);

                    context.SearchTerms.Add(searchTerm);
                    context.SaveChanges();

                }

                var result = new
                {
                    wiki = searchTerm.Wikipedia != null ? searchTerm.Wikipedia.Text : "",
                    allEvents = searchTerm.Events.Select(x => new
                    {
                        name = x.eventname,
                        time = x.start_time_display,
                        location = x.location,
                        venue = x.full_address,
                        url = x.event_url
                    }).ToList()
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex.Message);
            }
           
        }

     



        #region helpers

        private List<Data.DTO.AlleventsDto.Event> AlleventsApi(string data)
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
            var uri = "https://api.allevents.in/events/search/?" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{body}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = client.PostAsync(uri, content).Result;
                //var Text = response.Content.ReadAsStringAsync();


                //var readAsStringAsync = response.Content.ReadAsStringAsync();
                //var bla = readAsStringAsync.Result;

                //var yourTypeInstance = await response.Content.ReadAsAsync<RootObject>().Result();


                List<Data.DTO.AlleventsDto.Event> allevents = new List<Data.DTO.AlleventsDto.Event>();
                if ((int)response.StatusCode == 200)
                {
                    string stringData = response.Content.ReadAsStringAsync().Result;
                    List<Data.DTO.AlleventsDto.Event> apiData = JsonConvert.DeserializeObject<RootObject>(stringData).data;
                    if (apiData != null) allevents = apiData;
                }

                return allevents;
            }

        }


        private async Task<string> WikipediaApi(string data)
        {
            string uri = $"https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&titles={data}&exintro&redirects=1";
            var client = new HttpClient();
            
            HttpResponseMessage response = await client.GetAsync(uri);
            var resultText = response.Content.ReadAsStringAsync().Result;
            //string s1 = resultText.Substring(resultText.LastIndexOf("extract"));
            //string s2 = s1.Substring(s1.LastIndexOf("<p>"));
            //s2 =  s2.Remove(s2.IndexOf("</p>") + 4);
            var index = resultText.IndexOf("extract");
            if (index == -1)
            {
                return "no results";
            }
            else
            {
                string result = resultText.Substring(index + 10);
                result = result.Remove(result.Length - 5);
                result = result.Replace(@"\n", "<br />");
                //string replacement = Regex.Replace(result, @"\t|\n|\r", "");
                
                return result;
                
            }
        }

        #endregion



        #region persist

        private void persistWiki(SearchTerm newTerm, string wikiText)
        {
            Wikipedia wiki = new Wikipedia();
            wiki.Text = wikiText;
            newTerm.Wikipedia = wiki;
        }


        private void persistEvents(SearchTerm newTerm, List<AlleventsDto.Event> events)
        {
            List<Data.Model.Event> dbEvents = new List<Data.Model.Event>();

            dbEvents = events.Select(x => new Data.Model.Event {
               banner_url = x.banner_url,
               end_url = x.end_url,
               eventname = x.eventname,
               event_id = x.event_id,
               event_url = x.event_url,
               featured = x.featured,
               full_address = x.venue != null ? x.venue.full_address : "",
               label = x.label,
               location = x.location,
               object_type = x.object_type,
               share_url = x.share_url,
               start_time = x.start_time,
               start_time_display = x.start_time_display,
               thumb_url = x.thumb_url,
               thumb_url_large = x.thumb_url_large
            }).ToList();

            newTerm.Events = dbEvents;
        }

        

        #endregion

    }
}