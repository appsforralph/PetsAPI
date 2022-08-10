using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Common.Model
{
    public class ImageResponse
    {
        public int page { get; set; }
        public int limit { get; set; }
        public List<Image> results {get; set;}
    }
}
