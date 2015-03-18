using System;
using System.Collections.Generic;
using System.Linq;

using Iocy.Core.Exceptions;

namespace Iocy.Core
{
    public class ServiceRegistration<I>
    {
        private readonly IocyContainer _container;

        private Type _type;

        private readonly IDictionary<string, object> _parameters = new Dictionary<string, object>(); 

        public ServiceRegistration(IocyContainer container)
        {
            _container = container;
        }

        public ServiceRegistration<I> ImplementedBy<T>() where T : I
        {
            _type = typeof(T);
            return this;
        }

        public ServiceRegistration<I> DependsOn<PT>(string name, PT value)
        {
            _parameters.Add(name, value);
            return this;
        }

        public IocyContainer End()
        {
            var constructors = _type.GetConstructors();
            if (constructors.Length != 1)
            {
                throw new ServiceRegistrationException(_type, "Services with only one constructor supported");
            }

            var constructorInfo = constructors[0];
            var parameters = constructorInfo.GetParameters();
            if (parameters.Length != _parameters.Keys.Count)
            {
                throw new ServiceRegistrationException(_type, "Parameters don´t match");
            }

            var instance = constructorInfo.Invoke(parameters.Select(
                x =>
                    {
                        object parObj;
                        if (!_parameters.TryGetValue(x.Name, out parObj))
                        {
                            throw new ServiceRegistrationException(_type, "Not defined parameter " + x.Name);
                        }

                        return parObj;

                    }).ToArray());

            return _container.For((I)instance);

        }
    }
}