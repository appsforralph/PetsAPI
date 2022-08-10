using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Common.Settings
{
    public class AppSettings
    {
        public AppSettings()
        {

        }
        public bool isProduction { get; set; }
        public string apiKey { get; set; }

        public string dogapiKey { get; set; }
        public string dogApiURL { get; set; }

        public string catApiURL { get; set; }
        public string catapiKey { get; set; }

        public string allowedOrigins { get; set; }
    }

}
