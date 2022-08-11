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
    public class CatService : ICatService
    {
        public ICatHttpClient _catHttpClient;
        public IMapper _mapper;
        public IMemoryCache _memoryCache;
        public CatService(
            IMemoryCache memoryCache,
            ICatHttpClient catHttpClient,
            IMapper mapper
            )
        {
            _catHttpClient = catHttpClient;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<CatDetails>> Get(BaseRequest req)
        {
            IEnumerable<CatDetails> result;
            
            var key = new { req.has_breeds, req.breed_id, req.Limit, req.Page }.GetHashCode();

            //Added cache set for 10mins
            if (_memoryCache.TryGetValue(key, out result)) return result;

            result = await _catHttpClient.Get(req);
            _memoryCache.Set(key, result, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));

            return result;
        }

        public async Task<IEnumerable<Image>> GetImageList(BaseRequest req)
        {
            var cats = await _catHttpClient.Get(req);
            var catImageMap = _mapper.Map<Image[]>(cats);
            return catImageMap;
        }

        public async Task<ImageDetails> GetImage(string image_id)
        {
            var cat = await _catHttpClient.GetImage(image_id);
            return cat;
        }
    }
}
