using ClassLibraryAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiAppAPI.Services
{
    // https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/rest
    public class RestService : IRestService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        private const string restUrl = "http://localhost:5214/api/Comments";

        public List<CommentDTO> Comments { get; private set; }

        public RestService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<CommentDTO>> RefreshDataAsync()
        {
            Comments = new List<CommentDTO>();

            Uri uri = new Uri(string.Format(restUrl, string.Empty));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Comments = JsonSerializer.Deserialize<List<CommentDTO>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Comments;
        }

        public async Task SendCommentAsync(CommentDTO item)
        {
            Uri uri = new Uri(string.Format(restUrl, string.Empty));

            try
            {
                string json = JsonSerializer.Serialize<CommentDTO>(item, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"\tComment successfully saved.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
