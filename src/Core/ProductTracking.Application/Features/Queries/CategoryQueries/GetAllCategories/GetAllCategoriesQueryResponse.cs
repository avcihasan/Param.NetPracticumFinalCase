using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Queries.CategoryQueries.GetAllCategories
{
    public class GetAllCategoriesQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
