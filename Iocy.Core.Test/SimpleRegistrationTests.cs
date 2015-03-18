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
            Assert.AreEqual(service, stupidService.GetNumber());
        }

        [TestMethod]
        public void RegisterByType()
        {
            var iocy = new IocyContainer();
            
            iocy.For<IStupidService>().ImplementedBy<StupidService>();

            var resolved = iocy.Reslove<IStupidService>();
            Assert.AreEqual(typeof(StupidService), resolved.GetType());
        }
    }

    public interface IStupidService
    {
        int GetNumber();
    }

    class StupidService : IStupidService
    {
        public int GetNumber()
        {
            return 42;
        }
    }
}
