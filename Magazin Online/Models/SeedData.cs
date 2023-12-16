using Magazin_Online.Data;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace Magazin_Online.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider
serviceProvider)
        {
            using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService
            <DbContextOptions<ApplicationDbContext>>()))
            {
                
                
                if (context.Roles.Any())
                {
                    
                    return; 
                }
                
                context.Roles.AddRange(
                new IdentityRole
                {
                    Id = "17ee7c66-59b2-4140-8cef-9caf2dd0a060",
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = "17ee7c66-59b2-4140-8cef-9caf2dd0a061",
                    Name = "Contributor",
                    NormalizedName = "Contributor".ToUpper()
                },
                new IdentityRole
                {
                    Id = "17ee7c66-59b2-4140-8cef-9caf2dd0a062",
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                },
                new IdentityRole
                {
                    Id = "17ee7c66-59b2-4140-8cef-9caf2dd0a063",
                    Name = "Guest",
                    NormalizedName = "Guest".ToUpper()
                }

                );
               
                var hasher = new PasswordHasher<ApplicationUser>();
               
                context.Users.AddRange(
                new ApplicationUser
                {
                    Id = "692fc6bd-871e-4fb2-893e-f7213727f9b0",
                    // primary key
                    UserName = "admin@test.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "ADMIN@TEST.COM",
                    Email = "admin@test.com",
                    NormalizedUserName = "ADMIN@TEST.COM",
                    PasswordHash = hasher.HashPassword(null, "Admin1!")
                },
                new ApplicationUser
                {
                    Id = "692fc6bd-871e-4fb2-893e-f7213727f9b1",
                    // primary key
                    UserName = "contributor@test.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "CONTRIBUTORR@TEST.COM",
                    Email = "contributor@test.com",
                    NormalizedUserName = "CONTRIBUTOR@TEST.COM",
                    PasswordHash = hasher.HashPassword(null, "Contributor1!")
                },

                new ApplicationUser
                {

                    Id = "692fc6bd-871e-4fb2-893e-f7213727f9b2",
                    // primary key
                    UserName = "user@test.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "USER@TEST.COM",
                    Email = "user@test.com",
                    NormalizedUserName = "USER@TEST.COM",
                    PasswordHash = hasher.HashPassword(null, "User1!")
                },
                new ApplicationUser 
                {
                    Id = "692fc6bd-871e-4fb2-893e-f7213727f9b3", 
                    UserName = "guest",
                    Email = "",
                    EmailConfirmed = true, 
                    NormalizedEmail = "",
                    NormalizedUserName = "GUEST",
                    PasswordHash = "",
                }
                );

                // ASOCIEREA USER-ROLE
                context.UserRoles.AddRange(
                new IdentityUserRole<string>
                {

                    RoleId = "17ee7c66-59b2-4140-8cef-9caf2dd0a060",
                    UserId = "692fc6bd-871e-4fb2-893e-f7213727f9b0"
                },

                new IdentityUserRole<string>
                {
                    RoleId = "17ee7c66-59b2-4140-8cef-9caf2dd0a061",
                    UserId = "692fc6bd-871e-4fb2-893e-f7213727f9b1"
                },

                new IdentityUserRole<string>
                {
                    RoleId = "17ee7c66-59b2-4140-8cef-9caf2dd0a062",
                    UserId = "692fc6bd-871e-4fb2-893e-f7213727f9b2"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "17ee7c66-59b2-4140-8cef-9caf2dd0a063",
                    UserId = "692fc6bd-871e-4fb2-893e-f7213727f9b3"
                }
                );
              
                context.SaveChanges();
            }
        }
    }
}
