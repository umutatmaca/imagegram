using Imagegram.Domain.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Imagegram.ImageConverter.Startup))]

namespace Imagegram.ImageConverter
{
    public class Startup : FunctionsStartup
    {
        private IConfigurationRoot Configuration { get; set; }
        public override void Configure(IFunctionsHostBuilder functionsBuilder)
        {
            var configBuilder = new ConfigurationBuilder()
                 .SetBasePath(Environment.CurrentDirectory)
                 .AddEnvironmentVariables();

#if DEBUG
            configBuilder.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
#endif

            Configuration = configBuilder.Build();

            functionsBuilder.Services.AddSingleton<IImageConverter, Infrastructure.File.ImageConverter>();
        }
    }
}
