using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WooliesX.TechChallenge;
using WooliesX.TechChallenge.BusinessLogic;
using WooliesX.TechChallenge.Services;

[assembly: FunctionsStartup(typeof(Startup))]
namespace WooliesX.TechChallenge
{
    public class Startup: FunctionsStartup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Startup()
        {

        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services
                .AddTransient<IUserManager, UserManager>()
                 .AddTransient<IProductManager, ProductManager>()
                 .AddTransient<IResourceService, ResourceService>();
        }


    }
}
