using System;

namespace Iocy.Core
{
    public class ServiceRegistration<I>
    {
        private readonly IocyContainer _container;

        public ServiceRegistration(IocyContainer container)
        {
            _container = container;
        }

        public IocyContainer ImplementedBy<T>() where T : I
        {
            var service = Activator.CreateInstance<T>();
            return _container.For<I>(service);
        }
    }
}