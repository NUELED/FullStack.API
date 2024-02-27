using FullStack.API.Data;
using FullStack.API.Model;

namespace FullStack.API.Initializer
{
    public static class StackInitializer
    {
        public static WebApplication Seed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<FullstackDbContext>();
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
                }
                catch (Exception)
                {

                    throw;
                }

                return app;
            }


        }

    }
}
