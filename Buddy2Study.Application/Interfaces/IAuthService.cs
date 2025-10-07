using System.Threading.Tasks;
using Buddy2Study.Application.Dtos;

namespace Buddy2Study.Application.Interfaces
{
    public interface IAuthService
    {
        // Normal user login
        Task<TokenDto> LoginAsync(string username, string password);

        
    }
}
