
using Finance.Core.Entities;
using Finance.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Finance.Application.Dto
{
    public class RequestDto : EntityDto
    {
        public string Number { get; set; }
        public double PaymentAmount { get; set; }
        public double TotalProfit { get; set; }
        public int PeriodInMonth { get; set; }
        public long StatusId { get; set; }
        public string StatusName { get; set; }

    }
}
