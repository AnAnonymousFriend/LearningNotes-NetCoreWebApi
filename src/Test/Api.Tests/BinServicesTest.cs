using API.Core.IServices;
using API.Core.Services;
using NUnit.Framework;

namespace Api.Tests
{
    public class BinServicesTest
    {
       

        [Test]
        public void ShoulGetBinList()
        {
            var sut = new Class1();
             var act = sut.Add(1, 2);
            Assert.AreEqual(act, 3); ;

        }

    }
}