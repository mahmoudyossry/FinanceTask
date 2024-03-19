using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Finance.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.DataConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        private readonly IPasswordHasher<ApplicationUser> passwordHasher;

        public UserConfiguration(IPasswordHasher<ApplicationUser> passwordHasher)
        {
            this.passwordHasher = passwordHasher;
        }

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");

            ApplicationUser user = new ApplicationUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                FirstName="mahmoud",
                LastName="yossry",
                UserName = "admin@default.com",
                NormalizedUserName = "ADMIN@DEFAULT.COM",
                Email = "admin@default.com",
                NormalizedEmail = "ADMIN@DEFAULT.COM",
                LockoutEnabled = false,
                PhoneNumber = "1234567890",
                //PasswordHash = passwordHasher.HashPassword(null, "Changeme@123")
                PasswordHash = "AQAAAAEAACcQAAAAEP58XALsM2pt+nD29nW0WGXXHitS9TdlLwsP9srB665keuWPiXW6pNcN5p81uYEOqA==",
                SecurityStamp = "86e94f96-8995-4181-88e6-693df7c2bcfd",
                ConcurrencyStamp = "023eeac9-72a3-4e9d-bc8e-29ae4e2fcb68",

            };


            builder.HasData
            (
                user
            );
        }
    }

}
