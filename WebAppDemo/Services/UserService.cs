using WebAppDemo.Configuration;

namespace WebAppDemo.Services;

/// <summary>
/// Implementation of user authentication service.
/// </summary>
public class UserService : IUserService
{
    private readonly UserConfig _userConfig;

    public UserService(UserConfig userConfig)
    {
        _userConfig = userConfig;
    }

    public bool ValidateUser(string email, string password)
    {
        // Exemple simple : valider un utilisateur avec des données en dur
        return email == _userConfig.Email && password == _userConfig.Password;
    }
}
