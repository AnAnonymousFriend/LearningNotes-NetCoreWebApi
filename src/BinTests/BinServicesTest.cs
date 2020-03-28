using API.Core.IServices;
using NUnit.Framework;
using Xunit;

namespace BinTests
{
    public class BinServicesTest
    {
        private readonly IBinServices _binServices;
        public BinServicesTest(IBinServices BindvertisementServices)
        {
            this._binServices = BindvertisementServices;
        }

        [Test]
        public  void ShoulGetBinList()
        {
            var sut =  _binServices.Add(1,2);
            Assert.Equals(sut,3);

        }
    }
}