using System;
using CliMenu;
using CliParameters.CliMenuCommands;
using LibDataInput;
using Newtonsoft.Json.Linq;

namespace CodeTools.MenuCommands;

public sealed class JsonPropertyCliMenuCommand : CliMenuCommand
{
    private readonly JProperty _jProperty;

    public JsonPropertyCliMenuCommand(JProperty jProperty) : base(jProperty.Name, EMenuAction.LoadSubMenu)
    {
        _jProperty = jProperty;
    }

    protected override bool RunBody()
    {
        return true;
    }

    public override CliMenuSet GetSubMenu()
    {
        var jsonEditSubMenuSet = new CliMenuSet(Name);

        //var deleteTaskCommand = new JsonDeletePropertyCliMenuCommand(_jsonFileName);
        //jsonEditSubMenuSet.AddMenuItem(deleteTaskCommand);

        foreach (var cliMenuCommand in JsonCliMenuCommandFactory.Create(_jProperty.Value))
            jsonEditSubMenuSet.AddMenuItem(cliMenuCommand);

        //var jsonString = File.ReadAllText(_jsonFileName);
        //JObject jsonJObject = JObject.Parse(jsonString);

        //appSetJObject.Type

        //var deleteTaskCommand = new DeleteJsonFileRecordCliMenuCommand(_parametersManager, Name);
        //taskSubMenuSet.AddMenuItem(deleteTaskCommand);
        //taskSubMenuSet.AddMenuItem(new EditJsonFileNameNameCliMenuCommand(_parametersManager, Name));
        //taskSubMenuSet.AddMenuItem(new RunJsonSortCliMenuCommand(_logger, Name));
        //taskSubMenuSet.AddMenuItem(new RunJsonEditCliMenuCommand(_logger, Name));
        ////ეს საჭირო იქნება, თუ ამ მენიუში საჭირო გახდება ამოცანის დამატებითი რედაქტორების შექმნა
        //var parameters = (CodeToolsParameters)_parametersManager.Parameters;
        //var task = parameters.GetTask(Name);
        var key = ConsoleKey.Escape.Value().ToLower();
        jsonEditSubMenuSet.AddMenuItem(key, new ExitToMainMenuCliMenuCommand("Exit to level up menu", null),
            key.Length);
        return jsonEditSubMenuSet;
    }
}