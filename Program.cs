using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using MudBlazor;
using CourseProject.Areas.Identity;
using CourseProject.Services;
using CourseProject.Models;
using CourseProject.Data;
using CourseProject.Hubs;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

var cs = config.GetConnectionString("DefaultConnection");
var apiSecret = config["AccountSettings:ApiSecret"];

builder.Services.AddDbContextFactory<ApplicationDbContext>(options => options.UseSqlServer(cs));
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<ViewService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AzureStorageHelper>();   
builder.Services.AddScoped<SearchService>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 1;
    options.Password.RequiredUniqueChars = 1;
    options.SignIn.RequireConfirmedAccount = false;
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();
builder.Services.AddMudServices();
builder.Services.AddMudMarkdownServices();
builder.Services.AddAuthentication()
  .AddGoogle(googleOptions =>
{
    googleOptions.ClientId = config["Authentication:Google:ClientId"]!;
    googleOptions.ClientSecret = config["Authentication:Google:ClientSecret"]!;
}).AddTwitter(twitterOptions =>
{
    twitterOptions.ConsumerKey = config["Authentication:Twitter:ConsumerAPIKey"];
    twitterOptions.ConsumerSecret = config["Authentication:Twitter:ConsumerSecret"];
});

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseResponseCompression();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();
app.MapBlazorHub();
app.MapHub<CommentHub>("/commenthub");
app.MapFallbackToPage("/_Host");

app.Run();
