using TaskManagerWebApp.Models;

namespace TaskManagerWebApp.Services;

public class TaskService : ITaskService
{
    private readonly IIdGeneratorService _idGeneratorService;
    private readonly Dictionary<int, TaskItem> tasks = new();

    public TaskService(IIdGeneratorService idGeneratorService)
    {
        _idGeneratorService = idGeneratorService;
    }

    public IEnumerable<TaskItem> GetAllTasks()
    {
        return tasks.Values;
    }

    public bool TryGetTaskById(int id, out TaskItem? taskItem)
    {
        return tasks.TryGetValue(id, out taskItem);
    }

    public TaskItem AddTask(string title, TaskItemStatus status, DateTime dueDate)
    {
        var id = _idGeneratorService.GenerateTaskId();
        var task = new TaskItem(id, title, status, dueDate);
        tasks[id] = task;
        return task;
    }

    public bool TryUpdateTask(int id, TaskItem dataToUpdate, out TaskItem? taskItem)
    {
        if (tasks.ContainsKey(id))
        {
            taskItem = new TaskItem(id, dataToUpdate.Title, dataToUpdate.Status, dataToUpdate.DueDate);
            tasks[id] = taskItem;
            return true;
        }

        // Task not found
        taskItem = null;
        return false;
    }

    public bool TryDeleteTask(int id)
    {
        return tasks.Remove(id);
    }
}
