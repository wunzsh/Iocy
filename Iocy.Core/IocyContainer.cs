using System;
using System.Collections.Concurrent;

using Iocy.Core.Exceptions;
using Iocy.Core.Registration;

namespace Iocy.Core
{
    public class IocyContainer
    {
        private readonly ComponentRegistry _componentRegistry;

        public IocyContainer(IServiceRegistration[] serviceRegistrations)
        {
            _componentRegistry = new ComponentRegistry(serviceRegistrations);
        }

        public T Reslove<T>()
        {
            return _componentRegistry.Resolve<T>();
        }
    }
}
