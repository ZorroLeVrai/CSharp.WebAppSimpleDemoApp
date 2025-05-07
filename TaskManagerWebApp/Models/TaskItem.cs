namespace TaskManagerWebApp.Models;

public class TaskItem
{
    public int Id { get; }
    public string Title { get; }
    public TaskItemStatus Status { get; }
    public DateTime DueDate { get; }

    public TaskItem(int id, string title, TaskItemStatus status, DateTime dueDate)
    {
        Id = id;
        Title = title;
        Status = status;
        DueDate = dueDate;
    }

    public override string ToString()
    {
        return $"[TaskItem: {Id}, Title: {Title}, Status: {Status}, DueDate: {DueDate}]";
    }
}
