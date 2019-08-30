namespace ExampleCQRS.DataReplicator.App
{
    using ExampleCQRS.DataReplicator.Consumer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public class Startup : IHostedService, IDisposable
    {
        private IConfigurationRoot configuration;
        private Bootstrap bootstrap;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting data replication app....");

            var bootstrap = new Bootstrap();

            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables();

            configuration = builder.Build();

            bootstrap.Setup(configuration);

            Console.WriteLine("Finished starting data replication app....");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            bootstrap?.Stop();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            bootstrap?.Stop();
            bootstrap = null;
        }
    }
}
