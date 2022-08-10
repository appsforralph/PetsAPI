using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Common.Model
{
    public class BaseResponse
    {
        public int page { get; set; }
        public int limit { get; set; }
        public IEnumerable<PetDetails> results { get; set; }
    }
}
