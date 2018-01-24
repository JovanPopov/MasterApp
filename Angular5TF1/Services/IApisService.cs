using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;
using static Angular5TF1.Data.DTO.AlleventsDto;

namespace Angular5TF1.Services
{
    public interface IApisService
    {
        List<Event> Allevents(string data);
        Task<string> Wikipedia(string data);
        List<TwitterStatus> Twitter(string data);
    }
}
