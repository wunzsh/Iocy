using System;
using System.Collections.Generic;

using Iocy.Core.Exceptions;
using Iocy.Core.Registration;

namespace Iocy.Core
{
    public class ComponentRegistry
    {
        //TODO rewrite to concurrent collections
        private readonly Dictionary<Type, object> _resolved = new Dictionary<Type, object>();
        private readonly Dictionary<Type, IServiceRegistration> _registered = new Dictionary<Type, IServiceRegistration>();

        public ComponentRegistry(IServiceRegistration[] serviceRegistrations)
        {
            Process(serviceRegistrations);
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        private object Resolve(Type type)
        {
            if (_resolved.ContainsKey(type))
            {
                return _resolved[type];
            }

            IServiceRegistration registration;
            if (!_registered.TryGetValue(type, out registration))
            {
                throw new ServiceNotRegisteredException(type.ToString());
            }

            var obj = ResolveInternal(registration);
            _resolved.Add(type, obj);

            return obj;
        }

        private object ResolveInternal(IServiceRegistration registration)
        {
            var implType = registration.Implementation;
            var constructors = implType.GetConstructors();
            if (constructors.Length != 1)
            {
                throw new ServiceRegistrationException(implType, "Services with only one constructor supported");
            }

            var constructorInfo = constructors[0];
            var parameterInfos = constructorInfo.GetParameters();
            var dependOnParameters = registration.DependOnParameters;
            var parameters = new List<object>();
            for (var i = 0; i < parameterInfos.Length; i++)
            {
                var curParameterInfo = parameterInfos[i];
                object parameter;
                if (dependOnParameters.TryGetValue(curParameterInfo.Name, out parameter))
                {
                    parameters.Add(parameter);
                    continue;
                }

                parameter = Resolve(curParameterInfo.ParameterType);
                parameters.Add(parameter);
            }

            return constructorInfo.Invoke(parameters.ToArray());
        }

        private void Process(IServiceRegistration[] serviceRegistrations)
        {
            foreach (var registration in serviceRegistrations)
            {
                if (registration.Instance != null)
                {
                    _resolved.Add(registration.Interface, registration.Instance);
                    continue;
                }

                _registered.Add(registration.Interface, registration);
            }
        }
    }
}