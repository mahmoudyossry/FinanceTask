using Finance.Core.IDto;
using Finance.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Core.Repositories
{
    public interface IRolePermissionRepository<T> where T : class
    {
        Task<Tuple<ICollection<T>, int>> GetAllAsync(IPagingInputDto pagingInputDto);
        Task<T> GetByIdAsync(long id);
        Task<T> AddAsync(T entity);
        Task<IList<T>> AddRangeAsync(IList<T> entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(IEnumerable<T> entities);
        Task<string[]> GetRolePermissionsIdsByRoleID(string id);
        Task<List<T>> GetRemovedRolePermissionsIdsByRoleID(string id, string[] permissionIds);
    }
}
