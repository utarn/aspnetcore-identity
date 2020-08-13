using Microsoft.AspNetCore.Identity;

namespace aspnetcore_identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}