//Created by ProjectMainClassCreatorForCliAppWithMenu at 11/3/2025 5:54:44 PM

using CliMenu;
using CliParameters.CliMenuCommands;
using CliTools;
using CliTools.CliMenuCommands;
using LibDataInput;
using LibParameters;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using CodeTools.MenuCommands;

namespace CodeTools;

// ReSharper disable once ConvertToPrimaryConstructor
public sealed class CodeTools : CliAppLoop
{
    private readonly ILogger _logger;
    private readonly ParametersManager _parametersManager;

    public CodeTools(ILogger logger, ParametersManager parametersManager)
    {
        _logger = logger;
        _parametersManager = parametersManager;
    }

    public override CliMenuSet BuildMainMenu()
    {
        var parameters = (CodeToolsParameters)_parametersManager.Parameters;

        var mainMenuSet = new CliMenuSet("Main Menu");

        //ძირითადი პარამეტრების რედაქტირება
        var codeToolsParametersEditor = new CodeToolsParametersEditor(parameters, _parametersManager, _logger);
        mainMenuSet.AddMenuItem(new ParametersEditorListCliMenuCommand(codeToolsParametersEditor));

        //საჭირო მენიუს ელემენტები

        var newAppTaskCommand = new NewTaskCommand(_parametersManager);
        mainMenuSet.AddMenuItem(newAppTaskCommand);
        foreach (var kvp in parameters.Tasks.OrderBy(o => o.Key))
        {
            mainMenuSet.AddMenuItem(new TaskSubMenuCommand(_logger, _parametersManager, kvp.Key));
        }

        //პროგრამიდან გასასვლელი
        var key = ConsoleKey.Escape.Value().ToLower();
        mainMenuSet.AddMenuItem(key, new ExitCliMenuCommand(), key.Length);

        return mainMenuSet;
    }
}