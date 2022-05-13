using Core.Domain;

namespace Manager.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
