//Created by DeleteTaskCommandCreator at 11/3/2025 5:54:44 PM

using CliMenu;
using CodeTools.Models;
using LibDataInput;
using LibParameters;
using SystemToolsShared;

namespace CodeTools.MenuCommands;

// ReSharper disable once ConvertToPrimaryConstructor
public sealed class DeleteTaskCommand : CliMenuCommand
{
    private readonly ParametersManager _parametersManager;
    private readonly string _taskName;

    public DeleteTaskCommand(ParametersManager parametersManager, string taskName) : base("Delete Task",
        EMenuAction.LevelUp)
    {
        _parametersManager = parametersManager;
        _taskName = taskName;
    }

    protected override bool RunBody()
    {
        var parameters = (CodeToolsParameters)_parametersManager.Parameters;
        var task = parameters.GetTask(_taskName);
        if (task == null)
        {
            StShared.WriteErrorLine($"Task {_taskName} does not found", true);
            return false;
        }

        if (!Inputer.InputBool($"This will Delete  Task {_taskName}.are you sure ? ", false, false)) return false;

        parameters.RemoveTask(_taskName);
        _parametersManager.Save(parameters, $"Task {_taskName} deleted.");
        return true;
    }
}