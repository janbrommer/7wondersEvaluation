using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false);
var configuration = builder.Configuration;
var azureConfig = configuration.GetSection("Azure").Get<AzureConfiguration>();

// Add services to the container.
builder.Services.AddSingleton(client =>
{
    HttpClient httpClient = new HttpClient();    
    return new CustomVisionClient(httpClient, azureConfig ?? throw new ArgumentNullException(nameof(azureConfig)));    
});
builder.Services.AddSingleton(client =>
{
    return new BlobClient(azureConfig ?? throw new ArgumentNullException(nameof(azureConfig)));
});
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
