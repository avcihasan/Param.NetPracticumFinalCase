using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Domain.Entities
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; }

    }
}

