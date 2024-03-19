using Finance.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Services.Interfaces
{
    public interface IService<TEntity, TDto, TGetAllDto> 
        where TEntity : class
        where TDto : class
        where TGetAllDto : class
    {
        Task<PagingResultDto<TGetAllDto>> GetAll(PagingInputDto pagingInputDto);
        Task<TDto> GetById(long id);
        Task Delete(long id);
        Task<TDto> Create(TDto input);
        Task<TDto> Update(TDto input);
    }

    public interface IService<TEntity, TDto, TUpdatDto, TGetAllDto>
    where TEntity : class
    where TDto : class
    where TUpdatDto : class
    where TGetAllDto : class
    {
        Task<PagingResultDto<TGetAllDto>> GetAll(PagingInputDto pagingInputDto);
        Task<TDto> GetById(long id);
        Task Delete(long id);
        Task<TDto> Create(TDto input);
        Task<TDto> Update(TUpdatDto input);
    }
}
