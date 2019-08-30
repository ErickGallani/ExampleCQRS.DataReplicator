namespace ExampleCQRS.DataReplicator.App
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {
            await new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHostedService<Startup>();
               })
               .RunConsoleAsync();
        }
    }
}
