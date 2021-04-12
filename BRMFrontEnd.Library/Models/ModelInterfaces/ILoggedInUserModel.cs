using System;
namespace BRMFrontEnd.Library.Models.ModelInterfaces {
    public interface ILoggedInUserModel {
        string Token { get; set; }
        string Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string EmailAddress { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
