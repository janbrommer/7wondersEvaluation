var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<CustomVisionClient>(client =>
{
    HttpClient httpClient = new HttpClient();    
    return new CustomVisionClient(httpClient, "e461e900a8cb4efe8f28027a75583a93");
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
