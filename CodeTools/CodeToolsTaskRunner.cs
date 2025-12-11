//Created by ProjectTaskRunnerCreator at 11/3/2025 5:54:44 PM

using System;
using CodeTools.Models;
using Microsoft.Extensions.Logging;
using SystemToolsShared;

namespace CodeTools;

public sealed class CodeToolsTaskRunner
{
    private readonly ILogger _logger;
    private readonly CodeToolsParameters _par;
    private readonly TaskModel? _task;
    private readonly string? _taskName;

    public CodeToolsTaskRunner(ILogger logger, CodeToolsParameters par, string taskName, TaskModel task)
    {
        _logger = logger;
        _par = par;
        _taskName = taskName;
        _task = task;
    }

    public CodeToolsTaskRunner(ILogger logger, CodeToolsParameters par)
    {
        _logger = logger;
        _par = par;
        _taskName = null;
        _task = null;
    }

    public void Run()
    {
        try
        {
        }
        catch (Exception e)
        {
            StShared.WriteException(e, true);
            throw;
        }
    }
}