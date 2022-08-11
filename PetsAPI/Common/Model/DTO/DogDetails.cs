using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Common.Model
{
    public class DogDetails
    {


        public string id { get; set; }
        public string name { get; set; }
        public string bred_for { get; set; }
        public string breed_group { get; set; }
        public string life_span { get; set; }
        public string temperament { get; set; }
        public string origin { get; set; }
        public string reference_image_id { get; set; }

        public Image image { get; set; }
        public Weight weight { get; set; }
        public Height height { get; set; }





    }
}
