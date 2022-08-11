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
    public class DogServiceTest
    {

        public IDogService _dogService;
        public string validImageID = "BkKZWlcVX";
        public string invalidImageID = "notBkKZWlcVX";
        public DogServiceTest()
        {
            var dogData = DummyData();
            var dogImageData = DummyImage();

            var _dogHttpClient = Mock.Of<IDogHttpClient>(x =>
                x.Get(It.Is<BaseRequest>(req =>
                       req.Page == 1 && req.Limit == 20 )) == Task.FromResult(dogData) &&
                x.Get(It.Is<BaseRequest>(req =>
                       req.Page == 1 && req.Limit == 0)) == Task.FromResult(new List<DogDetails>())&&
                x.GetImage(validImageID) == Task.FromResult(dogImageData) &&
                x.GetImage(invalidImageID) == Task.FromResult(new ImageDetails())
            );

            var _memoryCache = new MemoryCache(new MemoryCacheOptions());
            var _mapper = Mock.Of<IMapper>();


            _dogService = new DogService(_memoryCache, _dogHttpClient, _mapper);
        }

        public List<DogDetails> DummyData()
        {
            var list = new List<DogDetails>();
            var image1 = new Image
            {
                id= "BkKZWlcVX",
                width= 674,
                height= 450,
                url= "https://cdn2.thedogapi.com/images/BkKZWlcVX.jpg"
            };

            var dog1 = new DogDetails
            {
                id = "62",
                name = "Bull Terrier (Miniature)",
                bred_for = "An elegant man's fashion statement",
                life_span = "11 – 14 years",
                temperament = "Trainable, Protective, Sweet-Tempered, Keen, Active, Territorial",
                reference_image_id = "BkKZWlcVX",
                image = image1

            };

            list.Add(dog1);
            return list;
        }
        public ImageDetails DummyImage()
        {
            var imageDetails = new ImageDetails
            {
                id = "r1ifZl5E7",
                url = "https://cdn2.thedogapi.com/images/r1ifZl5E7.jpg",
                width = 850,
                height = 638
            };

            return imageDetails;
        }
        [Fact]
        public async void Get_ShouldReturnData()
        {
            var req = new BaseRequest()
            {
                has_breeds = true,
                Limit = 20,
                Page = 1
            };

            var results = await _dogService.Get(req);

            Assert.True(results != null);

        }

        [Fact]
        public async void Get_ShouldNotReturnData()
        {
            var req = new BaseRequest()
            {
                has_breeds = true,
                Limit = 20,
                Page = 0
            };

            var results = await _dogService.Get(req);
            Assert.True(results == null);

        }
        [Fact]
        public async void GetImage_Should_ReturnData()
        {
            var results = await _dogService.GetImage(validImageID);
            Assert.True(results != null);

        }

        [Fact]
        public async void GetImage_Should_NOTReturnData()
        {
            var results = await _dogService.GetImage(invalidImageID);
            Assert.True(string.IsNullOrEmpty(results.id));
        }
    }
}
