using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace PoweredSoft.ObjectStorage.Core
{
    public interface IObjectStorageCollection<TEntity>
    {
        string CollectionName { get; }

        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> GetAsync(object key, CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        IQueryable<TEntity> AsQueryable();
        List<PropertyInfo> GetObjectKeys();
    }
}
