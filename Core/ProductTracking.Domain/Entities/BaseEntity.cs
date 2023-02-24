using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Domain.Entities
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CratedDate { get; set; }
    }
}
