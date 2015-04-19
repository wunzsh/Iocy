using Iocy.Core.Registration;
using Iocy.Core.Test.TestClasses;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iocy.Core.Test
{
    [TestClass]
    public class SimpleRegistrationTests
    {
        [TestMethod]
        public void RegisterInstance()
        {
            var service = new StupidService();
            var iocy = new IocyContainer(new[]
                                             {
                                                 new ServiceRegistration<IStupidService>().For(service)
                                             });

            var stupidService = iocy.Reslove<IStupidService>();
            Assert.AreEqual(service, stupidService);
        }

        [TestMethod]
        public void RegisterByType()
        {
            var iocy = new IocyContainer(new[]
                                             {
                                                 new ServiceRegistration<IStupidService>().ImplementedBy<StupidService>()
                                             });

            var resolved = iocy.Reslove<IStupidService>();
            Assert.AreEqual(typeof(StupidService), resolved.GetType());
        }

        [TestMethod]
        public void RegisterByTypeWithDepends()
        {
            var iocy = new IocyContainer(new[]
                                             {
                                                 new ServiceRegistration<IStupidService>().ImplementedBy<ParameterizedStupidService>()
                                                                                          .DependsOn("first", "test")
                                                                                          .DependsOn("second", 15)
                                             });


            var resolved = iocy.Reslove<IStupidService>();
            Assert.AreEqual(typeof(ParameterizedStupidService), resolved.GetType());
        }

        [TestMethod]
        public void RecursiveResolve()
        {
            var iocy = new IocyContainer(new IServiceRegistration[]
                                             {
                                                 new ServiceRegistration<IStupidService>().ImplementedBy<DependendOnAnotherStupidService>()
                                                                                          .DependsOn("first", "test")
                                                                                          .DependsOn("second", 15),
                                                 new ServiceRegistration<ISecondStupidService>().ImplementedBy<SecondStupidService>() 
                                             });


            var resolved = iocy.Reslove<IStupidService>();
            Assert.AreEqual(typeof(DependendOnAnotherStupidService), resolved.GetType());
        }
    }
}
