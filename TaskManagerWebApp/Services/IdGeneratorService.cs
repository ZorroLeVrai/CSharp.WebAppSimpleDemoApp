namespace TaskManagerWebApp.Services;

public class IdGeneratorService : IIdGeneratorService
{
    private int taskId = 0;

    public int GenerateTaskId()
    {
        return ++taskId;
    }
}
