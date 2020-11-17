using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NewsProject.Models
{
    public class NewsDbContext : IdentityDbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {

        }

        public DbSet<News> News { get; set; }
    }
}
