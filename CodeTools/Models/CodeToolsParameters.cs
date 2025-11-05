//Created by ProjectParametersClassCreator at 11/3/2025 5:54:44 PM

using System.Collections.Generic;
using LibParameters;

namespace CodeTools.Models;

public sealed class CodeToolsParameters : IParameters
{
    public string? LogFolder { get; set; }
    public Dictionary<string, TaskModel> Tasks { get; set; } = [];
    public List<string> JsonFilesForSortPaths { get; set; } = [];

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
        if (oldTaskName == newTaskName) return true;

        if (!Tasks.ContainsKey(oldTaskName)) return false;

        return !Tasks.ContainsKey(newTaskName);
    }

    public bool RemoveTask(string taskName)
    {
        return Tasks.Remove(taskName);
    }

    public bool AddTask(string newTaskName, TaskModel task)
    {
        return Tasks.TryAdd(newTaskName, task);
    }

    public bool AddJsonFileName(string newJsonFileName)
    {
        if (string.IsNullOrWhiteSpace(newJsonFileName) || JsonFilesForSortPaths.Contains(newJsonFileName)) return false;
        JsonFilesForSortPaths.Add(newJsonFileName);
        return true;
    }
}