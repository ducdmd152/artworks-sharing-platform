using ArtHubBO.Entities;
using ArtHubDAO.DAO;
using ArtHubDAO.Data;
using ArtHubDAO.Interface;
using ArtHubRepository.DapperService;
using Microsoft.Data.SqlClient;
using System.Data;
using ArtHubService.Service;
using User.Pages.Filter;

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

//Add session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IBaseDAO<>), typeof(BaseDAO<>));

string connectionString = builder.Configuration.GetConnectionString("DBDefault");
// Register IDbConnection in DI container
builder.Services.AddScoped<IDbConnection>((sp) => new SqlConnection(connectionString));

builder.Services.AddMemoryCache();

builder.Services.AddHttpContextAccessor();

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
