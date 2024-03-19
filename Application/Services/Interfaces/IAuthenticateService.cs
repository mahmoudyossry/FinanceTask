using Microsoft.AspNetCore.Identity;
using Finance.Application.Dto;
using Finance.Application.Dto;
using Finance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Services.Interfaces
{
    public interface IAuthenticateService
    {
        Task<object> Login(LoginDto model);
    }
}
