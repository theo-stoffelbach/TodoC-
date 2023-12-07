using Microsoft.EntityFrameworkCore;

namespace UltimateProject.Model
{

    public class EFContext : DbContext
    {

        public DbSet<TodoModel> TodoModels { get; set; }
        public DbSet<UserModel> UserModels { get; set; }

        private const string connectionString = "Server=(LocalDb)\\MSSQLLocalDb;Database=UltimateProjectV1;Trusted_Connection=True;";
        private static EFContext _instance;
        private static readonly object _lock = new object();
        private EFContext() { }

        /// <summary>
        /// Is a singleton pattern
        /// </summary>
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

        /// <summary>
        /// Is a method to configure the connection to the database
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
