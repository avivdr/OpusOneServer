using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using OpusOneServer.DTO;
using OpusOneServerBL.Models;
using OpusOneServerBL.MusicModels;


namespace OpusOneServer.Service
{
    public class OpenOpusService
    {
        public enum Period
        {
            Medieval,
            Renaissance,
            Baroque,
            Classical,
            Early_Romantic,
            Romantic,
            Late_Romantic,
            _20th_Century,
            Post_War,
            _21st_Century,
        }
        public enum Genre
        {
            Keyboard,
            Chamber,
            Orchestral,
            Stage,
            Vocal
        }
        public static List<string> Periods { get; private set; } = new List<string>()
        {
            "Medieval",
            "Renaissance",
            "Baroque",
            "Classical",
            "Early Romantic",
            "Romantic",
            "Late Romantic",
            "20th Century",
            "Post-War",
            "21st Century"
        };

        public static List<string> Genres { get; private set; } = new List<string>()
        {
            "Keyboard",
            "Chamber",
            "Orchestral",
            "Stage",
            "Vocal"
        };

        readonly HttpClient httpClient;
        const string URL = @"https://api.openopus.org";
        readonly JsonSerializerOptions options;

        public OpenOpusService()
        {
            httpClient = new HttpClient();
            options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };
        }

        public async Task<OmniSearchDTO?> OmniSearch(string query, int offset = 0)
        {
            if (query.Length < 3)
                return null;

            try
            {
                var response = await httpClient.GetAsync($@"{URL}/omnisearch/{query}/{offset}.json");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<OmniSearchResult>(content, options);

                    if (result?.Status?.Success == "true")
                    {
                        result.ToOmniSearchDTO();
                    }
                }
            }
            catch (Exception) { }

            return null;
        }

        public async Task<List<Composer>?> SearchComposerByName(string query)
        {
            if (query.Length < 4)
                return null;

            try
            {
                var response = await httpClient.GetAsync($@"{URL}/composer/list/search/{query}.json");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ComposerResult>(content, options);

                    if (result?.Status.Success == "true")
                        return result.Composers;
                    if (result?.Status.Error == "No composers found")
                        return new List<Composer>();
                }
            }
            catch (Exception) { }

            return null;
        }
    }
}
