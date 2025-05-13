namespace WebAppDemo.Services;

/// <summary>
/// Interface for user authentication service.
/// </summary>
public interface IUserService
{
    bool ValidateUser(string email, string password);
}
