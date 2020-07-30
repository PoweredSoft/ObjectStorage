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
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateManyAsync<TField>(Expression<Func<TEntity, bool>> predicate, 
            Expression<Func<TEntity, TField>> fieldExpression, TField value, 
            CancellationToken cancellationToken = default);

        Task UpdateManyAsync<TField, TField2>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TField>> fieldExpression, TField value,
            Expression<Func<TEntity, TField2>> fieldExpression2, TField2 value2,
            CancellationToken cancellationToken = default);

        Task UpdateManyAsync<TField, TField2, TField3>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TField>> fieldExpression, TField value,
            Expression<Func<TEntity, TField2>> fieldExpression2, TField2 value2,
            Expression<Func<TEntity, TField3>> fieldExpression3, TField3 value3,
            CancellationToken cancellationToken = default);

        Task UpdateOneAsync<TField>(Expression<Func<TEntity, bool>> predicate, 
            Expression<Func<TEntity, TField>> fieldExpression, TField value,
            CancellationToken cancellationToken = default);
        Task UpdateOneAsync<TField, TField2>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TField>> fieldExpression, TField value,
            Expression<Func<TEntity, TField2>> fieldExpression2, TField2 value2,
            CancellationToken cancellationToken = default);
        Task UpdateOneAsync<TField, TField2, TField3>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TField>> fieldExpression, TField value,
            Expression<Func<TEntity, TField2>> fieldExpression2, TField2 value2,
            Expression<Func<TEntity, TField3>> fieldExpression3, TField3 value3,
            CancellationToken cancellationToken = default);
    }
}
