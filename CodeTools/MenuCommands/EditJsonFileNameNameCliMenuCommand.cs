//Created by EditTaskNameCommandCreator at 11/3/2025 5:54:44 PM

using System.Threading;
using System.Threading.Tasks;
using AppCliTools.CliMenu;
using AppCliTools.LibMenuInput;
using CodeTools.Models;
using ParametersManagement.LibParameters;
using SystemTools.SystemToolsShared;

namespace CodeTools.MenuCommands;

public sealed class EditJsonFileNameNameCliMenuCommand : CliMenuCommand
{
    private readonly string _jsonFileName;
    private readonly ParametersManager _parametersManager;

    // ReSharper disable once ConvertToPrimaryConstructor
    public EditJsonFileNameNameCliMenuCommand(ParametersManager parametersManager, string jsonFileName) : base(
        "Edit Record of Json file", EMenuAction.LevelUp)
    {
        _parametersManager = parametersManager;
        _jsonFileName = jsonFileName;
    }

    protected override async ValueTask<bool> RunBody(CancellationToken cancellationToken = default)
    {
        var parameters = (CodeToolsParameters)_parametersManager.Parameters;
        if (parameters.JsonFilesForSortPaths.Contains(_jsonFileName))
        {
            StShared.WriteErrorLine($"Record of Json file {_jsonFileName} does not found", true);
            return false;
        }

        //ამოცანის სახელის რედაქტირება
        var newJsonFileName = MenuInputer.InputFilePath("change Json file Name ", _jsonFileName);
        if (string.IsNullOrWhiteSpace(newJsonFileName))
        {
            return false;
        }

        if (_jsonFileName == newJsonFileName)
        {
            return false;
        }

        parameters.JsonFilesForSortPaths.Remove(_jsonFileName);
        parameters.JsonFilesForSortPaths.Add(newJsonFileName);
        await _parametersManager.Save(parameters, $" Task Renamed from {_jsonFileName} To {newJsonFileName}", null,
            cancellationToken);

        return true;
    }

    protected override string GetStatus()
    {
        return _jsonFileName;
    }
}
