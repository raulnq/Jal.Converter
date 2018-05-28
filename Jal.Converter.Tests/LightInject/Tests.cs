using System;
using Jal.Converter.Interface;
using Jal.Converter.LightInject.Installer;
using Jal.Converter.Tests.Impl;
using Jal.Converter.Tests.Model;
using Jal.Finder.Impl;
using Jal.Locator.LightInject.Installer;
using LightInject;
using NUnit.Framework;
using Shouldly;

namespace Jal.Converter.Tests.LightInject
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("Name", 19)]
        [TestCase("A", 10000)]
        [TestCase("_", 999)]
        public void Convert_WithCustomerRequestToCustomerA_ShouldBe(string name, int age)
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;

            var finder = AssemblyFinder.Builder.UsePath(directory).Create;

            var container = new ServiceContainer();

            container.RegisterFrom<ServiceLocatorCompositionRoot>();

            var assemblies = finder.GetAssemblies(x => x.FullName.Contains("Jal.Converter.Tests"));

            container.RegisterConverter(assemblies);

            var sut = container.GetInstance<IModelConverter>();

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

        [Test]
        [TestCase("Name", 19)]
        [TestCase("A", 10000)]
        [TestCase("_", 999)]
        public void Convert_WithCustomerRequestToCustomerB_ShouldBe(string name, int age)
        {
            var container = new ServiceContainer();

            container.RegisterFrom<ServiceLocatorCompositionRoot>();

            container.Register<IConverter<CustomerRequest, Customer>, CustomerRequestCustomerConverter>();

            container.RegisterConverter();

            var sut = container.GetInstance<IModelConverter>();

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

