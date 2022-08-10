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
    public class DogHttpClient : IDogHttpClient
    {

        private readonly HttpClient _client;
        private readonly ILogger<DogHttpClient> _logger;
        private readonly AppSettings _settings;

        public DogHttpClient(HttpClient client, ILogger<DogHttpClient> logger, IOptionsSnapshot<AppSettings> settings)
        {
            _client = client;
            _logger = logger;
            _settings = settings.Value;
            _client.DefaultRequestHeaders.Add("x-api-key", _settings.dogapiKey);

        }

        public async Task<List<DogDetails>> GetDog(BaseRequest req)
        {
            try
            {
                var requestUrl = "/v1/breeds?page=" + req.Page + "&limit=" + req.Limit;
                var response = await _client.GetAsync(requestUrl);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<List<DogDetails>>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured fetching Dog Breeds from the Dog API, could be un supported parameter or bad url.");
                _logger.LogDebug(ex.ToString());

                // Return empty.
                return new List<DogDetails>();

            }
        }

        public async Task<ImageResponse> GetImage(string image_id)
        {
            try
            {
                var requestUrl = "v1/images/"+ image_id;
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


        public async Task<DogDetails> GetBreed(string breed_id)
        {
            try
            {
                var requestUrl = "/v1/breeds?:"+ breed_id;
                var response = await _client.GetAsync(requestUrl);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<DogDetails>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured fetching Dog Breeds from the Dog API, could be un supported parameter or bad url.");
                _logger.LogDebug(ex.ToString());

                // Return empty.
                return new DogDetails();

            }
        }


    }
}
