using Microsoft.Extensions.Options;
using WebAppCours.Configuration;

namespace WebAppCours.Services;

public class ConfigurationService : IConfigurationService
{
    private readonly AppSettings _appSettings;

    public ConfigurationService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public string Key { get => _appSettings.Key; }
    public bool IsFlagged { get => _appSettings.IsFlagged; }
    public int Number { get => _appSettings.Number; }
}
