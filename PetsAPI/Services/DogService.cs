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
        public IMemoryCache _memoryCache;
        public DogService(IMemoryCache memoryCache,
            IDogHttpClient dogHttpClient,
            IMapper mapper
            )
        {
            _dogHttpClient = dogHttpClient;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<DogDetails>> Get(BaseRequest req)
        {
            IEnumerable<DogDetails> result;
            var key = new { req.has_breeds, req.breed_id, req.Limit, req.Page }.GetHashCode();

            //Added cache set for 10mins
            if (_memoryCache.TryGetValue(key, out result)) return result;

            result = await _dogHttpClient.Get(req);
            _memoryCache.Set(key, result, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));

            return result;
        }

        public async Task<IEnumerable<Image>> GetImageList(BaseRequest req)
        {
            var dog = await _dogHttpClient.GetImageList(req);
            var dogImageMap = _mapper.Map<Image[]>(dog);
            return dogImageMap;
        }

        public async Task<ImageDetails> GetImage(string image_id)
        {
            var dogs = await _dogHttpClient.GetImage(image_id);
            return dogs;
        }


    }
}
