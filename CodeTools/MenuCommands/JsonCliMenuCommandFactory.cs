using System;
using System.Collections.Generic;
using System.Linq;
using AppCliTools.CliMenu;
using Newtonsoft.Json.Linq;

namespace CodeTools.MenuCommands;

public sealed class JsonCliMenuCommandFactory
{
    public static List<CliMenuCommand> Create(JToken jsonToken)
    {
        switch (jsonToken.Type)
        {
            case JTokenType.None:
                break;
            case JTokenType.Object:
                return ((JObject)jsonToken).Properties().Select(CliMenuCommand (s) => new JsonPropertyCliMenuCommand(s))
                    .ToList();
            case JTokenType.Array:
                break;
            case JTokenType.Constructor:
                break;
            case JTokenType.Property:
                break;
            case JTokenType.Comment:
                break;
            case JTokenType.Integer:
                break;
            case JTokenType.Float:
                break;
            case JTokenType.String:
                break;
            case JTokenType.Boolean:
                break;
            case JTokenType.Null:
                break;
            case JTokenType.Undefined:
                break;
            case JTokenType.Date:
                break;
            case JTokenType.Raw:
                break;
            case JTokenType.Bytes:
                break;
            case JTokenType.Guid:
                break;
            case JTokenType.Uri:
                break;
            case JTokenType.TimeSpan:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return [];
    }

    public static List<CliMenuCommand> Create(JObject jsonJObject)
    {
        switch (jsonJObject.Type)
        {
            case JTokenType.None:
                break;
            case JTokenType.Object:
                return jsonJObject.Properties().Select(CliMenuCommand (s) => new JsonPropertyCliMenuCommand(s))
                    .ToList();
            case JTokenType.Array:
                break;
            case JTokenType.Constructor:
                break;
            case JTokenType.Property:
                break;
            case JTokenType.Comment:
                break;
            case JTokenType.Integer:
                break;
            case JTokenType.Float:
                break;
            case JTokenType.String:
                break;
            case JTokenType.Boolean:
                break;
            case JTokenType.Null:
                break;
            case JTokenType.Undefined:
                break;
            case JTokenType.Date:
                break;
            case JTokenType.Raw:
                break;
            case JTokenType.Bytes:
                break;
            case JTokenType.Guid:
                break;
            case JTokenType.Uri:
                break;
            case JTokenType.TimeSpan:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return [];
    }
}
