using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Core.Global
{
    public static class AppExceptions
    {
        public static IDictionary<int, string> ExceptionMessages = new Dictionary<int, string>()
        {
            {1, "Record Not Existed"},
            {2, "Model is not valid"},
            {3, "Not Authorized"},
            {4, "Property Not Access"},
            {5, "Wrong Credentials"},
            {6, "Request Number Exist"},
            {7, "MapperIssue"},
            {8, "Record Already Exist"},
            {9, "Record Creation Failed"},
        };
    }
}
