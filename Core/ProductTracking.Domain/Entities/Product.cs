using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Domain.Entities
{
    public class Product: BaseEntity
    {
        public decimal UnitPrice { get; set; }

        public Category Category { get; set; }

    }
}

