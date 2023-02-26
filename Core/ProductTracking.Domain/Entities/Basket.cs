using ProductTracking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Domain.Entities
{
    public class Basket:BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
