using Microsoft.AspNetCore.Identity;

namespace OnlineStore.AuthServer.Data.Entities
{
    /// <summary>
    /// Provides a custom implementation of an Asp.Net identity user.
    /// Here we can provide additional custom properties
    /// </summary>
    public class ApplicationUser : IdentityUser { }
}
