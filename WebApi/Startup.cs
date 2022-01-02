using Application;
using Application.Common;
using Application.Model.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using System;
using System.Text;
using System.Text.Json.Serialization;

namespace WebApi;
/// <summary>
/// 
/// </summary>
public class Startup
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    /// <summary>
    /// 
    /// </summary>
    public IConfiguration Configuration { get; }
    /// <summary>
    ///This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        #region Api Versioning
        // Add API Versioning to the Project
        services.AddApiVersioning(config =>
        {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
        });
        #endregion
        #region options Pattren
        JWTOptions jWTOptions = new ();
        Configuration.GetSection(JWTOptions.JWT).Bind(jWTOptions);
        //inject configrations in the pipline  injected like this (IOptions<JWTOptions> options)
        services.Configure<JWTOptions>(Configuration.GetSection(JWTOptions.JWT));
        #endregion

        services.AddApplication();
        services.AddPersistence(Configuration);

        #region Add Authentication & Swagger
        services.AddAuthentication(options =>
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
        #region Swagger
        services.AddSwaggerGen(c =>
        {
            c.IncludeXmlComments(string.Format(@"{0}\CvMaker.xml", System.AppDomain.CurrentDomain.BaseDirectory));
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Cv Maker",
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization Bearer",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
             {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                            Array.Empty<string>()
                     }
             });
        });
        #endregion
        #endregion

        services.AddControllers()
         .AddJsonOptions(o => o.JsonSerializerOptions
             .ReferenceHandler = ReferenceHandler.IgnoreCycles);

    }
    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseExceptionHandler("/development");
        }
        else
        {
            app.UseExceptionHandler("/live");
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        #region Swagger
        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger();

        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
        // specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cv Maker");
        });
        #endregion
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

