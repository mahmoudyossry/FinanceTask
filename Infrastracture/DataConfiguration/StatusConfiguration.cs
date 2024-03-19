using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Finance.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Core.Entities;

namespace Finance.Infrastructure.DataConfiguration
{
    public class StatusConfiguration : IEntityTypeConfiguration<RequestStatus>
    {
        public void Configure(EntityTypeBuilder<RequestStatus> builder)
        {
            builder.ToTable("RequestStatus");
            builder.HasData
            (
                new RequestStatus()
                {
                   Id = 1,
                   NameAr="مؤهل للشروط",
                   NameEn= "Qualified"
                },
                new RequestStatus()
                {
                   Id = 2,
                   NameAr="غير مؤهل للشروط",
                   NameEn= "Disqualified"
                }

            );
        }
    }
}
