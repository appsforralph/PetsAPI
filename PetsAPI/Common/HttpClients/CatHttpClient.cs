using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetsAPI.Common.Interface;
using PetsAPI.Common.Model;
using PetsAPI.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PetsAPI.Common.HttpClients
{
    public class CatHttpClient : ICatHttpClient
    {

        private readonly HttpClient _client;
        private readonly ILogger<CatHttpClient> _logger;
        private readonly AppSettings _settings;

        public CatHttpClient(HttpClient client, ILogger<CatHttpClient> logger, IOptionsSnapshot<AppSettings> settings)
        {
            _client = client;
            _logger = logger;
            _settings = settings.Value;
            _client.DefaultRequestHeaders.Add("x-api-key", _settings.catapiKey);
        }

        public async Task<List<CatDetails>> Get(BaseRequest req)
        {
            try
            {
                var requestUrl = "v1/images/search?has_breeds=true&page=" + req.Page + "&limit=" + req.Limit;
                var response = await _client.GetAsync(requestUrl);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<List<CatDetails>>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured fetching Dog Breeds from the Cat API, could be un supported parameter or bad url.");
                _logger.LogDebug(ex.ToString());

                // Return empty.
                return new List<CatDetails>();

            }
        }

        public async Task<ImageResponse> GetImage(string image_id)
        {
            try
            {
                var requestUrl = "v1/images/" + image_id;
                var response = await _client.GetAsync(requestUrl);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<ImageResponse>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured fetching Dog Breeds from the Dog API, could be un supported parameter or bad url.");
                _logger.LogDebug(ex.ToString());

                // Return empty.
                return new ImageResponse();

            }
        }
    }
}
