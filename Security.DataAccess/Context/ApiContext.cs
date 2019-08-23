using Microsoft.EntityFrameworkCore;
using Security.Business.Models;

namespace Security.DataAccess.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
