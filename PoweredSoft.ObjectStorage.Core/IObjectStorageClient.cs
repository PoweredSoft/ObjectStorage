using System;

namespace PoweredSoft.ObjectStorage.Core
{
    public interface IObjectStorageClient
    {
        IObjectStorageCollection<TEntity> GetCollection<TEntity>();
    }
}
