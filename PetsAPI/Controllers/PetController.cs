using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PetsAPI.Common.Interface;
using PetsAPI.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetsAPI.Controllers
{
    [Route("v1/")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly ILogger<PetController> _logger;
        private readonly IPetService _petService;
        public PetController(ILogger<PetController> logger, IPetService petService)
        {
            _logger = logger;
            _petService = petService;
        }

        [HttpGet]
        [Route("breeds")]
        public async Task<BaseResponse> Get([FromQuery] BaseRequest req)
        {
            var pets = await _petService.Get(req);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pets.Item2));

            var response = new BaseResponse()
            {
                results = pets.Item1,
                page = req.Page,
                limit = req.Limit
            };

            return response;
        }
    }
}
