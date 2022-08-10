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
            req.has_breeds = true;
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



        public async Task<Tuple<IEnumerable<Image>, PaginationMetadata>>  GetImageList(string breed_id, BaseRequest req)
        {

            if (!string.IsNullOrEmpty(breed_id))
            {
                req.breed_id = breed_id;
            }

           var dogBreeds = await _dogService.GetImageList(req);
           var catBreeds = await _catService.GetImageList(req);

            var dogMapImage = _mapper.Map<Image[]>(dogBreeds);
            var catMapImage = _mapper.Map<Image[]>(catBreeds);


            //var catMap = _mapper.Map<PetDetails[]>(cats);
            var pets = new List<Image>(dogMapImage.Concat(catMapImage)).OrderBy(p => p.id);

            var paginationMetadata = new PaginationMetadata(pets.Count(), req.Page, req.Limit);

            var filteredPets = pets
                .Skip((req.Page - 1) * req.Limit)
                .Take(req.Limit);

            var result = Tuple.Create(filteredPets, paginationMetadata);


            return result;
        }


        public async Task<Image> GetImage(string image_id)
        {

            var combineImageList = new List<Image>();

            var dogs = await _dogService.GetImage(image_id);
            var cats = await _catService.GetImage(image_id);

            combineImageList.Add(_mapper.Map<Image>(dogs));
            combineImageList.Add(_mapper.Map<Image>(cats));
            //combineImageList.Add(cats);
            //var catMap = _mapper.Map<PetDetails[]>(cats);
            var pets = combineImageList.Where(i => i.id == image_id).OrderBy(p => p.id);

            return pets.SingleOrDefault();
        }
    }
}
