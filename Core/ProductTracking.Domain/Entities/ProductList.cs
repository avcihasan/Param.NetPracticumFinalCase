using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductTracking.Domain.Entities.Identity;

namespace ProductTracking.Domain.Entities
{
    public class ProductList : BaseEntity
    {
        public ICollection<Product> Products { get; set; }

        public string AppUserId { get; set; }
        public AppUser User { get; set; }
    }
}
