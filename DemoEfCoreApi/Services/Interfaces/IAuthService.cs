namespace DemoEfCoreApi.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(string username);
        bool ValidateUser(string username, string password);
    }
}
