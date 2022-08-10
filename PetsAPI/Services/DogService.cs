using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetsAPI.Common.Interface;
using PetsAPI.Common.Model;
using PetsAPI.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Services
{
    public class DogService : IDogService
    {
        public IDogHttpClient _dogHttpClient;
        public IMapper _mapper;
        public DogService(ILogger<DogService> logger,
            IOptions<AppSettings> settings,
            IMemoryCache memoryCache,
            IDogHttpClient dogHttpClient,
            IMapper mapper
            )
        {
            _dogHttpClient = dogHttpClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DogDetails>> Get(BaseRequest req)
        {
            var dogs = await _dogHttpClient.GetDog(req);
            return dogs;
        }

        public async Task<ImageResponse> GetBreedImage(string breed_id)
        {
            var dogs = await _dogHttpClient.GetImage(breed_id);
            return dogs;
        }

        public async Task<ImageResponse> GetImage(string image_id)
        {
            var dogs = await _dogHttpClient.GetImage(image_id);
            return dogs;
        }


    }
}
