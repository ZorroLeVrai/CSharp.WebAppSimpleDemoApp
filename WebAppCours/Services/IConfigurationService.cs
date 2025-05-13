namespace WebAppCours.Services;

public interface IConfigurationService
{
    bool IsFlagged { get; }
    string Key { get; }
    int Number { get; }
}