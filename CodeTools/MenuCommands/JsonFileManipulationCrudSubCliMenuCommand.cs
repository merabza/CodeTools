//Created by TaskSubMenuCommandCreator at 11/3/2025 5:54:44 PM

using System;
using CliMenu;
using CliParameters.CliMenuCommands;
using CodeTools.Models;
using LibDataInput;
using LibParameters;
using Microsoft.Extensions.Logging;

namespace CodeTools.MenuCommands;

// ReSharper disable once ConvertToPrimaryConstructor
public sealed class JsonFileManipulationCrudSubCliMenuCommand : CliMenuCommand
{
    private readonly ILogger _logger;
    private readonly ParametersManager _parametersManager;

    public JsonFileManipulationCrudSubCliMenuCommand(ILogger logger, ParametersManager parametersManager,
        string fileName) : base(fileName, EMenuAction.LoadSubMenu)
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
        var jsonFileManipulationCrudSubMenuSet = new CliMenuSet(Name);
        var deleteJsonFileRecordCliMenuCommand = new DeleteJsonFileRecordCliMenuCommand(_parametersManager, Name);
        jsonFileManipulationCrudSubMenuSet.AddMenuItem(deleteJsonFileRecordCliMenuCommand);
        jsonFileManipulationCrudSubMenuSet.AddMenuItem(
            new EditJsonFileNameNameCliMenuCommand(_parametersManager, Name));
        //jsonFileManipulationCrudSubMenuSet.AddMenuItem(new RunJsonSortCliMenuCommand(_logger, Name));
        jsonFileManipulationCrudSubMenuSet.AddMenuItem(new RunJsonEditCliMenuCommand(_logger, Name));
        //ეს საჭირო იქნება, თუ ამ მენიუში საჭირო გახდება ამოცანის დამატებითი რედაქტორების შექმნა
        var parameters = (CodeToolsParameters)_parametersManager.Parameters;
        var task = parameters.GetTask(Name);
        var key = ConsoleKey.Escape.Value().ToLower();
        jsonFileManipulationCrudSubMenuSet.AddMenuItem(key,
            new ExitToMainMenuCliMenuCommand("Exit to level up menu", null), key.Length);
        return jsonFileManipulationCrudSubMenuSet;
    }
}

