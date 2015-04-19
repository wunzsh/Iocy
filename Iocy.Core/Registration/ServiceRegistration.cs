using System;
using System.Collections.Generic;


namespace Iocy.Core.Registration
{
    public class ServiceRegistration<I> : IServiceRegistration
    {
        private Type _type;

        private readonly IDictionary<string, object> _dependOnParameters = new Dictionary<string, object>();

        private object _instance;

        public ServiceRegistration<I> For(I instance)
        {
            _type = typeof(I);
            _instance = instance;
            return this;
        }

        public ServiceRegistration<I> ImplementedBy<T>() where T : I
        {
            _type = typeof(T);
            return this;
        }

        public ServiceRegistration<I> DependsOn<PT>(string name, PT value)
        {
            _dependOnParameters.Add(name, value);
            return this;
        }

        public Type Implementation
        {
            get
            {
                return _type;
            }
        }

        public Type Interface
        {
            get
            {
                return typeof(I);
            }
        }

        public object Instance
        {
            get
            {
                return _instance;
            }
        }

        public IDictionary<string, object> DependOnParameters
        {
            get
            {
                return _dependOnParameters;
            }
        }
    }
}