using Jal.Converter.Impl;
using Jal.Converter.Interface;
using Jal.Converter.Tests.Impl;
using Jal.Converter.Tests.Model;
using Jal.Locator.Interface;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Jal.Converter.Tests
{
    public class ConverterFactoryTests
    {
        [Test]
        public void Convert_With_ShouldNotBeNull()
        {
            var locator = new Mock<IServiceLocator>();

            locator.Setup(x=>x.Resolve<IConverter<CustomerRequest, Customer>>()).Returns(new CustomerRequestCustomerConverter());

            var sut = new ConverterFactory(locator.Object);

            var converter = sut.Create<CustomerRequest, Customer>();

            converter.ShouldNotBeNull();
        }


    }
}