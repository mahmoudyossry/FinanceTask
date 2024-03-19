using Finance.Core.Entities;
using Finance.Core.IDto;
using Finance.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Core.Repositories
{
    public interface IRequestRepository : IRepository<Request>
    {
        Task<bool> IsNumberValid( string number);
        Task<Tuple<ICollection<Request>, int>> GetAllAsync(IRequestPagingInputDto requestPagingInputDto);


    }
}