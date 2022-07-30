using DotnetCoreWebApiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoreWebApiProject.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
