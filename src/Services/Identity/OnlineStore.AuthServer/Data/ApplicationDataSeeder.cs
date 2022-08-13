using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.AuthServer.Data.Entities;
using Serilog;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineStore.AuthServer.Data
{
    /// <summary>
    /// Provides methods to seed sample data to Asp.Net Identity related tables
    /// </summary>
    public class ApplicationDataSeeder
    {
        #region Fields

        private readonly IPasswordHasher<ApplicationUser> _hasher = new PasswordHasher<ApplicationUser>();

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a default user to the database
        /// </summary>
        /// <param name="context">Database context</param>
        /// <returns>Task object representing the asynchronous operation</returns>
        public async Task SeedUsersAsync(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                var user = new ApplicationUser
                {
                    UserName = "test1@user.com",
                    NormalizedUserName = "TEST1@USER.COM",
                    Email = "test@user.com",
                    NormalizedEmail = "TEST@USER.COM",
                    EmailConfirmed = true,
                    PhoneNumber = "1212121212",
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),

                };
                user.PasswordHash = _hasher.HashPassword(user, "Test123@");

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task SeedUsersRolesDataAsync(IServiceProvider serviceProvider)
        {
            //var services = new ServiceCollection();
            //using (var serviceProvider = services.BuildServiceProvider())
            //{
            //    using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //    {

            var roleMgr = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var buyerRole = roleMgr.FindByNameAsync("Buyer").Result;
            if (buyerRole == null)
            {
                buyerRole = new IdentityRole
                {
                    Name = "Buyer"
                };
                _ = roleMgr.CreateAsync(buyerRole).Result;
            }

            var managerRole = roleMgr.FindByNameAsync("Manager").Result;
            if (managerRole == null)
            {
                managerRole = new IdentityRole
                {
                    Name = "Manager"
                };
                _ = roleMgr.CreateAsync(managerRole).Result;
            }

            var userMgr = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var test1User = userMgr.FindByNameAsync("test1@user.com").Result;
            if (test1User == null)
            {
                test1User = new ApplicationUser
                {
                    UserName = "test1@user.com",
                    NormalizedUserName = "TEST1@USER.COM",
                    Email = "test1@user.com",
                    NormalizedEmail = "TEST1@USER.COM",
                    EmailConfirmed = true,
                    PhoneNumber = "1212121212",
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                };
                test1User.PasswordHash = _hasher.HashPassword(test1User, "Test123@");

                var result = userMgr.CreateAsync(test1User, "Test123@").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(test1User, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Test1"),
                            new Claim(JwtClaimTypes.GivenName, "Test1"),
                            new Claim(JwtClaimTypes.FamilyName, "Test1 Family Name"),
                            new Claim(JwtClaimTypes.WebSite, "http://test1.com"),
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                if (!userMgr.IsInRoleAsync(test1User, buyerRole.Name).Result)
                {
                    _ = userMgr.AddToRoleAsync(test1User, buyerRole.Name).Result;
                }

                Log.Debug("test1User created");
            }
            else
            {
                Log.Debug("test1User already exists");
            }

            var test2User = userMgr.FindByNameAsync("test2@user.com").Result;
            if (test2User == null)
            {
                test2User = new ApplicationUser
                {
                    UserName = "test2@user.com",
                    NormalizedUserName = "TEST2@USER.COM",
                    Email = "test2@user.com",
                    NormalizedEmail = "TEST2@USER.COM",
                    EmailConfirmed = true,
                    PhoneNumber = "2222222222",
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                };
                test2User.PasswordHash = _hasher.HashPassword(test2User, "Test123@");

                var result = userMgr.CreateAsync(test2User, "Test123@").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(test2User, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Test2"),
                            new Claim(JwtClaimTypes.GivenName, "Test2"),
                            new Claim(JwtClaimTypes.FamilyName, "Test2 Family Name"),
                            new Claim(JwtClaimTypes.WebSite, "http://test2.com"),
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                if (!userMgr.IsInRoleAsync(test2User, managerRole.Name).Result)
                {
                    _ = userMgr.AddToRoleAsync(test2User, managerRole.Name).Result;
                }

                Log.Debug("test2User created");
            }
            else
            {
                Log.Debug("test2User already exists");
            }
            //    }
            //}
        }

        #endregion
    }
}
