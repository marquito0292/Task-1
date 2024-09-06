using ApiTask1.Service;
using ApiTask1.Service.Interface;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System.Net;

namespace ApiTask1;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }
    public virtual IServiceProvider ConfigureServices(IServiceCollection services)
    {


        services.AddOptions();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddHealthChecks()
               .AddCheck("self", () => HealthCheckResult.Healthy());

        services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);


        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Api Task 1",
                Version = "v1",
                Description = "Api Task 1"
            });

        });
        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.SetIsOriginAllowed((host) => true)
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
        });


        RegisterServices(services);

        var container = new ContainerBuilder();
        container.Populate(services);

        return new AutofacServiceProvider(container.Build());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        var pathBase = Configuration["PATH_BASE"];

        if (!string.IsNullOrEmpty(pathBase))
        {
            loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
            app.UsePathBase(pathBase);
        }

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger().UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"{(!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty)}/swagger/v1/swagger.json", "Api Task 1");

            c.OAuthClientId("apiTask");
            c.OAuthClientSecret(string.Empty);
            c.OAuthRealm(string.Empty);
            c.OAuthAppName("Api Task 1");
        });

        app.UseRouting();
        app.UseCors("CorsPolicy");
        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();
            endpoints.MapGet("/", () => "Api Task 1");
        });
    }



    public void RegisterServices(IServiceCollection services)
    {
        //Cremos nuestro Servicio De Operacion
        services.AddScoped<ICalculadoraService, CalculadoraService>();

    }
}
