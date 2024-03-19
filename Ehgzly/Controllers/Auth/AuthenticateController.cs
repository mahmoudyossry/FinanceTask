using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Finance.Application.Dto;
using Finance.Application.Filters;
using Finance.Application.Middlewares;
using Finance.Application.Services;
using Finance.Application.Services.Interfaces;
using Finance.Core.Global;
using Finance.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Finance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService authenticateService;
        private readonly IUserService userService;

        public AuthenticateController(
            IAuthenticateService authenticateService,
            IUserService userService
            )
        {
            this.authenticateService = authenticateService;
            this.userService = userService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto model)
        {
            return Ok (await authenticateService.Login(model));
        }



    }
}
