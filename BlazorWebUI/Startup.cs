using Application.Features.StudentFeatures.Commands;
using Application.Model;
using BlazorWebUI.Services;
using MudBlazor.Services;
using Persistence;

namespace BlazorWebUI;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddMudServices();
        #region options Pattren
        JWTOptions jWTOptions = new();
        Configuration.GetSection(JWTOptions.JWT).Bind(jWTOptions);
        //inject configrations in the pipline  injected like this (IOptions<JWTOptions> options)
        services.Configure<JWTOptions>(Configuration.GetSection(JWTOptions.JWT));
        #endregion

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly, typeof(CreateStudentCommand).Assembly));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<StudentService>();
        services.AddScoped<CountryService>();
        services.AddScoped<ClassService>();
        services.AddScoped<LoadingService>();
        services.AddPersistence(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}
