using PetsAPI.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Common.Interface
{
    public interface IDogHttpClient
    {
        Task<List<DogDetails>> GetDog(BaseRequest req);
    }
}
