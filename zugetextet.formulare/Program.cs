using Microsoft.EntityFrameworkCore;
using zugetextet.formulare.Data;
using zugetextet.formulare.Services;
using zugetextet.formulare.Services.Implementation;
using zugetextet.formulare.Settings;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Get app metadata/config
var settingsConfig = builder.Configuration.GetSection("Config");
AppMetaData = settingsConfig.Get<AppMetaData>();
builder.Services.Configure<AppMetaData>(settingsConfig);

// migrations
// dotnet tool install dotnet-ef
// dotnet ef migrations add -p ./zugetextet.formulare/zugetextet.formulare.csproj -c ApplicationDbContext "init"
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration["Database"],
            b =>
            {
                b.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
            })
        .EnableSensitiveDataLogging());
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddControllersWithViews();

#pragma warning disable ASP0000
builder.Services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>().Database.Migrate();
#pragma warning restore ASP0000

builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddScoped<IFormDataService, FormDataService>();
builder.Services.AddScoped<ILoginDataService, LoginDataService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<IAttachmentVersionService, AttachmentVersionService>();
builder.Services.AddScoped<IAppMetaDataService, AppMetaDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");


app.Run();

public partial class Program
{
    public static AppMetaData AppMetaData { get; private set; } = null!;
}