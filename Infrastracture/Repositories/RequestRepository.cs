using Finance.Core.Entities;
using Finance.Core.IDto;
using Finance.Core.Repositories;
using Finance.Infrastructure.Data;
using Finance.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
namespace Finance.Infrastructure.Repositories
{
	public class RequestRepository : Repository<Request>, IRequestRepository
	{
		private readonly DbSet<Request> dbSet;

		public RequestRepository(FinanceContext context) : base(context)
		{
			dbSet = context.Set<Request>();
		}

		public Task<Request> GetByIdWithAllChildren(long id)
		{
			return dbSet
				.Include(x => x.RequestStatus)
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<bool> IsNumberValid(string number)
		{
			var last = await dbSet.FirstOrDefaultAsync(j => j.Number.Equals(number));
			return last == null ? true : false;
		}


		public async Task<Tuple<ICollection<Request>, int>> GetAllAsync(
			IRequestPagingInputDto RequestPagingInputDto)
		{
			var query = context.Set<Request>()
                .Include(x => x.RequestStatus)
				.AsQueryable();

            if (RequestPagingInputDto.StatusId > 0)
                query = query.Where(x => x.StatusId == RequestPagingInputDto.StatusId);
			
			if (!string.IsNullOrEmpty(RequestPagingInputDto.Number))
                query = query.Where(x => x.Number.Contains(RequestPagingInputDto.Number));

            var order = query.OrderBy(RequestPagingInputDto.OrderByField + " " + RequestPagingInputDto.OrderType);

            var page = order.Skip((RequestPagingInputDto.PageNumber * RequestPagingInputDto.PageSize) - RequestPagingInputDto.PageSize)
				.Take(RequestPagingInputDto.PageSize);
					

			var total = await query.CountAsync();

			return new Tuple<ICollection<Request>, int>(await page.ToListAsync(), total);
		}

		

        
    }
}

