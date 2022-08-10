using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Common.Model
{
    public class BaseRequest
    {
        private int limitPerPage;
        private int _maxlimitPerPage = 20;


        public int Limit {
            get => limitPerPage;
            set => limitPerPage = value > _maxlimitPerPage ? _maxlimitPerPage : value;
        }
        public int Page { get; set; } = 1;
    }
}
