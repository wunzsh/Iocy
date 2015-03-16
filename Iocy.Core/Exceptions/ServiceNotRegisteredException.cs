using System;

namespace Iocy.Core.Exceptions
{
    public class ServiceNotRegisteredException:Exception
    {
        public ServiceNotRegisteredException(string serviceType)
            : base(serviceType)
        {
        }
    }
}