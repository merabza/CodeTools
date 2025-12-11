using System;
using System.Linq;
using CliMenu;
using CliParameters.CliMenuCommands;
using CodeTools.Models;
using LibDataInput;
using LibParameters;

namespace CodeTools.MenuCommands;

public sealed class JsonManipulationSubMenuCommand : CliMenuCommand
{
    private readonly ParametersManager _parametersManager;

    // ReSharper disable once ConvertToPrimaryConstructor
    public JsonManipulationSubMenuCommand(ParametersManager parametersManager) : base("Json Editor",
        EMenuAction.LoadSubMenu)
    {
        _parametersManager = parametersManager;
    }

    protected override bool RunBody()
    {
        return true;
    }

    public override CliMenuSet GetSubMenu()
    {
        var menuSet = new CliMenuSet("Json Editor");

        var parameters = (CodeToolsParameters)_parametersManager.Parameters;

        //parameters.JsonFilesForSortPaths
        var newAppTaskCommand = new NewJsonFileCommand(_parametersManager);
        menuSet.AddMenuItem(newAppTaskCommand);
        foreach (var jsonFileName in parameters.JsonFilesForSortPaths.OrderBy(o => o))
            menuSet.AddMenuItem(new JsonFileManipulationCrudSubCliMenuCommand(_parametersManager, jsonFileName));

        var key = ConsoleKey.Escape.Value().ToLower();
        menuSet.AddMenuItem(key, new ExitToMainMenuCliMenuCommand("Exit to level up menu", null), key.Length);
        return menuSet;
    }
}