using Identity.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistence.Database.Configuration
{
    public class ApplicationRoleConfiguration
    {
        public ApplicationRoleConfiguration(EntityTypeBuilder<ApplicationRole> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.HasMany(x=>x.UserRoles)
                .WithOne(x=>x.Role)
                .HasForeignKey(x=>x.RoleId)
                .IsRequired();

            SeedData(entityBuilder);
        }

        private void SeedData(EntityTypeBuilder entityTypeBuilder)
        {
            var role = new ApplicationRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            entityTypeBuilder.HasData(role);
        }
    }
}
