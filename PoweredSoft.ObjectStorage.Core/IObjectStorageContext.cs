using System;

namespace PoweredSoft.ObjectStorage.Core
{
    public interface IObjectStorageContext
    {
        IObjectStorageCollection<TEntity> GetCollection<TEntity>();
    }
}
