using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductTracking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Persistence.Contexts
{
    public class ProductTrackingDbContext:IdentityDbContext<AppUser,AppRole,string>
    {
        public ProductTrackingDbContext(DbContextOptions options) : base(options) { }
    }
}
