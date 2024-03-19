using Microsoft.AspNetCore.Identity;
using Finance.Application.Dto;
using Finance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Core.IDto;

namespace Finance.Application.Services.Interfaces
{
    public interface IRequestService : IService<Request,RequestDto,RequestDto>
    {
        Task<PagingResultDto<RequestDto>> GetAll(RequestPagingInputDto pagingInputDto);
    }
}
