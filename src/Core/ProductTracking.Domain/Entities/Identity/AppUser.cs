using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Domain.Entities.Identity
{
    public class AppUser:IdentityUser<string>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
#nullable enable
        public string? RefreshToken { get; set; }
#nullable disable
        public DateTime? RefreshTokenEndDate { get; set; }
        public ICollection<Basket> Baskets { get; set; }
    }
}
