using Microsoft.AspNetCore.Identity;
using Finance.Application.Dto;
using Finance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<PagingResultDto<UserAllDto>> GetAll(UserPagingInputDto pagingInputDto);
        Task<UserDto> GetById(string id);
        Task<UserDto> GetByIdWithoutChildren(string id);

        Task<UserDto> Create(UserDto input);

        Task<SessionDto> GetUserSession();
        Task<UserAllDto[]> GetUsersPermission(string permission);

    }
}
