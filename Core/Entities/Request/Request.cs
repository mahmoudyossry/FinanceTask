using Finance.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Core.Entities
{
    public class Request :FullAuditEntity
    {
        public string Number { get; set; }
        public double PaymentAmount { get; set; }
        public double TotalProfit { get; set; }
        public int PeriodInMonth { get; set; }
        public long StatusId { get; set; }
        [ForeignKey("StatusId")]
        public RequestStatus RequestStatus { get; set; }

    }
}
