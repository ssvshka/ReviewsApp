using CourseProject.Areas.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Azure.Identity;
using CloudinaryDotNet;
using dotenv.net;
using Microsoft.AspNetCore.DataProtection;
using CourseProject.Data;
using CourseProject.Models;
using CourseProject.Services;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

var cs = config.GetConnectionString("DefaultConnection");
var apiSecret = config["AccountSettings:ApiSecret"];

builder.Services.AddDbContextFactory<ApplicationDbContext>(options => options.UseSqlServer(cs));
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<ViewService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AzureStorageHelper>();   
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
builder.Services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
{
    microsoftOptions.ClientId = config["Authentication:Microsoft:ClientId"]!;
    microsoftOptions.ClientSecret = config["Authentication:Microsoft:ClientSecret"]!;
}).AddGoogle(googleOptions =>
{
    googleOptions.ClientId = config["Authentication:Google:ClientId"]!;
    googleOptions.ClientSecret = config["Authentication:Google:ClientSecret"]!;
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


app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
