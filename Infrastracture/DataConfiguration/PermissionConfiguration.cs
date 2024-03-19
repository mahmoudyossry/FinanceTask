using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Finance.Core.Entities.Authorization;

namespace Finance.Infrastructure.DataConfiguration
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");
            builder.HasData
            (
                new Permission()
                {
                    Id = 1,
                    Name = "Request",
                    Desc = "Request",
                    CategoryName = "test"
                },
                new Permission()
                {
                    Id = 2,
                    Name = "Request.Create",
                    Desc = "Request.Create",
                    CategoryName = "test"
                },
                new Permission()
                {
                    Id = 3,
                    Name = "User",
                    Desc = "User",
                    CategoryName = "test"
                },
                new Permission()
                {
                    Id = 4,
                    Name = "User.Create",
                    Desc = "User.Create",
                    CategoryName = "test"
                }

            );
        }
    }
}