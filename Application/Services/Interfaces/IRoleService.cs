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
    public interface IRoleService
    {
        Task<PagingResultDto<RoleDto>> GetAll(PagingInputDto pagingInputDto);
        Task<RoleDto> GetById(string id);
        Task Delete(string id);
        Task<RoleDto> Create(RoleDto input);

        Task<IList<PermissionDto>> GetAllPermissions();
        Task<IList<PermissionTreeDto>> GetAllPermissionsTree();
    }
}
