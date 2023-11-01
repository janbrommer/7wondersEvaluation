using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.Configure<AzureConfiguration>(configuration.GetSection("Azure"));

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddSingleton<CustomVisionClient>();
builder.Services.AddSingleton<BlobClient>();

// Add database
var connectionString = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GameContext>(options =>
    options.UseSqlite(connectionString)
);

builder.Services.AddRazorPages();

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
