using AppCliTools.CliTools.DependencyInjection;
using CodeTools.Models;
using Microsoft.Extensions.DependencyInjection;
using ParametersManagement.LibParameters.DependencyInjection;
using Serilog.Events;

namespace CodeTools.DependencyInjection;

public static class CodeToolsServices
{
    public static IServiceCollection AddServices(this IServiceCollection services, string appName,
        CodeToolsParameters par, string parametersFileName)
    {
        // @formatter:off
        services
            .AddSerilogLoggerService(LogEventLevel.Information, appName, par.LogFolder)
            .AddApplication(x =>
            {
                x.AppName = appName;
            })
            .AddMainParametersManager(x =>
            {
                x.ParametersFileName = parametersFileName;
                x.Par = par;
            });

        // @formatter:on

        return services;
    }
}
