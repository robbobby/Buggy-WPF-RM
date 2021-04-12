using System.Threading.Tasks;
using BRMWindowsUI.Models;
namespace BRMFrontEnd.Library.Api {
    public interface IApiHelper {
        Task<AuthenticatedUser> Authenticate(string userName, string password);
        Task GetAndSetLoggedInUserInfo(string token);
    }
}
