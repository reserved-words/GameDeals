using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDeals.Core.Model
{
    public class PagedResult<T>
    {
        public PagedResult()
        {

        }

        public PagedResult(List<T> results, int limit, int page, int totalResults)
        {
            Results = results;
            Limit = limit;
            TotalPages = (totalResults / limit) + 1;
            TotalResults = totalResults;
            Page = page;
            NextPage = Page == TotalPages ? (int?)null : Page + 1;
            PreviousPage = Page == 1 ? (int?)null : Page - 1;
        }

        public List<T> Results { get; set; }
        public int Limit { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
        public int? NextPage { get; set; }
        public int? PreviousPage { get; set; }
    }
}
