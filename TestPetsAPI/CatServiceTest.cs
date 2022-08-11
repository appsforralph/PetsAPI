using System;
using Xunit;
using PetsAPI;
using Moq;
using PetsAPI.Common.Interface;
using PetsAPI.Services;
using PetsAPI.Common.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using PetsAPI.Common.Settings;
using Microsoft.Extensions.Options;
using AutoMapper;

namespace TestPetsAPI
{
    public class CatServiceTest
    {
        private ICatService _catService;
        public string validImageID = "J2PmlIizw";
        public string invalidImageID = "notJ2PmlIizw123s";
        public CatServiceTest()
        {

            
            var catData = DummyData();
            var catImageData = DummyImage();

            var _catHttpClient = Mock.Of<ICatHttpClient>(x =>
                x.Get(It.Is<BaseRequest>(req =>
                       req.Page == 1 && req.Limit == 20 && req.has_breeds == true )) == Task.FromResult(catData) &&
                x.Get(It.Is<BaseRequest>(req =>
                       req.Page == 1 && req.Limit == 0 && req.has_breeds == true)) == Task.FromResult(new List<CatDetails>())&&
                x.GetImage(validImageID) == Task.FromResult(catImageData) &&
                x.GetImage(invalidImageID) == Task.FromResult(new ImageDetails()) 

            );

            var _casinologger = Mock.Of<ILogger<ICatHttpClient>>();
            var _memoryCache = new MemoryCache(new MemoryCacheOptions());
            var _mapper = Mock.Of<IMapper>();


            _catService = new CatService(_memoryCache, _catHttpClient, _mapper);
        }

        public List<CatDetails> DummyData()
        {
            var list = new List<CatDetails>();
            var catbreedList = new List<Breed>();

            var catbreed1 = new Breed
            {
                id = "abys",
                name = "Abyssinian",
                cfa_url = "http://cfa.org/Breeds/BreedsAB/Abyssinian.aspx",
                vetstreet_url = "http://www.vetstreet.com/cats/abyssinian",
                vcahospitals_url = "https://vcahospitals.com/know-your-pet/cat-breeds/abyssinian",
                temperament = "Active, Energetic, Independent, Intelligent, Gentle",
                origin = "Egypt",
                country_codes = "EG",
                country_code = "EG",
                description = "The Abyssinian is easy to care for, and a joy to have in your home. They’re affectionate cats and love both people and other animals.",
                life_span = "14 - 15",
                indoor = 0,
                lap = 1,
                alt_names = "",
                adaptability = 5,
                affection_level = 5,
                child_friendly = 3,
                dog_friendly = 4,
                energy_level = 5,
                grooming = 1,
                health_issues = 2,
                intelligence = 5,
                shedding_level = 2,
                social_needs = 5,
                stranger_friendly = 5,
                vocalisation = 1,
                experimental = 0,
                hairless = 0,
                natural = 1,
                rare = 0,
                rex = 0,
                suppressed_tail = 0,
                short_legs = 0,
                wikipedia_url = "https://en.wikipedia.org/wiki/Abyssinian_(cat)",
                hypoallergenic = 0,
                reference_image_id = "0XYvRd7oD"
            };

            catbreedList.Add(catbreed1);

            var cat1 = new CatDetails
            {
                breeds = catbreedList,
                id= "xnzzM6MBI",
                url= "https://cdn2.thecatapi.com/images/xnzzM6MBI.jpg",
                width= 2592,
                height= 1629
            };

            list.Add(cat1);
            return list;
        }

        public ImageDetails DummyImage()
        {
            var imageDetails = new ImageDetails
            {
                id = "J2PmlIizw",
                url = "https://cdn2.thecatapi.com/images/J2PmlIizw.jpg",
                width = 1080,
                height = 1350
            };

            return imageDetails;
        }

        [Fact]
        public async void Get_Should_ReturnData()
        {
            var req = new BaseRequest()
            {
                has_breeds = true,
                Limit = 20,
                Page = 1
            };

            var results = await _catService.Get(req);

            Assert.True(results != null );

        }

        [Fact]
        public async void Get_Should_NOTReturnData()
        {
            var req = new BaseRequest()
            {
                has_breeds = true,
                Limit = 20,
                Page = 0
            };

            var results = await _catService.Get(req);

            Assert.True(results == null);

        }
        [Fact]
        public async void GetImage_Should_ReturnData()
        {
            var results = await _catService.GetImage(validImageID);
            Assert.True(results != null);

        }

        [Fact]
        public async void GetImage_Should_NOTReturnData()
        {
            var results = await _catService.GetImage(invalidImageID);
            Assert.True(string.IsNullOrEmpty(results.id));
        }
    }
}
