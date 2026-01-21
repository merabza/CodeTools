//Created by NewTaskCommandCreator at 11/3/2025 5:54:44 PM

using System;
using AppCliTools.CliMenu;
using AppCliTools.LibDataInput;
using CodeTools.Models;
using ParametersManagement.LibParameters;
using SystemTools.SystemToolsShared;

namespace CodeTools.MenuCommands;

// ReSharper disable once ConvertToPrimaryConstructor
public sealed class NewTaskCommand : CliMenuCommand
{
    private readonly ParametersManager _parametersManager;

    public NewTaskCommand(ParametersManager parametersManager) : base("New Task")
    {
        _parametersManager = parametersManager;
    }

    protected override bool RunBody()
    {
        MenuAction = EMenuAction.Reload;
        var parameters = (CodeToolsParameters)_parametersManager.Parameters;

        //ამოცანის შექმნის პროცესი დაიწყო
        Console.WriteLine("Create new Task started");

        var newTaskName = Inputer.InputText("New Task Name", null);
        if (string.IsNullOrEmpty(newTaskName))
        {
            return false;
        }

        //ახალი ამოცანის შექმნა და ჩამატება ამოცანების სიაში
        if (!parameters.AddTask(newTaskName, new TaskModel()))
        {
            StShared.WriteErrorLine($"Task with Name {newTaskName} does not created", true);
            return false;
        }

        //პარამეტრების შენახვა (ცვლილებების გათვალისწინებით)
        _parametersManager.Save(parameters, "Create New Task Finished");
        return true;
    }
}
