using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Common.Model
{
    public class PaginationMetadata
    {
        public PaginationMetadata(int totalCount, int currentPage, int itemPerPage)
        {
            this.totalCount = totalCount;
            this.totalPages = (int)Math.Ceiling(totalCount/(double)itemPerPage);
            this.currentPage = currentPage;
        }
        public int currentPage { get; set; }
        public int totalCount { get; set; }

        public int totalPages { get; set; }

        public bool hasPrevious => currentPage > 1;
        public bool hasNext => currentPage <= totalPages;
    }
}
