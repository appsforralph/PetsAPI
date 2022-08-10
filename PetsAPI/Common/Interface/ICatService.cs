using PetsAPI.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Common.Interface
{
    public interface ICatService
    {
        Task<IEnumerable<CatDetails>> Get(BaseRequest req);
        Task<ImageResponse> GetImage(string image_id);
    }
}
