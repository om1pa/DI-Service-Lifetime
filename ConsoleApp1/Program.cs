using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp1
{
    class Program
    {
        static Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            ExemplifyScoping(host.Services, "Scope 1");
            ExemplifyScoping(host.Services, "Scope 2");

            return host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddTransient<ITransientOperation, Operation>()
                        .AddScoped<IScopedOperation, Operation>()
                        .AddSingleton<ISingletonOperation, Operation>()
                        .AddTransient<OperationService>());

        static void ExemplifyScoping(IServiceProvider services, string scope)
        {
            using var serviceScope = services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            var logger = provider.GetRequiredService<OperationService>();
            logger.LogOperations($"{scope}-Call 1. ");

            Console.WriteLine("...");

            logger = provider.GetRequiredService<OperationService>();
            logger.LogOperations($"{scope}-Call 2. ");

            Console.WriteLine();
        }
    }
}

