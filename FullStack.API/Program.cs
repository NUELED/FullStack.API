using FullStack.API.Data;
using FullStack.API.Initializer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var dbHost = Environment.GetEnvironmentVariable("DB_HOST");                          //"DESKTOP-0FRMSIG";  
var dbName = Environment.GetEnvironmentVariable("DB_NAME");                         //"FullStackDb";
var dbPassword = Environment.GetEnvironmentVariable("DB_MSSQL_SA_PASSWORD");       //"password1";
var connectionString = $"Server={dbHost};Database={dbName};User ID=sa;Password={dbPassword}";//Trusted_Connection=True;Encrypt=False;This is for the Db.I took it out

builder.Services.AddDbContext<FullstackDbContext>(opt => opt.UseSqlServer(connectionString));
//builder.Services.AddDbContext<FullstackDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("FullstackConnectionString")));


//builder.Services.AddDbContext<FullstackDbContext>(opt =>opt.UseSqlServer(builder.Configuration.GetConnectionString("FullstackConnectionString")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors( policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() );//This permits external calls from another localhost address

// **********************THE "USECORS" CLASS ABOVE BASICALLY ALLOWS "CROSS DOMAIN REQUEST"*****************************.
app.UseAuthorization();

app.MapControllers();
app.Seed();
app.Run();
