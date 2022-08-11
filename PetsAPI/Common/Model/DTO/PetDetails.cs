using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Common.Model
{
    public class PetDetails
    {
        public string id { get; set; }
        public string name { get; set; }
        public string temperament { get; set; }
        public string origin { get; set; }
        public string country_code { get; set; }
        public string description { get; set; }
        public Image image { get; set; }

    }
}
