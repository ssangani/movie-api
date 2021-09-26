using System;
using EventScheduler.Api.Calculators;
using EventScheduler.Api.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EventScheduler.Api
{
  public class Startup
  {
    // TODO: Move this to appsettings
    private const string _baseAddress = @"https://candidate.hubteam.com/candidateTest/v3/problem/";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<IEventPlanner, EventPlanner>();
      services.AddHttpClient<IPartnerClient, PartnerClient>(client =>
      {
        client.BaseAddress = new Uri(_baseAddress);
      });
      services.AddHttpClient<IEventSchedulerClient, EventSchedulerClient>(client =>
      {
        client.BaseAddress = new Uri(_baseAddress);
      });

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "EventScheduler.Api", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventScheduler.Api v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
