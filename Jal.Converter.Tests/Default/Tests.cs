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
        private IModelConverter _modelConverter;

        [SetUp]
        public void SetUp()
        {
            var serviceLocator = ServiceLocator.Builder.Create as ServiceLocator;

            serviceLocator.Register(typeof(IConverter<CustomerRequest, Customer>), new CustomerRequestCustomerConverter());

            _modelConverter = ModelConverter.Builder.UseLocator(serviceLocator).Create;
        }

        [Test]
        [TestCase("Name", 19)]
        [TestCase("A", 10000)]
        [TestCase("_", 999)]
        public void Convert_WithCustomerRequestToCustomer_ShouldBe(string name, int age)
        {
            var customerRequest = new CustomerRequest
            {
                Name = name,
                Age = age
            };

            var customer = _modelConverter.Convert<CustomerRequest, Customer>(customerRequest);

            customer.Name.ShouldBe(customerRequest.Name);

            customer.Age.ShouldBe(customerRequest.Age);

            customer.Category.ShouldBe("None");
        }
    }
}

