using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Core.Global
{
    public class GlobalInfo
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public static string lang { get; set; }

        public void SetValues( string UserName, string UserId,string lang)
        {
            this.UserName = UserName;
            this.UserId = UserId;
            GlobalInfo.lang = lang;
        }
    }
}
