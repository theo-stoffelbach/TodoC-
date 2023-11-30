using Microsoft.EntityFrameworkCore;
using UltimateProject;

namespace UltimateProject.Model
{

    public class EFContext : DbContext
    {
        private const string connectionString = "Server=(LocalDb)\\MSSQLLocalDb;Database=UltimateProjectV1;Trusted_Connection=True;";

        private static EFContext _instance;
        private static readonly object _lock = new object();

        public DbSet<TodoModel> TodoModels { get; set; }
        public DbSet<UserModel> UserModels { get; set; }

        private EFContext() { }

        public static EFContext Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new EFContext();
                    }
                    return _instance;
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
