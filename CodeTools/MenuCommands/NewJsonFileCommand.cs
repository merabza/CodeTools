//Created by NewTaskCommandCreator at 11/3/2025 5:54:44 PM

using System;
using CliMenu;
using CodeTools.Models;
using LibMenuInput;
using LibParameters;
using SystemToolsShared;

namespace CodeTools.MenuCommands;

// ReSharper disable once ConvertToPrimaryConstructor
public sealed class NewJsonFileCommand : CliMenuCommand
{
    private readonly ParametersManager _parametersManager;

    public NewJsonFileCommand(ParametersManager parametersManager) : base("New Json file")
    {
        _parametersManager = parametersManager;
    }

    protected override bool RunBody()
    {
        MenuAction = EMenuAction.Reload;
        var parameters = (CodeToolsParameters)_parametersManager.Parameters;

        //ამოცანის შექმნის პროცესი დაიწყო
        Console.WriteLine("Create new Json file record started");

        var newJsonFileName = MenuInputer.InputFilePath("New Json file Name", null);
        if (string.IsNullOrEmpty(newJsonFileName)) return false;

        //ახალი ამოცანის შექმნა და ჩამატება ამოცანების სიაში
        if (!parameters.AddJsonFileName(newJsonFileName))
        {
            StShared.WriteErrorLine($"record with Name {newJsonFileName} does not created", true);
            return false;
        }

        //პარამეტრების შენახვა (ცვლილებების გათვალისწინებით)
        _parametersManager.Save(parameters, "Create New Json Record Finished");
        return true;
    }
}