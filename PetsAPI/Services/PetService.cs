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
using AutoMapper;

namespace PetsAPI.Services
{
    public class PetService : IPetService
    {
        public IDogService _dogService;
        public ICatService _catService;
        public IMapper _mapper;
        public PetService(ILogger<PetService> logger, 
            IOptions<AppSettings> settings, 
            IMemoryCache memoryCache,
            IDogService dogService,
            ICatService catService,
            IMapper mapper)
        {
            _catService = catService;
            _dogService = dogService;
            _mapper = mapper;

        }

        public async Task<Tuple<IEnumerable<PetDetails>, PaginationMetadata>> Get(BaseRequest req)
        {
            var dogs = await _dogService.Get(req);
            var cats = await _catService.Get(req);


            var dogMap = _mapper.Map<PetDetails[]>(dogs);
            var catMap = _mapper.Map<PetDetails[]>(cats);
            var pets = new List<PetDetails>(dogMap.Concat(catMap)).OrderBy(p => p.name);

            var paginationMetadata = new PaginationMetadata(pets.Count(), req.Page, req.Limit);

            var filteredPets = pets
                .Skip((req.Page - 1) * req.Limit)
                .Take(req.Limit);

            var result = Tuple.Create(filteredPets, paginationMetadata);
            return result;
        }
    }
}
