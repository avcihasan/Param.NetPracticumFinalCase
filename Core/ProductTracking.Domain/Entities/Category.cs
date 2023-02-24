using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Domain.Entities
{
    public class Category: BaseEntity
    {
        public ICollection<Product> Products { get; set; }
    }
}
