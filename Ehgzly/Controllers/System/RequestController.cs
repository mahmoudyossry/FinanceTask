using Microsoft.AspNetCore.Mvc;
using Finance.Application.Dto;
using Finance.Application.Filters;
using Finance.Application.Services.Interfaces;
using Finance.Core.Entities;
using Finance.Application.Services;

namespace Finance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AppAuthorize]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService service;

        public RequestController(IRequestService service)
        {
            this.service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("[action]")]
        [AppAuthorize(Permissions = "Request")]
        public async Task<PagingResultDto<RequestDto>> GetAll([FromQuery] RequestPagingInputDto pagingInputDto)
        {
            return await service.GetAll(pagingInputDto);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("[action]")]
        [AppAuthorize(Permissions = "Request")]
        public async Task<RequestDto> Get(long id)
        {
            return await service.GetById(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("[action]")]
        [AppAuthorize(Permissions = "Request.Create")]
        public async Task<ActionResult<RequestDto>> Create([FromBody] RequestDto input)
        {

            return await service.Create(input);
            
        }

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[Route("[action]")]
        ////[AppAuthorize(Permissions = "Request.Update")]
        //public async Task Update([FromBody] RequestDto input)
        //{
        //    await service.Update(input);
        //}

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[Route("[action]")]
        ////[AppAuthorize(Permissions = "Request.Delete")]
        //public async Task Delete(long id)
        //{
        //    await service.Delete(id);
        //}

    }
}
