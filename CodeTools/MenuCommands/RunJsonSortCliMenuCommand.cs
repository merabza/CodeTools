using System;
using System.Threading;
using CliMenu;
using CodeTools.ToolActions;
using Microsoft.Extensions.Logging;

namespace CodeTools.MenuCommands;

public class RunJsonSortCliMenuCommand : CliMenuCommand
{
    private readonly string _jsonFileName;
    private readonly ILogger _logger;

    public RunJsonSortCliMenuCommand(ILogger logger, string jsonFileName) : base("Run JSON Sorter", EMenuAction.Reload)
    {
        _logger = logger;
        _jsonFileName = jsonFileName;
    }

    protected override bool RunBody()
    {
        var runSortToolAction = new RunSortToolAction(_logger, _jsonFileName);

        try
        {
            // ReSharper disable once using
            // ReSharper disable once DisposableConstructor
            using var cts = new CancellationTokenSource();
            var token = cts.Token;
            token.ThrowIfCancellationRequested();

            return runSortToolAction.Run(token).Result;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation was canceled.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return false;
    }
}