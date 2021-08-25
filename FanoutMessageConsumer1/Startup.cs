using FanoutMessageConsumer1.Consumers;
using FanoutMessageConsumer1.Contexts;
using FanoutMessageConsumer1.Repositories;
using FanoutMessageConsumer1.Services;
using FanoutMessageLibrary.AutoMapper;
using FanoutMessageLibrary.Dtos;
using FanoutMessageLibrary.Models;
using FanoutMessageLibrary.Repositories;
using FanoutMessageLibrary.Services;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

namespace FanoutMessageConsumer1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Postgresql configuration
            services.AddDbContext<Consumer1DbContext>(options =>
               options.UseNpgsql(Configuration.GetConnectionString("ConsumerDbContext")));

            // Mass Transit and Rabbit MQ
            services.AddMassTransit(config =>
            {
                config.AddConsumer<ProducerConsumer1>();
                config.SetSnakeCaseEndpointNameFormatter();
                config.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(Configuration["RabbitMQSettings:HostAddress"]);
                    configurator.ConfigureEndpoints(context);
                    configurator.UseMessageRetry(retryConfigurator =>
                    {
                        retryConfigurator.Interval(3, TimeSpan.FromSeconds(5));
                    });
                });
            });
            services.AddMassTransitHostedService();

            services.AddAutoMapper(Assembly.GetAssembly(typeof(ProducerDetailsProfile)));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FanoutMessageConsumer1", Version = "v1" });
            });

            services.AddScoped<IRepository<ProducerDetails>, Consumer1Repository>();
            services.AddScoped<IService<ProducerDetailsDto>, Customer1Service>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FanoutMessageConsumer1 v1"));
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
