using FullStack.API.Data;
using FullStack.API.Helper;
using FullStack.API.Initializer;
using FullStack.API.Initializer.Interface;
using FullStack.API.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//var dbHost = Environment.GetEnvironmentVariable("DB_HOST");                          //"DESKTOP-0FRMSIG";  
//var dbName = Environment.GetEnvironmentVariable("DB_NAME");                         //"FullStackDb";
//var dbPassword = Environment.GetEnvironmentVariable("DB_MSSQL_SA_PASSWORD");       //"password1";
//var connectionString = $"Server={dbHost};Database={dbName};User ID=sa;Password={dbPassword}"; //Trusted_Connection=True;Encrypt=False;This is for the Db.I took it out
//builder.Services.AddDbContext<FullstackDbContext>(opt => opt.UseSqlServer(connectionString));


builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddDbContext<FullstackDbContext>(opt =>opt.UseSqlServer(builder.Configuration.GetConnectionString("MyFullstackConnect")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fullstack_Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please Bearer and then token in the field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                     },
                      new string[] { }
                   }
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddIdentity<AppUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<FullstackDbContext>();

var apiSettingsSection = builder.Configuration.GetSection("APISettings");
builder.Services.Configure<APISettings>(apiSettingsSection);

var appSetiings = apiSettingsSection.Get<APISettings>();
var key = Encoding.ASCII.GetBytes(appSetiings.ValidKey);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = appSetiings.ValidIssuer,
        ValidAudience = appSetiings.ValidAudience,
        ClockSkew = TimeSpan.Zero
    };
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// **********************THE "USECORS" CLASS ABOVE BASICALLY ALLOWS "CROSS DOMAIN REQUEST"*****************************.
app.UseCors( policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() );//This permits external calls from another localhost address
app.UseRouting();
//SeedDatabase();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Seed();
app.Run();

//void SeedDatabase()
//{
//    using (var scope = app.Services.CreateScope())
//    {
//        var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
//        DbInitializer.Initialize();
//    }
//}
