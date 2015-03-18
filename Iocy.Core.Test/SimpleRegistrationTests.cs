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
            var iocy = new IocyContainer();
            var service = new StupidService();
            iocy.For<IStupidService>(service);

            var stupidService = iocy.Reslove<IStupidService>();
            Assert.AreEqual(service, stupidService);
        }

        [TestMethod]
        public void RegisterByType()
        {
            var iocy = new IocyContainer();
            
            iocy.For<IStupidService>().ImplementedBy<StupidService>().End();

            var resolved = iocy.Reslove<IStupidService>();
            Assert.AreEqual(typeof(StupidService), resolved.GetType());
        }

        [TestMethod]
        public void RegisterByTypeWithDepends()
        {
            var iocy = new IocyContainer();

            iocy.For<IStupidService>().ImplementedBy<ParameterizedStupidService>()
                                        .DependsOn("first", "test")
                                        .DependsOn("second", 15)
                                        .End();
            
            var resolved = iocy.Reslove<IStupidService>();
            Assert.AreEqual(typeof(ParameterizedStupidService), resolved.GetType());
        }
    }
}
