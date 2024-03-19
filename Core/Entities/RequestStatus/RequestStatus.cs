using Finance.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Core.Entities
{
    public class RequestStatus :Entity
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
    }
}
