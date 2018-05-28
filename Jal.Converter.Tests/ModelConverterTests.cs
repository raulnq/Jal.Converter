using System;
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
    public class ModelConverterTests
    {
        [Test]
        public void Convert_WithObjectParameter_ShouldNotBeNull()
        {
            var factory = new Mock<IConverterFactory>();

            factory.Setup(x => x.Create<CustomerRequest, Customer>()).Returns(new CustomerRequestCustomerConverter());

            var sut = new ModelConverter(factory.Object);

            var customer = sut.Convert(typeof(CustomerRequest),typeof(Customer), new CustomerRequest());

            customer.ShouldNotBeNull();

            customer.ShouldBeOfType<Customer>();
        }


        [Test]
        public void Convert_With_ShouldNotBeNull()
        {
            var factory = new Mock<IConverterFactory>();

            factory.Setup(x => x.Create<CustomerRequest, Customer>()).Returns(new CustomerRequestCustomerConverter());

            var sut = new ModelConverter(factory.Object);

            var customer = sut.Convert<CustomerRequest, Customer>(new CustomerRequest());

            customer.ShouldNotBeNull();

            customer.ShouldBeOfType<Customer>();
        }

        [Test]
        public void Convert_WithDynamic_ShouldNotBeNull()
        {
            var factory = new Mock<IConverterFactory>();

            factory.Setup(x => x.Create<CustomerRequest, Customer>()).Returns(new CustomerRequestCustomerConverter());

            var sut = new ModelConverter(factory.Object);

            var customer = sut.Convert<CustomerRequest, Customer>(new CustomerRequest(),new {});

            customer.ShouldNotBeNull();

            customer.ShouldBeOfType<Customer>();
        }

        [Test]
        public void Convert_WithDestination_ShouldNotBeNull()
        {
            var factory = new Mock<IConverterFactory>();

            factory.Setup(x => x.Create<CustomerRequest, Customer>()).Returns(new CustomerRequestCustomerConverter());

            var sut = new ModelConverter(factory.Object);

            var customer = sut.Convert(new CustomerRequest(), new Customer());

            customer.ShouldNotBeNull();

            customer.ShouldBeOfType<Customer>();
        }

        [Test]
        public void Convert_WithDestinationObjectParameter_ShouldNotBeNull()
        {
            var factory = new Mock<IConverterFactory>();

            factory.Setup(x => x.Create<CustomerRequest, Customer>()).Returns(new CustomerRequestCustomerConverter());

            var sut = new ModelConverter(factory.Object);

            var customer = sut.Convert(typeof(CustomerRequest), typeof(Customer), new CustomerRequest(), new Customer());

            customer.ShouldNotBeNull();

            customer.ShouldBeOfType<Customer>();
        }

        [Test]
        public void Convert_WithDestinationAndDynamic_ShouldNotBeNull()
        {
            var factory = new Mock<IConverterFactory>();

            factory.Setup(x => x.Create<CustomerRequest, Customer>()).Returns(new CustomerRequestCustomerConverter());

            var sut = new ModelConverter(factory.Object);

            var customer = sut.Convert(new CustomerRequest(), new Customer(), new {});

            customer.ShouldNotBeNull();

            customer.ShouldBeOfType<Customer>();
        }

        [Test]
        public void Convert_WithNoConveter_ShouldThrowException()
        {
            var factory = new Mock<IConverterFactory>();

            factory.Setup(x => x.Create<CustomerRequest, Customer>()).Throws<Exception>();

            var sut = new ModelConverter(factory.Object);

            Should.Throw<Exception>(
                () => { var customer = sut.Convert<CustomerRequest, Customer>(new CustomerRequest()); });
        }

        [Test]
        public void Builder_WithDestinationAndDynamic_ShouldNotBeNull()
        {
            var locator = new Mock<IServiceLocator>();

            var instance = ModelConverter.Create(locator.Object);

            instance.ShouldNotBeNull();

            instance.ShouldBeOfType<ModelConverter>();

            instance.ShouldBeAssignableTo<IModelConverter>();
        }
    }
}