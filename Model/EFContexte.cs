using Microsoft.EntityFrameworkCore;
using UltimateProject;

namespace UltimateProject.Model
{

    public class EFContext : DbContext
    {
        private const string connectionString = "Server=(LocalDb)\\MSSQLLocalDb;Database=UltimateProject;Trusted_Connection=True;";

        public DbSet<TodoModel> TodoModels { get; set; }
        public DbSet<UserModel> UserModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
