//Created by ConsoleProgramClassCreator at 11/3/2025 5:54:44 PM

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using AppCliTools.CliParameters;
using AppCliTools.CliTools;
using CodeTools;
using CodeTools.DependencyInjection;
using CodeTools.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ParametersManagement.LibParameters;
using Serilog;
using Serilog.Events;
using SystemTools.SystemToolsShared;

ILogger<Program>? logger = null;
try
{
    Console.WriteLine("Loading...");

    const string appName = "CodeTools";

    var argParser = new ArgumentsParser<CodeToolsParameters>(args, appName);

    switch (argParser.Analysis())
    {
        case EParseResult.Ok:
            break;
        case EParseResult.Usage:
            return 1;
        case EParseResult.Error:
            return 2;
        default:
            throw new SwitchExpressionException();
    }

    var serviceCollection = new ServiceCollection();

    // ReSharper disable once using
    await using ServiceProvider serviceProvider = serviceCollection
        .AddServices(appName, argParser.Par!, argParser.ParametersFileName!).BuildServiceProvider();

    (CliAppLoopParameters? cliLoopPar, logger) = CliAppLoopParameters.Create<Program>(serviceProvider);
    if (cliLoopPar is null)
    {
        return 3;
    }

    var cliAppLoop = new CliAppLoop(cliLoopPar);

    return await cliAppLoop.Run() ? 0 : 100;






    //var par = (CodeToolsParameters?)argParser.Par;
    //if (par is null)
    //{
    //    StShared.WriteErrorLine("ConsoleTestParameters is null", true);
    //    return 3;
    //}

    //var parametersFileName = argParser.ParametersFileName;
    //var servicesCreator = new ServicesCreator(par.LogFolder, null, "CodeTools");
    //// ReSharper disable once using
    //var serviceProvider = servicesCreator.CreateServiceProvider(LogEventLevel.Information);

    //if (serviceProvider == null)
    //{
    //    StShared.WriteErrorLine("Logger not created", true);
    //    return 4;
    //}

    //logger = serviceProvider.GetService<ILogger<Program>>();
    //if (logger is null)
    //{
    //    StShared.WriteErrorLine("logger is null", true);
    //    return 5;
    //}

    //var codeTools = new CodeToolsCliAppLoop(logger, new ParametersManager(parametersFileName, par));

    //// ReSharper disable once using
    //// ReSharper disable once DisposableConstructor
    //using var cts = new CancellationTokenSource();
    //var token = cts.Token;
    //token.ThrowIfCancellationRequested();

    //return await codeTools.Run(token) ? 0 : 1;
}
catch (Exception e)
{
    StShared.WriteException(e, true, logger);
    return 7;
}
finally
{
    await Log.CloseAndFlushAsync();
}
