using API.Core.IServices;
using API.Core.Services;
using NUnit.Framework;
using Xunit;

namespace Api.Tests
{
    public class BinServicesTest
    {
        private readonly IBinServices _binServices;
        public BinServicesTest(IBinServices BindvertisementServices) 
        {
            this._binServices = BindvertisementServices;
        }

        [Fact]
        public void ShoulGetBinList() 
        {
            var sut = _binServices.GetBinList();

      
        }

    }
}