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
using Finance.Core.IDto;
using Finance.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Finance.Infrastructure.Repositories.Base;

namespace Finance.Application.Services
{
    public class RequestService : BaseService<Request, RequestDto, RequestDto>, IRequestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPasswordHasher<ApplicationUser> passwordHasher;

        public RequestService(
             IUnitOfWork unitOfWork, IPasswordHasher<ApplicationUser> passwordHasher
            ) :base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.passwordHasher = passwordHasher;
        }
        public  async Task<PagingResultDto<RequestDto>> GetAll(RequestPagingInputDto pagingInputDto)
        {
            var entities=await unitOfWork.Request.GetAllAsync(pagingInputDto);
            var list = Mapper.MapperObject.Mapper.Map<IList<RequestDto>>(entities.Item1);

            var response = new PagingResultDto<RequestDto>
            {
                Result = list,
                Total = entities.Item2
            };

            return response;
        }
        public override async Task<RequestDto> Create(RequestDto input)
        {
            if (input.PeriodInMonth < 1 || input.TotalProfit < 0 || input.PaymentAmount < 0)
                throw new AppException(ExceptionEnum.ModelNotValid);
            if(!await unitOfWork.Request.IsNumberValid(input.Number))
                throw new AppException(ExceptionEnum.RequestNumberExist);

            var entitiy = Mapper.MapperObject.Mapper.Map<Request>(input);
            if (entitiy is null)
            {
                throw new AppException(ExceptionEnum.MapperIssue);
            }

            var request = await unitOfWork.Request.AddAsync(entitiy);
            await unitOfWork.CompleteAsync();

            var response = Mapper.MapperObject.Mapper.Map<RequestDto>(request);
            return response;
        }
    }
}
