using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BRMDataManager.Library.DataAccess;
using BRMDataManager.Library.Models;
using Microsoft.AspNet.Identity;

namespace BRMDataManager.Controllers {
    [System.Web.Mvc.Authorize]
    public class UserController : ApiController {

        public UserModel GetById() {
            string userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();
            return data.GetUserById(userId).First();
        }
    }
}
