//Created by DeleteTaskCommandCreator at 11/3/2025 5:54:44 PM

using AppCliTools.CliMenu;
using AppCliTools.LibDataInput;
using CodeTools.Models;
using ParametersManagement.LibParameters;
using SystemTools.SystemToolsShared;

namespace CodeTools.MenuCommands;

// ReSharper disable once ConvertToPrimaryConstructor
public sealed class DeleteJsonFileRecordCliMenuCommand : CliMenuCommand
{
    private readonly string _jsonFileName;
    private readonly ParametersManager _parametersManager;

    public DeleteJsonFileRecordCliMenuCommand(ParametersManager parametersManager, string jsonFileName) : base(
        "Delete Json File Record", EMenuAction.LevelUp)
    {
        _parametersManager = parametersManager;
        _jsonFileName = jsonFileName;
    }

    protected override bool RunBody()
    {
        var parameters = (CodeToolsParameters)_parametersManager.Parameters;
        if (parameters.JsonFilesForSortPaths.Contains(_jsonFileName))
        {
            StShared.WriteErrorLine($"Record with File name {_jsonFileName} does not found", true);
            return false;
        }

        if (!Inputer.InputBool($"This will Delete record of JsonFile {_jsonFileName}. are you sure ? ", false, false))
        {
            return false;
        }

        parameters.JsonFilesForSortPaths.Remove(_jsonFileName);
        _parametersManager.Save(parameters, $"record of JsonFile {_jsonFileName} deleted.");
        return true;
    }
}
