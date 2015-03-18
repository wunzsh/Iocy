namespace Iocy.Core.Test.TestClasses
{
    public class ParameterizedStupidService : IStupidService
    {
        private readonly string _first;

        private readonly int _second;

        public ParameterizedStupidService(string first, int second)
        {
            _first = first;
            _second = second;
        }

        public int GetNumber()
        {
            return _first.Length + _second;
        }
    }
}