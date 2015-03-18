using System;

namespace Iocy.Core.Exceptions
{
    public class ServiceRegistrationException : Exception
    {
        public ServiceRegistrationException(Type serviceType, string error)
            : base(serviceType +" "+error)
        {
        }
    }
}