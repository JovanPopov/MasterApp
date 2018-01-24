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
using Angular5TF1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TweetSharp;
using static Angular5TF1.Data.DTO.AlleventsDto;

namespace Angular5TF1.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class SearchController : BaseController
    {
        private readonly IConfiguration _configuration;
        private  DataContext context;
        private IApisService _apisService;

        public SearchController(IConfiguration configuration, DataContext dataContext, IApisService apisService)
        {
            context = dataContext;
            _configuration = configuration;
            _apisService = apisService;
        }

        public IActionResult Search(string data)
        {
            try
            {
                SearchTerm searchTerm = context.SearchTerms.Include(x => x.Wikipedia).Include(x => x.Events).Include(x => x.Tweets).Where(x => x.Term == data).FirstOrDefault();

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
                    List<Data.DTO.AlleventsDto.Event> events = _apisService.Allevents(data);
                    string wikiText = _apisService.Wikipedia(data).Result;
                    List<TwitterStatus> tweets = _apisService.Twitter(data);

                    //database
                    WikiDb(searchTerm, wikiText);
                    EventsDb(searchTerm, events);
                    TweetesDb(searchTerm, tweets);

                    context.SearchTerms.Add(searchTerm);
                    context.SaveChanges();

                }

                var result = new
                {
                    wiki = searchTerm.Wikipedia != null ? searchTerm.Wikipedia.Text : "",
                    allEvents = searchTerm.Events.OrderBy(x => x.start_time).Select(x => new
                    {
                        name = x.eventname,
                        time = x.start_time_display,
                        location = x.location,
                        venue = x.full_address,
                        url = x.event_url
                    }).ToList(),
                    tweets = searchTerm.Tweets.OrderBy(x => x.Date).Select(x => new {
                        x.Author,
                        Date = x.Date != null? x.Date.ToString() : "",
                        x.Text,
                        x.Url
                    }).ToList()
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex.Message);
            }
           
        }


        #region db

        private void WikiDb(SearchTerm newTerm, string wikiText)
        {
            Wikipedia wiki = new Wikipedia();
            wiki.Text = wikiText;
            newTerm.Wikipedia = wiki;
        }


        private void EventsDb(SearchTerm newTerm, List<AlleventsDto.Event> events)
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

        private void TweetesDb(SearchTerm newTerm, List<TwitterStatus> tweets)
        {
            List<Tweet> newTweets = new List<Tweet>();
            newTweets = tweets.Select(x => new Tweet
            {
                Author = x.User.Name,
                Date = x.CreatedDate,
                Text = x.Text,
                Url = "https://twitter.com/" + x.Author.ScreenName + "/status/" + x.IdStr
            }).ToList();

            newTerm.Tweets = newTweets;
        }

        #endregion

    }
}