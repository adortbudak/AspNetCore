using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace AspNetCore.Entities
{
    public class RestaurantDbContext : IdentityDbContext<User>
    {
        public DbSet<Restaurant> Restaurants { get; set; }
       
    }
}
