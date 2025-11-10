using LW4_Task2_MiA.Models;
using System.Security.Claims;

namespace LW4_Task6_MiA.Service
{
    public interface ITokenService
    {
        string CreateAccessToken(User user);
        string CreateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
