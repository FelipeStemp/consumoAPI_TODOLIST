using ConsumoAPI.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient<Post>();
builder.Services.AddHttpClient<Put>();
builder.Services.AddHttpClient<Get>();
builder.Services.AddHttpClient<Delete>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.WebHost.UseUrls("https://*:443");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
