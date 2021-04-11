using System.Net.Http;
using System.Threading.Tasks;
using BRMWindowsUI.Models;
namespace BRMWindowsUI.Helpers {
    public interface IApiHelper {
        Task<AuthenticatedUser> Authenticate(string userName, string password);
    }
}
