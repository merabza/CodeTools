using System;
using System.Threading;
using System.Threading.Tasks;
using AppCliTools.CliMenu;
using CodeTools.ToolActions;
using Microsoft.Extensions.Logging;

namespace CodeTools.MenuCommands;

public sealed class RunJsonSortCliMenuCommand : CliMenuCommand
{
    private readonly string _jsonFileName;
    private readonly ILogger _logger;

    public RunJsonSortCliMenuCommand(ILogger logger, string jsonFileName) : base("Run JSON Sorter", EMenuAction.Reload)
    {
        _logger = logger;
        _jsonFileName = jsonFileName;
    }

    protected override async ValueTask<bool> RunBody(CancellationToken cancellationToken = default)
    {
        var runSortToolAction = new RunSortToolAction(_logger, _jsonFileName);

        try
        {
            return await runSortToolAction.Run(cancellationToken);
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
