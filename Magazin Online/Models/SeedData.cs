using Magazin_Online.Data;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                // Verificam daca in baza de date exista cel putin un rol
                // insemnand ca a fost rulat codul
                // De aceea facem return pentru a nu insera rolurile inca o data
                // Acesta metoda trebuie sa se execute o singura data
                if (context.Roles.Any())
                {
                    return; // baza de date contine deja roluri
                }
                // CREAREA ROLURILOR IN BD
                // daca nu contine roluri, acestea se vor crea
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
                // o noua instanta pe care o vom utiliza pentru
                //crearea parolelor utilizatorilor
                // parolele sunt de tip hash
                var hasher = new PasswordHasher<ApplicationUser>();
                // CREAREA USERILOR IN BD
                // Se creeaza cate un user pentru fiecare rol
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
