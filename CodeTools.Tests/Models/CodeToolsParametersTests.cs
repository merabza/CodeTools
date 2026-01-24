using CodeTools.Models;

namespace CodeTools.Tests.Models;

public sealed class CodeToolsParametersTests
{
    [Fact]
    public void CheckBeforeSave_ShouldReturnTrue()
    {
        // Arrange
        var parameters = new CodeToolsParameters();

        // Act
        var result = parameters.CheckBeforeSave();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetTask_WhenTaskExists_ShouldReturnTask()
    {
        // Arrange
        var parameters = new CodeToolsParameters();
        var taskName = "TestTask";
        var task = new TaskModel();
        parameters.Tasks[taskName] = task;

        // Act
        var result = parameters.GetTask(taskName);

        // Assert
        Assert.NotNull(result);
        Assert.Same(task, result);
    }

    [Fact]
    public void GetTask_WhenTaskDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var parameters = new CodeToolsParameters();

        // Act
        var result = parameters.GetTask("NonExistentTask");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void CheckNewTaskNameValid_WhenOldAndNewNamesAreSame_ShouldReturnTrue()
    {
        // Arrange
        var parameters = new CodeToolsParameters();
        var taskName = "TestTask";
        parameters.Tasks[taskName] = new TaskModel();

        // Act
        var result = parameters.CheckNewTaskNameValid(taskName, taskName);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CheckNewTaskNameValid_WhenOldTaskDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var parameters = new CodeToolsParameters();

        // Act
        var result = parameters.CheckNewTaskNameValid("NonExistentTask", "NewTask");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CheckNewTaskNameValid_WhenNewNameDoesNotExist_ShouldReturnTrue()
    {
        // Arrange
        var parameters = new CodeToolsParameters();
        var oldTaskName = "OldTask";
        parameters.Tasks[oldTaskName] = new TaskModel();

        // Act
        var result = parameters.CheckNewTaskNameValid(oldTaskName, "NewTask");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CheckNewTaskNameValid_WhenNewNameAlreadyExists_ShouldReturnFalse()
    {
        // Arrange
        var parameters = new CodeToolsParameters();
        var oldTaskName = "OldTask";
        var newTaskName = "NewTask";
        parameters.Tasks[oldTaskName] = new TaskModel();
        parameters.Tasks[newTaskName] = new TaskModel();

        // Act
        var result = parameters.CheckNewTaskNameValid(oldTaskName, newTaskName);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void RemoveTask_WhenTaskExists_ShouldReturnTrueAndRemoveTask()
    {
        // Arrange
        var parameters = new CodeToolsParameters();
        var taskName = "TestTask";
        parameters.Tasks[taskName] = new TaskModel();

        // Act
        var result = parameters.RemoveTask(taskName);

        // Assert
        Assert.True(result);
        Assert.False(parameters.Tasks.ContainsKey(taskName));
    }

    [Fact]
    public void RemoveTask_WhenTaskDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var parameters = new CodeToolsParameters();

        // Act
        var result = parameters.RemoveTask("NonExistentTask");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddTask_WhenTaskDoesNotExist_ShouldReturnTrueAndAddTask()
    {
        // Arrange
        var parameters = new CodeToolsParameters();
        var taskName = "TestTask";
        var task = new TaskModel();

        // Act
        var result = parameters.AddTask(taskName, task);

        // Assert
        Assert.True(result);
        Assert.True(parameters.Tasks.ContainsKey(taskName));
        Assert.Same(task, parameters.Tasks[taskName]);
    }

    [Fact]
    public void AddTask_WhenTaskAlreadyExists_ShouldReturnFalse()
    {
        // Arrange
        var parameters = new CodeToolsParameters();
        var taskName = "TestTask";
        var existingTask = new TaskModel();
        var newTask = new TaskModel();
        parameters.Tasks[taskName] = existingTask;

        // Act
        var result = parameters.AddTask(taskName, newTask);

        // Assert
        Assert.False(result);
        Assert.Same(existingTask, parameters.Tasks[taskName]);
    }

    [Fact]
    public void AddJsonFileName_WhenFileNameIsValid_ShouldReturnTrueAndAddFileName()
    {
        // Arrange
        var parameters = new CodeToolsParameters();
        var fileName = "test.json";

        // Act
        var result = parameters.AddJsonFileName(fileName);

        // Assert
        Assert.True(result);
        Assert.Contains(fileName, parameters.JsonFilesForSortPaths);
    }

    [Fact]
    public void AddJsonFileName_WhenFileNameAlreadyExists_ShouldReturnFalse()
    {
        // Arrange
        var parameters = new CodeToolsParameters();
        var fileName = "test.json";
        parameters.JsonFilesForSortPaths.Add(fileName);

        // Act
        var result = parameters.AddJsonFileName(fileName);

        // Assert
        Assert.False(result);
        Assert.Single(parameters.JsonFilesForSortPaths);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void AddJsonFileName_WhenFileNameIsNullOrWhiteSpace_ShouldReturnFalse(string? fileName)
    {
        // Arrange
        var parameters = new CodeToolsParameters();

        // Act
        var result = parameters.AddJsonFileName(fileName!);

        // Assert
        Assert.False(result);
        Assert.Empty(parameters.JsonFilesForSortPaths);
    }

    [Fact]
    public void Tasks_ShouldBeInitializedAsEmptyDictionary()
    {
        // Arrange & Act
        var parameters = new CodeToolsParameters();

        // Assert
        Assert.NotNull(parameters.Tasks);
        Assert.Empty(parameters.Tasks);
    }

    [Fact]
    public void JsonFilesForSortPaths_ShouldBeInitializedAsEmptyList()
    {
        // Arrange & Act
        var parameters = new CodeToolsParameters();

        // Assert
        Assert.NotNull(parameters.JsonFilesForSortPaths);
        Assert.Empty(parameters.JsonFilesForSortPaths);
    }

    [Fact]
    public void LogFolder_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var parameters = new CodeToolsParameters();

        // Assert
        Assert.Null(parameters.LogFolder);
    }

    [Fact]
    public void LogFolder_ShouldBeSettable()
    {
        // Arrange
        var parameters = new CodeToolsParameters();
        var logFolder = "C:\\Logs";

        // Act
        parameters.LogFolder = logFolder;

        // Assert
        Assert.Equal(logFolder, parameters.LogFolder);
    }
}
