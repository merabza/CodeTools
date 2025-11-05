using CliMenu;
using CliParameters.CliMenuCommands;
using CodeTools.Models;
using LibParameters;
using System;
using System.Linq;
using LibDataInput;
using Microsoft.Extensions.Logging;

namespace CodeTools.MenuCommands;

public sealed class JsonManipulationSubMenuCommand : CliMenuCommand
{
    private readonly ILogger _logger;
    private readonly ParametersManager _parametersManager;

    public JsonManipulationSubMenuCommand(ILogger logger, ParametersManager parametersManager) : base("Json Sorter",EMenuAction.LoadSubMenu)
    {
        _logger = logger;
        _parametersManager = parametersManager;
    }

    protected override bool RunBody()
    {
        return true;
    }

    public override CliMenuSet GetSubMenu()
    {
        var menuSet = new CliMenuSet("Json Sorter");

        var parameters = (CodeToolsParameters)_parametersManager.Parameters;

        //parameters.JsonFilesForSortPaths
        var newAppTaskCommand = new NewJsonFileCommand(_parametersManager);
        menuSet.AddMenuItem(newAppTaskCommand);
        foreach (var jsonFileName in parameters.JsonFilesForSortPaths.OrderBy(o => o))
        {
            menuSet.AddMenuItem(new JsonFileManipulationCrudSubCliMenuCommand(_logger, _parametersManager, jsonFileName));
        }


        var key = ConsoleKey.Escape.Value().ToLower();
        menuSet.AddMenuItem(key, new ExitToMainMenuCliMenuCommand("Exit to level up menu", null), key.Length);
        return menuSet;
    }
}