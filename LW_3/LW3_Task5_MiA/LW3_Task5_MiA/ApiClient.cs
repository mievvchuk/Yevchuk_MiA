using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TVMazeClient.Models;

namespace TVMazeClient.Services
{
    public class ApiClient
    {
        private static readonly HttpClient client = new HttpClient();
        private const string BaseUrl = "https://api.tvmaze.com";

        // Отримання списку шоу
        public async Task<List<Show>> GetShowsAsync(string search = "")
        {
            try
            {
                var url = $"{BaseUrl}/search/shows?q={search}";
                var response = await client.GetStringAsync(url);
                var shows = JsonConvert.DeserializeObject<List<ApiShowResponse>>(response);
                return shows?.ConvertAll(s => new Show
                {
                    Id = s.Show.Id,
                    Name = s.Show.Name,
                    Language = s.Show.Language,
                    Premiered = s.Show.Premiered,
                    Summary = s.Show.Summary
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching shows: " + ex.Message);
            }
        }

        // Отримання деталей шоу
        public async Task<Show> GetShowDetailsAsync(int id)
        {
            try
            {
                var url = $"{BaseUrl}/shows/{id}";
                var response = await client.GetStringAsync(url);
                var show = JsonConvert.DeserializeObject<Show>(response);
                return show;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching show details: " + ex.Message);
            }
        }

        // Внутрішній клас для парсингу відповіді API
        private class ApiShowResponse
        {
            public Show Show { get; set; }
        }
    }
}

