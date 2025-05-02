namespace WebAppDemo.Services;

public interface IMyConfigurationService
{
    string Key { get; }
    bool IsFlagged { get; }
    int Number { get; }
}
