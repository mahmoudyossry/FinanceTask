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
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.ToTable("Requests");
            builder.HasData
            (
                new Request()
                {
                   Id = 1,
                   CreateDate = DateTime.Now,
                   Number="#1",
                   PaymentAmount = 100,
                   PeriodInMonth = 3,
                   TotalProfit = 25,
                   StatusId = 1,
                },   new Request()
                {
                   Id = 2,
                   CreateDate = DateTime.Now,
                   Number="#2",
                   PaymentAmount = 120,
                   PeriodInMonth = 2,
                   TotalProfit = 30,
                   StatusId = 2,
                }


            );
        }
    }
}
