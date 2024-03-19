using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Finance.Application.Dto;
using Finance.Application.Mapper;
using Finance.Application.Services.Interfaces;
using Finance.Core;
using Finance.Core.Entities.Base;
using Finance.Core.Global;
using Finance.Core.Repositories;
using Finance.Core.UnitOfWork;
using Finance.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Web;

namespace Finance.Application.Services
{
    public class UserService : IUserService
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly ApplicationUserManager userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRoleRepository<IdentityUserRole<string>> userRole;
        private readonly GlobalInfo globalInfo;
        private readonly IPasswordHasher<ApplicationUser> passwordHasher;
        private readonly AppSettingsConfiguration appSettings;

        public UserService(ApplicationUserManager userManager
            , RoleManager<ApplicationRole> roleManager
            , IUnitOfWork unitOfWork
            , IUserRoleRepository<IdentityUserRole<string>> userRole
            , GlobalInfo globalInfo
            ,IPasswordHasher<ApplicationUser> passwordHasher
            ,AppSettingsConfiguration appSettings
            )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.unitOfWork = unitOfWork;
            this.userRole = userRole;
            this.globalInfo = globalInfo;
            this.passwordHasher = passwordHasher;
            this.appSettings = appSettings;
        }
        public virtual async Task<PagingResultDto<UserAllDto>> GetAll(UserPagingInputDto PagingInputDto)
        {
            var query = userManager.Users.AsQueryable();


            var order = query.OrderBy(PagingInputDto.OrderByField + " " + PagingInputDto.OrderType);

            var page = order.Skip((PagingInputDto.PageNumber * PagingInputDto.PageSize) - PagingInputDto.PageSize).Take(PagingInputDto.PageSize);

            var total = await query.CountAsync();

            var list = MapperObject.Mapper
                .Map<IList<UserAllDto>>(await page.ToListAsync());

            var response = new PagingResultDto<UserAllDto>
            {
                Result = list,
                Total = total
            };

            return response;
        }
   

        public async Task<UserDto> GetById(string id)
        {
            var entity = await userManager.Users
                .Include(x => x.UserRoles)
                .FirstOrDefaultAsync(x => x.Id == id);
            var response = MapperObject.Mapper.Map<UserDto>(entity);

            return response;
        }
        public async Task<UserDto> GetByIdWithoutChildren(string id)
        {
            var entity = await userManager.Users
                .FirstOrDefaultAsync(x => x.Id == id);
            var response = MapperObject.Mapper.Map<UserDto>(entity);

            return response;
        }

   

        public async Task<UserDto> Create(UserDto input)
        {
            var userExists = await userManager.GetUserByEmailAsync(input.Email);
            if (userExists != null)
                throw new AppException(ExceptionEnum.RecordAlreadyExist);
            
            var user = MapperObject.Mapper.Map<ApplicationUser>(input);
            user.UserName = input.Email;
            unitOfWork.BeginTran();

            //saving user
            var result = await userManager.CreateAsync(user, input.Password);
            if (!result.Succeeded)
                throw new AppException(ExceptionEnum.RecordCreationFailed);
            input.Id=user.Id;
            var userRoles = new List<IdentityUserRole<string>>()
            { new IdentityUserRole<string> { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = user.Id } };

            await userRole.AddRangeAsync(userRoles);

            await unitOfWork.CompleteAsync();
            unitOfWork.CommitTran();
            input.Password = "";
            return input;
        }



        public async Task<SessionDto> GetUserSession()
        {
            var (user, permissions) = await userManager.GetUserSession(globalInfo.UserId);

            var response = MapperObject.Mapper.Map<SessionDto>(user);

            response.Permissions = permissions;

            return response;
        }
        public async Task<UserAllDto[]> GetUsersPermission(string permission)
        {
            var users= await userManager.GetUsersPermission(permission);
            var response = MapperObject.Mapper.Map<UserAllDto[]>(users);
            return response;
        }        
   
    }
}
