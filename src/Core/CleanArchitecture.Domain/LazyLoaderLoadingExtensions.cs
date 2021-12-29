using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain
{
    public static class LazyLoaderLoadingExtensions
    {
        public static TEntity Load<TEntity>(
            this Action<object, string> loader,
            object entity,
            ref TEntity navigationField,
            [CallerMemberName] string navigationName = null)
            where TEntity : class
        {
            loader?.Invoke(entity, navigationName);

            return navigationField;
        }
    }
}
