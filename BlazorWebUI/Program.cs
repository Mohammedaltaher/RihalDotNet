using Application;
using BlazorWebUI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Persistence;
using NLog.Web;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Application.Model;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
}).UseNLog();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#region options Pattren
JWTOptions jWTOptions = new();
builder.Configuration.GetSection(JWTOptions.JWT).Bind(jWTOptions);
//inject configrations in the pipline  injected like this (IOptions<JWTOptions> options)
builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(JWTOptions.JWT));
#endregion

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
       .AddJwtBearer(jwt =>
       {

           jwt.SaveToken = true;
           jwt.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = jWTOptions.Issuer,
               ValidAudience = jWTOptions.Issuer,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jWTOptions.Secret))
           };

       });
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
app.UseAuthentication();
app.UseAuthorization();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
