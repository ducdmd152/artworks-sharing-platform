using ArtHubBO.Entities;
using ArtHubDAO.DAO;
using ArtHubDAO.Data;
using ArtHubDAO.Interface;
using ArtHubRepository.DapperService;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json.Serialization;
using Admin;
using ArtHubService.Service;
using User.Pages.Filter;
using StackExchange.Redis;
using Microsoft.AspNetCore.DataProtection;
DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Add service dependency injection
builder.Services.Scan(scan => scan
    .FromAssembliesOf(new Type[] { typeof(DapperQueryService), typeof(AccountService)})
        .AddClasses(classes => classes.Where(type =>
            type.Name.EndsWith("Repository") || type.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders(); // Clear previous providers to avoid duplication
    logging.AddConsole(); // Add console logger
});

// var redis = ConnectionMultiplexer
//     .Connect(Environment.GetEnvironmentVariable("REDIS_URL"));
// // Configure Redis Based Distributed Session
// var redisConfigurationOptions = builder.Configuration["REDIS_URL"];
//
// builder.Services.AddDataProtection()
//     .PersistKeysToStackExchangeRedis(redis, "Secrets-admin-data-protection");
// builder.Services.AddStackExchangeRedisCache(redisCacheConfig =>
// {
//     redisCacheConfig.ConfigurationOptions = ConfigurationOptions.Parse(redisConfigurationOptions);
// });

//Add session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IBaseDAO<>), typeof(BaseDAO<>));
builder.Services.AddTransient<IHelper, Helper>();

string connectionString = builder.Configuration["DATABASE_URL"];
// Register IDbConnection in DI container
builder.Services.AddScoped<IDbConnection>((sp) => new SqlConnection(connectionString));
builder.Services.AddDbContext<ArtHubDbContext>(options =>
    options.UseSqlServer(builder.Configuration["DATABASE_URL"]));

builder.Services.AddMemoryCache();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseMiddleware<LoginMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
