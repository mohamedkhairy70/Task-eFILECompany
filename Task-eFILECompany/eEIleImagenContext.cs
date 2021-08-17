using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Task_eFILECompany
{
    public class eEIleImagenContext : DbContext
    {

        public DbSet<ImageDetail> ImageDetails { get; set; }


        public eEIleImagenContext(DbContextOptions<eEIleImagenContext> options) : base(options) 
        {
            //this.Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.Entity<ImageDetail>().HasIndex(b => b.Img)
            // .IsUnique();
        }
    }
    public class eEIleContextFactory : IDesignTimeDbContextFactory<eEIleImagenContext>
    {
        public eEIleImagenContext CreateDbContext(string[]? args = null)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var optionsBuilder = new DbContextOptionsBuilder<eEIleImagenContext>();
            optionsBuilder
                // Uncomment the following line if you want to print generated
                // SQL statements on the console.
                //.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            return new eEIleImagenContext(optionsBuilder.Options);
        }
    }
}
