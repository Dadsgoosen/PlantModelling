
using Grpc.Net.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlantSimulator.Runtime;
using PlantSimulatorClient.Simulations;
using PlantSimulatorClient.Simulations.Protos;
using PlantSimulatorClient.Simulations.Services;
using SimulationServerService = PlantSimulatorClient.Simulations.Services.SimulationServerService;

namespace PlantSimulatorClient
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddPlantSimulator(Configuration);
            services.AddPlantSimulatorRuntimeBroker();
            services.AddTransient<SimulationIpOption>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(provider => GrpcChannel.ForAddress(Configuration["SimulationServer:Address"]));
            services.AddTransient(provider =>
                new SimulationClientService.SimulationClientServiceClient(provider.GetService<GrpcChannel>()));
            services.AddTransient<ISimulatorEventHandler, GrpcSimulationEventHandler>();
            services.AddHostedService<SimulationServerHost>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<SimulationServerService>();
            });
        }
    }
}
