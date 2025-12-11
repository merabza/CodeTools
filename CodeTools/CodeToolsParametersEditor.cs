//Created by ProjectParametersEditorClassCreator at 11/3/2025 5:54:44 PM

using CliParameters;
using CliParameters.FieldEditors;
using CodeTools.Models;
using LibParameters;

namespace CodeTools;

public sealed class CodeToolsParametersEditor : ParametersEditor
{
    public CodeToolsParametersEditor(IParameters parameters, ParametersManager parametersManager) : base(
        "CodeTools Parameters Editor", parameters, parametersManager)
    {
        FieldEditors.Add(new FolderPathFieldEditor(nameof(CodeToolsParameters.LogFolder)));
    }
}