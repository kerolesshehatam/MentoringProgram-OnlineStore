using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using OnlineStore.AuthServer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.String;

namespace OnlineStore.AuthServer.Services
{
    /// <summary>
    /// Provides an implementation of the <see cref="IProfileService"/> to add custom claims
    /// to the user. These claims will be available in the access token
    /// </summary>
    public class UserProfileService : IProfileService
    {
        #region Fields
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileService"/> class
        /// </summary>
        /// <param name="userManager">Asp.Net Identity user manager</param>
        public UserProfileService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        #endregion

        /// <inheritdoc /> 
        //public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        //{
        //    var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));
        //    var subClaimValue = subject.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

        //    var user = await _userManager.FindByIdAsync(subClaimValue);
        //    if (user == null)
        //        throw new ArgumentException("Invalid sub claim");

        //    // Note: Here we can add additional user claims as required
        //    // These claims will be available in the access token generated
        //    context.IssuedClaims = new List<Claim>
        //    {
        //        new Claim(JwtClaimTypes.Subject, user.Id),
        //        new Claim(JwtClaimTypes.PreferredUserName, user.UserName),
        //        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
        //    };
        //}

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            ApplicationUser user = await _userManager.FindByIdAsync(sub);
            ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

            List<Claim> claims = userClaims.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            if (_userManager.SupportsUserRole)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, roleName));
                    if (_roleManager.SupportsRoleClaims)
                    {
                        IdentityRole role = await _roleManager.FindByNameAsync(roleName);
                        if (role != null)
                        {
                            claims.AddRange(await _roleManager.GetClaimsAsync(role));
                        }
                    }
                }
            }

            context.IssuedClaims = claims;
        }

        /// <inheritdoc /> 
        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));
            var subClaimValue = subject.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

            var user = await _userManager.FindByIdAsync(subClaimValue);
            context.IsActive = false;

            if (user != null)
            {
                if (_userManager.SupportsUserSecurityStamp)
                {
                    var stamp = subject.Claims.FirstOrDefault(x => x.Type == "security_stamp")?.Value;
                    if (!IsNullOrWhiteSpace(stamp))
                    {
                        var securityStampFromDatabase = await _userManager.GetSecurityStampAsync(user);
                        if (stamp != securityStampFromDatabase)
                            return;
                    }
                }

                context.IsActive = !user.LockoutEnabled || !user.LockoutEnd.HasValue || user.LockoutEnd < DateTime.UtcNow;
            }
        }
    }
}
