//Created by ConsoleProgramClassCreator at 11/3/2025 5:54:44 PM

using System;
using SystemToolsShared;
using CliParameters;
using LibParameters;
using CodeTools;
using CodeTools.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

ILogger<Program>? logger = null;
try
{
    Console.WriteLine("Loading...");

    const string appName = "CodeTools";

    //პროგრამის ატრიბუტების დაყენება 
    ProgramAttributes.Instance.AppName = appName;

    var argParser = new ArgumentsParser<CodeToolsParameters>(args, appName, null);
    switch (argParser.Analysis())
    {
        case EParseResult.Ok: break;
        case EParseResult.Usage: return 1;
        case EParseResult.Error: return 2;
        default: throw new ArgumentOutOfRangeException();
    }

    var par = (CodeToolsParameters?)argParser.Par;
    if (par is null)
    {
        StShared.WriteErrorLine("ConsoleTestParameters is null", true);
        return 3;
    }

    var parametersFileName = argParser.ParametersFileName;
    var servicesCreator = new ServicesCreator(par.LogFolder, null, "CodeTools");
    // ReSharper disable once using
    var serviceProvider = servicesCreator.CreateServiceProvider(LogEventLevel.Information);

    if (serviceProvider == null)
    {
        StShared.WriteErrorLine("Logger not created", true);
        return 4;
    }

    logger = serviceProvider.GetService<ILogger<Program>>();
    if (logger is null)
    {
        StShared.WriteErrorLine("logger is null", true);
        return 5;
    }

    var codeTools = new CodeTools.CodeToolsCliAppLoop(logger, new ParametersManager(parametersFileName, par));

    return codeTools.Run() ? 0 : 1;
}
catch (Exception e)
{
    StShared.WriteException(e, true, logger);
    return 7;
}
finally
{
    Log.CloseAndFlush();
}