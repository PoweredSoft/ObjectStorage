using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;

namespace PoweredSoft.ObjectStorage.MongoDB.AspNetCore
{
    public class ObjectIdModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType == typeof(ObjectId))
                return new ObjectIdModelBinder();
            return null;
        }
    }
}
