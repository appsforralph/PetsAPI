﻿using PetsAPI.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Common.Interface
{
    public interface IDogService
    {
        Task<IEnumerable<DogDetails>> Get(BaseRequest req);
    }
}
