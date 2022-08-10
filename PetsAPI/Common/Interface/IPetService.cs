using PetsAPI.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Common.Interface
{
    public interface IPetService
    {
        Task<Tuple<IEnumerable<PetDetails>, PaginationMetadata>> Get(BaseRequest req);
        Task<Tuple<IEnumerable<Image>, PaginationMetadata>> GetImageList(string breed_id, BaseRequest req);
        Task<Image> GetImage(string image_id);

    }
}
