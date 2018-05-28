using Jal.Converter.Impl;
using Jal.Converter.Interface;
using Jal.Converter.Tests.Impl;
using Jal.Converter.Tests.Model;
using Jal.Locator.Impl;
using NUnit.Framework;
using Shouldly;

namespace Jal.Converter.Tests.Default
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("Name", 19)]
        [TestCase("A", 10000)]
        [TestCase("_", 999)]
        public void Convert_WithCustomerRequestToCustomer_ShouldBe(string name, int age)
        {
            var locator = new ServiceLocator();

            locator.Register(typeof(IConverter<CustomerRequest, Customer>), new CustomerRequestCustomerConverter());

            var sut = ModelConverter.Create(locator);

            var customerRequest = new CustomerRequest
            {
                Name = name,
                Age = age
            };

            var customer = sut.Convert<CustomerRequest, Customer>(customerRequest);

            customer.Name.ShouldBe(customerRequest.Name);

            customer.Age.ShouldBe(customerRequest.Age);

            customer.Category.ShouldBe("None");
        }
    }
}

