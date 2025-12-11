using System.Threading;
using System.Threading.Tasks;
using LibToolActions;
using Microsoft.Extensions.Logging;

namespace CodeTools.ToolActions;

public sealed class RunSortToolAction : ToolAction
{
    private readonly string _jsonFileName;

    public RunSortToolAction(ILogger logger, string jsonFileName) : base(logger, "Sort JSON File", null, null, true)
    {
        _jsonFileName = jsonFileName;
    }

    protected override ValueTask<bool> RunAction(CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(true);
    }
}