using ArtHubBO.Entities;
using ArtHubDAO.DAO;
using ArtHubDAO.Data;
using ArtHubDAO.Interface;
using ArtHubRepository.DapperService;
using Microsoft.Data.SqlClient;
using System.Data;
using ArtHubService.Service;

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

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IBaseDAO<>), typeof(BaseDAO<>));
builder.Services.AddDbContext<ArtHubDbContext>();

string connectionString = builder.Configuration.GetConnectionString("DBDefault");
// Register IDbConnection in DI container
builder.Services.AddScoped<IDbConnection>((sp) => new SqlConnection(connectionString));

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
