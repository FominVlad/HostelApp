using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using EventBus.Base.Standard.Configuration;
using EventBus.RabbitMQ.Standard.Configuration;
using EventBus.RabbitMQ.Standard.Options;
using GreenPipes;
using Hostel.MessageListener.Consumers;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hostel.MessageListener
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //var rabbitMqOptions = Configuration.GetSection("RabbitMq").Get<RabbitMqOptions>();

            //services.AddRabbitMqConnection(rabbitMqOptions);
            //services.AddRabbitMqRegistration(rabbitMqOptions);
            //services.AddEventBusHandling(EventBusExtension.GetHandlers());

            services.AddMassTransit(x =>
            {
                x.AddConsumer<TicketConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("wombat.rmq.cloudamqp.com", 5671, "xbixproe", h =>
                    {
                        h.Username("xbixproe");
                        h.Password("tzt2ZXI-FTHVXDN6lZ4y3KrdoyZtK_kO");

                        h.UseSsl(s =>
                        {
                            s.Protocol = SslProtocols.Tls12;
                        }); ;
                    });
                    //cfg.UseHealthCheck(provider);
                    //cfg.Host(new Uri("rabbitmq://localhost"), h =>
                    //{
                    //    h.Username("guest");
                    //    h.Password("guest");
                    //});
                    cfg.ReceiveEndpoint("ticketQueue228", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<TicketConsumer>(provider);
                    });
                }));
            });
            services.AddMassTransitHostedService();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
