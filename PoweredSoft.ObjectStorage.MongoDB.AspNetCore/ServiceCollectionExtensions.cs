using System;
using Microsoft.Extensions.DependencyInjection;

namespace PoweredSoft.ObjectStorage.MongoDB.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddMongoDBMvcBinders(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddMvcOptions(options =>
            {
                options.ModelBinderProviders.Insert(0, new ObjectIdModelBinderProvider());
            });

            return mvcBuilder;
        }
    }
}
