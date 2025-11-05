using CliMenu;
using CliParameters.CliMenuCommands;
using CodeTools.Models;
using CodeTools.ToolActions;
using LibDataInput;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading;

namespace CodeTools.MenuCommands;

public class RunJsonEditCliMenuCommand : CliMenuCommand
{
    private readonly string _jsonFileName;
    private readonly ILogger _logger;

    public RunJsonEditCliMenuCommand(ILogger logger, string jsonFileName) : base("Run JSON Editor", EMenuAction.LoadSubMenu)
    {
        _logger = logger;
        _jsonFileName = jsonFileName;
    }

    protected override bool RunBody()
    {
        return true;
    }

    public override CliMenuSet GetSubMenu()
    {
        var jsonEditSubMenuSet = new CliMenuSet(Name);

        var deleteTaskCommand = new ClearJsonCliMenuCommand(_jsonFileName);
        jsonEditSubMenuSet.AddMenuItem(deleteTaskCommand);

        
        var jsonString = File.ReadAllText(_jsonFileName);
        JObject jsonJObject = JObject.Parse(jsonString);

        foreach (var cliMenuCommand in  JsonCliMenuCommandFactory.Create(jsonJObject))
        {
            jsonEditSubMenuSet.AddMenuItem(cliMenuCommand);
        }

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
        jsonEditSubMenuSet.AddMenuItem(key, new ExitToMainMenuCliMenuCommand("Exit to level up menu", null), key.Length);
        return jsonEditSubMenuSet;
    }
}


/*
     public enum JTokenType
    {
        /// <summary>
        /// No token type has been set.
        /// </summary>
        None = 0,

        /// <summary>
        /// A JSON object.
        /// </summary>
        Object = 1,

        /// <summary>
        /// A JSON array.
        /// </summary>
        Array = 2,

        /// <summary>
        /// A JSON constructor.
        /// </summary>
        Constructor = 3,

        /// <summary>
        /// A JSON object property.
        /// </summary>
        Property = 4,

        /// <summary>
        /// A comment.
        /// </summary>
        Comment = 5,

        /// <summary>
        /// An integer value.
        /// </summary>
        Integer = 6,

        /// <summary>
        /// A float value.
        /// </summary>
        Float = 7,

        /// <summary>
        /// A string value.
        /// </summary>
        String = 8,

        /// <summary>
        /// A boolean value.
        /// </summary>
        Boolean = 9,

        /// <summary>
        /// A null value.
        /// </summary>
        Null = 10,

        /// <summary>
        /// An undefined value.
        /// </summary>
        Undefined = 11,

        /// <summary>
        /// A date value.
        /// </summary>
        Date = 12,

        /// <summary>
        /// A raw JSON value.
        /// </summary>
        Raw = 13,

        /// <summary>
        /// A collection of bytes value.
        /// </summary>
        Bytes = 14,

        /// <summary>
        /// A Guid value.
        /// </summary>
        Guid = 15,

        /// <summary>
        /// A Uri value.
        /// </summary>
        Uri = 16,

        /// <summary>
        /// A TimeSpan value.
        /// </summary>
        TimeSpan = 17
    }
*/