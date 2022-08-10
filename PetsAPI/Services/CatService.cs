﻿using Microsoft.Extensions.Caching.Memory;
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
        public CatService(
            ILogger<CatService> logger,
            IOptions<AppSettings> settings,
            IMemoryCache memoryCache,
            ICatHttpClient catHttpClient)
        {
            _catHttpClient = catHttpClient;
        }

        public async Task<IEnumerable<CatDetails>> Get(BaseRequest req)
        {
            var cats = await _catHttpClient.Get(req);
            return cats;
        }
    }
}