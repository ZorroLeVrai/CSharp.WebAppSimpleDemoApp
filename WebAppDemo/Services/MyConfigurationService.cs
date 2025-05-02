using Microsoft.Extensions.Options;
using WebAppDemo.Configuration;

namespace WebAppDemo.Services;

public class MyConfigurationService : IMyConfigurationService
{
    public string Key { get; }
    public bool IsFlagged { get; }
    public int Number { get; }

    public MyConfigurationService(IOptions<AppSettings> options)
    {
        var appSettings = options.Value;
        Key = appSettings.Key ?? string.Empty;
        IsFlagged = appSettings.IsFlagged;
        Number = appSettings.Number;
    }
}
