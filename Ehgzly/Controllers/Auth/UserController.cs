using Microsoft.AspNetCore.Mvc;
using Finance.Application.Dto;
using Finance.Application.Filters;
using Finance.Application.Services.Interfaces;


namespace Finance.API.Controllers
{
   // [AppAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    //[InputValidationActionFilter]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("[action]")]
       // [AppAuthorize(Permissions = "Request")]
        public async Task<PagingResultDto<UserAllDto>> GetAll([FromQuery] UserPagingInputDto pagingInputDto)
        {
            return await userService.GetAll(pagingInputDto);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("[action]")]
       // [AppAuthorize(Permissions = "User.Create")]
        public async Task<ActionResult<UserDto>> Create([FromBody] UserDto input)
        {
            return await userService.Create(input);
        }

    }
}
