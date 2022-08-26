using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "9beb5e04-622a-45c1-8944-4b8eadad0f40",
                    UserId = "54864d00-e632-42cf-a8ee-1cfff1d5d90e"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "0d3a0d1a-e678-4634-a835-148a8bfdef1f",
                    UserId = "0276c349-55d7-425f-9901-bfb19f119459"
                }
            );
        }
    }
}
