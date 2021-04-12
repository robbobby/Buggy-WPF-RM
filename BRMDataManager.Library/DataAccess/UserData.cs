using System.Collections.Generic;
using BRMDataManager.Library.Internal.DataAccess;
using BRMDataManager.Library.Models;
namespace BRMDataManager.Library.DataAccess {
    public class UserData {
        public List<UserModel> GetUserById(string id) {
            SqlDataAccess sqlDataAccess = new SqlDataAccess();
            var objectWithId = new {
                Id = id
            };
            List<UserModel> output = sqlDataAccess.LoadData<UserModel, dynamic>("dbo.spUserLookUp", objectWithId, "BRMData");

            return output;
        } 
    }
}
