using FullStack.API.Data;
using FullStack.API.Initializer.Interface;
using FullStack.API.Model;
using Microsoft.Extensions.DependencyInjection;

namespace FullStack.API.Initializer
{
    public static class StackInitializer
    {
        public static WebApplication Seed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                //using var context = scope.ServiceProvider.GetRequiredService<FullstackDbContext>();
                //var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<FullstackDbContext>();
                var dbInitializer = serviceProvider.GetRequiredService<IDbInitializer>();
              
                try
                {
                    context.Database.EnsureCreated();

                    var riffs = context.players.FirstOrDefault();
                    if (riffs == null)
                    {
                        context.players.AddRange(
                                 new Player { Name ="Messi",Email="messi@gmail.com", Phone=98171626, Salary=1000,Club="football" },
                                 new Player { Name ="Becks",Email="becks@gmail.com", Phone=563516264, Salary=2000,Club="football" },                
                                 new Player { Name ="Foden",Email="foden@gmail.com", Phone=542422646, Salary=1500,Club="football" }                  
                                  );
                        context.SaveChanges();
                    }

                    dbInitializer.Initialize();

                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
                    throw; // Re-throw the exception
                }

                return app;
            }


        }

    }
}
