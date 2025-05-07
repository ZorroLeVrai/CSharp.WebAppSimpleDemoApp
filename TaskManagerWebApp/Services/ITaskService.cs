using TaskManagerWebApp.Models;

namespace TaskManagerWebApp.Services
{
    public interface ITaskService
    {
        TaskItem? AddTask(string title, TaskItemStatus status, DateTime dueDate);
        IEnumerable<TaskItem> GetAllTasks();
        bool TryDeleteTask(int id);
        bool TryGetTaskById(int id, out TaskItem? taskItem);
        bool TryUpdateTask(int id, TaskItem dataToUpdate, out TaskItem? taskItem);
    }
}