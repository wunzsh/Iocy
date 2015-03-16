using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iocy.Core.Test
{
    [TestClass]
    public class SimpleRegistrationTests
    {
        [TestMethod]
        public void RegisterAndReslove()
        {
            var iocy = new IocyContainer();
            iocy.For<IStupidService>(new StupidService());

            var stupidService = iocy.Reslove<IStupidService>();
            Assert.AreEqual(42, stupidService.GetNumber());
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
