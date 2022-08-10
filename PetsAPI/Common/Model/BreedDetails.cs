using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Common.Model
{
    public class BreedDetails
    {
        public Weight weight { get; set; }
        public Height height { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string country_code { get; set; }
        public string bred_for { get; set; }
        public string breed_group { get; set; }
        public string life_span { get; set; }
        public string temperament { get; set; }
        public string reference_image_id { get; set; }
    }
}
