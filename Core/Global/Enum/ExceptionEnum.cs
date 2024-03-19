using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Core.Global
{
    public enum ExceptionEnum
    {
        RecordNotExist = 1,
        ModelNotValid = 2,
        NotAuthorized = 3,
        PropertyNotAccess = 4,
        WrongCredentials = 5,
        RequestNumberExist = 6,
        MapperIssue=7,
        RecordAlreadyExist = 8,
        RecordCreationFailed = 9,
    }
}
