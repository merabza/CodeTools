//Created by ProjectParametersClassCreator at 11/3/2025 5:54:44 PM

using LibParameters;
using System.Collections.Generic;

namespace CodeTools.Models;

public sealed class CodeToolsParameters : IParameters
{
    public string? LogFolder { get; set; }
    public Dictionary<string, TaskModel> Tasks { get; set; } = [];

    public bool CheckBeforeSave()
    {
        return true;
    }

    public TaskModel? GetTask(string taskName)
    {
        return Tasks.GetValueOrDefault(taskName);
    }

    public bool CheckNewTaskNameValid(string oldTaskName, string newTaskName)
    {
        if (oldTaskName == newTaskName)
        {
            return true;
        }

        if (!Tasks.ContainsKey(oldTaskName))
        {
            return false;
        }

        return !Tasks.ContainsKey(newTaskName);
    }

    public bool RemoveTask(string taskName)
    {
        if (!Tasks.ContainsKey(taskName))
        {
            return false;
        }

        Tasks.Remove(taskName);
        return true;
    }

    public bool AddTask(string newTaskName, TaskModel task)
    {
        return Tasks.TryAdd(newTaskName, task);
    }
}