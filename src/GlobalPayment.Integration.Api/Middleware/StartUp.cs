using AutoMapper;
using GlobalPayment.Integration.Api.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

[assembly: FunctionsStartup(typeof(GlobalPayment.Integration.Api.Middleware.StartUp))]
namespace GlobalPayment.Integration.Api.Middleware;


public class StartUp : FunctionsStartup
{
    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        builder.ConfigurationBuilder.AddEnvironmentVariables();
        base.ConfigureAppConfiguration(builder);
    }


    public override void Configure(IFunctionsHostBuilder builder)
    {       
        builder.Services.AddAutoMapper(Assembly.GetAssembly(this.GetType()));

        builder.Services.AddSingleton<IAdminService>((s) => new AdminService());
    }
}