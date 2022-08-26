using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "54864d00-e632-42cf-a8ee-1cfff1d5d90e",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "admin@localhost.com",
                    Nombre = "Ciro",
                    Apellido = "Daniele",
                    UserName = "yosoyciro",
                    NormalizedUserName = "yosoyciro",
                    PasswordHash = hasher.HashPassword(null, "Chiar@3004"),
                    EmailConfirmed = true,
                },
                new ApplicationUser
                {
                    Id = "0276c349-55d7-425f-9901-bfb19f119459",
                    Email = "cdaniele@localhost.com",
                    NormalizedEmail = "cdaniele@localhost.com",
                    Nombre = "Ciro",
                    Apellido = "Daniele",
                    UserName = "cdaniele",
                    NormalizedUserName = "cdaniele",
                    PasswordHash = hasher.HashPassword(null, "Chiar@3004"),
                    EmailConfirmed = true,
                }
            );
        }
    }
}
