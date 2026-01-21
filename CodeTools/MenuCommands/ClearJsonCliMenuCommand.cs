//Created by DeleteTaskCommandCreator at 11/3/2025 5:54:44 PM

using System.IO;
using AppCliTools.CliMenu;
using AppCliTools.LibDataInput;

namespace CodeTools.MenuCommands;

// ReSharper disable once ConvertToPrimaryConstructor
public sealed class ClearJsonCliMenuCommand : CliMenuCommand
{
    private readonly string _jsonFileName;

    public ClearJsonCliMenuCommand(string jsonFileName) : base("Delete Json File Record", EMenuAction.LevelUp)
    {
        _jsonFileName = jsonFileName;
    }

    protected override bool RunBody()
    {
        if (!Inputer.InputBool($"This will Delete All data from Json File {_jsonFileName}. are you sure ? ", false,
                false))
        {
            return false;
        }

        File.WriteAllText(_jsonFileName, "");

        return true;
    }
}
