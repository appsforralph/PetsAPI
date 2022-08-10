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
        public CatService(
            ILogger<CatService> logger,
            IOptions<AppSettings> settings,
            IMemoryCache memoryCache,
            ICatHttpClient catHttpClient,
            IMapper mapper
            )
        {
            _catHttpClient = catHttpClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CatDetails>> Get(BaseRequest req)
        {
            var cats = await _catHttpClient.Get(req);
            return cats;
        }

        public async Task<ImageResponse> GetImage(string image_id)
        {
            var cat = await _catHttpClient.GetImage(image_id);
            return cat;
        }
    }
}
