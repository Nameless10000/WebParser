using Microsoft.EntityFrameworkCore;

namespace HangFireContext
{
    public class HangfireDbContext : DbContext
    {
        public HangfireDbContext(DbContextOptions options) : base(options) { }
    }
}