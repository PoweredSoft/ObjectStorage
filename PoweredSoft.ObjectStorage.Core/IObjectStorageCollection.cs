using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PoweredSoft.ObjectStorage.Core
{
    public interface IObjectStorageCollection<TEntity>
    {
        string CollectionName { get; }

        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        IQueryable<TEntity> AsQueryable();
    }
}
