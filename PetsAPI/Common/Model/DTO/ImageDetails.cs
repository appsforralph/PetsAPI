using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Common.Model
{
    public class ImageDetails
    {
        public string id { get; set; }
        public string url { get; set; }
        public List<Breed> breeds { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
