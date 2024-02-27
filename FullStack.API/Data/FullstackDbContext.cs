using FullStack.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace FullStack.API.Data
{
    public class FullstackDbContext : DbContext
    {
        public FullstackDbContext(DbContextOptions<FullstackDbContext> options) : base(options)
        {
            try
            {
               

                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


        }

        public DbSet<Player> players { get; set; }  
    }
}
