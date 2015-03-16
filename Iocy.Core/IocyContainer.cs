using System;
using System.Collections.Concurrent;

using Iocy.Core.Exceptions;

namespace Iocy.Core
{
    public class IocyContainer
    {
        private readonly ConcurrentDictionary<Type, object> _objects = new ConcurrentDictionary<Type, object>();

        public IocyContainer For<T>(T instance)
        {
            _objects.TryAdd(typeof(T), instance);
            return this;
        }
        public T Reslove<T>()
        {
            object value;
            if (_objects.TryGetValue(typeof(T), out value))
            {
                return (T)value;
            }

            throw new ServiceNotRegisteredException(typeof(T).Name);
        }
    }
}
