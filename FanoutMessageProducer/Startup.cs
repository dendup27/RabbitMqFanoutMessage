using FanoutMessageLibrary.AutoMapper;
using FanoutMessageLibrary.Dtos;
using FanoutMessageLibrary.Models;
using FanoutMessageLibrary.Repositories;
using FanoutMessageLibrary.Services;
using FanoutMessageProducer.Contexts;
using FanoutMessageProducer.Repositories;
using FanoutMessageProducer.Services;
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

namespace FanoutMessageProducer
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
            services.AddDbContext<ProducerDbContext>(options =>
               options.UseNpgsql(Configuration.GetConnectionString("ProducerDbContext")));

            // Mass Transit and Rabbit MQ
            services.AddMassTransit(config =>
            {
                config.SetSnakeCaseEndpointNameFormatter();
                config.UsingRabbitMq((context, config) =>
                {
                    config.Host(Configuration["RabbitMQSettings:HostAddress"]);
                    config.UseMessageRetry(retryConfigurator =>
                    {
                        retryConfigurator.Interval(3, TimeSpan.FromSeconds(5));
                    });
                    //config.ExchangeType = ExchangeType.Fanout;
                });
            });
            services.AddMassTransitHostedService();

            services.AddAutoMapper(Assembly.GetAssembly(typeof(ProducerDetailsProfile)));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FanoutMessageProducer", Version = "v1" });
            });

            services.AddScoped<IRepository<ProducerDetails>, ProducerRepository>();
            services.AddScoped<IService<ProducerDetailsDto>, ProducerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FanoutMessageProducer v1"));
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
